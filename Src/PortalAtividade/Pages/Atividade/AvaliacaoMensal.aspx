<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" MaintainScrollPositionOnPostback="true" CodeBehind="AvaliacaoMensal.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.AvaliacaoMensal" %>
<%@ Register Src="~/UserControl/FiltroMesOperador.ascx" TagPrefix="uc1" TagName="FiltroMesOperador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Resumo Mensal</h2>

    <uc1:FiltroMesOperador ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <h3 class="pt-4">Atividades Concluídas</h3>
    <div class="table-responsive">
        <asp:GridView ID="gdvConcluido" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
            GridLines="None" OnPreRender="GdvConcluido_PreRender" OnRowDataBound="GdvConcluido_RowDataBound" EnableViewState="True" ShowFooter="true">
            <EmptyDataTemplate>
                <div class="box-alerta-emptyData">
                    <div class="text-left">
                        <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                    </div>
                </div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="NroChamado" ItemStyle-Wrap="false" />
                <asp:BoundField HeaderText="NroAtividade" ItemStyle-Wrap="false" />
                <asp:BoundField HeaderText="DscChamado" />
                <asp:BoundField HeaderText="DscAtividade" />
                <asp:BoundField HeaderText="Conclusão" />
                <asp:TemplateField HeaderText="Avaliação" ItemStyle-Width="20px">
                    <ItemTemplate>
                        <asp:Image ID="imgCheck" ImageUrl="~/Content/images/check.png" Visible="false" runat="server" />
                        <asp:Image ID="imgWait" ImageUrl="~/Content/images/wait.png" ToolTip="Aguardando avaliação Gestor" Visible="false" runat="server" />
                        <asp:Image ID="imgVisto" ImageUrl="~/Content/images/visto.png" Visible="false" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Ponderação" />
                <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" CssClass="btn btn-outline-primary" OnCommand="EditarAtividade" Text="Detalhe" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Font-Bold="true" />
        </asp:GridView>
    </div>

    <asp:PlaceHolder ID="phAndamento" runat="server">
        <h3 class="pt-4">Atividade em Andamento</h3>
        <div class="table-responsive">
            <asp:GridView ID="gdvAndamento" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnPreRender="GdvAndamento_PreRender" OnRowDataBound="GdvAndamento_RowDataBound" EnableViewState="True">
                <EmptyDataTemplate>
                    <div class="box-alerta-emptyData">
                        <div class="text-left">
                            <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                        </div>
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="NroChamado" ItemStyle-Wrap="false" />
                    <asp:BoundField HeaderText="NroAtividade" ItemStyle-Wrap="false" />
                    <asp:BoundField HeaderText="DscChamado" />
                    <asp:BoundField HeaderText="DscAtividade" />
                    <asp:BoundField HeaderText="Data Planejada" />
                    <asp:BoundField HeaderText="Qtd Confirmação" />
                </Columns>
            </asp:GridView>
        </div>
    </asp:PlaceHolder>    

</asp:Content>
