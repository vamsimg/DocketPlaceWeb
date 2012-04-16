<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ViewStore.aspx.cs" Inherits="WebApp.manage.Stores.ViewStore" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">

     <h1>Store Details for <asp:Literal ID="StoreLiteral" runat="server"></asp:Literal> </h1>
		
	<span class="leftLabel" >Store ID:</span>
		<asp:Label ID="StoreIDLabel" runat="server" CssClass="textField"></asp:Label>			     
	<div class="brclear"></div>

	<span class="leftLabel" >Store Password:</span>
		<asp:Label ID="StorePasswordLabel" runat="server" CssClass="textField"></asp:Label>			     
	<div class="brclear"></div>

     <asp:Label ID="StoreContactLabel" runat="server" Text=" Store Contact:" CssClass="leftLabel" ></asp:Label>
		<asp:Label ID="StoreContactFieldLabel" runat="server" CssClass="textField"></asp:Label>			     

     <div class="brclear"></div> 
     <br />
      
     <asp:Label ID="AddressLabel" runat="server" Text=" Address" CssClass="leftLabel" ></asp:Label>
	     <asp:Label ID="AddressFieldLabel" runat="server" CssClass="textField"></asp:Label>		

     <div class="brclear"></div> 
	<br />
			
     <asp:Label ID="SuburbLabel" runat="server" Text=" Suburb:" CssClass="leftLabel" ></asp:Label>
		<asp:Label ID="SuburbFieldLabel" runat="server" CssClass="textField"></asp:Label>			    
	
     <div class="brclear"></div>   			
	
     <asp:Label ID="StateLabel" runat="server" Text=" State:" CssClass="leftLabel" ></asp:Label>
		<asp:Label ID="StateFieldLabel" runat="server" CssClass="textField"></asp:Label>		

     <div class="brclear"></div>   			
	
						
     <asp:Label ID="PostcodeLabel" runat="server" Text=" Postcode:" CssClass="leftLabel" ></asp:Label>
	     <asp:Label ID="PostcodeFieldLabel" runat="server" CssClass="textField"></asp:Label>
		
     <div class="brclear"></div>   			

     <asp:Label ID="PrintersLabel" runat="server" Text="Number of Printers:" CssClass="leftLabel" ></asp:Label>
	    <asp:Label ID="PrintersFieldLabel" runat="server" CssClass="textField"></asp:Label>		

     <div class="brclear"></div>
     
     <asp:Label ID="VolumeLabel" runat="server" Text="Estimated receipts per day:" CssClass="leftLabel"></asp:Label>
	     <asp:Label ID="VolumeFieldLabel" runat="server" CssClass="textField"></asp:Label>		
	     
     <div class="brclear"></div>     

</asp:Content>

