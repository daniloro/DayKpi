<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MeusChamados.aspx.cs" Inherits="PortalAtividade.Pages.Relatorio.MeusChamados" %>
<%@ Register Src="~/UserControl/Chart/Tabela.ascx" TagPrefix="uc1" TagName="Tabela" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.load('visualization', '1.1', { packages: ['table'] });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Meus Chamados</h2>
    <h4 class="pb-2">Veja o status atual de todos Chamados onde você já atuou.</h4>

    <uc1:Tabela runat="server" ID="ucMeusChamados" />
</asp:Content>