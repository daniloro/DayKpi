<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RelatorioKpi.aspx.cs" Inherits="PortalAtividade.Pages.Adm.RelatorioKpi" %>
<%@ Register Src="~/UserControl/Chart/OrgChart.ascx" TagPrefix="uc1" TagName="OrgChart" %>
<%@ Register Src="~/UserControl/Chart/Tabela.ascx" TagPrefix="uc1" TagName="Tabela" %>
<%@ Register Src="~/UserControl/Kpi/Backlog.ascx" TagPrefix="uc1" TagName="Backlog" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.charts.load('current', { packages: ['orgchart', 'corechart'] });
        google.load('visualization', '1.1', { packages: ['table'] });
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Relatório KPI</h2>

    <div class="row">
        <div class="col-md-6">
            <p style="color: #182daa"><asp:Literal runat="server" ID="ltlMesAno" /> - <asp:Literal runat="server" ID="ltlGestor" /></p>
        </div>
        <div class="col-md-6 text-right">
            <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
        </div>
    </div>

    <h3 class="pt-4 pb-2">1. Objetivo</h3>
    <p style="color:darkblue"><asp:Literal ID="ltlObjetivoResp" runat="server" /></p>

    <h3 class="pt-4 pb-2">2. Organograma da Equipe</h3>    
    <uc1:OrgChart runat="server" ID="ucOrganograma" />
    <h6 class="pt-4"><asp:Literal ID="ltlOrganograma" runat="server" /></h6>
    <p style="color:darkblue"><asp:Literal ID="ltlOrganogramaResp" runat="server" /></p>

    <h3 class="pt-4 pb-2">3. Missão da Equipe</h3>
    <p style="color:darkblue"><asp:Literal ID="ltlMissaoResp" runat="server" /></p>

    <h3 class="pt-4 pb-2">4. Demandas atendidas em <asp:Literal runat="server" ID="ltlMesAno3" /> (Principais ou Super Relevantes)</h3>
    <uc1:Tabela runat="server" ID="ucPrincipaisDemandas" />

    <h3 class="pt-4 pb-2">5. Entregas para <asp:Literal runat="server" ID="ltlMesAno4" /> (Principais ou Super Relevantes)</h3>
    <uc1:Tabela runat="server" ID="ucPrincipaisDemandasProximoMes" />

    <h3 class="pt-4 pb-2">6. Meta</h3>    
    <h6 class="pt-4"><asp:Literal ID="ltlMetaAnterior" Text="Meta estabelecida para " runat="server" /></h6>
    <p style="color:darkblue"><asp:Literal ID="ltlMetaAnteriorResp" runat="server" /></p>
    <h6 class="pt-4"><asp:Literal ID="ltlMeta1" runat="server" /></h6>
    <p style="color:darkblue"><asp:Literal ID="ltlMetaResp1" runat="server" /></p>
    <h6 class="pt-4"><asp:Literal ID="ltlMeta2" runat="server" /></h6>
    <p style="color:darkblue"><asp:Literal ID="ltlMetaResp2" runat="server" /></p>

    <h3 class="pt-4 pb-2">7. Plano (Meta) e Prazo para redução do Backlog</h3>    
    <uc1:Backlog runat="server" ID="ucBacklog" />

    <h3 class="pt-4 pb-2">8. Plano (Meta) e Prazo para redução dos Chamados Em Fila</h3>
    <asp:PlaceHolder ID="phEmFila" runat="server" />

    <h3 class="pt-4 pb-2">9. Aberto x Concluído</h3>
    <asp:PlaceHolder ID="phAbertoConcluido" runat="server" />

    <h3 class="pt-4 pb-2">10. Planejamento das atividades dos analistas</h3>
    <asp:PlaceHolder ID="phAtendimento" runat="server" />

    <h3 class="pt-4 pb-2">11. GMUD</h3>
    <uc1:Tabela runat="server" ID="ucGmud" />
    <h6 class="pt-4"><asp:Literal ID="ltlGmud" runat="server" /></h6>
    <p style="color:darkblue"><asp:Literal ID="ltlGmudResp" runat="server" /></p>

    <h3 class="pt-4 pb-2">12. Performance da Equipe</h3>
    <asp:PlaceHolder ID="phPerformance" runat="server" />

    <h3 class="pt-4 pb-2">13. Horas Extras</h3>
    <uc1:Tabela runat="server" ID="ucHoraExtra" />

    <div style="height:200px"></div>
</asp:Content>