<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EventoProblema.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.EventoProblema" %>
<%@ Register Src="~/UserControl/FiltroMesGestor.ascx" TagPrefix="uc1" TagName="FiltroMesGestor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Eventos</h2>
    <h3 class="pb-2">Problemas em Produção</h3>

    <asp:PlaceHolder ID="phConsulta" runat="server">
        <uc1:FiltroMesGestor ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

        <div class="table-responsive">
            <asp:GridView ID="gdvEvento" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnPreRender="GdvEvento_PreRender" OnRowDataBound="GdvEvento_RowDataBound" EnableViewState="True">
                <EmptyDataTemplate>
                    <div class="box-alerta-emptyData">
                        <div class="text-center">
                            <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                        </div>
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Data Evento" />
                    <asp:BoundField HeaderText="Nome Sistema" />
                    <asp:BoundField HeaderText="Tipo Evento" />                                        
                    <asp:BoundField HeaderText="Evento" />
                    <asp:BoundField HeaderText="NroChamado" />                    
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" CssClass="btn btn-outline-primary" Text="Editar" OnCommand="EditarEvento" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <asp:LinkButton runat="server" ID="btnIncluir" CssClass="btn btn-primary" OnClick="BtnIncluir_Click" Text="Incluir &raquo;" />
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phEdicao" runat="server">
        <div class="row">
            <div class="col-sm-12 col-md-4 col-lg-4">
                <div class="form-group">
                    <label>Sistema</label>
                    <asp:DropDownList ID="ddlSistema" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-4 col-lg-4">
                <div class="form-group">
                    <label>Tipo Evento</label>
                    <asp:DropDownList ID="ddlTipoEvento" CssClass="form-control" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-4 col-lg-4">
                <div class="form-group">
                    <label>Data Evento</label>
                    <asp:TextBox runat="server" ID="txtDataEvento" CssClass="form-control" MaxLength="10" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="form-group">
                    <label>Evento</label>
                    <asp:TextBox runat="server" ID="txtEvento" CssClass="form-control" MaxLength="100" />
                </div>
            </div>            
        </div>        
        <div class="row">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="form-group">
                    <label>Descrição</label>
                    <asp:TextBox runat="server" ID="txtDescricao" CssClass="form-control" TextMode="MultiLine" Height="200px" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="form-group">
                    <label>Correção</label>
                    <asp:TextBox runat="server" ID="txtCorrecao" CssClass="form-control" TextMode="MultiLine" Height="200px" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-2 col-lg-2">
                <div class="form-group">
                    <label>NroChamado</label>
                    <asp:TextBox runat="server" ID="txtNroChamado" CssClass="form-control" MaxLength="14" />
                </div>
            </div>
            <div class="col-sm-12 col-md-10 col-lg-10">
                <div class="form-group">
                    <label>DscChamado</label>
                    <asp:Label runat="server" ID="lblDscChamado" CssClass="form-control" BorderColor="#F5F5F5" Font-Bold="true" />
                </div>
            </div>            
        </div>        
        <div class="row">
            <div class="col-sm-12 col-md-2 col-lg-2">
                <div class="form-group">
                    <label>NroChamado Correção</label>
                    <asp:TextBox runat="server" ID="txtNroChamadoCorrecao" CssClass="form-control" MaxLength="14" />
                </div>
            </div>
            <div class="col-sm-12 col-md-10 col-lg-10">
                <div class="form-group">
                    <label>DscChamado Correção</label>
                    <asp:Label runat="server" ID="lblDscChamadoCorrecao" CssClass="form-control" BorderColor="#F5F5F5" Font-Bold="true" />
                </div>
            </div>            
        </div>        
        <div class="row pt-4">
            <div class="col-md-2">
                <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
                <asp:LinkButton runat="server" ID="btnAlterar" CssClass="btn btn-primary" OnClick="BtnAlterar_Click" Text="Alterar &raquo;" />
            </div>
        </div>

    </asp:PlaceHolder>

    <script>
        $("[id$=txtDataEvento]").mask("00/00/0000").numeric();
        $("[id$=txtNroChamado]").numeric({ allow: "RA\\ " });
        $("[id$=txtNroChamadoCorrecao]").numeric({ allow: "RA\\ " });

        $("[id$=txtEvento],[id$=txtDescricao],[id$=txtCorrecao]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });

        $("[id$=btnAlterar]").click(function () {
            DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente alterar?', $(this));
            return false;
        });
    </script>
</asp:Content>
