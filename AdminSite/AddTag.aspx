﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminMaster.Master" AutoEventWireup="true" CodeBehind="AddTag.aspx.cs" Inherits="AdminSite.AddTag" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    <div class="conatiner p-5">
            <div style="border-radius: 1rem; margin: 0 auto; display: flex; flex-direction: column; align-items: center;">
                    <div class="col-md-4 mb-3">
                        <asp:label runat="server" CssClass="form-label" for="txtName" Text="Ime oznake"></asp:label>
                        <asp:TextBox class="form-control" ID="txtName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="reqName" ControlToValidate="txtName" ErrorMessage="Unesite ime apartmana!" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <asp:label runat="server" CssClass="form-label" for="txtEngName" Text="Ime oznake na engleskom"></asp:label>
                        <asp:TextBox CssClass="form-control" ID="txtEngName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator Style="color: red;" runat="server" ID="reqEngName" ControlToValidate="txtEngName" ErrorMessage="Unesite englesko ime apartmana!" />
                    </div>
                    <div class="col-md-4 mb-3">
                        <asp:label runat="server" CssClass="form-label" for="ddlType" Text="Vrsta oznake"></asp:label>
                        <asp:DropDownList ID="ddlType" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                <div class="btn-group">
                    <asp:Button ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-primary" runat="server" Text="Dodaj" />
                    <asp:Button ID="btnCancel" OnClick="btnCancel_Click" CssClass="btn btn-primary" CausesValidation="false" runat="server" Text="Odustani" />
                </div>
            </div>
        </div>
</asp:Content>
