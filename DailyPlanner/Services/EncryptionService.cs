namespace DailyPlanner.Services
{
    using System.Security.Cryptography;
    using System.Text;

    public interface IEncryptionService
    {
        string Deencryption(string deEncrypt);

        string Encryption(string toEncrypt);
    }

    public class EncryptionService : IEncryptionService
    {
        public string Encryption(string toEncrypt)
        {
            //Encryption Example
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(toEncrypt);
            using (Aes aes = Aes.Create())
            {
                byte[] keyData = Convert.FromBase64String("quVmj0O+przPy7ViW4DxbpvbAu7gSMLwJ7cgzWqmuFU=");
                aes.Key = keyData; //key should be stored in key vault, i need to figure that out 
                byte[] ivData = Convert.FromBase64String("tbxYjYULJBgrWCJm5xMiOw==");
                aes.IV = ivData; //iv should be stored in key vault, i need to figure that out 
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    byte[] encryptedBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(encryptedBytes);
                }
            }
        }

        public string Deencryption(string deEncrypt)
        {
            //Encryption Example
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(deEncrypt);
            using (Aes aes = Aes.Create())
            {
                byte[] keyData = Convert.FromBase64String("quVmj0O+przPy7ViW4DxbpvbAu7gSMLwJ7cgzWqmuFU=");
                aes.Key = keyData; //key should be stored in key vault, i need to figure that out 
                byte[] ivData = Convert.FromBase64String("tbxYjYULJBgrWCJm5xMiOw==");
                aes.IV = ivData; //iv should be stored in key vault, i need to figure that out 

                byte[] cipher = Convert.FromBase64String(deEncrypt);
                using (MemoryStream memoryStream = new MemoryStream(cipher))
                {
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Read);
                    var msPlain = new MemoryStream();
                    cryptoStream.CopyTo(msPlain);
                    var decryptedBytes = msPlain.ToArray();
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }
        }
    }
}
