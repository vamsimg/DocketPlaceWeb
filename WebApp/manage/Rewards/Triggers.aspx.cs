using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocketPlace.Business;
using WebApp.AppCode;
using System.Data;

namespace WebApp.manage.Rewards
{
	public partial class Triggers : System.Web.UI.Page
	{
		private Admin loggedInAdmin;
		private Company currentCompany;
		private int maxStoreTriggers = 3;

		protected void Page_Load(object sender, EventArgs e)
		{
			loggedInAdmin = Helpers.GetLoggedInAdmin();
			currentCompany = Helpers.GetCurrentCompany();

			if (!Helpers.IsAuthorizedOwner(loggedInAdmin, currentCompany))
			{
				Response.Redirect("/status.aspx?error=notsuperuser");
			}


			PopuplateBreadcrumbs();

			if (!IsPostBack)
			{
				PopulateStores();
				PopulateAds();
				PopulateTriggers();
			}

		}

		private void PopulateTriggers()
		{
			// Create the output table.
			DataTable triggerList = new DataTable();

			triggerList.Columns.Add("trigger_id");
			triggerList.Columns.Add("suburb");
			triggerList.Columns.Add("trigger_type");
			triggerList.Columns.Add("value");
			triggerList.Columns.Add("uploadedad_id");
			triggerList.Columns.Add("ad_title");
			triggerList.Columns.Add("priority");

			foreach (Store selectedStore in currentCompany.StoresBycompany_)
			{
				foreach (Trigger item in selectedStore.TriggersBystore_.OrderBy(t => t.priority))
				{
					triggerList.Rows.Add(item.trigger_id, selectedStore.suburb, item.type, item.value, item.uploadedad_id, item.uploadedad_.title, item.priority);
				}
			}

			TriggersGridView.DataSource = triggerList;
			TriggersGridView.DataBind();
		}

		private void PopulateStores()
		{
			StoresDropDownList.DataSource = currentCompany.StoresBycompany_;
			StoresDropDownList.DataBind();
		}

		private void PopulateAds()
		{
			AdsDropDownList.DataSource = currentCompany.UploadedAdsBycompany_.Where(p => p.is_active = true);
			AdsDropDownList.DataTextField = "title";
			AdsDropDownList.DataValueField = "uploadedad_id";
			AdsDropDownList.DataBind();
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
			Level3.Text = "Configure Triggers";

			Literal arrows3 = new Literal();
			arrows3.Text = " >> ";

			breadCrumbPanel.Controls.Add(arrows3);
			breadCrumbPanel.Controls.Add(Level3);
		}


		/// <summary>
		/// Check for security vulnerability
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void AdsDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			int uploadedad_id = Convert.ToInt32(AdsDropDownList.SelectedValue);
			UploadedAd current_ad = UploadedAd.GetUploadedAd(uploadedad_id);
			AdImage.ImageUrl = Helpers.GenerateImage(current_ad.data);

			FooterLiteral.Text = current_ad.footer;

		}


		protected void TriggerTypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
		{
			switch (TriggerTypeDropDownList.SelectedValue)
			{
				case ("Points"):
					PointsPanel.Visible = true;
					PointsPanel.Enabled = true;
					PurchasePanel.Visible = false;
					PurchasePanel.Enabled = false;
					MemberPanel.Visible = false;
					MemberPanel.Enabled = false;
					break;
				case ("Purchase"):
					PurchasePanel.Visible = true;
					PurchasePanel.Enabled = true;
					MemberPanel.Visible = false;
					MemberPanel.Enabled = false;
					PointsPanel.Visible = false;
					PointsPanel.Enabled = false;
					break;
				case ("Member"):
					MemberPanel.Visible = true;
					MemberPanel.Enabled = true;
					PurchasePanel.Visible = false;
					PurchasePanel.Enabled = false;
					PointsPanel.Visible = false;
					PointsPanel.Enabled = false;
					break;
			}
		}


