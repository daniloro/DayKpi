<%@ Page Title="Atividade" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Atividade.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.Atividade" %>
<%@ Register Src="~/UserControl/FiltroGrupo.ascx" TagPrefix="uc1" TagName="FiltroGrupo" %>
<%@ Register Src="~/UserControl/AtividadeHistorico.ascx" TagPrefix="uc1" TagName="AtividadeHistorico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4"><%: Title %></h2>
    <h3>Lista de Atividades Pendentes</h3>

    <asp:PlaceHolder runat="server" ID="phLista">    
        <hr />
        
        <uc1:FiltroGrupo ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

        <div class="form-group row mb-4">
            <div class="col-md-2 font-weight-bold">
                Total
            </div>
            <div class="col-md-4">
                <asp:Literal runat="server" ID="ltlTotal" />
            </div>
            <div class="col-md-2 font-weight-bold">
                Vencido
            </div>
            <div class="col-md-4">
                <asp:Literal runat="server" ID="ltlVencido" />
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
                    <asp:BoundField HeaderText="NroChamado" ItemStyle-Wrap="false" />
                    <asp:BoundField HeaderText="DscChamado" />
                    <asp:BoundField HeaderText="NroAtividade" ItemStyle-Wrap="false" />
                    <asp:BoundField HeaderText="DscAtividade" />
                    <asp:BoundField HeaderText="Operador" />
                    <asp:BoundField HeaderText="Qtd Alteração" />
                    <asp:BoundField HeaderText="Data Estimada" />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetalhe" CssClass="btn btn-outline-primary" OnCommand="ObterAtividade" Text="Detalhe" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phAtividade">
        <hr />

        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                <asp:Literal runat="server" ID="ltlNroChamado" />
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlDscChamado" />
            </div>
        </div>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                <asp:Literal runat="server" ID="ltlNroAtividade" />
            </div>
            <div class="col-md-6">
                <asp:Literal runat="server" ID="ltlDscAtividade" />
            </div>
            <div class="col-md-2 font-weight-bold">
                Qtd Alteração
            </div>
            <div class="col-md-2">
                <asp:Literal runat="server" ID="ltlQtdRepactuacao" />
            </div>
        </div>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Operador
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlOperador" />
            </div>
        </div>        
        <div class="form-group row mb-4">
            <div class="col-md-2 font-weight-bold">
                Data Estimada
            </div>
            <div class="col-md-10">
                <asp:Label runat="server" ID="lblDataFinal" />
            </div>
        </div>

        <div class="table-responsive">
            <uc1:AtividadeHistorico runat="server" id="ucHistorico" />
        </div>

        <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />        
    </asp:PlaceHolder>
</asp:Content>