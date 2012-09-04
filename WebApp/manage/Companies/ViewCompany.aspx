<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewCompany.aspx.cs" Inherits="WebApp.manage.Companies.ViewCompany" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

     <h1><asp:Literal ID="CompanyNameLiteral" runat="server"></asp:Literal></h1>     

	<div id="detatilsPanel" style="float:left;">

		<asp:Label ID="NameLabel" runat="server" Text=" Company Name:" CssClass="leftLabel" ></asp:Label>
			<asp:Label ID="NameFieldLabel" runat="server" CssClass="textField"></asp:Label>
			
		<div class="brclear"></div>   
		
		
		<asp:Label ID="ABNLabel" runat="server" Text=" ABN:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="ABNFieldLabel" runat="server" CssClass="textField"></asp:Label>		

		<div class="brclear"></div>   
		
		<asp:Label ID="ContactNameLabel" runat="server" Text=" Contact Name:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="ContactNameFieldLabel" runat="server" CssClass="textField"></asp:Label>
		<div class="brclear"></div>   
	     
		<asp:Label ID="ContactEmailLabel" runat="server" Text=" Email:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="ContactEmailFieldLabel" runat="server" CssClass="textField"></asp:Label>
		
		<div class="brclear"></div>    
	     
		<asp:Label ID="AddressLabel" runat="server" Text=" Address" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="AddressFieldLabel" runat="server" CssClass="textField"></asp:Label>	
		<div class="brclear"></div> 
				
		<asp:Label ID="SuburbLabel" runat="server" Text=" Suburb:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="SuburbFieldLabel" runat="server" CssClass="textField"></asp:Label>	
		<div class="brclear"></div>   			
		
		<asp:Label ID="StateLabel" runat="server" Text=" State:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="StateFieldLabel" runat="server" CssClass="textField"></asp:Label>
		<div class="brclear"></div>   			
		
							
		<asp:Label ID="PostcodeLabel" runat="server" Text=" Postcode:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="PostcodeFieldLabel" runat="server" CssClass="textField"></asp:Label>
		<div class="brclear"></div>   			
		
		<asp:Label ID="PhoneNumberLabel" runat="server" Text=" Phone Number:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="PhoneFieldLabel" runat="server" CssClass="textField"></asp:Label>
		<div class="brclear"></div> 
		
		<asp:Label ID="FaxLabel" runat="server" Text="Fax Number:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="FaxFieldLabel" runat="server" CssClass="textField"></asp:Label>
			
		<div class="brclear"></div> 
			
		
		<asp:Label ID="MobileNumberLabel" runat="server" Text=" Mobile Number:" CssClass="leftLabel"></asp:Label>
			<asp:Label ID="MobileFieldLabel" runat="server" CssClass="textField"></asp:Label>
		<div class="brclear"></div> 
		
		 <asp:Label ID="TechnicalContactLabel" runat="server" Text=" Technical Contact" CssClass="leftLabel" ></asp:Label>
			<asp:Label ID="TechnicalContactFieldLabel" runat="server" CssClass="textField"></asp:Label>

		<div class="brclear"></div> 
		<br />	
		<asp:Label ID="WebsiteLabel" runat="server" Text="Company Website:" CssClass="leftLabel" ></asp:Label>
			<asp:Label ID="WebsiteFieldLabel" runat="server" CssClass="textField"></asp:Label>

		<div class="brclear"></div> 
		
		<asp:Label ID="NotesLabel" runat="server" Text="Notes" CssClass="leftLabel" ></asp:Label>
			<asp:Label ID="NotesFieldLabel" runat="server" CssClass="textField"></asp:Label>

		<div class="brclear"></div> 

		<asp:Label ID="RetailerLabel" runat="server" Text="Are you an advertiser or retailer ?" CssClass="leftLabel" ></asp:Label>
			<asp:Label ID="ReatilerFieldLabel" runat="server" CssClass="textField"></asp:Label>
			
		<div class="brclear"></div>  		

		<asp:HyperLink ID="EditCompanyHyperLink" runat="server" NavigateUrl="/manage/Companies/UpdateCompany.aspx">Update Company</asp:HyperLink>

	</div>
     

</asp:Content>

