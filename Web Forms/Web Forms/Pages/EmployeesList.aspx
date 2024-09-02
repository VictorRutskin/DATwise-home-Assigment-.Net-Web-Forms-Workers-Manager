<%@ Page Title="EmployeesList" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesList.aspx.cs" Inherits="Web_Forms.Pages.EmployeesList" Async="true" %>

<%@ Register TagPrefix="uc" TagName="AdvancedSearch" Src="~/UserControls/AdvancedSearch.ascx" %>
<%@ Register TagPrefix="uc" TagName="Popup" Src="~/UserControls/Popup.ascx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <uc:Popup ID="PopupControl" runat="server" />

    <h1>Employee List</h1>

    <!-- Search Controls -->
    <uc:AdvancedSearch ID="AdvancedSearchControl" runat="server" />

    <!-- SqlDataSource -->
    <asp:SqlDataSource ID="SqlDataSource1" runat="server"
        ConnectionString="<%$ ConnectionStrings:DATwiseDbConnection %>"
        SelectCommand="SELECT * FROM Employees"
        DeleteCommand="DELETE FROM Employees WHERE EmployeeID=@EmployeeID"
        InsertCommand="INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate) VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate)">
        <DeleteParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="HireDate" Type="DateTime" />
        </InsertParameters>
    </asp:SqlDataSource>

    <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
        DataSourceID="SqlDataSource1"
        OnRowDeleting="gvEmployees_RowDeleting">
        <Columns>
            <asp:BoundField DataField="EmployeeID" HeaderText="ID" ReadOnly="True" />
            <asp:BoundField DataField="FirstName" HeaderText="First Name" />
            <asp:BoundField DataField="LastName" HeaderText="Last Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:BoundField DataField="Phone" HeaderText="Phone" />
            <asp:BoundField DataField="HireDate" HeaderText="Hire Date" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:HyperLinkField DataNavigateUrlFields="EmployeeID" DataNavigateUrlFormatString="~/EmployeesForm.aspx?EmployeeID={0}"
                Text="Edit" HeaderText="Actions" />

            <asp:CommandField ShowDeleteButton="True" />
        </Columns>
    </asp:GridView>

    <asp:HyperLink ID="hlAddEmployee" runat="server" NavigateUrl="EmployeeForm.aspx">Add New Employee</asp:HyperLink>

</asp:Content>
