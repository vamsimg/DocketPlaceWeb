using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp
{
     public partial class VoucherValidator : System.Web.UI.Page
     {
          protected void Page_Load(object sender, EventArgs e)
          {

          }

          protected void TestConnectionButton_Click(object sender, EventArgs e)
          {
               if (IsValid)
               {
                    int storeID = Convert.ToInt32(StoreIDTextBox.Text.Trim());
                    string password = StorePasswordTextBox.Text.Trim();

                    Store currentStore = Store.GetStore(storeID);

                    if (currentStore == null)
                    {
                         TestConnectionErrorLabel.Text = "Store not found";
                    }
                    else if (currentStore.password != password)
                    {
                         TestConnectionErrorLabel.Text = "Password Incorrect";
                    }
               }
          }

          protected void ValidateVoucherButton_Click(object sender, EventArgs e)
          {
               if (IsValid)
               {
                    try
                    {
                         int storeID = Convert.ToInt32(StoreIDTextBox.Text.Trim());
                         string password = StorePasswordTextBox.Text.Trim();
                         string voucherCode = VoucherCodeTextBox.Text.Trim();
                         Store currentStore = Store.GetStore(storeID);

                         if (currentStore == null)
                         {
                              TestConnectionErrorLabel.Text = "Store not found";
                         }
                         else if (currentStore.password != password)
                         {
                              TestConnectionErrorLabel.Text = "Password Incorrect";
                         }
                         else
                         {
                              string[] components = voucherCode.Split('-');
                              int voucherID = Convert.ToInt32(components[0]);
                              string code = components[1];

                              Voucher foundVoucher = Voucher.GetVoucher(voucherID);
                              if (foundVoucher == null)
                              {
                                   ValidateVoucherErrorLabel.Text = "Voucher not found, try scanning the barcode again.";
                              }
                              else if (foundVoucher.code != code)
                              {
                                   ValidateVoucherErrorLabel.Text = "Code incorrect, try scanning the barcode again.";
                              }
                              else if(foundVoucher.expiry_datetime < DateTime.UtcNow.AddDays(1))
                              {
                                   ValidateVoucherErrorLabel.Text = "Voucher has expired";
                              }
                              else if (Helpers.isDateSet(foundVoucher.used_datetime))
                              {
                                   ValidateVoucherErrorLabel.Text = "Voucher has already been used";
                              }
                              else
                              {
                                   ValidateVoucherErrorLabel.Text = "Voucher is VALID. Value = $" + foundVoucher.dollar_value.ToString();
                              }
                         }
                    }
                    catch(Exception ex)
                    {
                         LogHelper.WriteError(ex.ToString());
                         ValidateVoucherErrorLabel.Text = "An error has occurred , contact help@0to10rm.com";
                    }
               }
          }
     }
}