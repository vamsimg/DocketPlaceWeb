using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;

namespace WebApp.manage.Rewards
{
	public partial class ConfigureRewards : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;



		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			if (currentCompany.is_rewards == false || currentCompany.RewardSettingsBycompany_.Count == 0)
			{
				Response.Redirect("/manage/Rewards/RewardsHome.aspx");
			}

			if (!IsPostBack)
			{
				PPDDropDownList.SelectedValue = currentCompany.RewardSettingsBycompany_[0].points_per_dollar.ToString();

				EnableVouchersCheckBox.Checked = currentCompany.RewardSettingsBycompany_[0].enable_vouchers;

				VouchersPanel.Enabled = currentCompany.RewardSettingsBycompany_[0].enable_vouchers;
				VouchersPanel.Visible = currentCompany.RewardSettingsBycompany_[0].enable_vouchers;

				PointsThresholdDropDownList.SelectedValue = currentCompany.RewardSettingsBycompany_[0].points_threshold.ToString();
				VoucherAmountDropDownList.SelectedValue = ((int)currentCompany.RewardSettingsBycompany_[0].voucher_amount).ToString();

				ExpiryDropDownList.SelectedValue = currentCompany.RewardSettingsBycompany_[0].expiry_days.ToString();

				CostLiteral.Text = CalculateCostPerCustomer(currentCompany.RewardSettingsBycompany_[0].points_per_dollar, currentCompany.RewardSettingsBycompany_[0].points_threshold, currentCompany.RewardSettingsBycompany_[0].voucher_amount);
			}

			PopuplateBreadcrumbs();
		}

		private void CheckPermission()
		{
			if (!(Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}
		}

		private void PopuplateBreadcrumbs()
		{
			Panel breadCrumbPanel = (Panel)this.Master.FindControl("BreadCrumbPanel");

			HyperLink Level1 = new HyperLink();
			Level1.Text = "Company";
			Level1.NavigateUrl = "/manage/Company/ViewCompany.aspx";

			Literal arrows1 = new Literal();
			arrows1.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows1);
			breadCrumbPanel.Controls.Add(Level1);

			HyperLink Level2 = new HyperLink();
			Level2.Text = "Customers & Rewards";
			Level2.NavigateUrl = "/manage/Rewards/RewardsHome.aspx";

			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);

			HyperLink Level3 = new HyperLink();
			Level3.Text = "Configure Settings";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}

		protected void EnableVouchersCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			VouchersPanel.Enabled = EnableVouchersCheckBox.Checked;
			VouchersPanel.Visible = EnableVouchersCheckBox.Checked;
		}

		private string CalculateCostPerCustomer(int pointsPerDollar, int pointsThreshold, Decimal voucherAmount)
		{
			Decimal spend = pointsThreshold / pointsPerDollar;
			Decimal yield = 100 * voucherAmount / spend;


			string cost = "Costs you " + yield.ToString("#0") + "%. If a customer spends $" + spend + " they get a $" + voucherAmount.ToString("#0") + " voucher";
			return cost;
		}


		protected void CalculateCostButton_Click(object sender, EventArgs e)
		{
			int ppd = Convert.ToInt32(PPDDropDownList.SelectedItem.Value);
			int threshold = Convert.ToInt32(PointsThresholdDropDownList.SelectedItem.Value);
			decimal voucher = Convert.ToDecimal(VoucherAmountDropDownList.SelectedItem.Value);

			CostLiteral.Text = CalculateCostPerCustomer(ppd, threshold, voucher);
		}

		protected void SaveButton_Click(object sender, EventArgs e)
		{
			try
			{
				currentCompany.RewardSettingsBycompany_[0].points_per_dollar = Convert.ToInt32(PPDDropDownList.SelectedItem.Value);

				currentCompany.RewardSettingsBycompany_[0].enable_vouchers = EnableVouchersCheckBox.Checked;

				currentCompany.RewardSettingsBycompany_[0].points_threshold = Convert.ToInt32(PointsThresholdDropDownList.SelectedItem.Value);
				currentCompany.RewardSettingsBycompany_[0].voucher_amount = Convert.ToDecimal(VoucherAmountDropDownList.SelectedItem.Value);
				currentCompany.RewardSettingsBycompany_[0].expiry_days = Convert.ToInt32(ExpiryDropDownList.SelectedItem.Value);

				currentCompany.RewardSettingsBycompany_[0].Save();

				CostLiteral.Text = CalculateCostPerCustomer(currentCompany.RewardSettingsBycompany_[0].points_per_dollar, currentCompany.RewardSettingsBycompany_[0].points_threshold, currentCompany.RewardSettingsBycompany_[0].voucher_amount);

				SaveErrorLiteral.Text = "Settings saved successfully.";
			}
			catch
			{
				SaveErrorLiteral.Text = "An error has occurred please contact help@docketplace.com.au";
				throw;
			}
		}
	
	}
}