<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AtividadeAndamento.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.AtividadeAndamento" %>
<%@ Register Src="~/UserControl/FiltroGrupo.ascx" TagPrefix="uc1" TagName="FiltroGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Daily</h2>
    <h3>Lista de Atividades em Andamento</h3>

    <uc1:FiltroGrupo ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <div class="form-group row mb-4">
        <div class="col-md-2 font-weight-bold">
            Total
        </div>
        <div class="col-md-4">
            <asp:Literal runat="server" ID="ltlTotal" />
        </div>
        <div class="col-md-2 font-weight-bold">
            Pendente
        </div>
        <div class="col-md-4">
            <asp:Literal runat="server" ID="ltlPendente" />
        </div>
    </div>

    <div class="table-responsive">
        <asp:GridView ID="gdvAtividade" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
            GridLines="None" OnPreRender="GdvAtividade_PreRender" OnRowDataBound="GdvAtividade_RowDataBound" EnableViewState="True">
            <EmptyDataTemplate>
                <div class="box-alerta-emptyData">
                    <div class="text-center">
                        <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                    </div>
                </div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Operador" />
                <asp:BoundField HeaderText="NroChamado" ItemStyle-Wrap="false" />
                <asp:BoundField HeaderText="DscChamado" />
                <asp:BoundField HeaderText="NroAtividade" ItemStyle-Wrap="false" />
                <asp:BoundField HeaderText="Tipo" />
                <asp:BoundField HeaderText="Data Planejada" />
                <asp:TemplateField HeaderText="Status" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnConfirmar" CssClass="btn btn-outline-primary" OnCommand="ConfirmarAtividade" Text="Confirmar" Visible="false" runat="server" />
                        <asp:LinkButton ID="btnAlterarData" CssClass="btn btn-outline-primary" OnCommand="AlterarDataAtividade" Text="Alterar Data" Visible="false" runat="server" />
                        <asp:LinkButton ID="btnExcluir" CssClass="btn btn-outline-primary" OnCommand="ExcluirAtividade" Text="Excluir" Visible="false" runat="server" />
                        <asp:LinkButton ID="btnIncluir" CssClass="btn btn-outline-primary" OnCommand="IncluirAtividade" Text="Incluir" Visible="false" runat="server" />
                        <asp:Image ID="imgVisto" ImageUrl="~/Content/images/visto.png" Visible="false" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
