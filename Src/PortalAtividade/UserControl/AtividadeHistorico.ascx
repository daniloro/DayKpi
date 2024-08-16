<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AtividadeHistorico.ascx.cs" Inherits="PortalAtividade.UserControl.AtividadeHistorico" %>

<asp:GridView ID="gdvHistorico" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
    GridLines="None" OnPreRender="GdvHistorico_PreRender" OnRowDataBound="GdvHistorico_RowDataBound" EnableViewState="True">
    <EmptyDataTemplate>
        <div class="box-alerta-emptyData">
            <div class="text-center">
                <p>Nenhuma ocorrência encontrada para esta consulta!</p>
            </div>
        </div>
    </EmptyDataTemplate>
    <Columns>
        <asp:BoundField HeaderText="Data Alteração" />
        <asp:BoundField HeaderText="Descrição" />
        <asp:BoundField HeaderText="Operador" />                    
        <asp:BoundField HeaderText="Data Planejada Conclusão" />
        <asp:BoundField HeaderText="Observação" />
    </Columns>
</asp:GridView>