using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

using System.Text;
using System.IO.Compression;

namespace WebServicesApp.AppCode
{
     //Only need Gzip for the moment . Regular Zip code for when and if we use an API.
     public static class ZipHelper
     {

          /// <summary>
          /// Uses the builtin .NET GZIP compression not the IONIC ZLIB one!!!!
          /// </summary>
          /// <param name="compressedText"></param>
          /// <returns></returns>
          public static string DecompressFromGzip(string compressedText)
          {
               byte[] gzBuffer = Convert.FromBase64String(compressedText);
               using (MemoryStream ms = new MemoryStream())
               {
                    int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                    ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                    byte[] buffer = new byte[msgLength];

                    ms.Position = 0;
                    using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress))
                    {
                         zip.Read(buffer, 0, buffer.Length);
                    }

                    return Encoding.UTF8.GetString(buffer);
               }
          }

          //public static string CompressToZip(string input)
          //{
          //     string serverPath = System.Web.HttpContext.Current.Server.MapPath("/temp/");
          //     string fileName = Path.GetRandomFileName();


          //     string zipFilePath = serverPath + fileName + ".zip";

          //     using (ZipFile zip = new ZipFile())
          //     {
          //          ZipEntry e = zip.AddEntry("data", input);
          //          zip.Save(zipFilePath);
          //     }

          //     byte[] zippedData = File.ReadAllBytes(zipFilePath);

          //     File.Delete(zipFilePath);

          //     return Convert.ToBase64String(zippedData);

          //}


          //public static string DecompressFromZip(string compressedText)
          //{
          //     string serverPath = System.Web.HttpContext.Current.Server.MapPath("/temp/");
          //     string fileName = Path.GetRandomFileName();
          //     string zipFilePath = serverPath + fileName + ".zip";

          //     byte[] zipBuffer = Convert.FromBase64String(compressedText);

          //     File.WriteAllBytes(zipFilePath, zipBuffer);


          //     string unzippedText = "";
          //     using (ZipFile zip = ZipFile.Read(zipFilePath))
          //     {
          //          foreach (ZipEntry entry in zip.Entries)
          //          {
          //               var ms = new MemoryStream();
          //               entry.Extract(ms);
          //               unzippedText += Encoding.UTF8.GetString(ms.ToArray());
          //          }
          //     }

          //     File.Delete(zipFilePath);
          //     return unzippedText;
          //}


          //private static byte[] StreamFile(string filename)
          //{
          //     string serverPath = System.Web.HttpContext.Current.Server.MapPath("/temp/");

          //     // Create a byte array of file stream length
          //     byte[] ImageData = File.ReadAllBytes(serverPath + filename);


          //     return ImageData; //return the byte data
          //}

     }
}