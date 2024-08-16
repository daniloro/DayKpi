<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AtividadeManual.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.AtividadeManual" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Atividade Manual</h2>
    <h3>Informe a atividade e prazo estimado para conclusão</h3>

    <hr />

    <div class="form-group row mb-2">
        <div class="col-md-2 font-weight-bold">
            Operador
        </div>
        <div class="col-md-10">
            <asp:Literal runat="server" ID="ltlOperador" />
        </div>
    </div>

    <div class="container pt-4 pb-2">        
        <div class="row">
            <div class="col-sm-12 col-md-2 col-lg-2">
                <div class="form-group">
                    <label>NroAtividade</label>
                    <asp:TextBox ID="txtNroAtividade" CssClass="form-control" MaxLength="14" runat="server" />
                </div>
            </div>
            <div class="col-sm-12 col-md-2 col-lg-2">
                <p style="padding-top:15px"></p>                  
                    <asp:LinkButton ID="btnConsultar" CssClass="btn btn-primary" OnClick="BtnConsultar_Click" Text="Consultar" runat="server" />
            </div>
        </div>
        <p></p>
        
        <asp:PlaceHolder ID="phConsulta" runat="server" Visible="false">
            <hr />
            <asp:PlaceHolder ID="phDescricao" runat="server" Visible="false">
                <div class="form-group row mb-2">
                    <div class="col-md-2 font-weight-bold">
                        <asp:Literal runat="server" ID="ltlNroChamado" />
                    </div>
                    <div class="col-md-10">
                        <asp:Literal runat="server" ID="ltlDscChamado" />
                    </div>
                </div>
            </asp:PlaceHolder>

            <div class="form-group row mb-2">
                <div class="col-md-2 font-weight-bold">
                    <asp:Literal runat="server" ID="ltlNroAtividade" />
                </div>
                <div class="col-md-10">
                    <asp:Literal runat="server" ID="ltlDscAtividade" />
                </div>
            </div>

            <asp:PlaceHolder ID="phDescricaoManual" runat="server" Visible="false">
                <div class="row">
                    <div class="col-sm-12 col-md-12 col-lg-12">
                        <div class="form-group">
                            <label>Descrição</label>
                            <asp:TextBox runat="server" ID="txtDescricao" MaxLength="100" CssClass="form-control" />
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

            <div class="row">
                <div class="col-sm-12 col-md-2 col-lg-2">
                    <div class="form-group">
                        <label>Data Final</label>
                        <asp:TextBox ID="txtDataFinal" CssClass="form-control" MaxLength="10" runat="server" />
                    </div>
                </div>            
            </div>
        </asp:PlaceHolder>        

        <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
        <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Salvar" runat="server" Visible="false" />
    </div>

    <script>        
        $("[id$=txtDataFinal]").mask("00/00/0000").numeric();
        $("[id$=txtDscManual]").alphanumeric({ allow: ".,- '" });
        $("[id$=txtNroAtividade]").numeric({ allow: "RA\\ " });
    </script>
</asp:Content>