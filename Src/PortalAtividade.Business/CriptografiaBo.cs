using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PortalAtividade.Business
{
    public class CriptografiaBo
    {
        #region Constantes

        /// <summary>         
        /// Chave da Criptografia      
        /// </summary>         
        private const string CryptoKey = "D3Vkp1AV@L1@cA01";

        /// <summary>         
        /// Vetor de bytes utilizados para a criptografia (Chave Externa)         
        /// </summary>
        private const string VetorKey = "A%@KP1#22!TI8P)2";

        #endregion

        /// <summary>         
        /// Metodo de criptografia de valor         
        /// </summary>         
        /// <param name="text">valor a ser criptografado</param> 
        /// <returns>valor criptografado</returns>    
        public static string Encrypt(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return null;

                byte[] bKey = Encoding.UTF8.GetBytes(CryptoKey);
                byte[] bIv = Encoding.UTF8.GetBytes(VetorKey);
                byte[] bText = Encoding.UTF8.GetBytes(text);

                Rijndael rijndael = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };

                var mStream = new MemoryStream();
                var encryptor = new CryptoStream(
                    mStream,
                    rijndael.CreateEncryptor(bKey, bIv),
                    CryptoStreamMode.Write);

                encryptor.Write(bText, 0, bText.Length);
                encryptor.FlushFinalBlock();

                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao criptografar", ex);
            }
        }

        /// <summary>         
        /// Metodo de descriptografia de valor         
        /// </summary>         
        /// <param name="text">valor a ser descriptografado</param> 
        /// <returns>valor descriptografado</returns>    
        public static string Decrypt(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text)) return null;
                                              
                byte[] bKey = Encoding.UTF8.GetBytes(CryptoKey);
                byte[] bIv = Encoding.UTF8.GetBytes(VetorKey);
                byte[] bText = Convert.FromBase64String(text);

                Rijndael rijndael = new RijndaelManaged
                {
                    Mode = CipherMode.CBC
                };

                var mStream = new MemoryStream();
                                           
                var decryptor = new CryptoStream(
                    mStream,
                    rijndael.CreateDecryptor(bKey, bIv),
                    CryptoStreamMode.Write);
                                   
                decryptor.Write(bText, 0, bText.Length);                                                
                decryptor.FlushFinalBlock();
                                     
                UTF8Encoding utf8 = new UTF8Encoding();                                    
                return utf8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao descriptografar", ex);
            }
        }
    }
}
