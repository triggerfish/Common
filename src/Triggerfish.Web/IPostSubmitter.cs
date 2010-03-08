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
	/// Determines what type of post to perform.
	/// </summary>
	public enum PostTypeEnum
	{
		/// <summary>
		/// Does a get against the source.
		/// </summary>
		Get,
		/// <summary>
		/// Does a post against the source.
		/// </summary>
		Post
	}

	/// <summary>
	/// Submits post data to a url.
	/// </summary>
	public interface IPostSubmitter
	{
		/// <summary>
		/// Gets or sets the url to submit the post to.
		/// </summary>
		string Url { get; set; }

		/// <summary>
		/// Gets or sets the items to post.
		/// </summary>
		QueryString PostItems { get; set; }

		/// <summary>
		/// Gets or sets the type of action to perform against the url.
		/// </summary>
		PostTypeEnum Type { get; set; }

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <returns>a string containing the result of the post.</returns>
		string Post();

		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <param name="url">The url to post to.</param>
		/// <returns>a string containing the result of the post.</returns>
		string Post(string url);
	
		/// <summary>
		/// Posts the supplied data to specified url.
		/// </summary>
		/// <param name="url">The url to post to.</param>
		/// <param name="postItems">The values to post.</param>
		/// <returns>a string containing the result of the post.</returns>
		string Post(string url, QueryString postItems);
	}
}
