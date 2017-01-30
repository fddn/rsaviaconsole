using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RSAviaConsole
{
    class Program
    {
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        public enum KeySizes
        {
            SIZE_512 = 512,
            SIZE_1024 = 1024,
            SIZE_2048 = 2048,
            SIZE_952 = 952,
            SIZE_1369 = 1369
        };

        static void Main(string[] args)
        {
            Console.Write("Text: \n\t");
            string message = Console.ReadLine();
            Console.WriteLine();
            generateKeys();
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(message));
            byte[] decrypted = Decrypt(encrypted);

            Console.WriteLine("Encrypted byte array:\n\t{0}\n",BitConverter.ToString(encrypted).Replace("-",""));
            Console.WriteLine("Decrypted text:\n\t{0}\n",Encoding.UTF8.GetString(decrypted));



            Console.ReadLine();
        }
        
        static void generateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        } 

        static byte[] Encrypt(byte[] input)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);
                encrypted = rsa.Encrypt(input, true);
            }
            return encrypted;
        }

        static byte[] Decrypt(byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                decrypted = rsa.Decrypt(input, true);
            }
            return decrypted;
        }
    }
}
