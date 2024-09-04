<%@ Page Title="EmployeesList" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesList.aspx.cs" Inherits="Web_Forms.Pages.EmployeesList" Async="true" %>
<%@ Register TagPrefix="uc" TagName="AdvancedSearch" Src="~/UserControls/AdvancedSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="Popup" Src="~/UserControls/Popup.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="<%= ResolveUrl("~/Content/_employees-list.css") %>" rel="stylesheet" type="text/css" />

    <%-- popup --%>
    <uc:Popup ID="PopupControl" runat="server" />

    <div class="main-container">

        <%-- title --%>
        <h1>Employee List</h1>

        <!-- Search Controls -->
        <uc:AdvancedSearch ID="AdvancedSearchControl" runat="server" />

        <div class="semi-container">
            <%-- GridView --%>
            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
                OnRowDeleting="gvEmployees_RowDeleting"
                OnPageIndexChanging="gvEmployees_PageIndexChanging" 
                AllowPaging="True" 
                PageSize="6" 
                CssClass="employee-table">
                <PagerStyle CssClass="gridview-pager" />
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="ID" ReadOnly="True" />
                    <asp:TemplateField HeaderText="First Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>' CssClass="form-label"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Last Name">
                        <ItemTemplate>
                            <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>' CssClass="form-label"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' CssClass="form-label"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <ItemTemplate>
                            <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' CssClass="form-label"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hire Date">
                        <ItemTemplate>
                            <asp:Label ID="lblHireDate" runat="server" Text='<%# Bind("HireDate", "{0:yyyy-MM-dd}") %>' CssClass="form-label"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:HyperLink ID="hlEdit" runat="server" 
                                NavigateUrl='<%# String.Format("~/EmployeesForm?EmployeeID={0}", Eval("EmployeeID")) %>'
                                CssClass="btn btn-edit" 
                                Text="Edit" />
                            <asp:LinkButton ID="btnDelete" runat="server" 
                                CommandName="Delete" 
                                CommandArgument='<%# Eval("EmployeeID") %>' 
                                CssClass="btn btn-delete" 
                                Text="Delete" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
            
        <%-- button --%>
        <div class="add-button-space">
              <asp:HyperLink ID="hlAddEmployee" runat="server" NavigateUrl="~/EmployeesForm" CssClass="btn btn-add">Add New Employee</asp:HyperLink>
        </div>
    </div>
</asp:Content>

