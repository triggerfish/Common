using System;
using System.Collections.Generic;
using System.Text;

namespace Triggerfish.Security
{
	/// <summary>
	/// Interface to encrypt a string
	/// </summary>
	public interface IEncryptor
	{
		/// <summary>
		/// Encrypt the plaintext string
		/// </summary>
		/// <param name="plainText">The plaintext string</param>
		/// <returns>The encryted string</returns>
		string Encrypt(string plainText);

		/// <summary>
		/// Tests whether the plaintext string is equal to a text
		/// string that is encrypted
		/// </summary>
		/// <param name="plainText">The plaintext</param>
		/// <param name="encryptedText">The encrypted text</param>
		/// <returns>True if the strings match, false otherwise</returns>
		bool IsMatch(string plainText, string encryptedText);
	}
}
