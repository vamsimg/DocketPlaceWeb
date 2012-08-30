using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DocketPlace.Business;
using System.Drawing;

namespace WebApp.AppCode
{
	/// <summary>
	/// Summary description for QRHelper
	/// </summary>
	public static class QRHelper
	{          		
		/// <summary>
		/// Base64 encoded image of the QR Code for the voucher
		/// </summary>
		/// <param name="newVoucher"></param>
		/// <returns></returns>
		public static string GenerateQRVoucher(Voucher newVoucher)
		{
			try
			{
				string url = "http://www.docketplace.com.au/ValidateVoucher.aspx?voucher_id=" + newVoucher.voucher_id.ToString() + "&voucher_code=" + newVoucher.code;


				string QRCodeGeneratorUrl = "https://chart.googleapis.com/chart?chs=200x200&cht=qr&chl=" + url;

				// Download web image
				Image qrcode = null;
				qrcode = DownloadImage(QRCodeGeneratorUrl);

				return BusinessHelper.EncodeAd(qrcode);
			}
			catch (Exception ex)
			{
				LogHelper.WriteError("VoucherId:" + newVoucher.voucher_id.ToString() + "voucher_code=" + newVoucher.code + ex.ToString());
			}
			return null;
		}



		/// <summary>
		/// Function to download Image from website
		/// </summary>
		/// <param name="_URL">URL address to download image</param>
		/// <returns>Image</returns>
		public static Image DownloadImage(string _URL)
		{
			Image _tmpImage = null;

			try
			{
				// Open a connection
				System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(_URL);

				_HttpWebRequest.AllowWriteStreamBuffering = true;

				// You can also specify additional header values like the user agent or the referer: (Optional)
				_HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1)";
				_HttpWebRequest.Referer = "http://www.google.com/";

				// set timeout for 20 seconds (Optional)
				_HttpWebRequest.Timeout = 20000;

				// Request response:
				System.Net.WebResponse _WebResponse = _HttpWebRequest.GetResponse();

				// Open data stream:
				System.IO.Stream _WebStream = _WebResponse.GetResponseStream();

				// convert webstream to image
				_tmpImage = Image.FromStream(_WebStream);

				// Cleanup
				_WebResponse.Close();
			}
			catch
			{
				throw;
			}

			return _tmpImage;
		}
	}
}