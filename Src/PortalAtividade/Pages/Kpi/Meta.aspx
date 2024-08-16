<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Meta.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.Meta" %>
<%@ Register Src="~/UserControl/FiltroMesGestor.ascx" TagPrefix="uc1" TagName="FiltroMesGestor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Meta</h2>

    <uc1:FiltroMesGestor ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <asp:PlaceHolder ID="phAnterior" runat="server">
        <div class="row pt-4">
            <div class="col-md-12">
                <div class="form-group">
                    <label><asp:Literal ID="ltlPerguntaAnterior" runat="server" /></label>
                    <asp:TextBox ID="txtAnaliseAnterior" TextMode="MultiLine" Height="250px" CssClass="form-control" Enabled="false" runat="server" />
                </div>                
            </div>
        </div>
    </asp:PlaceHolder>
    
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