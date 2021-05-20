using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace Simple.Framework
{
    public static class ConvertImage
    {
        public static byte[] ToByteArray(string imageLocation)
        {
            byte[] bytes = null;

            try
            {
                bytes = File.ReadAllBytes(imageLocation);
            }
            catch
            {
            }

            return bytes;
        }

        public static byte[] ToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            
            return ms.ToArray();
        }

        public static Image ToImage(string imageLocation)
        {
            byte[] bytes = ToByteArray(imageLocation);

            return ToImage(bytes);
        }

        public static Image ToImage(byte[] byteArrayIn)
        {
            Image returnImage = null;

            try
            {
                if (byteArrayIn != null)
                {
                    MemoryStream ms = new MemoryStream(byteArrayIn);
                    returnImage = Image.FromStream(ms);
                }
            }
            catch(Exception ex)
            {
                string str = String.Empty;
            }

            return returnImage;
        }
    }
}
