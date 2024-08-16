<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorarioEquipe.aspx.cs" Inherits="PortalAtividade.Pages.Ponto.HorarioEquipe" %>
<%@ Register Src="~/UserControl/FiltroMesGrupo.ascx" TagPrefix="uc1" TagName="FiltroMesGrupo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4 pb-2">Ponto</h2>
    <h3>Hora Extra Mensal</h3>

    <asp:PlaceHolder runat="server" ID="phLista">
        <uc1:FiltroMesGrupo ID="ucFiltro" OnConsultarFiltro="CarregarFiltro" runat="server" />

        <div class="table-responsive">
            <asp:GridView ID="gdvPonto" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnPreRender="GdvPonto_PreRender" OnRowDataBound="GdvPonto_RowDataBound" OnRowCommand="GdvPonto_RowCommand" EnableViewState="True">
                <EmptyDataTemplate>
                    <div class="box-alerta-emptyData">
                        <div class="text-center">
                            <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                        </div>
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Nome" FooterText="Total" />
                    <asp:BoundField HeaderText="HE Total" />
                    <asp:BoundField HeaderText="HE Último dia" />
                    <asp:BoundField HeaderText="Lançamento Manual" />
                    <asp:BoundField HeaderText="Lançamento Pendente" />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnDetalhe" CssClass="btn btn-outline-primary" Text="Detalhe" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>            
            </asp:GridView>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phDetalhe" Visible="false">
        <hr />
        
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
                Período
            </div>
            <div class="col-md-6">
                <asp:Literal runat="server" ID="ltlPeriodo" />
            </div>
            <div class="col-md-4 text-right">
                <asp:LinkButton runat="server" ID="lbSalvar" CssClass="btn btn-primary btn-lg" OnClick="BtnSalvar_Click" Text="Salvar &raquo;" />
            </div>
        </div>

        <div class="table-responsive">
            <asp:GridView ID="gdvPontoDetalhe" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnPreRender="GdvPontoDetalhe_PreRender" OnRowDataBound="GdvPontoDetalhe_RowDataBound" EnableViewState="True">
                <EmptyDataTemplate>
                    <div class="box-alerta-emptyData">
                        <div class="text-center">
                            <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                        </div>
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Data" />
                    <asp:BoundField HeaderText="Semana" />
                    <asp:TemplateField HeaderText="Início">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInicio" MaxLength="5" CssClass="form-control" Width="70px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Almoço">
                        <ItemTemplate>
                            <asp:TextBox ID="txtAlmoco" MaxLength="5" CssClass="form-control" Width="70px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Retorno">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRetorno" MaxLength="5" CssClass="form-control" Width="70px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saída">
                        <ItemTemplate>
                            <asp:TextBox ID="txtSaida" MaxLength="5" CssClass="form-control" Width="70px" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>                                       
                </Columns>
            </asp:GridView>
        </div>

        <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />        
    </asp:PlaceHolder>

    <script>                
        $("[id*=txtInicio]").mask("99:99").numeric();
        $("[id*=txtAlmoco]").mask("99:99").numeric();
        $("[id*=txtRetorno]").mask("99:99").numeric();
        $("[id*=txtSaida]").mask("99:99").numeric();
    </script>

</asp:Content>


