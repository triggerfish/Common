using System;
using System.Collections.Generic;
using System.Text;

namespace Triggerfish.Authentication
{
	/// <summary>
	/// Encrypter interface implementation using the BCrypt algorithm
	/// </summary>
	public sealed class BCryptEncryptor : IEncryptor
	{
		/// <summary>
		/// Encrypt the plaintext string
		/// </summary>
		/// <param name="plainText">The plaintext string</param>
		/// <returns>The encryted string</returns>
		public string Encrypt(string plainText)
		{
			return BCrypt.HashPassword(plainText, BCrypt.GenerateSalt());
		}

		/// <summary>
		/// Tests whether the plaintext string is equal to a text
		/// string that is encrypted
		/// </summary>
		/// <param name="plainText">The plaintext</param>
		/// <param name="encryptedText">The encrypted text</param>
		/// <returns>True if the strings match, false otherwise</returns>
		public bool IsMatch(string plainText, string encryptedText)
		{
			return BCrypt.CheckPassword(plainText, encryptedText);
		}
	}
}
