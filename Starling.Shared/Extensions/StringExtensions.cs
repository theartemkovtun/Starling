using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Starling.Shared.Extensions
{
    public static class StringExtensions
    {
        public static string MimeType(this string filename)
        {
            return AvailableMimeTypes[Path.GetExtension(filename).ToLowerInvariant()];
        }
        
        private static Dictionary<string, string> AvailableMimeTypes => 
            new Dictionary<string, string>  
            {  
                {".txt", "text/plain"},  
                {".pdf", "application/pdf"},  
                {".doc", "application/vnd.ms-word"},  
                {".docx", "application/vnd.ms-word"},  
                {".xls", "application/vnd.ms-excel"},  
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"}, 
                {".xlsm", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"}, 
                {".png", "image/png"},  
                {".jpg", "image/jpeg"},  
                {".jpeg", "image/jpeg"},  
                {".gif", "image/gif"},  
                {".csv", "text/csv"}  
            };
        
        public static string Sha256(this string source)
        {
            using var sha256Hash = SHA256.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
        
        public static string Sha512(this string source)
        {
            using var sha256Hash = SHA512.Create();
            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));

            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
        
        public static byte[] Sha256AsBytes(this string source)
        {
            using var sha256Hash = SHA256.Create();
            return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(source));
        }
    }
}