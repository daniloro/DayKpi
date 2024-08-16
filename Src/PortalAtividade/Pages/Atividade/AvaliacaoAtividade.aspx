<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvaliacaoAtividade.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.AvaliacaoAtividade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Detalhe Atividade</h2>    
    <br />

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
        <div class="col-md-10">
            <asp:Literal runat="server" ID="ltlDscAtividade" />
        </div>
    </div>
    <div class="form-group row mb-2">
        <div class="col-md-2 font-weight-bold">
            Operador
        </div>
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblOperador" />
        </div>
    </div>
    <div class="form-group row mb-2">
        <div class="col-md-2 font-weight-bold">
            Data Conclusão
        </div>
        <div class="col-md-10">
            <asp:Label runat="server" ID="lblDataConclusao" />
        </div>
    </div>

    <br />

    <div class="form-group row mb-2">
        <div class="col-md-2 font-weight-bold">
            Tipo Atividade
        </div>
        <div class="col-md-4">
            <asp:Literal runat="server" ID="ltlTipoAtividade" />
        </div>
        <div class="col-md-4 font-weight-bold">
            Peso
        </div>
        <div class="col-md-2">
            <asp:Literal runat="server" ID="ltlPonderacao" />
        </div>
    </div>
    <div class="form-group row mb-2">
        <div class="col-md-6 font-weight-bold">            
        </div>
        <div class="col-md-4 font-weight-bold">
            Confirmação de Data Planejada
        </div>
        <div class="col-md-2">
            <asp:Literal runat="server" ID="ltlConfirmacao" />
        </div>        
    </div>
    <div class="form-group row mb-2">
        <div class="col-md-2 font-weight-bold">
            Repactuação
        </div>
        <div class="col-md-4">
            <asp:Literal runat="server" ID="ltlRepactuacao" />
        </div>
        <div class="col-md-4 font-weight-bold">
            Repactuação sem Justificativa
        </div>
        <div class="col-md-2">
            <asp:Literal runat="server" ID="ltlRepactuacaoSemJustificativa" />
        </div>        
    </div>
    <div class="form-group row mb-2">
        <div class="col-md-6 font-weight-bold">            
        </div>
        <div class="col-md-4 font-weight-bold">
            Avaliação Gestor
        </div>
        <div class="col-md-2">
            <asp:Literal runat="server" ID="ltlAvaliacaoGestor" />
        </div>        
    </div>
    <div class="form-group row mb-2">
        <div class="col-md-6 font-weight-bold">            
        </div>
        <div class="col-md-4 font-weight-bold" style="background-color: #F5F5F5">
            Ponderação Total
        </div>
        <div class="col-md-2 font-weight-bold" style="background-color: #F5F5F5">
            <asp:Literal runat="server" ID="ltlPonderacaoTotal" />
        </div>
    </div>
    <br />

    <asp:PlaceHolder ID="phAvaliacaoAuto" Visible="false" runat="server">
        <h4>Auto Avaliação</h4>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                <asp:Literal runat="server" Text="Destaque(s) em" ID="ltlDestaqueTituloAuto" />
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlDestaqueAuto" />
            </div>
        </div>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Observação
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlObservacaoAuto" />
            </div>
        </div>
        <br />
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phAvaliacaoGestor" Visible="false" runat="server">
        <h4>Avaliação Gestor</h4>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                <asp:Literal runat="server" Text="Destaque(s) em" ID="ltlDestaqueTituloGestor" />
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlDestaqueGestor" />
            </div>
        </div>
        <div class="form-group row mb-2">
            <div class="col-md-2 font-weight-bold">
                Observação
            </div>
            <div class="col-md-10">
                <asp:Literal runat="server" ID="ltlObservacaoGestor" />
            </div>
        </div>
        <br />
    </asp:PlaceHolder>

    <asp:PlaceHolder ID="phAvaliacao" Visible="false" runat="server">
        <h3>Avaliação</h3>
        <div class="row">
            <div class="col-md-6">
                <div class="form-group">
                    <label>Destaque(s) acima da expectativa:</label>
                    <asp:CheckBoxList runat="server" ID="cblDestaque" RepeatDirection="Vertical" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12 col-md-12 col-lg-12">
                <div class="form-group">
                    <label>Observação</label>
                    <asp:TextBox runat="server" ID="txtObservacao" TextMode="MultiLine" Height="120px" CssClass="form-control" />
                </div>
            </div>
        </div>
    </asp:PlaceHolder>

    <asp:LinkButton ID="btnVoltar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnVoltar_Click" Text="Voltar" runat="server" />
    <asp:LinkButton ID="btnIncluirAvaliacao" CssClass="btn btn-primary" OnClick="BtnIncluirAvaliacao_Click" Text="Incluir Avaliação" Visible="false" runat="server" />
    <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Salvar" Visible="false" runat="server" />

    <script>
        $("[id$=txtObservacao]").on('keypress', function (event) {
            ValidarCaractererEspecial(event);
        });
    </script>
    
</asp:Content>