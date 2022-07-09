<%@ Page Title="" MaintainScrollPositionOnPostback="true" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="AdminSite.Users" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="container p-4">
            <div class="form-group">
                <fieldset class="p-4">
                    <legend>List of registered users</legend>


                    <asp:Repeater runat="server" ID="Repeater">
                        <HeaderTemplate>
                            <table id="myTable" class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">Email</th>
                                        <th scope="col">Address</th>
                                        <th scope="col">Created at</th>
                                        <th scope="col">Deleted at</th>
                                        <th scope="col">Phone number</th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("Id") %></th>
                                <th><%# Eval("UserName") %></th>
                                <td><%# Eval("Email") %></td>
                                <td><%# Eval("Address") %></td>
                                <td><%# Eval("CreatedAt") %></td>
                                <td><%# Eval("DeletedAt") %></td>
                                <td><%# Eval("PhoneNumber") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>

                    </asp:Repeater>

                </fieldset>
            </div>
        </div>
</asp:Content>