		protected void CreateButton_Click(object sender, EventArgs e)
		{
			if (IsValid)
			{
				int maxTriggers = currentCompany.StoresBycompany_.Count * maxStoreTriggers;
				Store selectedStore = Store.GetStore(Convert.ToInt32(StoresDropDownList.SelectedValue));

				if (currentCompany.StoresBycompany_.Sum(s => s.TriggersBystore_.Count) < maxTriggers && selectedStore.TriggersBystore_.Count < maxTriggers)
				{
					string triggerType = TriggerTypeDropDownList.SelectedValue;
					string value = "";

					switch (triggerType)
					{
						case "Member":
							value = IsMemberRadioButtonList.SelectedValue;
							if (selectedStore.TriggersBystore_.Where(t => t.type == "Member" && t.value == value).Count() > 0)
							{
								CreateTriggerErrorLabel.Text = "This store already has a trigger for this condition. Delete it first if you wish to change the ad.";
								return;
							}
							break;
						case "Points":
							value = PointsTextBox.Text;
							break;
						case "Purchase":
							value = PurchaseTextBox.Text;
							break;
					}

					try
					{
						int uploadedad_id = Convert.ToInt32(AdsDropDownList.SelectedValue);



						Trigger newTrigger = Trigger.CreateTriggerBycreator_(loggedInAdmin);
						newTrigger.uploadedad_id = uploadedad_id;
						newTrigger.store_id = selectedStore.store_id;
						newTrigger.type = triggerType;
						newTrigger.value = value;
						newTrigger.notes = "";

						newTrigger.creation_datetime = DateTime.Now;
						newTrigger.is_active = true;

						newTrigger.priority = selectedStore.TriggersBystore_.Count() + 1;

						newTrigger.Save();

						currentCompany.Refresh();

						PopulateTriggers();
					}
					catch
					{
						CreateTriggerErrorLabel.Text = "An error has occurred. Please email help@docketplace.com.au";
						throw;
					}

				}
				else
				{
					CreateTriggerErrorLabel.Text = "You can have a maximum of  " + maxStoreTriggers.ToString() + " triggers per store. Delete one of the triggers above first.";
				}
			}
		}


		protected void DeleteImageButton_Command(object sender, CommandEventArgs e)
		{
			ImageButton ib = (ImageButton)sender;
			Trigger selectedTrigger = Trigger.GetTrigger(Convert.ToInt32(ib.CommandArgument));

			try
			{
				foreach (Trigger item in selectedTrigger.store_.TriggersBystore_.Where(t => t.priority > selectedTrigger.priority))
				{
					item.priority--;
					item.Save();
				}
				selectedTrigger.Delete();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{
				currentCompany.Refresh();
				PopulateTriggers();
			}
		}

		protected void UpImageButton_Command(object sender, CommandEventArgs e)
		{
			ImageButton ib = (ImageButton)sender;
			Trigger selectedTrigger = Trigger.GetTrigger(Convert.ToInt32(ib.CommandArgument));
			try
			{
				if (selectedTrigger.priority != 1)
				{
					Trigger higherTrigger = selectedTrigger.store_.TriggersBystore_.Where(t => t.priority == selectedTrigger.priority - 1).First();
					higherTrigger.priority++;
					higherTrigger.Save();

					selectedTrigger.priority--;
					selectedTrigger.Save();
				}
			}
			catch
			{
				throw;
			}
			finally
			{
				currentCompany.Refresh();
				PopulateTriggers();
			}
		}

		protected void DownImageButton_Command(object sender, CommandEventArgs e)
		{
			ImageButton ib = (ImageButton)sender;
			Trigger selectedTrigger = Trigger.GetTrigger(Convert.ToInt32(ib.CommandArgument));

			try
			{
				if (selectedTrigger.priority != maxStoreTriggers)
				{
					if (selectedTrigger.priority < selectedTrigger.store_.TriggersBystore_.Count)
					{
						Trigger lowerTrigger = selectedTrigger.store_.TriggersBystore_.Where(t => t.priority == selectedTrigger.priority + 1).First();
						lowerTrigger.priority--;
						lowerTrigger.Save();

						selectedTrigger.priority++;
						selectedTrigger.Save();
					}
				}
			}
			catch
			{
				throw;

			}
			finally
			{
				currentCompany.Refresh();
				PopulateTriggers();
			}
		}
	}
}