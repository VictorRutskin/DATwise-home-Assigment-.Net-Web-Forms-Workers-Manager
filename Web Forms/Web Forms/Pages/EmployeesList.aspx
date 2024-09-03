<%@ Page Title="EmployeesList" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesList.aspx.cs" Inherits="Web_Forms.Pages.EmployeesList" Async="true" %>
<%@ Register TagPrefix="uc" TagName="AdvancedSearch" Src="~/UserControls/AdvancedSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="Popup" Src="~/UserControls/Popup.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/_employees-list.css") %>" rel="stylesheet" type="text/css" />

    <%-- popup --%>
    <uc:Popup ID="PopupControl" runat="server" />

    <div class="container">
    <%-- title --%>
    <h1>Employee List</h1>

    <!-- Search Controls -->
    <uc:AdvancedSearch ID="AdvancedSearchControl" runat="server" />

    <%-- GridView --%>
    <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
        OnRowEditing="gvEmployees_RowEditing"
        OnRowDeleting="gvEmployees_RowDeleting"
        OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
        OnRowUpdating="gvEmployees_RowUpdating">
        <Columns>
            <asp:BoundField DataField="EmployeeID" HeaderText="ID" ReadOnly="True" />
            <asp:TemplateField HeaderText="First Name" >
                <ItemTemplate>
                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="form-label"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="txtFirstName" />
                        <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>' />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="form-label"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName" />
                        <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>' />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-label"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
                        <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone">
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' CssClass="form-label"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone" />
                        <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hire Date">
                <ItemTemplate>
                    <asp:Label ID="lblHireDate" runat="server" Text='<%# Bind("HireDate", "{0:yyyy-MM-dd}") %>' CssClass="form-label"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="form-group">
                        <asp:Label ID="lblHireDate" runat="server" Text="Hire Date:" AssociatedControlID="txtHireDate" />
                        <asp:TextBox ID="txtHireDate" runat="server" Text='<%# Bind("HireDate", "{0:yyyy-MM-dd}") %>' />
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="EmployeeID" DataNavigateUrlFormatString="~/EmployeesForm?EmployeeID={0}"
                Text="Edit" HeaderText="Actions"/>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>

    <%-- button --%>
    <asp:HyperLink ID="hlAddEmployee" runat="server" NavigateUrl="~/EmployeesForm">Add New Employee</asp:HyperLink>
   </div>

</asp:Content>

