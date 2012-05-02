<%@ Page Title="" Language="C#" MasterPageFile="~/web.Master" AutoEventWireup="true" CodeBehind="CreateNewCampaign.aspx.cs" Inherits="WebApp.manage.Campaigns.CreateNewCampaign" %>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContentPlaceHolder" Runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

	
	<h1>Create a New Campaign</h1>

	<asp:Panel ID="DetailsPanel" runat="server">
		

			<span class="leftLabel">Campaign Title</span>
					<asp:TextBox ID="TitleTextBox" runat="server" CssClass="textField" Columns="50" MaxLength="200" ></asp:TextBox>
					<asp:RequiredFieldValidator ID="TitleRequiredFieldValidator" runat="server" ErrorMessage="Enter a title for the ad." ControlToValidate="TitleTextBox" ValidationGroup="NewCampaign" ></asp:RequiredFieldValidator>
	              
			<div class="brclear"></div> 


			<span class="leftLabel">Notes</span>
						<asp:TextBox ID="NotesTextBox" runat="server" Columns="40" Rows="4" 
							MaxLength="1000" CssClass="textField" ValidationGroup="NewStore" TextMode="MultiLine">
						</asp:TextBox>
	          
			<div class="brclear"></div> 
			<br />

		    <span class="leftLabel">Start Date:</span>
					<asp:TextBox ID="StartDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
					<cc1:CalendarExtender ID="StartDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="StartDateTextBox"></cc1:CalendarExtender>
					<cc1:MaskedEditExtender ID="StartDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="StartDateTextBox"></cc1:MaskedEditExtender>
					<asp:RequiredFieldValidator ID="StartDateRequiredFieldValidator" runat="server" ErrorMessage="Start Date Required" Display="dynamic" ControlToValidate="StartDateTextBox" ValidationGroup="NewCampaign"></asp:RequiredFieldValidator>
	          
			<div class="brclear"></div> 

			<span class="leftLabel">End Date:</span>
					<asp:TextBox ID="EndDateTextBox" runat="server" Columns="10" CssClass="textField"></asp:TextBox>
					<cc1:CalendarExtender ID="EndDateCalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="EndDateTextBox"></cc1:CalendarExtender>
					<cc1:MaskedEditExtender ID="EndDateMaskedEditExtender" runat="server" Mask="99/99/9999" MaskType="Date" TargetControlID="EndDateTextBox"></cc1:MaskedEditExtender>
					<asp:RequiredFieldValidator ID="EndDateRequiredFieldValidator" runat="server" ErrorMessage="End Date Required" Display="dynamic" ControlToValidate="EndDateTextBox" ValidationGroup="NewCampaign" ></asp:RequiredFieldValidator>
					<asp:CompareValidator ID="DateCompareValidator" runat="server" ErrorMessage="End date must be later than start date." Display="Dynamic" ControlToCompare="StartDateTextBox" ControlToValidate="EndDateTextBox" Operator="GreaterThan" Type="Date" ValidationGroup="NewCampaign"></asp:CompareValidator>
			<div class="brclear"></div> 

		   <%--  <asp:Label ID="BudgetLabel" runat="server" Text="Budget:" CssClass="leftLabel" AssociatedControlID="NotesTextBox"></asp:Label>
				$<asp:TextBox ID="BudgetTextBox" runat="server" Columns="5" CssClass="textField"></asp:TextBox>
				<asp:RequiredFieldValidator ID="BudgetRequiredFieldValidator" runat="server" ErrorMessage="Please enter a budget for the campaign." ControlToValidate="BudgetTextBox" Display="Dynamic" ValidationGroup="NewCampaign"></asp:RequiredFieldValidator>
				<asp:RangeValidator ID="BudgetRangeValidator" runat="server" ErrorMessage="Please enter a dollar amount between $50 and $10000" ControlToValidate="BudgetTextBox" Display="Dynamic" MaximumValue="10000" MinimumValue="50" Type="Integer" ValidationGroup="NewCampaign"></asp:RangeValidator>

		    --%> <div class="brclear"></div> 
	          
			
			<span class="leftLabel">Setup Type</span>
				<asp:RadioButtonList ID="SetupRadioButtonList" runat="server"  CssClass="textField">
					<asp:ListItem Selected="True" Value="simple">Simple</asp:ListItem>
					<asp:ListItem Value="advanced">Advanced</asp:ListItem>
				</asp:RadioButtonList>
				<i>Use Simple if you are new to DocketPlace. Advanced campaigns can be created from simple ones.</i>
				
			<div class="brclear"></div> 

			<asp:Button ID="DetailsNextButton" runat="server" Text="Next" onclick="DetailsNextButton_Click" ValidationGroup="NewCampaign" CssClass="submitButton"/>		
			

			&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp or 

			<a href="/manage/Campaigns/Campaigns.aspx" >Cancel</a>
	          
	     	
			<div class="brclear"></div> 

			<asp:Label ID="CreateCampaignErrorLabel" runat="server" Text="" CssClass="errorMessage" EnableViewState="false" ></asp:Label>

		</asp:Panel>

	 <asp:Panel ID="StoresPanel" runat="server" Visible="false">
     
          <h2>Step 1 of 2. Decide on Stores to place Ads in.</h2>
          
         <%-- <asp:Panel ID="StoreSearchPanel" runat="server">
          
               <asp:Button ID="ShowOwnButton" runat="server" Text="Own Stores" onclick="ShowOwnButton_Click" />
               
               <asp:Button ID="ShowAllButton" runat="server" Text="Show  All Other Stores" onclick="ShowAllButton_Click" />
          </asp:Panel>
           
          <br />
          <br />--%>

          <asp:Panel ID="StoreResultsPanel" runat="server" >  

               <asp:Label ID="StoreSelectErrorLabel" runat="server" CssClass="errorMessage" EnableViewState="false"></asp:Label>

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
			
			&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp or 

			<a href="/manage/Campaigns/Campaigns.aspx" >Cancel</a>
	          
          </asp:Panel>
     </asp:Panel>

	
     <asp:Panel ID="AdsPanel" runat="server" Visible="false">
          

          <h2>Step 2 of 2. Pick Ads for the Selected Stores </h2>     
        
          <asp:Panel ID="AdsLibraryPanel" runat="server">

			<asp:Button ID="BackButton" runat="server" Text="Back" onclick="BackButton_Click" />     

     
               <asp:Button ID="SaveAdsButton" runat="server" Text="Save" onclick="SaveAdsButton_Click"  />	&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp or <a href="/manage/Campaigns/Campaigns.aspx" >Cancel</a>
               
               <br />     

               <asp:Label ID="SelectAdsErrorLabel" runat="server" CssClass="errorMessage" EnableViewState="false"></asp:Label>

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
                                   <asp:Image ID="AdImage" runat="server" ImageUrl='<%# WebApp.AppCode.Helpers.GenerateImage(Eval("data").ToString()) %>' Width="150px"  CssClass="textField" />
                                       
                                
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

   	

</asp:Content>

