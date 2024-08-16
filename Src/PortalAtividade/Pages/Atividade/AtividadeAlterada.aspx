<%@ Page Title="Atividade" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AtividadeAlterada.aspx.cs" Inherits="PortalAtividade.Atividade.AtividadeAlterada" %>
<%@ Register Src="~/UserControl/FiltroGrupo.ascx" TagPrefix="uc1" TagName="FiltroGrupo" %>
<%@ Register Src="~/UserControl/AtividadeHistorico.ascx" TagPrefix="uc1" TagName="AtividadeHistorico" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4"><%: Title %></h2>
    <h3>Informe o motivo da alteração da data de conclusão</h3>    

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
                    <asp:BoundField HeaderText="Qtd Confirmação" />
                    <asp:BoundField HeaderText="Data Anterior" />
                    <asp:BoundField HeaderText="Data Nova" />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" CssClass="btn btn-outline-primary" OnCommand="EditarAtividade" Text="Justificar" runat="server" />
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
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Data Anterior
            </div>
            <div class="col-md-10">
                <asp:Label runat="server" ID="lblDataAnterior" />
            </div>
        </div>
        <asp:PlaceHolder ID="phDataFinalLabel" runat="server">
            <div class="form-group row mb-2">
                <div class="col-md-2 font-weight-bold">
                    Data Nova
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" ID="lblDataFinal" />
                </div>
            </div>
        </asp:PlaceHolder>

        <div class="table-responsive pt-4">
            <uc1:AtividadeHistorico runat="server" id="ucHistorico" />
        </div>

        <div class="container pt-4 pb-2">
            <asp:PlaceHolder ID="phDataFinalTextBox" runat="server">
                <div class="row">
                    <div class="col-sm-12 col-md-2 col-lg-2">
                        <div class="form-group">
                            <label>Data Nova</label>
                            <asp:TextBox ID="txtDataFinal" CssClass="form-control" MaxLength="10" runat="server" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <asp:UpdatePanel ID="uppMotivo" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-12 col-md-12 col-lg-6">
                            <div class="form-group">
                                <label>Motivo</label>
                                <asp:DropDownList runat="server" ID="ddlTipoLancamento" CssClass="form-control"
                                    OnSelectedIndexChanged="DdlTipoLancamento_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Text="Pendente" Value="0" Selected="True" />
                                    <asp:ListItem Text="Aumento de Escopo" Value="7" />
                                    <asp:ListItem Text="Repactuação da Atividade" Value="8" />
                                    <asp:ListItem Text="Aguardando outra Equipe" Value="9" />
                                    <asp:ListItem Text="Repactuação da Atividade Anterior" Value="10" />
                                    <asp:ListItem Text="Repriorizado" Value="11" />
                                    <asp:ListItem Text="Aguardando Data da GMUD" Value="12" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-12 col-lg-4">
                            <div class="form-group">
                                <asp:PlaceHolder ID="phAtividadeAnterior" runat="server">
                                    <label>
                                        <asp:Literal runat="server" ID="ltlAtividadeAnterior" Text="Atividade" /></label>
                                    <asp:TextBox ID="txtNroAtividadeAnterior" CssClass="form-control" MaxLength="14" runat="server" />
                                </asp:PlaceHolder>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="row">
                <div class="col-sm-12 col-md-12 col-lg-12">
                    <div class="form-group">
                        <label>Observação</label>
                        <asp:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Height="120px" CssClass="form-control" />
                    </div>
                </div>
            </div>
            <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
            <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Salvar" runat="server" />
        </div>

    </asp:PlaceHolder>

    <script>        
        $("[id$=txtDataAgendamento]").mask("00/00/0000").numeric();        

        $("[id$=txtObservacao]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });

        //Evento disparado após UpdatePanel
        function pageLoad() {
            $("[id$=txtNroAtividadeAnterior]").numeric({ allow: "RA\\ " });
        }
    </script>

</asp:Content>
