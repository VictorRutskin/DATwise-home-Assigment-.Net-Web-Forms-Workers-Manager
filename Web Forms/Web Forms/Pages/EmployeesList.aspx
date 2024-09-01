<%@ Page Title="EmployeesList" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeesList.aspx.cs" Inherits="Web_Forms.Pages.EmployeesList" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

            <h1>Employee List</h1>

            <asp:HyperLink ID="hlAddEmployee" runat="server" NavigateUrl="EmployeeForm.aspx">Add New Employee</asp:HyperLink>

            <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" DataKeyNames="EmployeeID"
                OnRowEditing="gvEmployees_RowEditing"
                OnRowDeleting="gvEmployees_RowDeleting"
                OnRowCancelingEdit="gvEmployees_RowCancelingEdit"
                OnRowUpdating="gvEmployees_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="EmployeeID" HeaderText="ID" ReadOnly="True" />
                    <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                    <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                    <asp:BoundField DataField="Email" HeaderText="Email" />
                    <asp:BoundField DataField="Phone" HeaderText="Phone" />
                    <asp:BoundField DataField="HireDate" HeaderText="Hire Date" DataFormatString="{0:yyyy-MM-dd}" />
                    <asp:HyperLinkField DataNavigateUrlFields="EmployeeID" DataNavigateUrlFormatString="EmployeeForm.aspx?EmployeeID={0}"
                        Text="Edit" HeaderText="Actions" />
                    <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
</asp:Content>

