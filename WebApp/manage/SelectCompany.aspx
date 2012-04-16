<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="SelectCompany.aspx.cs" Inherits="WebApp.manage.SelectCompany" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

     <h3>Select Company to work With</h3>
     
     <asp:GridView ID="CompaniesGridView" runat="server" AutoGenerateColumns="false" 
         BorderStyle="None" GridLines="None" ShowHeader="False">          
     <Columns>
     <asp:TemplateField>
          <ItemTemplate>
               <asp:LinkButton ID="SelectCompanyLinkButton"  
                    runat="server"   CausesValidation="False"  
                    Text = '<%# Eval("name") %>' 
                    CommandArgument='<%# Eval("company_id") %>' 
                    oncommand="SelectCompanyLinkButton_Command"/>						
               </ItemTemplate>
          </asp:TemplateField>
     </Columns>
         
     </asp:GridView>


</asp:Content>

