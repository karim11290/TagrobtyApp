using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuizbeePlus.Commons
{
    public static class Extensions
    {
        public static string Status(this bool? isActive)
        {
            return isActive.HasValue ? isActive.Value ? "Active" : "InActive" : "InActive";
        }

        public static string StandardDate(this DateTime? dateTime)
        {
            //http://www.csharp-examples.net/string-format-datetime/
            return dateTime.HasValue ? String.Format("{0:d}", dateTime) : "";
        }

        public static string StandardDateTime(this DateTime? dateTime)
        {
            //http://www.csharp-examples.net/string-format-datetime/
            return dateTime.HasValue ? String.Format("{0:g}", dateTime) : "";
        }

        public static string IfNullShowAlternative(this object value, string Alternative)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()) || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return Alternative;
            }
            else
            {
                return value.ToString();
            }
        }

        public static string IfZeroShowAlternative(this int value, string Alternative)
        {
            if (value == 0)
            {
                return Alternative;
            }
            else
            {
                return value.ToString();
            }
        }
        
        public static List<int> CSVToListInt(this string CSV)
        {
            if (!string.IsNullOrEmpty(CSV))
            {
                return CSV.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select<string, int>(int.Parse).ToList();
            }
            else
            {
                return null;
            }
        }

        public static string TakeUsernameFromEmail(this string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                return email.Split('@').FirstOrDefault();
            }

            return string.Empty;
        }

        /// <summary>
        /// Shuffles List Items.
        /// https://stackoverflow.com/a/1262619/7978967
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            if (list != null)
            {
                System.Security.Cryptography.RNGCryptoServiceProvider provider = new System.Security.Cryptography.RNGCryptoServiceProvider();
                int n = list.Count;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do provider.GetBytes(box);
                    while (!(box[0] < n * (Byte.MaxValue / n)));
                    int k = (box[0] % n);
                    n--;
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }

 

    }



}