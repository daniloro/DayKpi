<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Organograma.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.Organograma" %>
<%@ Register Src="~/UserControl/FiltroMesGestor.ascx" TagPrefix="uc1" TagName="FiltroMesGestor" %>

<asp:Content ID="ContentHeader" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>

    <script type="text/javascript">
        google.charts.load('current', { packages: ['orgchart'] });

        var dadosChart1;

        function drawChart() {            
            var data = new google.visualization.DataTable();
            data.addColumn('string', 'Name');
            data.addColumn('string', 'Manager');
            data.addColumn('string', 'ToolTip');

            data.addRows(dadosChart1);

            var options = {
                allowHtml: true                
            };
                        
            var chart = new google.visualization.OrgChart(document.getElementById('container'));
            google.visualization.events.addListener(chart, 'select', toggleDisplay);
            chart.draw(data, options);

            function toggleDisplay() {
                var selection = chart.getSelection();

                if (selection.length > 0) {
                    $("[id$=hdLogin]").val(data.getValue(selection[0].row, 0));

                    var nome = data.getFormattedValue(selection[0].row, 0);

                    if (~nome.indexOf("<")) {
                        nome = nome.substring(0, nome.indexOf("<"));
                    }                    
                    $("[id$=txtNome]").val(nome);
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Organograma</h2>
        
    <uc1:FiltroMesGestor ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />
    
    <div id="container"></div>
    
    <asp:PlaceHolder ID="phEditarGestor" runat="server">
        <div class="row">
            <div class="col-sm-12 col-md-6 col-lg-2">
                <div class="form-group">
                    <label>Selecione um colaborador</label>
                    <asp:TextBox runat="server" ID="txtNome" Enabled="false" CssClass="form-control" />
                    <asp:HiddenField ID="hdLogin" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-2">
                <div class="mt-4 pt-2">
                    <asp:LinkButton runat="server" ID="lbAlterarGestor" CssClass="btn btn-primary" OnClick="LbAlterarGestor_Click" Text="Alterar Gestor &raquo;" />
                </div>
            </div>            
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phSalvarGestor" Visible="false" runat="server">
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Nome
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlNome" />
            </div>                
        </div>
        <div class="form-group row mb-4">
            <div class="col-md-2 font-weight-bold">
                Nome Gestor
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlNomeGestor" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-6 col-lg-2">
                <div class="form-group">
                    <label>Login Gestor</label>
                    <asp:TextBox runat="server" ID="txtGestor" MaxLength="8" CssClass="form-control" />                    
                </div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-2">
                <div class="mt-4 pt-2">
                    <asp:LinkButton ID="lbVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="LbVoltarGestor_Click" Text="Voltar" runat="server" />
                    <asp:LinkButton runat="server" ID="lbSalvarGestor" CssClass="btn btn-primary" OnClick="LbSalvarGestor_Click" Text="Salvar &raquo;" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phAnaliseKPI" runat="server">
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label><asp:Literal ID="ltlPergunta" runat="server" /></label>
                    <asp:TextBox ID="txtAnalise" TextMode="MultiLine" Height="250px" CssClass="form-control" runat="server" />
                </div>
            </div>
        </div>
        <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary mr-2" OnClick="BtnSalvar_Click" Text="Salvar" runat="server" />
        <asp:LinkButton ID="btnSalvarEnviar" CssClass="btn btn-primary" OnClick="BtnSalvarEnviar_Click" Text="Salvar e Enviar" runat="server" />        
    </asp:PlaceHolder>

    <script>
        $("[id$=txtGestor]").alphanumeric();

        $("[id$=lbAlterarGestor]").click(function () {
            if ($("[id$=txtNome]").val() === "") {
                $("[id$=txtNome]").addClass("input-validation-error");
                DayMensagens.adicionarMensagemAviso("Selecione um operador!");
            }
            return DayMensagens.verificarMensagemAviso();
        });

        $("[id$=btnSalvarEnviar]").click(function () {
            DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente enviar? Não poderá realizar alterações futuras.', $(this));
            return false;
        });

        $("[id$=txtAnalise]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });

        $("[id$=btnSalvarEnviar]").click(function () {
            DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente enviar? Não poderá realizar alterações futuras.', $(this));
            return false;
        });
    </script>
</asp:Content>