<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AbertoConcluido.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.AbertoConcluido" %>
<%@ Register Src="~/UserControl/FiltroMesGrupo.ascx" TagPrefix="uc1" TagName="FiltroMesGrupo" %>
<%@ Register Src="~/UserControl/Chart/ComboChart.ascx" TagPrefix="uc1" TagName="ComboChart" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });        
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Aberto X Concluído</h2>

    <uc1:FiltroMesGrupo ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <asp:Button ID="btnExcel" CssClass="btn btn-default xls" Text="Excel" OnClick="BtnExcel_Click" runat="server" />

    <uc1:ComboChart runat="server" ID="ucChart" />

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label><asp:Literal ID="ltlPergunta1" runat="server" /></label>
                <asp:TextBox ID="txtAnalise1" TextMode="MultiLine" Height="250px" CssClass="form-control" runat="server" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label><asp:Literal ID="ltlPergunta2" runat="server" /></label>
                <asp:TextBox ID="txtAnalise2" TextMode="MultiLine" Height="250px" CssClass="form-control" runat="server" />
            </div>
        </div>
    </div>    
    <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary mr-2" OnClick="BtnSalvar_Click" Text="Salvar" runat="server" />
    <asp:LinkButton ID="btnSalvarEnviar" CssClass="btn btn-primary" OnClick="BtnSalvarEnviar_Click" Text="Salvar e Enviar" runat="server" />    

    <script>
        $("[id$=txtAnalise1],[id$=txtAnalise2]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });

        $("[id$=btnSalvarEnviar]").click(function () {
            DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente enviar? Não poderá realizar alterações futuras.', $(this));
            return false;
        });
    </script>

</asp:Content>