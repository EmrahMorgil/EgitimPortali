using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Services
{
    public class FileUploader
    {
        public static string UploadFile(string file)
        {
            string[] allowedExtensions = { "jpg", "jpeg", "png", "gif", "bmp", "tif", "tiff", "svg", "webp", "ico", "psd", "heic", "heif",
                                            "mp4", "avi", "mov", "mkv", "wmv", "flv", "webm", "mpeg", "mpg", "3gp", "3g2", "ogg", "m4v", "ts",
                                            "pdf"};
            string[] parts = file.Split(',');
            bool containsAllowedExtension = allowedExtensions.Any(extension => parts[0].Contains(extension));
            string fileName = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() + parts[0];
            fileName = fileName.Replace(" ", "");
            string directory = Path.Combine("wwwroot", "docs", fileName);

            if (containsAllowedExtension)
            {
                try
                {
                    byte[] fileBytes = Convert.FromBase64String(parts[2]);
                    File.WriteAllBytes(directory, fileBytes);
                    return fileName;
                }
                catch (Exception)
                {
                    return null!;
                }
            }
            else
            {
                return null!;
            }

        }
    }
}
