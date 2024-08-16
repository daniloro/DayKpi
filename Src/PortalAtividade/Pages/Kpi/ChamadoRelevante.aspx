<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ChamadoRelevante.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.ChamadoRelevante" %>
<%@ Register Src="~/UserControl/FiltroMesGestor.ascx" TagPrefix="uc1" TagName="FiltroMesGestor" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Chamados Relevantes</h2>
    <h3><asp:Literal ID="ltlTitulo" Text="Chamados Relevantes" runat="server" /></h3>

    <uc1:FiltroMesGestor ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <div class="row mb-2">
        <div class="col-sm-12 col-md-6 col-lg-2">
            <div class="form-group">
                <label>NroChamado</label>
                <asp:TextBox runat="server" ID="txtNroChamado" MaxLength="14" CssClass="form-control" />
            </div>
        </div>
        <div class="col-sm-12 col-md-6 col-lg-2">
            <div class="mt-4 pt-2">
                <asp:LinkButton runat="server" ID="lbIncluirChamado" CssClass="btn btn-primary" OnClick="BtnIncluirChamado_Click" Text="Incluir &raquo;" />
            </div>
        </div>
    </div>

    <div class="table-responsive">
        <asp:GridView ID="gdvChamado" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
            GridLines="None" OnPreRender="GdvChamado_PreRender" OnRowDataBound="GdvChamado_RowDataBound" EnableViewState="True">
            <EmptyDataTemplate>
                <div class="box-alerta-emptyData">
                    <div class="text-center">
                        <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                    </div>
                </div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="NroChamado" />
                <asp:BoundField HeaderText="DscChamado" />
                <asp:BoundField HeaderText="Categoria" />
                <asp:BoundField HeaderText="SubCategoria" />
                <asp:BoundField HeaderText="Data Conclusão" />
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-right">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnExcluir" CssClass="btn btn-outline-danger" Text="Excluir" OnCommand="ExcluirChamadoRelevante" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script>
        $("[id$=txtNroChamado]").numeric({ allow: "RA\\ " });
    </script>
</asp:Content>