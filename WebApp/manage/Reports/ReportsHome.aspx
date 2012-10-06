<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="ReportsHome.aspx.cs" Inherits="WebApp.manage.Reports.ReportsHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">

     <h1>Reporting</h1>

     <a href ="DailyReports.aspx">Daily Reports</a>

	<br />
	<br />
	
	<a href ="WeeklyReport.aspx">Weekly Reports</a>

	<br />
	<br />

	<a href ="MonthlyReport.aspx">Monthly Reports</a>

	<br />
	<br />

	<a href ="YearlyReport.aspx">Yearly Reports</a>

	<br />
	<br />
     <a href ="CategoryAnalysis.aspx">Category Analysis</a>

     
	<br />
	<br />


     <asp:Button ID="CategoriesButton" runat="server" Text="Refresh Departments/Categories" onclick="CategoriesButton_Click" />
     <i>Only click this once per month or when you have added new categories in MYOB RM</i>
     <br />
          
     <asp:Label ID="CategoriesErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false"></asp:Label>






</asp:Content>
