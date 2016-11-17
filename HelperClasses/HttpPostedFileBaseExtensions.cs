using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace NivesBrelihPhotography.HelperClasses
{
    public class HttpPostedFileBaseExtensions
    {
        //image min bytes
        public const int ImageMinimumBytes = 512;

        //check if posted file is image
        public static bool IsImage(HttpPostedFileBase imageFile)
        {
            if (imageFile != null && imageFile.FileName!=null)
            {

                //-------------------------------------------
                //  Check the image mime types
                //-------------------------------------------
                if (imageFile.ContentType.ToLower() != "image/jpg" &&
                    imageFile.ContentType.ToLower() != "image/jpeg" &&
                    imageFile.ContentType.ToLower() != "image/pjpeg" &&
                    imageFile.ContentType.ToLower() != "image/gif" &&
                    imageFile.ContentType.ToLower() != "image/x-png" &&
                    imageFile.ContentType.ToLower() != "image/png")
                {
                    return false;
                }

                //-------------------------------------------
                //  Check the image extension
                //-------------------------------------------
                if (Path.GetExtension(imageFile.FileName).ToLower() != ".jpg"
                    && Path.GetExtension(imageFile.FileName).ToLower() != ".png"
                    && Path.GetExtension(imageFile.FileName).ToLower() != ".gif"
                    && Path.GetExtension(imageFile.FileName).ToLower() != ".jpeg")

                {
                    return false;
                }

                //-------------------------------------------
                //  Attempt to read the file and check the first bytes
                //-------------------------------------------
                try
                {
                    if (!imageFile.InputStream.CanRead)
                    {
                        return false;
                    }

                    if (imageFile.ContentLength < ImageMinimumBytes)
                    {
                        return false;
                    }

                    byte[] buffer = new byte[512];
                    imageFile.InputStream.Read(buffer, 0, 512);
                    string content = System.Text.Encoding.UTF8.GetString(buffer);
                    if (Regex.IsMatch(content, @"<script|<html|<head|<title|<body|<pre|<table|<a\s+href|<img|<plaintext|<cross\-domain\-policy",
                        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Multiline))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                //-------------------------------------------
                //  Try to instantiate new Bitmap, if .NET will throw exception
                //  we can assume that it's not a valid image
                //-------------------------------------------

                try
                {
                    using (var bitmap = new System.Drawing.Bitmap(imageFile.InputStream))
                    {
                    }
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
            return false;
        }

    }
}
