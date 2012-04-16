<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CreateAdMatches.aspx.cs" Inherits="WebApp.manage.AdMatches.CreateAdMatches" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
     
     <h1>Create New Ad Placements</h1>     
     
     
     <asp:Panel ID="StoresPanel" runat="server">
     
          <h2>1. Decide on Stores to place Ads in.</h2>
          
         <%-- <asp:Panel ID="StoreSearchPanel" runat="server">
          
               <asp:Button ID="ShowOwnButton" runat="server" Text="Own Stores" onclick="ShowOwnButton_Click" />
               
               <asp:Button ID="ShowAllButton" runat="server" Text="Show  All Other Stores" onclick="ShowAllButton_Click" />
          </asp:Panel>
           
          <br />
          <br />--%>

          <asp:Panel ID="StoreResultsPanel" runat="server">  

               <asp:Label ID="StoreSelectErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>

               <br />
               <br />
			<asp:GridView ID="StoresGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="store_id" CssClass="gridview" >
                    <Columns>					
                         <asp:TemplateField HeaderText="Select">					
					     <ItemTemplate>
						     <asp:CheckBox ID="StoreSelectCheckBox" runat="server"  />
					     </ItemTemplate>
					     <ItemStyle HorizontalAlign="Center" />
				     </asp:TemplateField>
                         <asp:BoundField DataField="store_id" HeaderText="Store ID" 
                              InsertVisible="False" ReadOnly="True" SortExpression="store_id" />                         
                         <asp:BoundField DataField="address" HeaderText="Address" 
                              SortExpression="address" />
                         <asp:BoundField DataField="suburb" HeaderText="Suburb" 
                              SortExpression="suburb" />
                         <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />
                         <asp:BoundField DataField="postcode" HeaderText="postcode" 
                              SortExpression="postcode" />
                         <asp:BoundField DataField="num_printers" HeaderText="# Printers" 
                              SortExpression="num_printers" />
                         <asp:BoundField DataField="avg_volume" HeaderText="Average Volume" 
                              SortExpression="avg_volume" />                    
                    	
                    </Columns>
               </asp:GridView>			

               <%--<asp:GridView ID="StoresGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="store_id" >
                    <Columns>					
                         <asp:TemplateField HeaderText="Select">					
					     <ItemTemplate>
						     <asp:CheckBox ID="StoreSelectCheckBox1" runat="server"  />
					     </ItemTemplate>
					     <ItemStyle HorizontalAlign="Center" />
				     </asp:TemplateField>
                         <asp:BoundField DataField="store_id" HeaderText="Store ID" 
                              InsertVisible="False" ReadOnly="True" SortExpression="store_id" />                         
                         <asp:BoundField DataField="address" HeaderText="Address" 
                              SortExpression="address" />
                         <asp:BoundField DataField="suburb" HeaderText="Suburb" 
                              SortExpression="suburb" />
                         <asp:BoundField DataField="state" HeaderText="State" SortExpression="state" />
                         <asp:BoundField DataField="postcode" HeaderText="postcode" 
                              SortExpression="postcode" />
                         <asp:BoundField DataField="num_printers" HeaderText="# Printers" 
                              SortExpression="num_printers" />
                         <asp:BoundField DataField="avg_volume" HeaderText="Average Volume" 
                              SortExpression="avg_volume" />                    
                    	
                    </Columns>
               </asp:GridView>--%>
			<br />
			<br />
			<asp:Button ID="SelectAdsButton" runat="server" Text="Select" onclick="SelectAdsButton_Click" />        

          </asp:Panel>
     </asp:Panel>

	
     <asp:Panel ID="AdsPanel" runat="server" Visible="false">
          <asp:Button ID="BackButton" runat="server" Text="Cancel" onclick="BackButton_Click" />          

          <h2>2. Pick Ads for the Selected Stores </h2>     
          
         <%-- Price for each printed ad is :  <asp:Label ID="PriceLabel" runat="server" ></asp:Label>   
          
          <br />    
          
          Budget for this Ad Group is :  <asp:Label ID="AdGroupBudgetLabel" runat="server" ></asp:Label>
          
          <br />
          <br />--%>

          <asp:Panel ID="AdsLibraryPanel" runat="server">

               <asp:Button ID="SaveAdsButton" runat="server" Text="Save" onclick="SaveAdsButton_Click"  />              
                              
               <br />     

               <asp:Label ID="SelectAdsErrorLabel" runat="server" CssClass="errorMessage"></asp:Label>

               <br />
               <br />
               
               <asp:Panel ID="AdLibraryPanel" runat="server" CssClass="AdLibraryPanel">
                
                    <asp:ListView ID="AdLibraryListView" runat="server" DataKeyNames="uploadedad_id" >
                         <ItemTemplate> 
                         <asp:Panel ID="DiscreteAdPanel" runat="server" CssClass="DiscreteAdPanel" >                             
                              <asp:CheckBox ID="SelectAdCheckBox" runat="server" Text="Select"/>   <asp:Hiddenfield ID="UploadedAdIDHiddenField" value='<%# Eval("uploadedad_id") %>' runat="server"/>
                              
                              <div class="brclear"></div>
                              
                              <asp:Label ID="QuantityLabel" runat="server" Text="How Many Ads ?" CssClass="leftLabel"></asp:Label>
                                   <asp:TextBox ID="QuantityTextBox" runat="server" Text="2" CssClass="textField" Columns="5"></asp:TextBox> x 100 
                              <div class="brclear"></div>        
                              
                              <asp:Label ID="TitleViewLabel" runat="server" Text="Title: " CssClass="leftLabel"></asp:Label>
                                   <asp:Label ID="titleLabel" runat="server" Text='<%# Eval("title") %>' CssClass="textField" />
                                   
                                   
                              <div class="brclear"></div>
                              
                              <asp:Label ID="AdViewLabel" runat="server" Text="Ad: " CssClass="leftLabel"></asp:Label>
                                   <asp:Image ID="AdImage" runat="server" ImageUrl='<%# Helpers.GenerateImage(Eval("data").ToString()) %>' Width="150px"  CssClass="textField" />
                                       
                                
                              <div class="brclear"></div>
                            
                              <asp:Label ID="FooterViewLabel" runat="server" Text="Footer: " CssClass="leftLabel"></asp:Label>
                                   <asp:Label ID="footerLabel" runat="server" Text='<%# Eval("footer") %>' CssClass="textField" />
                              
                              <div class="brclear"></div>
                              
                              <asp:Label ID="NotesViewLabel" runat="server" Text="Notes: " CssClass="leftLabel"></asp:Label>
                                   <asp:Label ID="notesLabel" runat="server" Text='<%# Eval("notes") %>' CssClass="textField" />
                                
                              
                              <div class="brclear"></div>                                   
                         </asp:Panel> 
                         <br />
                         </ItemTemplate>
                         
                         <LayoutTemplate>
                              <div ID="itemPlaceholderContainer" runat="server" >
                                   <span ID="itemPlaceholder" runat="server" />
                              </div>
                              
                         </LayoutTemplate>
                    </asp:ListView>
               </asp:Panel> 
          </asp:Panel>
     </asp:Panel>

   
     <asp:Panel ID="ExistingAdMatchesPanel" runat="server">  
	     
		<asp:Label ID="PageErrorLabel" runat="server"></asp:Label>    
		
          <asp:GridView ID="ExistingAdsRequestsGridView" runat="server" AutoGenerateColumns="False" Caption="Existing Ad Matches" CssClass="gridview">
               <Columns>
                    <asp:BoundField DataField="title" HeaderText="Title" ReadOnly="True" 
                         SortExpression="title" />                    
                    <asp:BoundField DataField="store" HeaderText="Store" ReadOnly="True" 
                         SortExpression="store" />
                    <asp:BoundField DataField="company" HeaderText="Company" 
                         ReadOnly="True" SortExpression="company" />
                    <asp:BoundField DataField="num_wanted" HeaderText="# Wanted" 
                         SortExpression="num_wanted" />
                    <asp:BoundField DataField="daily_quota" HeaderText="Daily Quota" 
                         SortExpression="daily_quota" />                                                            
               </Columns>
               
               
          </asp:GridView>    
     </asp:Panel>
    
     <div class="brclear"></div>

</asp:Content>

