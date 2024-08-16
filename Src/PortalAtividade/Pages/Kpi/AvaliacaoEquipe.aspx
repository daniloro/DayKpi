<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvaliacaoEquipe.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.AvaliacaoEquipe" %>
<%@ Register Src="~/UserControl/FiltroMesGrupo.ascx" TagPrefix="uc1" TagName="FiltroMesGrupo" %>
<%@ Register Src="~/UserControl/Chart/Tabela.ascx" TagPrefix="uc1" TagName="Tabela" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.load('visualization', '1.1', { packages: ['table'] });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Avaliação Mensal</h2>

    <uc1:FiltroMesGrupo ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <uc1:Tabela runat="server" ID="ucAvaliacao" />
</asp:Content>