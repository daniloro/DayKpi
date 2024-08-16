<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ObjetivoMissao.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.ObjetivoMissao" %>
<%@ Register Src="~/UserControl/FiltroMesGestor.ascx" TagPrefix="uc1" TagName="FiltroMesGestor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Objetivo e Missão</h2>

    <uc1:FiltroMesGestor ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" /> 
    
    <div class="row pt-4">
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