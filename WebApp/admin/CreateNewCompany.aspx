<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CreateNewCompany.aspx.cs" Inherits="WebApp.admin.CreateNewCompany" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
	<h2>Create a New Company</h2>
	
     <hr id="titleLine" />
	
	<br />
	
	<asp:Label ID="NameLabel" runat="server" Text="* Company Name:" CssClass="leftLabel" AssociatedControlID="NameTextBox"></asp:Label>
		<asp:TextBox ID="NameTextBox" runat="server" Columns="50" MaxLength="50" CssClass="textField"></asp:TextBox>
		<asp:RequiredFieldValidator ID="NameRequiredFieldValidator" runat="server" ErrorMessage="* Please enter a name for your company." ControlToValidate="NameTextBox" Display="dynamic" CssClass="errorMessage" ></asp:RequiredFieldValidator>
	
	<div class="brclear"></div>   
	
	
	<asp:Label ID="ABNLabel" runat="server" Text="* ABN:" CssClass="leftLabel" AssociatedControlID="ABNTextBox"></asp:Label>
		<asp:TextBox ID="ABNTextBox" runat="server" Columns="11" MaxLength="11" CssClass="textField"></asp:TextBox>
		<asp:RequiredFieldValidator ID="ABNRequiredFieldValidator" runat="server" ErrorMessage="* Please enter an ABN for your company." ControlToValidate="ABNTextBox" Display="dynamic" CssClass="errorMessage" ></asp:RequiredFieldValidator>
     <asp:RegularExpressionValidator ID="ABNRegularExpressionValidator" runat="server" ErrorMessage="ABN must be 11 digits long" ControlToValidate="ABNTextBox" Display="dynamic" CssClass="errorMessage" ValidationExpression="^\d{11}$"></asp:RegularExpressionValidator>
	<div class="brclear"></div>   
	
	<asp:Label ID="ContactNameLabel" runat="server" Text="* Contact Name:" CssClass="leftLabel" AssociatedControlID="ContactNameTextBox"></asp:Label>
		<asp:TextBox ID="ContactNameTextBox" runat="server" Columns="50" MaxLength="50" CssClass="textField"></asp:TextBox>
		<asp:RequiredFieldValidator ID="ContactNameRequiredFieldValidator" runat="server" ErrorMessage="* Please enter a contact for your company." ControlToValidate="ContactNameTextBox" Display="dynamic" CssClass="errorMessage" ></asp:RequiredFieldValidator>
	
     <div class="brclear"></div>   
     
     <asp:Label ID="ContactEmailLabel" runat="server" Text="* Email:" CssClass="leftLabel" AssociatedControlID="ContactEmailTextBox"></asp:Label>
		<asp:TextBox ID="ContactEmailTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="50"></asp:TextBox>
		<asp:RequiredFieldValidator ID="ContactEmailRequired" runat="server" ControlToValidate="ContactEmailTextBox" SetFocusOnError="True" ErrorMessage="*An email for the above contact is required" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>							   	
		<asp:RegularExpressionValidator ID="ContactEmailRegularExpressionValidator" runat="server" ErrorMessage="The email address is not valid."  CssClass="errorMessage"
			ControlToValidate="ContactEmailTextBox" ValidationExpression="^([a-zA-Z0-9_\-\.+]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" 
			Display="Dynamic">
		</asp:RegularExpressionValidator>
	
	<div class="brclear"></div>    
     
     <asp:Label ID="AddressLabel" runat="server" Text="* Address" CssClass="leftLabel" AssociatedControlID="AddressTextBox"></asp:Label>
		<asp:TextBox ID="AddressTextBox" runat="server" Columns="30" MaxLength="100" Rows="3" TextMode="MultiLine" CssClass="textField"></asp:TextBox>
	     <asp:RequiredFieldValidator ID="AddressRequiredFieldValidator" runat="server" ControlToValidate="AddressTextBox" SetFocusOnError="True" ErrorMessage="*An mailing or physical address is required" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>							   	
		
	<div class="brclear"></div> 
			
	<asp:Label ID="SuburbLabel" runat="server" Text="* Suburb:" CssClass="leftLabel" AssociatedControlID="SuburbTextBox"></asp:Label>
		<asp:TextBox ID="SuburbTextBox" runat="server" Columns="30" MaxLength="50" CssClass="textField"></asp:TextBox>
		<asp:RequiredFieldValidator ID="SuburbRequiredFieldValidator" runat="server" ControlToValidate="SuburbTextBox" SetFocusOnError="True" ErrorMessage="*A suburb is required" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>							   	
			
	<div class="brclear"></div>   			
	
	<asp:Label ID="StateLabel" runat="server" Text="* State:" CssClass="leftLabel" AssociatedControlID="StateDropDownList"></asp:Label>
		<asp:DropDownList ID="StateDropDownList" runat="server" CssClass="textField" >
			<asp:ListItem>NSW</asp:ListItem>
			<asp:ListItem>VIC</asp:ListItem>
			<asp:ListItem>QLD</asp:ListItem>
			<asp:ListItem>SA</asp:ListItem>
			<asp:ListItem>ACT</asp:ListItem>
			<asp:ListItem>TAS</asp:ListItem>
			<asp:ListItem>WA</asp:ListItem>
			<asp:ListItem>NT</asp:ListItem>
		</asp:DropDownList>
	
	<div class="brclear"></div>   			
	
						
	<asp:Label ID="PostcodeLabel" runat="server" Text="* Postcode:" CssClass="leftLabel" AssociatedControlID="PostcodeTextBox"></asp:Label>
		<asp:TextBox ID="PostcodeTextBox" runat="server" Columns="4" MaxLength="4" CssClass="textField"></asp:TextBox>
		<asp:RegularExpressionValidator ID="PostcodeRegularExpressionValidator" runat="server" ErrorMessage="Postcode must be a number" Display="Dynamic" ValidationExpression="\d{4}" ControlToValidate="PostcodeTextBox" ValidationGroup="Details"></asp:RegularExpressionValidator>
	     <asp:RequiredFieldValidator ID="PostcodeRequiredFieldValidator" runat="server" ControlToValidate="PostcodeTextBox" SetFocusOnError="True" ErrorMessage="*A postcode is required" Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>							   	
	
	<div class="brclear"></div>   			
	
	<asp:Label ID="PhoneNumberLabel" runat="server" Text="* Phone Number:" CssClass="leftLabel" AssociatedControlID="PhoneTextBox"></asp:Label>
		<asp:TextBox ID="PhoneTextBox" runat="server" Columns="15" MaxLength="15" CssClass="textField" ></asp:TextBox>
		<asp:RequiredFieldValidator ID="PhoneRequiredFieldValidator" runat="server" ErrorMessage="* Please enter a phone number for the organisation." ControlToValidate="PhoneTextBox" Display="dynamic" CssClass="errorMessage" ></asp:RequiredFieldValidator>
		
	<div class="brclear"></div> 
	
	<asp:Label ID="FaxLabel" runat="server" Text="Fax Number:" CssClass="leftLabel" AssociatedControlID="FaxTextBox"></asp:Label>
		<asp:TextBox ID="FaxTextBox" runat="server" Columns="15" MaxLength="15" 
          CssClass="textField" ></asp:TextBox>
		
	<div class="brclear"></div> 
		
	
	<asp:Label ID="MobileNumberLabel" runat="server" Text="* Mobile Number:" CssClass="leftLabel" AssociatedControlID="MobileTextBox"></asp:Label>
		<asp:TextBox ID="MobileTextBox" runat="server" Columns="10" MaxLength="10" CssClass="textField" ValidationGroup="Details" ></asp:TextBox>
		<asp:RegularExpressionValidator ID="MobileRegularExpressionValidator" runat="server" ErrorMessage="Invalid mobile number. Mobile must start with '04' and have no spaces."  ControlToValidate="MobileTextBox" Display="dynamic" CssClass="errorMessage" ValidationGroup="Details"  ValidationExpression="(04)?[0-9]{8}"></asp:RegularExpressionValidator>
	
	<div class="brclear"></div> 
	
	 <asp:Label ID="TechnicalContactLabel" runat="server" Text="* Technical Contact" CssClass="leftLabel" AssociatedControlID="TechnicalContactTextBox"></asp:Label>
		<asp:TextBox ID="TechnicalContactTextBox" runat="server" Columns="50" MaxLength="1000" Rows="5" TextMode="MultiLine" CssClass="textField"></asp:TextBox>
	     <cc1:TextBoxWatermarkExtender ID="TechnicalContactTextBox_TextBoxWatermarkExtender" 
               runat="server" Enabled="True" 
               TargetControlID="TechnicalContactTextBox" 
               WatermarkText="Please enter the details for your technical or IT support consultant or enter IN HOUSE if you do your support.">
           </cc1:TextBoxWatermarkExtender>
	     <asp:RequiredFieldValidator ID="TechnicalContactRequiredFieldValidator" runat="server" ControlToValidate="TechnicalContactTextBox" SetFocusOnError="True" ErrorMessage="*Please enter the details of your technical consultant." Display="dynamic" CssClass="errorMessage"></asp:RequiredFieldValidator>							   	
		
	<div class="brclear"></div> 
	<br />	
	<asp:Label ID="WebsiteLabel" runat="server" Text="Company Website:" CssClass="leftLabel" AssociatedControlID="WebsiteTextBox"></asp:Label>
		<asp:TextBox ID="WebsiteTextBox" runat="server" Columns="85" MaxLength="100" CssClass="textField"></asp:TextBox>
	
	<div class="brclear"></div> 
	
	<asp:Label ID="RetailerLabel" runat="server" Text="Are you an advertiser or retailer ?" CssClass="leftLabel" AssociatedControlID="RetailerRadioButtonList"></asp:Label>
		<asp:RadioButtonList ID="RetailerRadioButtonList"  runat="server">
			<asp:ListItem Value="True" Selected ="True" >Retailer</asp:ListItem>
			<asp:ListItem Value="False" >Advertiser</asp:ListItem>
		</asp:RadioButtonList>
		
	<div class="brclear"></div>  

	<asp:Label ID="StoreReceiptsLabel" runat="server" Text="Can we store the data from your receipts ?" CssClass="leftLabel" AssociatedControlID="StoreReceiptsRadioButtonList"></asp:Label>
		<asp:RadioButtonList ID="StoreReceiptsRadioButtonList"  runat="server">
			<asp:ListItem Value="True" Selected ="True" >Yes</asp:ListItem>
			<asp:ListItem Value="False" >No</asp:ListItem>
		</asp:RadioButtonList>
		
	<div class="brclear"></div>  
	
	<asp:Button ID="CreateCompanyButton" runat="server" Text="Create Company" onclick="CreateCompanyButton_Click"/>
	
	<div class="brclear"></div> 

	<asp:Label ID="CreateCompanyErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
	



</asp:Content>

