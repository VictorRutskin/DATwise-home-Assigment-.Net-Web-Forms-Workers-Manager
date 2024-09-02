<%@ Page Title="EmployeesForm" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesForm.aspx.cs" Inherits="Web_Forms.Pages.EmployeesForm" Async="true" %>
<%@ Register TagPrefix="uc" TagName="Popup" Src="~/UserControls/Popup.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <uc:Popup ID="PopupControl" runat="server" />

    <h1>Employee Form</h1>

    <asp:Label ID="lblFirstName" runat="server" Text="First Name:" AssociatedControlID="txtFirstName" />
    <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
    <asp:Label ID="lblFirstNameError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
    <br />

    <asp:Label ID="lblLastName" runat="server" Text="Last Name:" AssociatedControlID="txtLastName" />
    <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
    <asp:Label ID="lblLastNameError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
    <br />

    <asp:Label ID="lblEmail" runat="server" Text="Email:" AssociatedControlID="txtEmail" />
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:Label ID="lblEmailError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
    <br />

    <asp:Label ID="lblPhone" runat="server" Text="Phone:" AssociatedControlID="txtPhone" />
    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
    <asp:Label ID="lblPhoneError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
    <br />

    <asp:Label ID="lblHireDate" runat="server" Text="Hire Date:" AssociatedControlID="txtHireDate" />
    <asp:TextBox ID="txtHireDate" runat="server" TextMode="Date"></asp:TextBox>
    <asp:Label ID="lblHireDateError" runat="server" CssClass="text-danger" ForeColor="Red"></asp:Label>
    <br />


    <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
        ConnectionString="<%$ ConnectionStrings:DATwiseDbConnection %>" 
        SelectCommand="SELECT * FROM Employees WHERE EmployeeID = @EmployeeID" 
        InsertCommand="INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate) VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate)"
        UpdateCommand="UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, HireDate = @HireDate WHERE EmployeeID = @EmployeeID"
        DeleteCommand="DELETE FROM Employees WHERE EmployeeID = @EmployeeID">
        <SelectParameters>
            <asp:QueryStringParameter Name="EmployeeID" QueryStringField="EmployeeID" Type="Int32" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="HireDate" Type="DateTime" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
            <asp:Parameter Name="FirstName" Type="String" />
            <asp:Parameter Name="LastName" Type="String" />
            <asp:Parameter Name="Email" Type="String" />
            <asp:Parameter Name="Phone" Type="String" />
            <asp:Parameter Name="HireDate" Type="DateTime" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>

</asp:Content>
