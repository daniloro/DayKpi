<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GmudConsulta.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.GmudConsulta" %>

<%@ Register Src="~/UserControl/FiltroMes.ascx" TagPrefix="uc1" TagName="FiltroMes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">GMUD</h2>
    <h3>
        <asp:Literal ID="ltlTitulo" Text="GMUD Período" runat="server" /></h3>

    <asp:MultiView ID="mwGmud" runat="server">
        <asp:View ID="vwLista" runat="server">

            <uc1:FiltroMes ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

            <div class="form-group row mb-4">
                <div class="col-md-2 font-weight-bold">
                    Total de GMUD
                </div>
                <div class="col-md-4">
                    <asp:Literal runat="server" ID="ltlTotal" />
                </div>
                <div class="col-md-2 font-weight-bold">
                    Total de Chamados
                </div>
                <div class="col-md-4">
                    <asp:Literal runat="server" ID="ltlTotalChamado" />
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gdvGmud" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                    GridLines="None" OnPreRender="GdvGmud_PreRender" OnRowDataBound="GdvGmud_RowDataBound" EnableViewState="True">
                    <EmptyDataTemplate>
                        <div class="box-alerta-emptyData">
                            <div class="text-center">
                                <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                            </div>
                        </div>
                    </EmptyDataTemplate>
                    <Columns>
                        <asp:BoundField HeaderText="NroGmud" ItemStyle-Wrap="false" />
                        <asp:BoundField HeaderText="Sistema" />
                        <asp:BoundField HeaderText="Versão" />
                        <asp:BoundField HeaderText="Descrição" />
                        <asp:BoundField HeaderText="DataGmud" />
                        <asp:BoundField HeaderText="QtdChamado" />
                        <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDetalhe" CssClass="btn btn-outline-primary" Text="Detalhe" OnCommand="ObterGmud" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:View>

        <asp:View ID="vwDetalhe" runat="server">
            <hr />

            <div class="form-group row mb-2">
                <div class="col-md-2 font-weight-bold">
                    Nro GMUD
                </div>
                <div class="col-md-2">
                    <asp:Literal runat="server" ID="ltlNroGmud" />
                </div>
                <div class="col-md-2 font-weight-bold">
                    Data GMUD
                </div>
                <div class="col-md-2">
                    <asp:Literal runat="server" ID="ltlDataGmud" />
                </div>
            </div>
            <div class="form-group row mb-2">
                <div class="col-md-2 font-weight-bold">
                    Descrição
                </div>
                <div class="col-md-8">
                    <asp:Literal runat="server" ID="ltlDscGmud" />
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView ID="gdvGmudDetalhe" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                    GridLines="None" OnPreRender="GdvGmudDetalhe_PreRender" OnRowDataBound="GdvGmudDetalhe_RowDataBound" EnableViewState="True">
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
                    </Columns>
                </asp:GridView>
            </div>

            <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
        </asp:View>
    </asp:MultiView>
</asp:Content>
