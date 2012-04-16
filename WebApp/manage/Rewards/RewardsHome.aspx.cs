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
	public partial class RewardsHome : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		protected Company currentCompany;


		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();
			CheckPermission();

			PopuplateBreadcrumbs();

			if (!IsPostBack)
			{
				if (currentCompany.is_rewards == true)
				{
					if (currentCompany.RewardSettingsBycompany_.Count != 0)
					{
						EnableRewardsPanel.Visible = false;
						EnableRewardsPanel.Enabled = false;

						ActiveModulePanel.Visible = true;
						ActiveModulePanel.Enabled = true;
					}
				}
			}
		}

		private void CheckPermission()
		{
			if (!(Helpers.IsAuthorizedClerk(loggedInAdmin, currentCompany) || Helpers.IsAuthorizedAdmin(loggedInAdmin, currentCompany) || Helpers.IsSuperUser(loggedInAdmin)))
			{
				Response.Redirect("/status.aspx?error=notadmin");
			}

			if (Helpers.IsSuperUser(loggedInAdmin))
			{
				EnableRewardsPanel.Visible = true;
				EnableRewardsPanel.Enabled = true;
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
			Level2.Text = "Rewards";


			Literal arrows2 = new Literal();
			arrows2.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows2);
			breadCrumbPanel.Controls.Add(Level2);
		}


		protected void EnableRewardsButton_Click(object sender, EventArgs e)
		{
			if (Helpers.IsSuperUser(loggedInAdmin))
			{
				try
				{
					currentCompany.is_rewards = true;
					currentCompany.Save();

					if (currentCompany.RewardSettingsBycompany_.Count == 0)
					{
						RewardSetting newSetting = currentCompany.CreateRewardSetting();
						newSetting.points_per_dollar = 5;
						newSetting.enable_vouchers = true;
						newSetting.expiry_days = 30;
						newSetting.voucher_amount = 5;
						newSetting.points_threshold = 500;
						newSetting.Save();
					}
					Response.Redirect("/manage/Rewards/ConfigureRewards.aspx", true);
				}
				catch (Exception ex)
				{
					LogHelper.WriteError(ex.ToString());
					currentCompany.RollBack();
					EnableRewardsErrorLiteral.Text = "An error has occurred please contact help@docketplace.com.au";
				}
			}
		}
	}
}