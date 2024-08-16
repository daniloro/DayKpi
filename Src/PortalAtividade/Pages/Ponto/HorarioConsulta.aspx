<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorarioConsulta.aspx.cs" Inherits="PortalAtividade.Pages.Ponto.HorarioConsulta" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Horário</h2>
    <h3>Consultar Equipe</h3>

    <div class="table-responsive">
        <asp:GridView ID="gdvPonto" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
            GridLines="None" OnPreRender="GdvPonto_PreRender" OnRowDataBound="GdvPonto_RowDataBound">
            <EmptyDataTemplate>
                <div class="box-alerta-emptyData">
                    <div class="text-center">
                        <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                    </div>
                </div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Nome" />
                <asp:BoundField HeaderText="Início" />
                <asp:BoundField HeaderText="Almoço" />
                <asp:BoundField HeaderText="Retorno" />
                <asp:BoundField HeaderText="Saída" />                
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>