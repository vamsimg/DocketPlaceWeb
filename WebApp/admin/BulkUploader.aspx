<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="BulkUploader.aspx.cs" Inherits="WebApp.admin.BulkUploader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

     <h1>Bulk Uploader</h1>

	<asp:FileUpload ID="FileUpload1" runat="server" EnableViewState="false" />

	<asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" EnableViewState ="false" />
	
	
	<pre>
		<asp:Literal ID="Literal1" runat="server" EnableViewState="false"></asp:Literal>
	</pre>

</asp:Content>
