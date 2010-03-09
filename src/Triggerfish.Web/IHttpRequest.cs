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
	/// Determines what type of HTTP action to perform.
	/// </summary>
	public enum HttpAction
	{
		/// <summary>
		/// Does a get against the source
		/// </summary>
		Get,
		/// <summary>
		/// Does a post against the source
		/// </summary>
		Post
	}

	/// <summary>
	/// Submits a HTTP request
	/// </summary>
	public interface IHttpRequest
	{
		/// <summary>
		/// Gets or sets the url to submit the request to
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// Gets or sets the query/post parameters
		/// </summary>
		QueryString Parameters { get; set; }

		/// <summary>
		/// Gets or sets the type of HTTP action to perform
		/// </summary>
		HttpAction Action { get; set; }

		/// <summary>
		/// Sends the request to the specified url.
		/// </summary>
		/// <returns>The response from the action.</returns>
		string Send();

		/// <summary>
		/// Sends the request to the specified url.
		/// </summary>
		/// <param name="url">The url to submit the request to</param>
		/// <param name="parameters">The request parameters</param>
		/// <returns>The response from the action.</returns>
		string Send(string url, QueryString parameters);

		/// <summary>
		/// Posts the supplied data to specified url asyncronously.
		/// </summary>
		/// <param name="callback">Callback delegate</param>
		/// <param name="state">Request state object</param>
		/// <returns>An asynchronous result</returns>
		IAsyncResult BeginSend(AsyncCallback callback, object state);
	}
}
