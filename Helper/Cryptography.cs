using System.Security.Cryptography;
using System.Text;

namespace DawnPoets.Helper
{
    public static class Cryptography
    {
        public static string MakeHash(this string value)
        {
            //var hash = SHA1.Create();
            var hash = SHA256.Create();
            //var hash = SHA384.Create();
            //var hash = SHA512.Create();
            var encode = new ASCIIEncoding();
            var array = encode.GetBytes(value);

            array = hash.ComputeHash(array);

            var strHexa = new StringBuilder();
            
            foreach(var item in array)
            {
                strHexa.Append(item.ToString("x2"));
            }

            return strHexa.ToString();

        }
    }
}
