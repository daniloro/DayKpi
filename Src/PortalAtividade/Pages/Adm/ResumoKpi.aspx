<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResumoKpi.aspx.cs" Inherits="PortalAtividade.Pages.Adm.ResumoKpi" %>
<%@ Register Src="~/UserControl/FiltroMes.ascx" TagPrefix="uc1" TagName="FiltroMes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Resumo KPI</h2>    

    <uc1:FiltroMes ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />
    
    <p>Quantidade de itens com análise pendente de envio ou adequação. Antes da apresentação do KPI todos itens deverão estar zerados.</p>

    <div class="table-responsive">
        <asp:GridView ID="gdvGestor" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
            GridLines="None" OnPreRender="GdvGestor_PreRender" OnRowDataBound="GdvGestor_RowDataBound" EnableViewState="True">
            <EmptyDataTemplate>
                <div class="box-alerta-emptyData">
                    <div class="text-center">
                        <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                    </div>
                </div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Nome" />
                <asp:BoundField HeaderText="Tipo Equipe" />
                <asp:BoundField HeaderText="Objetivo" />                
                <asp:BoundField HeaderText="Organograma" />
                <asp:BoundField HeaderText="Meta" />
                <asp:BoundField HeaderText="Backlog" />
                <asp:BoundField HeaderText="EmFila" />
                <asp:BoundField HeaderText="Aberto Concluído" />
                <asp:BoundField HeaderText="Atendimento" />
                <asp:BoundField HeaderText="Performance" />
                <asp:BoundField HeaderText="GMUD" />                
                <asp:BoundField HeaderText="Hora Extra" />
                <asp:BoundField HeaderText="Atividade Vencida" />
                <asp:TemplateField HeaderText="" ItemStyle-CssClass="text-right">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDetalhe" CssClass="btn btn-outline-primary" Text="Detalhe" OnCommand="ObterKpi" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>