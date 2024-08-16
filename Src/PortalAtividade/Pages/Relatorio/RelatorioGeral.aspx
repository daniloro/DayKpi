<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioGeral.aspx.cs" MaintainScrollPositionOnPostback="true" Inherits="PortalAtividade.Pages.Relatorio.RelatorioGeral" %>
<%@ Register Src="~/UserControl/FiltroMesGrupo.ascx" TagPrefix="uc1" TagName="FiltroMesGrupo" %>
<%@ Register Src="~/UserControl/Chart/Bar.ascx" TagPrefix="uc1" TagName="Bar" %>
<%@ Register Src="~/UserControl/Chart/BarChart.ascx" TagPrefix="uc1" TagName="BarChart" %>
<%@ Register Src="~/UserControl/Chart/Tabela.ascx" TagPrefix="uc1" TagName="Tabela" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.load('visualization', '1.1', { packages: ['corechart', 'table'] });
        google.charts.load('current', { 'packages': ['bar'] });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Relatório</h2>
    <h3>Chamados na Fila</h3>

    <uc1:FiltroMesGrupo ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

    <asp:Button ID="btnExcel" CssClass="btn btn-default xls" Text="Excel" OnClick="BtnExcel_EmFila_Click" runat="server" />

    <uc1:BarChart runat="server" ID="ucBarChart" />    

    <div class="table-responsive pt-4">
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
                <asp:BoundField HeaderText="Grupo" FooterText="Total" />
                <asp:BoundField HeaderText="Desenv" />
                <asp:BoundField HeaderText="Definição" />                
                <asp:BoundField HeaderText="Code Review" />
                <asp:BoundField HeaderText="Merge" />                
                <asp:BoundField HeaderText="Total Na Fila" />
                <asp:BoundField HeaderText="Vencido" />
            </Columns>
            <FooterStyle Font-Bold="true" />
        </asp:GridView>
    </div>
    
    <uc1:Bar runat="server" ID="ucBar" />

    <h3 class="pt-4">Abertos X Concluídos</h3>

    <div class="table-responsive">
        <asp:GridView ID="gdvAbertoConcluido" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
            GridLines="None" OnPreRender="GdvAbertoConcluido_PreRender" OnRowDataBound="GdvAbertoConcluido_RowDataBound" EnableViewState="True">
            <EmptyDataTemplate>
                <div class="box-alerta-emptyData">
                    <div class="text-center">
                        <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                    </div>
                </div>
            </EmptyDataTemplate>
            <Columns>
                <asp:BoundField HeaderText="Grupo" FooterText="Total" />
                <asp:TemplateField HeaderText="Mes1">
                    <ItemTemplate>
                        <div>
                            <div style="display: inline-block; width: 100px">(+) Aberto</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlAberto1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Concluído</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlConcluido1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Cancelado</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlCancelado1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(=) Backlog</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Label runat="server" ID="lblBacklog1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; font-weight: bold; width: 100px">Total</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Literal runat="server" ID="ltlEmFila1" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div>
                            <div style="display: inline-block; width: 100px">(+) Aberto</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlAberto1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Concluído</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlConcluido1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Cancelado</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlCancelado1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(=) Backlog</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Label runat="server" ID="lblBacklog1" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; font-weight: bold; width: 100px">Total</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Literal runat="server" ID="ltlEmFila1" />
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mes2">
                    <ItemTemplate>
                        <div>
                            <div style="display: inline-block; width: 100px">(+) Aberto</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlAberto2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Concluído</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlConcluido2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Cancelado</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlCancelado2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(=) Backlog</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Label runat="server" ID="lblBacklog2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; font-weight: bold; width: 100px">Total</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Literal runat="server" ID="ltlEmFila2" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div>
                            <div style="display: inline-block; width: 100px">(+) Aberto</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlAberto2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Concluído</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlConcluido2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Cancelado</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlCancelado2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(=) Backlog</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Label runat="server" ID="lblBacklog2" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; font-weight: bold; width: 100px">Total</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Literal runat="server" ID="ltlEmFila2" />
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mes3">
                    <ItemTemplate>
                        <div>
                            <div style="display: inline-block; width: 100px">(+) Aberto</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlAberto3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Concluído</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlConcluido3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Cancelado</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlCancelado3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(=) Backlog</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Label runat="server" ID="lblBacklog3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; font-weight: bold; width: 100px">Total</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Literal runat="server" ID="ltlEmFila3" />
                            </div>
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                        <div>
                            <div style="display: inline-block; width: 100px">(+) Aberto</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlAberto3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Concluído</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlConcluido3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(-) Cancelado</div>
                            <div style="display: inline-block">
                                <asp:Literal runat="server" ID="ltlCancelado3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; width: 100px">(=) Backlog</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Label runat="server" ID="lblBacklog3" />
                            </div>
                        </div>
                        <div>
                            <div style="display: inline-block; font-weight: bold; width: 100px">Total</div>
                            <div style="display: inline-block; font-weight: bold">
                                <asp:Literal runat="server" ID="ltlEmFila3" />
                            </div>
                        </div>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

    <h3 class="pt-4">
        <asp:Literal runat="server" ID="ltlConcluidoMes1" /></h3>

    <div style="text-align:left" class="pb-2">
    <asp:Button ID="btnExcelConcluido" CssClass="btn btn-default xls" Text="Excel" OnClick="BtnExcel_Concluido_Click" runat="server" /></div>
    
    <uc1:Tabela runat="server" ID="ucConcluido" />

    <script>        

        $(document).ready(function () {

        });

    </script>

</asp:Content>
