using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocketPlace.Business.Framework;
using DocketPlace.Business;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Drawing.Imaging;

namespace WebServicesApp.AppCode
{
	public static class Helpers
	{
		public static string GenerateFiveDigitRandom()
		{
			Random r = new Random();
			int num = r.Next(0, 99999);

			string output = num.ToString().Trim();
			return output;
		}



		public static string DecodeFromBase64(string encodedData)
		{
			byte[] encodedDataAsBytes
			    = System.Convert.FromBase64String(encodedData);
			string returnValue =
			   System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
			return returnValue;
		}

		/// <summary>
		/// This is a hack to see if a date field is null in an object from the DAL.
		/// </summary>
		/// <param name="testDate"></param>
		/// <returns></returns>
		public static bool isDateSet(DateTime testDate)
		{
			DateTime test = new DateTime(0001, 01, 01);
			return (!(testDate == test));
		}


		/// <summary>
		/// Offsets are relative to Greenwich Mean Time. Server is US Pacific
		/// </summary>
		private static int serverDatetimeOffset = -8;

		/// <summary>
		/// Stores are Australian Eastern 
		/// </summary>
		private static int storeDatetimeOffset = 10;

		public static DateTime ConvertServerDateTimetoLocal(DateTime serverDatetime)
		{
			return serverDatetime.AddHours(storeDatetimeOffset - serverDatetimeOffset);
		}

	}
}