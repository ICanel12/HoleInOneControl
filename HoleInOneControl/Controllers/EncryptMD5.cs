using System.Text;
using System.Security.Cryptography;

namespace HoleInOneControl.Controllers
{
    public class EncryptMD5
    {
        public string Encrypt(string mensaje) {
            string hash = "coding con c#";
            byte[] data = UTF8Encoding.UTF8.GetBytes(mensaje);
            MD5 md5 = MD5.Create();
            TripleDES tripldes = TripleDES.Create();

            tripldes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripldes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripldes.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public string Dencrypt(string mensajeEn)
        {
            string hash = "coding con c#";
            byte[] data = Convert.FromBase64String(mensajeEn);
            MD5 md5 = MD5.Create();
            TripleDES tripldes = TripleDES.Create();

            tripldes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripldes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripldes.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
