<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="Stores.aspx.cs" Inherits="WebApp.manage.Stores.Stores" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	<h1>Stores</h1>
    
	 
     <asp:HyperLink ID="CreateNewStoreHyperLink" runat="server" NavigateUrl="/manage/Stores/CreateNewStore.aspx">Create a New Store</asp:HyperLink>

          
          <%-- View Ads for : <asp:TextBox ID="CurrentDateTextBox" runat="server" Columns="10" 
                                   AutoPostBack="True" EnableViewState="False" 
                                   OnTextChanged="CurrentDateTextBox_TextChanged"></asp:TextBox>
                              <cc1:CalendarExtender ID="CurrentDateCalendarExtender" 
                                        runat="server" Format="dd/MM/yyyy" TargetControlID="CurrentDateTextBox" 
                                        Enabled="True">
                              </cc1:CalendarExtender>
                              
                              <cc1:MaskedEditExtender ID="CurrentDateMaskedEditExtender" 
                                   runat="server" Mask="99/99/9999" MaskType="Date" 
                                   TargetControlID="CurrentDateTextBox" CultureAMPMPlaceholder="" 
                                   CultureCurrencySymbolPlaceholder="" CultureDateFormat="" 
                                   CultureDatePlaceholder="" CultureDecimalPlaceholder="" 
                                   CultureThousandsPlaceholder="" CultureTimePlaceholder="" Enabled="True">
                              </cc1:MaskedEditExtender>
                              
		                    <asp:RequiredFieldValidator ID="CurrentDateRequiredFieldValidator" 
                                   runat="server" ErrorMessage="Date Required" Display="Dynamic" 
                                   ControlToValidate="CurrentDateTextBox"></asp:RequiredFieldValidator>
     --%>
     <div class="brclear"></div> 
     <br />
     <asp:ListView ID="AllStoresListView" runat="server" DataKeyNames="store_id" >
          <ItemTemplate>
               
               <asp:Panel ID="StoresPanel" runat="server"  CssClass="AdMatchPanel">
				
                    <asp:Hiddenfield ID="StoreIDHiddenField" value='<%# Eval("store_id") %>' runat="server"/>
                    
                    <asp:Panel ID="DetailsPanel" runat="server" CssClass="DetailsPanel">
                    
					<h3>Details</h3>
					                        
                         <span class="leftLabel">Store ID:</span>
                              <asp:Label ID="store_idLabel" runat="server" Text='<%# Eval("store_id") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>

					<span class="leftLabel" >Store Password:</span>
						<asp:Label ID="StorePasswordLabel" runat="server" Text='<%# Eval("password")%>' CssClass="textField"></asp:Label>			     
					
					<div class="brclear"></div>
                       
                         <span class="leftLabel">Store Contact:</span>
                              <asp:Label ID="store_contactLabel" runat="server" Text='<%# Eval("store_contact") %>' CssClass="textField" />
                         <div class="brclear"></div>
                        
                         <span class="leftLabel">Address:</span>
                              <asp:Label ID="addressLabel" runat="server" Text='<%# Eval("address") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>
                         
                         <span class="leftLabel">Suburb:</span>
                              <asp:Label ID="suburbLabel" runat="server" Text='<%# Eval("suburb") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>
                         
                         <span class="leftLabel">State:</span>
                              <asp:Label ID="stateLabel" runat="server" Text='<%# Eval("state") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>
                         <span class="leftLabel">Postcode:</span>
                              <asp:Label ID="postcodeLabel" runat="server" Text='<%# Eval("postcode") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>
                         
                         <span class="leftLabel">Number of Printers:</span>
                              <asp:Label ID="num_printersLabel" runat="server" Text='<%# Eval("num_printers") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>
                         
                         <span class="leftLabel">Average Volume:</span>
                              <asp:Label ID="avg_volumeLabel" runat="server" Text='<%# Eval("avg_volume") %>' CssClass="textField" />
                         
                         <div class="brclear"></div>

					 <span class="leftLabel">Current Default Ad:</span>
                              <asp:Image ID="AdImage" runat="server" ImageUrl='<%#  Eval("image_location") %>'  CssClass="textField" />
                         
                         <div class="brclear"></div>
                         
                    </asp:Panel>

				<asp:Panel ID="AdsPanel" runat="server" CssClass="AdsPanel">
                        
					<h3>Current Ads </h3>
                    
				
					<asp:ListView ID="CurrentAdsListView" runat="server" DataKeyNames="ad_id">
                         <ItemTemplate>
                              <asp:Panel ID="TilePanel" runat="server" CssClass="TilePanel">
                              
							<asp:Label ID="titleLabel" runat="server" Text="Title: " CssClass="shortLeftLabel"></asp:Label>
                                    
								<asp:Label ID="titleFieldLabel" runat="server" Text='<%# Eval("title") %>' CssClass="textField" />
                                   
                                   <div class="brclear"></div>
                                   
                                                       
                                   <asp:Label ID="QuantityLabel" runat="server" Text="Quantity: " CssClass="shortLeftLabel"></asp:Label>
                                        <asp:Label ID="QuanityFieldLabel" runat="server" Text='<%# Eval("num_wanted") %>' CssClass="textField"></asp:Label>
                                   
                                   <div class="brclear"></div>
                                   
                                   <asp:Label ID="RemainingLabel" runat="server" Text="Remaining: " CssClass="shortLeftLabel"></asp:Label>
                                        <asp:Label ID="RemainingFieldLabel" runat="server" Text='<%# Eval("num_left") %>' CssClass="textField"></asp:Label>
                                                                      
                                   
                                   <div class="brclear"></div>
                                   
                                   <asp:Label ID="QuotaLabel" runat="server" Text="Daily Quota: " CssClass="shortLeftLabel"></asp:Label>
                                        <asp:Label ID="QuoteFieldLabel" runat="server" Text='<%# Eval("daily_quota") %>' CssClass="textField"></asp:Label>
                                   
                                   <div class="brclear"></div>
                                   
                                   <asp:Label ID="StartDateLabel" runat="server" Text="Start Date: " CssClass="shortLeftLabel"></asp:Label>
                                        <asp:Label ID="StartDateFieldLabel" runat="server" 
                                        Text='<%# Eval("start_datetime", "{0:d}") %>'  CssClass="textField"  />
                                   <div class="brclear"></div>
                         
                         
                                   <asp:Label ID="EndDateLabel" runat="server" Text="End Date: " CssClass="shortLeftLabel"></asp:Label>
                                        <asp:Label ID="EndDateFieldLabel" runat="server" 
                                        Text='<%# Eval("end_datetime", "{0:d}") %>'  CssClass="textField"  />
                                   <div class="brclear"></div>
               
                                             
                                   <asp:Image ID="AdImage" runat="server" ImageUrl='<%# WebApp.AppCode.Helpers.GenerateImage(Eval("data").ToString()) %>' Width="150px"  CssClass="textField"  BorderStyle="Solid" BorderColor="Black" BorderWidth="1px" />
                                    
                                   <div class="brclear"></div>
                              
                              </asp:Panel>
                         </ItemTemplate>
                         <LayoutTemplate>
                              <div ID="itemPlaceholderContainer" runat="server" style="">
                                   <span ID="itemPlaceholder" runat="server" />
                              </div>                                   
                         </LayoutTemplate>  
                    </asp:ListView>


                    </asp:Panel>

				<div class="brclear"></div>
                    
			     <asp:Panel ID="ControlsPanel" runat="server">                         
				     <asp:HyperLink ID="EditHyperLink" runat="server" NavigateUrl= '<%# "/manage/Stores/UpdateStoreDetails.aspx?store_id=" + Eval("store_id") %>'>Edit</asp:HyperLink>
			     </asp:Panel>
                
                
                    
                  
                    <div class="brclear"></div>
          
               </asp:Panel>
                    
                <br />
            
               
          </ItemTemplate>
          <LayoutTemplate>
               <div ID="itemPlaceholderContainer" runat="server">
                    <span ID="itemPlaceholder" runat="server"/>
               </div>
               <div style="">
               </div>
          </LayoutTemplate>
       


     </asp:ListView>

   


</asp:Content>

