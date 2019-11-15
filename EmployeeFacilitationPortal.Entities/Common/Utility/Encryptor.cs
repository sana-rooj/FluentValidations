using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace EmployeeFacilitationPortal.Entities.Common.Utility
{
    public class Encryptor
    {

        public static string encryptionKey = AppConfigurations.EncryptionKey;
        public static string encryptionIV = AppConfigurations.EncryptionIV;
        
        public static byte[] Encrypt(string toEncrypt)
        {
            byte[] src = Encoding.UTF8.GetBytes(toEncrypt);
            byte[] des = new byte[src.Length];
            using (var encryptor = new AesCryptoServiceProvider())
            {
                encryptionKey = AppConfigurations.EncryptionKey;
                encryptor.BlockSize = 128;
                encryptor.KeySize = 128;
               
                encryptor.IV = Encoding.UTF8.GetBytes(encryptionIV);
                encryptor.Key = Encoding.UTF8.GetBytes(encryptionKey);
                encryptor.Mode = CipherMode.CBC;
                //encryptor.Padding = PaddingMode.Zeros;

                using (ICryptoTransform encrypt = encryptor.CreateEncryptor(encryptor.Key, encryptor.IV))
                {
                    des = encrypt.TransformFinalBlock(src, 0, src.Length);
                    return des;
                }
            }
        }

        public static string Decrypt(byte[] toDecrypt)
        {
            byte[] src = toDecrypt;
            byte[] des = new byte[src.Length];
            using (var encryptor = new AesCryptoServiceProvider())
            {
                encryptor.BlockSize = 128;
                encryptor.KeySize = 128;
                
                encryptor.IV = Encoding.UTF8.GetBytes(encryptionIV);
                encryptor.Key = Encoding.UTF8.GetBytes(encryptionKey);
                encryptor.Mode = CipherMode.CBC;
                //encryptor.Padding = PaddingMode.Zeros;

                using (ICryptoTransform encrypt = encryptor.CreateDecryptor(encryptor.Key, encryptor.IV))
                {
                    des = encrypt.TransformFinalBlock(src, 0, src.Length);
                    var finalString = Encoding.UTF8.GetString(des);
                    return finalString;
                }
            }
        }
    }
}
