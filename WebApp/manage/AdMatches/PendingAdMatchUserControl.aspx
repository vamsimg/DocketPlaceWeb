<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="PendingAdMatchUserControl.aspx.cs" Inherits="WebApp.manage.AdMatches.PendingAdMatchUserControl" %>



<div class="AdMatch">

      <div class="AdMatchUtilities">
                   
          <asp:Button ID="AcceptButton" runat="server" Text="Accept" onclick="AcceptButton_Click" />
          <br />
          <asp:Button ID="RejectButton" runat="server" Text="Reject" onclick="RejectButton_Click" />        
          
     </div>

     <div class="AdMatchDetails">
          
          <asp:Label ID="AdvertiserLabel" runat="server" Text="Advertiser:" CssClass="leftLabel" AssociatedControlID="AdvertiserHyperLink"></asp:Label>
               <asp:HyperLink ID="AdvertiserHyperLink" runat="server"  CssClass="textField"></asp:HyperLink>  

          <div class="brclear"></div> 

           <asp:Label ID="StoreLabel" runat="server" Text="Store:" CssClass="leftLabel" AssociatedControlID="StoreHyperLink"></asp:Label>
               <asp:HyperLink ID="StoreHyperLink" runat="server"  CssClass="textField"></asp:HyperLink>  
          
          <div class="brclear"></div> 

          <asp:Label ID="BudgetLabel" runat="server" Text="Budget:" CssClass="leftLabel" AssociatedControlID="BudgetFieldLabel"></asp:Label>
               <asp:Label ID="BudgetFieldLabel" runat="server" CssClass="textField" ></asp:Label>
          <div class="brclear"></div> 
          <br />

     </div>
     
     <div class="AdMatchAdRequests">

          <asp:ListView ID="RequestedAdsListView" runat="server" DataKeyNames="uploadedad_id">
               <ItemTemplate>
                    <asp:Panel ID="TilePanel" runat="server" CssClass="TilePanel">
                    
                         <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>                     
                         
                         <asp:Label ID="QuantityLabel" runat="server" Text="Requested: " CssClass="shortLeftLabel"></asp:Label>
                              <asp:Label ID="QuanityFieldLabel" runat="server" Text='<%# Eval("num_wanted") %>' CssClass="textField"></asp:Label>
                         
                         <div class="brclear"></div>
                         
                         <asp:Label ID="QuotaLabel" runat="server" Text="Daily Quota: " CssClass="shortLeftLabel"></asp:Label>
                              <asp:Label ID="QuoteFieldLabel" runat="server" Text='<%# Eval("daily_quota") %>' CssClass="textField"></asp:Label>
                        
                         <div class="brclear"></div>
                         
                         <asp:Image ID="AdImage" runat="server" ImageUrl='<%# Helpers.GenerateImage(Eval("data").ToString()) %>' Width="100px"  CssClass="textField" />
                         
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
</div>