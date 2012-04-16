<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdMatchUserControl.ascx.cs" Inherits="WebApp.manage.AdGroups.AdMatchUserControl" %>

<div class="AdMatch">

      <div class="AdMatchUtilities">
          <%--<asp:Button ID="PublishButton" runat="server" Text="Publish" onclick="PublishButton_Click" />
          
          <br />--%>
          <asp:Label ID="StatusLabel" runat="server">Status:</asp:Label>                       
               <asp:Label ID="StatusFieldLabel" runat="server"></asp:Label>                       
          
          <br />
          <br />
			
		<asp:Button ID="PrintButton" runat="server" Text="Start Print" Enabled="false" onclick="PrintButton_Click" />
		
		<br />
          <br />          
		<asp:Button ID="DeleteMatchButton" runat="server" Text="Delete Ad Placement"  onclick="DeleteMatchButton_Click" />
		
		<br />
          <br />

     </div>

     <div class="AdMatchDetails">
          
          <asp:Label ID="RetailerLabel" runat="server" Text="Retailer:" CssClass="leftLabel" AssociatedControlID="RetailerHyperLink"></asp:Label>
               <asp:HyperLink ID="RetailerHyperLink" runat="server"  CssClass="textField"></asp:HyperLink>  

          <div class="brclear"></div> 

           <asp:Label ID="StoreLabel" runat="server" Text="Store:" CssClass="leftLabel" AssociatedControlID="StoreHyperLink"></asp:Label>
               <asp:HyperLink ID="StoreHyperLink" runat="server"  CssClass="textField"></asp:HyperLink>  
          
          <div class="brclear"></div> 

       <%--   <asp:Label ID="BudgetLabel" runat="server" Text="Budget:" CssClass="leftLabel" AssociatedControlID="BudgetFieldLabel"></asp:Label>
               <asp:Label ID="BudgetFieldLabel" runat="server" CssClass="textField" ></asp:Label>--%>
          <div class="brclear"></div> 
          <br />

     </div>
     
     <div class="AdMatchAdRequests">

          <asp:ListView ID="RequestedAdsListView" runat="server" DataKeyNames="uploadedad_id">
               <ItemTemplate>
                    <asp:Panel ID="TilePanel" runat="server" CssClass="TilePanel">
                    
                         <asp:Label ID="TitleLabel" runat="server" Text="Title: " CssClass="shortLeftLabel"></asp:Label>
						<asp:Label ID="TitleFieldLabel" runat="server" Text='<%# Eval("title") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>                     
                         
                         <asp:Label ID="QuantityLabel" runat="server" Text="# Requested: " CssClass="shortLeftLabel"></asp:Label>
                              <asp:Label ID="QuanityFieldLabel" runat="server" Text='<%# Eval("num_wanted") %>' CssClass="textField"></asp:Label>
                         
                         <div class="brclear"></div>

                         <asp:Label ID="PrintedLabel" runat="server" Text="# Printed: " CssClass="shortLeftLabel"></asp:Label>
                              <asp:Label ID="PrintedFieldLabel" runat="server" Text='<%# Eval("num_printed")  %>' CssClass="textField"></asp:Label>
                         
                         <div class="brclear"></div>
                         
                         <asp:Label ID="QuotaLabel" runat="server" Text="Daily Quota: " CssClass="shortLeftLabel"></asp:Label>
                              <asp:Label ID="QuoteFieldLabel" runat="server" Text='<%# Eval("daily_quota") %>' CssClass="textField"></asp:Label>
                        
                         <div class="brclear"></div>
					
                         <asp:Image ID="AdImage" runat="server" ImageUrl='<%# Helpers.GenerateImage(Eval("data").ToString()) %>' Width="100px"  CssClass="textField" />
		
					<div class="brclear"></div>
					
					
                          
                         <div class="brclear"></div>
                    </asp:Panel>
               </ItemTemplate>
               <LayoutTemplate>
                    <div ID="itemPlaceholderContainer" runat="server" style="">
                         <span ID="itemPlaceholder" runat="server" />
                    </div>                                   
               </LayoutTemplate>                    
          </asp:ListView>
     </div>

    
      <div class="brclear"></div> 
