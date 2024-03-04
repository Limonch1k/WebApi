using System;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace GeneralObject.StringCryptography;

public static class StringCryptography
{
    private static byte[] keyBytes = new byte[] { 15, 35, 50, 100, 1,2,3,4,5,6,7,8,9,10,15,0 };

    private static byte[] iv = new byte[] {10, 20, 30, 40,1,2,3,4,5,6,7,8,9,10,15,0};
    public static string EncryptString(this string text)
    {
        byte[]  encryptedBytes = null;

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform encryptor = aes.CreateEncryptor(keyBytes, iv);

            byte[] bytes = Encoding.UTF8.GetBytes(text);

            string str = Convert.ToBase64String(bytes);

            byte[] plainBytes = Convert.FromBase64String(str);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(iv, 0, iv.Length);// тут можно менять чтобы было несколько вариантов шифров для 1 строки

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    encryptedBytes = memoryStream.ToArray();
                }
            }
        }

        string encryptedText = Convert.ToBase64String(encryptedBytes);
        return encryptedText;
    }

    public static string DecryptString(this string text)
    {
        byte[] utf8encryptedBytes = Convert.FromBase64String(text);

        string str = Convert.ToBase64String(utf8encryptedBytes);

        using (Aes aes = Aes.Create())
        {
            ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes,iv);

            using (MemoryStream memoryStream = new MemoryStream(utf8encryptedBytes, iv.Length, utf8encryptedBytes.Length - iv.Length))
            {
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                {
                    
                    byte[] decryptedBytes = new byte[9999];
                    int bytesRead = 0;
                    int count = 0;
                    while ((bytesRead = cryptoStream.Read(decryptedBytes, bytesRead, utf8encryptedBytes.Length - iv.Length)) > 0)
                    {
                        count += bytesRead;
                    }
                   
                    //string decryptedText = Convert.ToBase64String(decryptedBytes, 0, decryptedByteCount);
                    string decryptedText = Encoding.UTF8.GetString(decryptedBytes, 0, count);
                    return decryptedText;
                }
            }
        }
    }   
}

