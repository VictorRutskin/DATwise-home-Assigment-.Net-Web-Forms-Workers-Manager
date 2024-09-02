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
        UpdateCommand="UPDATE Employees SET FirstName=@FirstName, LastName=@LastName, Email=@Email, Phone=@Phone, HireDate=@HireDate WHERE EmployeeID=@EmployeeID"
        DeleteCommand="DELETE FROM Employees WHERE EmployeeID=@EmployeeID"
        InsertCommand="INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate) VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate)">
        <UpdateParameters>
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="HireDate" Type="DateTime" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </UpdateParameters>
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
        OnRowEditing="gvEmployees_RowEditing"
        OnRowDeleting="gvEmployees_RowDeleting"
        OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
        OnRowUpdating="gvEmployees_RowUpdating">
        <Columns>
            <asp:BoundField DataField="EmployeeID" HeaderText="ID" ReadOnly="True" />
            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <asp:Label ID="lblFirstName" runat="server" Text='<%# Bind("FirstName") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtFirstName" runat="server" Text='<%# Bind("FirstName") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <asp:Label ID="lblLastName" runat="server" Text='<%# Bind("LastName") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtLastName" runat="server" Text='<%# Bind("LastName") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <asp:Label ID="lblEmail" runat="server" Text='<%# Bind("Email") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEmail" runat="server" Text='<%# Bind("Email") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone">
                <ItemTemplate>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Bind("Phone") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtPhone" runat="server" Text='<%# Bind("Phone") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Hire Date">
                <ItemTemplate>
                    <asp:Label ID="lblHireDate" runat="server" Text='<%# Bind("HireDate", "{0:yyyy-MM-dd}") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtHireDate" runat="server" Text='<%# Bind("HireDate", "{0:yyyy-MM-dd}") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:HyperLinkField DataNavigateUrlFields="EmployeeID" DataNavigateUrlFormatString="EmployeeForm.aspx?EmployeeID={0}"
                Text="Edit" HeaderText="Actions" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>

    <asp:HyperLink ID="hlAddEmployee" runat="server" NavigateUrl="EmployeeForm.aspx">Add New Employee</asp:HyperLink>

</asp:Content>
