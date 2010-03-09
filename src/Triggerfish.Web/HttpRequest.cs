using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;
using System.Net;
using System.IO;

namespace Triggerfish.Web
{
	/// <summary>
	/// Submits a HTTP request
	/// </summary>
	public class HttpRequest : IHttpRequest
	{
		/// <summary>
		/// Gets or sets the url to submit the request to
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets the query/post parameters
		/// </summary>
		public QueryString Parameters { get; set; }

		/// <summary>
		/// Gets or sets the type of HTTP action to perform
		/// </summary>
		public HttpAction Action { get; set; }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public HttpRequest()
		{
			Action = HttpAction.Post;
		}

		/// <summary>
		/// Constructor that accepts a url as a parameter
		/// </summary>
		/// <param name="url">The url where the post will be submitted to.</param>
		public HttpRequest(string url)
			: this()
		{
			Url = url;
		}

		/// <summary>
		/// Constructor allowing the setting of the url and items to post.
		/// </summary>
		/// <param name="url">the url for the post.</param>
		/// <param name="postItems">The values for the post.</param>
		public HttpRequest(string url, QueryString postItems)
			: this(url)
		{
			Parameters = postItems;
		}

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <returns>a string containing the result of the post.</returns>
		public string Send()
		{
			string result = SendRequest(Url, Parameters.ToString(Action));
			return result;
		}

		/// <summary>
		/// Sends the request to the specified url.
		/// </summary>
		/// <param name="url">The url to submit the request to</param>
		/// <param name="parameters">The request parameters</param>
		/// <returns>The response from the action.</returns>
		public string Send(string url, QueryString parameters)
		{
			Url = url;
			Parameters = parameters;
			return Send();
		}

		/// <summary>
		/// Posts the supplied data to specified url asyncronously.
		/// </summary>
		/// <param name="callback">Callback delegate</param>
		/// <param name="state">Request state object</param>
		/// <returns>An asynchronous result</returns>
		public IAsyncResult BeginSend(AsyncCallback callback, object state)
		{
			HttpWebRequest request = BuildRequest(Url, Parameters.ToString(Action));
			return request.BeginGetResponse(callback, state);
		}

		private HttpWebRequest BuildRequest(string url, string parameters)
		{
			HttpWebRequest request = null;
			if (Action == HttpAction.Post)
			{
				Uri uri = new Uri(url);
				request = (HttpWebRequest)WebRequest.Create(uri);
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = parameters.Length;
				using (Stream writeStream = request.GetRequestStream())
				{
					UTF8Encoding encoding = new UTF8Encoding();
					byte[] bytes = encoding.GetBytes(parameters);
					writeStream.Write(bytes, 0, bytes.Length);
				}
			}
			else
			{
				Uri uri = new Uri(url + "?" + parameters);
				request = (HttpWebRequest)WebRequest.Create(uri);
				request.Method = "GET";
			}

			return request;
		}

		private string SendRequest(string url, string parameters)
		{
			HttpWebRequest request = BuildRequest(url, parameters);

			string result = string.Empty;
			using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
			{
				using (Stream responseStream = response.GetResponseStream())
				{
					using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
					{
						result = readStream.ReadToEnd();
					}
				}
			}
			return result;
		}
	}
}
