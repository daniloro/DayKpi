<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="PortalAtividade.ErrorPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Alerta</h2>
    <h3 class="pb-4">Funcionalidade Temporariamente indisponível. Tente novamente mais tarde.</h3>
    <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" Text="Voltar" PostBackUrl="~/Default.aspx" runat="server" />
</asp:Content>