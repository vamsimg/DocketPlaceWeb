<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CustomerLists.aspx.cs" Inherits="WebApp.manage.Rewards.CustomerLists" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

	<h1>Customer Lists</h1>

	<a href="CreateNewCustomerList.aspx">Create a new List</a>
	
	<br />
	<br />
	

	<asp:GridView ID="RewardsSummaryGridView" runat="server" AutoGenerateColumns="False" EnableViewState="false" DataSourceID="CustomerListsSqlDataSource" >
		<Columns>			
			<asp:BoundField DataField="title" HeaderText="Title" />
			<asp:HyperLinkField DataNavigateUrlFields="customerlist_id" ItemStyle-CssClass="action"
				DataTextFormatString ="{0:G}"
				 Text="View"
				DataNavigateUrlFormatString="/manage/Rewards/ViewCustomerList.aspx?customerlist_id={0}"/>				
		</Columns>
	</asp:GridView>

	<asp:SqlDataSource ID="CustomerListsSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:ConnString %>" 
			SelectCommand="select customerlist_id, title
			FROM CustomerLists
			WHERE company_id = @company_id">
		<SelectParameters>
			<asp:SessionParameter Name="company_id" SessionField="company_id" Type="Int32" />
		</SelectParameters>
	</asp:SqlDataSource>	

</asp:Content>

