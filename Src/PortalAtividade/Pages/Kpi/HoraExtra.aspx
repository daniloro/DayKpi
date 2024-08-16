<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HoraExtra.aspx.cs" Inherits="PortalAtividade.Pages.Kpi.HoraExtra" %>
<%@ Register Src="~/UserControl/FiltroMesGestor.ascx" TagPrefix="uc1" TagName="FiltroMesGestor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Hora Extra</h2>
    <h3 class="pb-2">Controle de Horas-Extras</h3>

    <asp:PlaceHolder ID="phConsulta" runat="server">
        <uc1:FiltroMesGestor ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

        <div class="form-group row mb-4">
            <div class="col-md-2 font-weight-bold">
                Hora Extra Total
            </div>
            <div class="col-md-4">
                <asp:Literal runat="server" ID="ltlTotal" />
            </div>
            <div class="col-md-2 font-weight-bold">
                50 %
            </div>
            <div class="col-md-4">
                <asp:Literal runat="server" ID="ltlTotal50" />
            </div>
        </div>
        <div class="form-group row mb-4">
            <div class="col-md-2 font-weight-bold">
                100 %
            </div>
            <div class="col-md-4">
                <asp:Literal runat="server" ID="ltlTotal100" />
            </div>
            <div class="col-md-2 font-weight-bold">
                Noturno
            </div>
            <div class="col-md-4">
                <asp:Literal runat="server" ID="ltlTotalNoturno" />
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gdvPonto" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnPreRender="GdvPonto_PreRender" OnRowDataBound="GdvPonto_RowDataBound" EnableViewState="True">
                <EmptyDataTemplate>
                    <div class="box-alerta-emptyData">
                        <div class="text-center">
                            <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                        </div>
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Nome" />
                    <asp:BoundField HeaderText="Atraso" />
                    <asp:BoundField HeaderText="50%" />
                    <asp:BoundField HeaderText="100%" />
                    <asp:BoundField HeaderText="Noturno" />                    
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" CssClass="btn btn-outline-primary" Text="Editar" OnCommand="EditarPonto" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phEdicao" runat="server">
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Mês
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlMes" />
            </div>            
        </div>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Nome
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlNome" />
            </div>
        </div>

        <div class="row">
            <div class="col-sm-12 col-md-6 col-lg-3">
                <div class="form-group">
                    <label>Atraso</label>
                    <asp:TextBox runat="server" ID="txtAtraso" MaxLength="5" CssClass="form-control" />
                </div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-3">
                <div class="form-group">
                    <label>50%</label>
                    <asp:TextBox runat="server" ID="txtCinquentaPorcento" MaxLength="5" CssClass="form-control" />
                </div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-3">
                <div class="form-group">
                    <label>100%</label>
                    <asp:TextBox runat="server" ID="txtCemPorcento" MaxLength="5" CssClass="form-control" />
                </div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-3">
                <div class="form-group">
                    <label>Noturno</label>
                    <asp:TextBox runat="server" ID="txtNoturno" MaxLength="5" CssClass="form-control" />
                </div>
            </div>
        </div>
        
        <div class="row">
            <div class="col-sm-12 col-md-8 col-lg-12">
                <div class="form-group">
                    <label>Observação</label>
                    <asp:TextBox runat="server" ID="txtObservacao" CssClass="form-control" TextMode="MultiLine" Height="300px" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-2">
                <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
                <asp:LinkButton runat="server" ID="btnIncluir" CssClass="btn btn-primary" OnClick="BtnIncluirPonto_Click" Text="Alterar &raquo;" />
            </div>
        </div>

    </asp:PlaceHolder>

    <script>
        $("[id$=txtAtraso]").numeric({ allow: ":" });
        $("[id$=txtCinquentaPorcento]").numeric({ allow: ":" });
        $("[id$=txtCemPorcento]").numeric({ allow: ":" });
        $("[id$=txtNoturno]").numeric({ allow: ":" });

        $("[id$=txtObservacao]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });
    </script>
</asp:Content>