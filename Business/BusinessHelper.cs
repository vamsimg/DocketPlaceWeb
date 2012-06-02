using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace DocketPlace.Business
{
    public class BusinessHelper
    {
        public static string computeSHAhash(string password, DateTime salt)
        {

            byte[] tmpSource;
            byte[] result;

            string password_salted = password + salt.ToString();

            tmpSource = ASCIIEncoding.ASCII.GetBytes(password_salted);

            SHA512 shaM = new SHA512Managed();
            result = shaM.ComputeHash(tmpSource);

            string password_hashed = ByteArrayTostring(result);

            return password_hashed;
        }

        public static string ByteArrayTostring(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
         

	   /// <summary>
	   /// Images should be PNG to reduce size.
	   /// </summary>
	   /// <param name="new_ad"></param>
	   /// <returns></returns>
	
	   public static string EncodeAd(System.Drawing.Image new_ad)
	   {
		   MemoryStream stream = new MemoryStream();
		   new_ad.Save(stream, ImageFormat.Png);

		   byte[] bitmapData = stream.ToArray();
		   string outputBase64EncodedImage = Convert.ToBase64String(bitmapData);

		   return outputBase64EncodedImage;
	   }

	   public static Image DecodeAd(string inputBase64EncodedImage)
	   {
		   byte[] data = Convert.FromBase64String(inputBase64EncodedImage);
		   MemoryStream ms = new MemoryStream(data);

		   Image img = Image.FromStream(ms);
		   return img;
	   }

    }
}
