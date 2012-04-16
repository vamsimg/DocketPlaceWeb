<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="UpdateStoreDetails.aspx.cs" Inherits="WebApp.manage.Stores.UpdateStoreDetails" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

     <h1>Update Details for <asp:Literal ID="StoreLiteral" runat="server"></asp:Literal></h1>

     <asp:Label ID="StoreListErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>
	
	<br />

	<asp:Label ID="StoreIDLabel" runat="server" Text="Store ID:" CssClass="leftLabel"></asp:Label>
		<span class="textField"><asp:Literal ID="StoreIDLiteral" runat="server"></asp:Literal></span>
	
	<div class="brclear"></div> 
     <br />
              
     <asp:Label ID="StoreContactLabel" runat="server" Text="* Store Contact:" CssClass="leftLabel" AssociatedControlID="StoreContactTextBox"></asp:Label>
	     <asp:TextBox ID="StoreContactTextBox" runat="server" Columns="40" Rows="4" 
               MaxLength="1000" CssClass="textField" ValidationGroup="UpdateStore" TextMode="MultiLine">
		</asp:TextBox>		    
	     <asp:RequiredFieldValidator ID="StoreContactRequiredFieldValidator" 
               runat="server" 
               ErrorMessage="* Please give the contact and phone details for the local manager of the stor. " 
               ControlToValidate="StoreContactTextBox" Display="dynamic" CssClass="errorMessage" 
               ValidationGroup="UpdateStore" > 
		</asp:RequiredFieldValidator>
		
     <div class="brclear"></div> 
     <br />
      
     <asp:Label ID="AddressLabel" runat="server" Text="* Address" CssClass="leftLabel" AssociatedControlID="AddressTextBox"></asp:Label>
	     <asp:TextBox ID="AddressTextBox" runat="server" Columns="40" MaxLength="100" Rows="4" TextMode="MultiLine" CssClass="textField"></asp:TextBox>
          <asp:RequiredFieldValidator ID="AddressRequiredFieldValidator" runat="server" ControlToValidate="AddressTextBox" SetFocusOnError="True" ErrorMessage="*A physical address is required" Display="dynamic" CssClass="errorMessage"  ValidationGroup="UpdateStore"></asp:RequiredFieldValidator>							   	
		
     <div class="brclear"></div> 
	<br />
			
     <asp:Label ID="SuburbLabel" runat="server" Text="* Suburb:" CssClass="leftLabel" AssociatedControlID="SuburbTextBox"></asp:Label>
	     <asp:TextBox ID="SuburbTextBox" runat="server" Columns="30" MaxLength="50" CssClass="textField"></asp:TextBox>
	     <asp:RequiredFieldValidator ID="SuburbRequiredFieldValidator" runat="server" ControlToValidate="SuburbTextBox" SetFocusOnError="True" ErrorMessage="*A suburb is required" Display="dynamic" CssClass="errorMessage"  ValidationGroup="UpdateStore"></asp:RequiredFieldValidator>							   	
			
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
	     <asp:RegularExpressionValidator ID="PostcodeRegularExpressionValidator" runat="server" ErrorMessage="Postcode must be a number" Display="Dynamic" ValidationExpression="\d{4}" ControlToValidate="PostcodeTextBox" ValidationGroup="UpdateStore"></asp:RegularExpressionValidator>
          <asp:RequiredFieldValidator ID="PostcodeRequiredFieldValidator" runat="server" ControlToValidate="PostcodeTextBox" SetFocusOnError="True" ErrorMessage="*A postcode is required" Display="dynamic" CssClass="errorMessage"  ValidationGroup="UpdateStore"></asp:RequiredFieldValidator>							   	
	
     <div class="brclear"></div>   			

     <asp:Label ID="PrintersLabel" runat="server" Text="Number of Printers:" CssClass="leftLabel" AssociatedControlID="PrintersDropDownList"></asp:Label>
	     <asp:DropDownList ID="PrintersDropDownList" runat="server" CssClass="textField" >
		     <asp:ListItem Selected="true">1</asp:ListItem>
		     <asp:ListItem>2</asp:ListItem>
		     <asp:ListItem>3</asp:ListItem>
		     <asp:ListItem>4</asp:ListItem>
		     <asp:ListItem>5</asp:ListItem>
		     <asp:ListItem>6</asp:ListItem>
		     <asp:ListItem>7</asp:ListItem>
		     <asp:ListItem>8</asp:ListItem>
	     </asp:DropDownList>
	     
     <div class="brclear"></div>             
	
     <asp:Label ID="AvailableAdsLabel" runat="server" Text="Select a default Ad to print:" CssClass="leftLabel" AssociatedControlID="AdsDropDownList"></asp:Label>
	     <asp:DropDownList ID="AdsDropDownList" runat="server" CssClass="textField" 
			AutoPostBack="True" onselectedindexchanged="AdsDropDownList_SelectedIndexChanged" >			     
	     </asp:DropDownList>
		<asp:Image ID="AdImage" runat="server"  CssClass="textField" />
					

	     
     <div class="brclear"></div>      

     <asp:Button ID="UpdateStoreButton" runat="server" Text="Update Store Details"  ValidationGroup="UpdateStore" onclick="UpdateStoreButton_Click"/>
     <br />

     <asp:Label ID="ErrorLabel" runat="server" Text="" CssClass="errorMessage"></asp:Label>   
 	
     <div class="brclear"></div> 

</asp:Content>

