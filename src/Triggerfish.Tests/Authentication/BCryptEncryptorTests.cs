using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Triggerfish.Authentication;

namespace Triggerfish.Tests.Authentication
{
	[TestClass]
	public class BCryptEncryptorTests
	{
		[TestMethod]
		public void ShouldHashString()
		{
			// Arrange
			string str = "Test String";
			BCryptEncryptor encryptor = new BCryptEncryptor();
			
			// Act
			string strEncrypted = encryptor.Encrypt(str);

			// Assert
			Assert.AreNotEqual(strEncrypted, str);
		}

		[TestMethod]
		public void ShouldMatchStrings()
		{
			// Arrange
			string str = "Test String";
			BCryptEncryptor encryptor = new BCryptEncryptor();

			// Act
			string strEncrypted = encryptor.Encrypt(str);

			// Assert
			Assert.IsTrue(encryptor.IsMatch(str, strEncrypted));
		}

		[TestMethod]
		public void ShouldNotMatchStrings()
		{
			// Arrange
			string str = "Test String";
			BCryptEncryptor encryptor = new BCryptEncryptor();

			// Act
			string strEncrypted = encryptor.Encrypt(str);

			// Assert
			Assert.IsFalse(encryptor.IsMatch("testing", strEncrypted));
		}
	}
}
