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
		/// Add a name value pair to the collection
		/// </summary>
		/// <param name="name">The name</param>
		/// <param name="value">The value associated to the name</param>
		/// <returns>The QueryString object</returns>
		public new QueryString Add(string name, string value)
		{
			string existingValue = base[name];
			if (string.IsNullOrEmpty(existingValue))
				base.Add(name, HttpUtility.UrlEncodeUnicode(value));
			else
				base[name] += "," + HttpUtility.UrlEncodeUnicode(value);
			return this;
		}

		/// <summary>
		/// Overrides the default indexer
		/// </summary>
		/// <param name="name">Name index into the collection</param>
		/// <returns>The associated decoded value for the specified name</returns>
		public new string this[string name]
		{
			get
			{
				return HttpUtility.UrlDecode(base[name]);
			}
		}

		/// <summary>
		/// Overrides the default indexer
		/// </summary>
		/// <param name="index">Index into the collection</param>
		/// <returns>The associated decoded value for the specified index</returns>
		public new string this[int index]
		{
			get
			{
				return HttpUtility.UrlDecode(base[index]);
			}
		}

		/// <summary>
		/// Checks if a name already exists within the query string collection
		/// </summary>
		/// <param name="name">The name to check</param>
		/// <returns>True if the name exists, false otherwise</returns>
		public bool Contains(string name)
		{
			string existingValue = base[name];
			return !string.IsNullOrEmpty(existingValue);
		}

		/// <summary>
		/// Outputs the QueryString object to a string as a HTTP GET
		/// </summary>
		/// <returns>The encoded QueryString as it would appear in a browser</returns>
		public override string ToString()
		{
			return ToString(PostTypeEnum.Get);
		}

		/// <summary>
		/// Outputs the QueryString object to a string
		/// </summary>
		/// <param name="type">The type of HTTP action the query will be used in</param>
		/// <returns>The encoded QueryString as it would appear in a browser</returns>
		public string ToString(PostTypeEnum type)
		{
			StringBuilder builder = new StringBuilder();
			for (var i = 0; i < base.Keys.Count; i++)
			{
				if (!string.IsNullOrEmpty(base.Keys[i]))
				{
					string prefix = String.Empty;
					if (type == PostTypeEnum.Get)
					{
						prefix = "?";
					}
					foreach (string val in base[base.Keys[i]].Split(','))
					{
						builder.Append((builder.Length == 0) ? prefix : "&").Append(HttpUtility.UrlEncodeUnicode(base.Keys[i])).Append("=").Append(val);
					}
				}
			}
			return builder.ToString();
		}
	}
}
