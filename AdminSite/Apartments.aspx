﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="Apartments.aspx.cs" Inherits="AdminSite.Apartments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <asp:Panel runat="server" ID="pnlApartments">
        <div class="container" style="display:flex; flex-direction:column; justify-content:center; align-items:flex-start; width:30%">
            <asp:Label ID="lblStatusFilter" CssClass="form-label" runat="server" for="ddlStatusFilter" Text="Filtriraj po statusu"></asp:Label>
            <asp:DropDownList AutoPostBack="true" ID="ddlStatusFilter" CssClass="form-select" runat="server"></asp:DropDownList>
            <asp:Label ID="lblCityFilter" CssClass="form-label" runat="server" for="ddlCityFilter" Text="Filtriraj po gradu"></asp:Label>
            <asp:DropDownList AutoPostBack="true" ID="ddlCityFilter" CssClass="form-select"  runat="server"></asp:DropDownList>
        </div>
        <div class="container">
                <fieldset class="p-4">
                    <legend>List of Apartments</legend>
                    <asp:Repeater runat="server" ID="Repeater">
                        <HeaderTemplate>
                            <table id="myTable" class="table">
                                <thead>
                                    <tr>
                                        <th scope="col">#</th>
                                        <th scope="col">Name</th>
                                        <th scope="col">City</th>
                                        <th scope="col">Address</th>
                                        <th scope="col">Adults</th>
                                        <th scope="col">Children</th>
                                        <th scope="col">Rooms</th>
                                        <th scope="col">Beach distance</th>
                                        <th scope="col">Price</th>
                                        <th hidden="hidden"></th>
                                        <th hidden="hidden"></th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("Id") %></th>
                                <th><%# Eval("Name") %></th>
                                <td><%# Eval("City") %></td>
                                <td><%# Eval("Address") %></td>
                                <td><%# Eval("MaxAdults") %></td>
                                <td><%# Eval("MaxChildren") %></td>
                                <td><%# Eval("TotalRooms") %></td>
                                <td><%# Eval("BeachDistance") %></td>
                                <td><%# Eval("Price") %></td>
                                <td> 
                                    <asp:Button OnClick="btnUredi_Click" CssClass="btn btn-link" CommandArgument='<%# Eval("Id") %>' Text="Uredi" ID="btnUredi" runat="server" CausesValidation="False"  UseSubmitBehavior="false"/>
                                </td>
                                <td> 
                                    <asp:Button OnClick="btnDelete_Click" CssClass="btn btn-danger" CommandArgument='<%# Eval("Id") %>' Text="Izbrisi" ID="btnDelete" runat="server"/>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                        </table>
                        </FooterTemplate>

                    </asp:Repeater>

                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-primary" runat="server" Text="Dodaj" />
                </fieldset>
            </div>
    </asp:Panel>
</asp:Content>

