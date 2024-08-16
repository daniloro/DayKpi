<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Performance.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.Performance" %>
<%@ Register Src="~/UserControl/FiltroMesGestorOperador.ascx" TagPrefix="uc1" TagName="FiltroMesGestorOperador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Performance Mensal</h2>

    <uc1:FiltroMesGestorOperador ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <h5 class="pt-4"><asp:Literal ID="ltlNomeOperador" runat="server" /></h5>

    <div class="form-group row mb-4">
        <div class="col-md-2 font-weight-bold">
            Ponderação Atividade Concluída
        </div>
        <div class="col-md-2">
            <asp:Literal runat="server" ID="ltlPonderacao" />
        </div>
        <div class="col-md-2 font-weight-bold">
            Ponderação Total (Atividade + Confirmação + Avaliação)
        </div>
        <div class="col-md-2">
            <asp:Literal runat="server" ID="ltlPonderacaoTotal" />
        </div>
    </div>

    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label><asp:Literal ID="ltlPergunta1" runat="server" /></label>
                <asp:DropDownList ID="ddlAnalise1" CssClass="form-control" runat="server">
                    <asp:ListItem Text="Selecione" Value="0" runat="server" />
                    <asp:ListItem Text="1" Value="1" runat="server" />
                    <asp:ListItem Text="2" Value="2" runat="server" />
                    <asp:ListItem Text="3" Value="3" runat="server" />
                    <asp:ListItem Text="4" Value="4" runat="server" />
                    <asp:ListItem Text="5" Value="5" runat="server" />
                </asp:DropDownList>
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
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label><asp:Literal ID="ltlPergunta3" runat="server" /></label>
                <asp:TextBox ID="txtAnalise3" TextMode="MultiLine" Height="250px" CssClass="form-control" runat="server" />
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="form-group">
                <label><asp:Literal ID="ltlPergunta4" runat="server" /></label>
                <asp:TextBox ID="txtAnalise4" TextMode="MultiLine" Height="250px" CssClass="form-control" runat="server" />
            </div>
        </div>
    </div>
    <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary mr-2" OnClick="BtnSalvar_Click" Text="Salvar" runat="server" />
    <asp:LinkButton ID="btnSalvarEnviar" CssClass="btn btn-primary" OnClick="BtnSalvarEnviar_Click" Text="Salvar e Enviar" runat="server" />

    <script>
        $("[id$=txtAnalise2],[id$=txtAnalise3],[id$=txtAnalise4]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });

        $("[id$=btnSalvarEnviar]").click(function () {
            DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente enviar? Não poderá realizar alterações futuras.', $(this));
            return false;
        });
    </script>

</asp:Content>
