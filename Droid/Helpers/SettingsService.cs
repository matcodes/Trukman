using System;
using Android.Content;
using Xamarin.Forms;

namespace Trukman.Droid.Helpers
{
	public static class SettingsService
	{
		static ISharedPreferences preferences;

		//=====DON'T MODIFY THESE VALUES AFTER PUBLISHING APP! =========================|
		/*private const string initVector = "random_code_here";
		private const string passPhrase = "long_random_code_here_like_guid_for_eg"
		private const int keysize = 256;*/
		//==============================================================================|

		static SettingsService()
		{
			preferences = Forms.Context.GetSharedPreferences("trukman.settings", FileCreationMode.Private);
		}

		public static void AddOrUpdateSetting<T>(string key, T value)
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");

			var editor = preferences.Edit();
			//var encryptedValue = EncryptString(value.ToString());
			editor.PutString(key, value.ToString());
			editor.Apply();
		}

		public static T GetSetting<T>(string key, T defaultValue = default(T))
		{
			if (string.IsNullOrEmpty(key))
				throw new ArgumentNullException("key");

			if (!preferences.Contains(key))
			{
				if (defaultValue != null)
					AddOrUpdateSetting<T>(key, defaultValue);

				return defaultValue;
			}

			//return (T)Convert.ChangeType(DecryptString(preferences.GetString(key, string.Empty)), typeof(T));
			return (T)Convert.ChangeType(preferences.GetString(key, string.Empty), typeof(T));

		}
		/*
		private string EncryptString(string plainText)
		{
			if (string.IsNullOrEmpty(plainText))
				return string.Empty;

			byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
			byte[] keyBytes = password.GetBytes(keysize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey.Mode = CipherMode.CBC;
			ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
			cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
			cryptoStream.FlushFinalBlock();
			byte[] cipherTextBytes = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(cipherTextBytes);
		}

		private string DecryptString(string cipherText)
		{
			if (string.IsNullOrEmpty(cipherText))
				return string.Empty;

			byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
			byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
			PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
			byte[] keyBytes = password.GetBytes(keysize / 8);
			RijndaelManaged symmetricKey = new RijndaelManaged();
			symmetricKey.Mode = CipherMode.CBC;
			ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
			MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
			byte[] plainTextBytes = new byte[cipherTextBytes.Length];
			int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
		}*/
	}
}
