using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Specialized;
using System.Text;

namespace Triggerfish.Web
{
	/// <summary>
	/// A chainable query string helper class.
	/// Example usage :
	/// string strQuery = QueryString.Current.Add("id", "179").ToString();
	/// string strQuery = new QueryString().Add("id", "179").ToString();
	/// </summary>
	public class QueryString : NameValueCollection
	{
		/// <summary>
		/// Overridden indexer to decode the value
		/// </summary>
		/// <param name="index">The index</param>
		/// <returns>The decoded value</returns>
		public new string this[int index]
		{
			get { return HttpUtility.UrlDecode(base[index]); }
		}

		/// <summary>
		/// Overridden indexer to decode the value
		/// </summary>
		/// <param name="index">The key name</param>
		/// <returns>The decoded value</returns>
		public new string this[string name]
		{
			get { return HttpUtility.UrlDecode(base[name]); }
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public QueryString() { }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="queryString">An existing query string</param>
		public QueryString(string queryString)
		{
			FillFromString(queryString);
		}

		/// <summary>
		/// Tests whether or not the given key exists
		/// </summary>
		/// <param name="key">The key</param>
		/// <returns>True if the key exists, false otherwise</returns>
		public bool Contains(string key)
		{
			string val = base[key];
			return !String.IsNullOrEmpty(val);
		}

		/// <summary>
		/// Adds a new parameter
		/// </summary>
		/// <param name="key">The parameter key</param>
		/// <param name="value">The parameter value</param>
		/// <returns>This to enable method chaining</returns>
		public new QueryString Add(string key, string value)
		{
			base.Add(key, value);
			return this;
		}

		/// <summary>
		/// Extracts a QueryString from a full URL
		/// </summary>
		/// <param name="s">The string to extract the QueryString from</param>
		/// <returns>A string representing only the QueryString</returns>
		public string ExtractQueryString(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				if (s.Contains("?"))
					return s.Substring(s.IndexOf("?") + 1);
			}
			return s;
		}

		/// <summary>
		/// Returns a QueryString object based on a string
		/// </summary>
		/// <param name="s">The string to parse</param>
		/// <returns>The QueryString object </returns>
		public QueryString FillFromString(string s)
		{
			base.Clear();
			if (string.IsNullOrEmpty(s)) return this;
			foreach (string keyValuePair in ExtractQueryString(s).Split('&'))
			{
				if (string.IsNullOrEmpty(keyValuePair)) continue;
				string[] split = keyValuePair.Split('=');
				base.Add(split[0],
					split.Length == 2 ? split[1] : "");
			}
			return this;
		}

		/// <summary>
		/// Outputs the QueryString object to a string as a HTTP GET
		/// </summary>
		/// <returns>The encoded QueryString as it would appear in a browser</returns>
		public override string ToString()
		{
			return ToString(HttpAction.Get);
		}

		/// <summary>
		/// Outputs the QueryString object to a string
		/// </summary>
		/// <param name="type">The type of HTTP action the query will be used in</param>
		/// <returns>The encoded QueryString as it would appear in a browser</returns>
		public string ToString(HttpAction type)
		{
			StringBuilder sb = new StringBuilder();
			if (type == HttpAction.Get)
			{
				sb.Append("?");
			} 

			foreach (string key in Keys)
			{
				if (sb.Length > 1)
					sb.Append("&");
				sb.AppendFormat("{0}={1}", HttpUtility.UrlEncodeUnicode(key), HttpUtility.UrlEncodeUnicode(this[key]));
			}

			return sb.ToString();
		}
	}
}
