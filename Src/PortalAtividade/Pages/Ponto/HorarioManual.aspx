<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HorarioManual.aspx.cs" Inherits="PortalAtividade.Pages.Ponto.HorarioManual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Horário</h2>
    <h3>Entrada Manual</h3>

    <hr />

    <div class="row pb-4">
        <div class="col-md-3">
            <div class="form-group">
                <label>Hora Início</label>
                <asp:TextBox runat="server" ID="txtHoraInicio" MaxLength="5" CssClass="form-control" />
                <asp:Label runat="server" ID="lblHoraInicio" CssClass="form-control" />
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Hora Almoço</label>
                <asp:TextBox runat="server" ID="txtHoraAlmoco" MaxLength="5" CssClass="form-control" />
                <asp:Label runat="server" ID="lblHoraAlmoco" CssClass="form-control" />
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Hora Retorno</label>
                <asp:TextBox runat="server" ID="txtHoraRetorno" MaxLength="5" CssClass="form-control" />
                <asp:Label runat="server" ID="lblHoraRetorno" CssClass="form-control" />
            </div>
        </div>
        <div class="col-md-3">
            <div class="form-group">
                <label>Hora Saída</label>
                <asp:TextBox runat="server" ID="txtHoraSaida" MaxLength="5" CssClass="form-control" />
                <asp:Label runat="server" ID="lblHoraSaida" CssClass="form-control" />
            </div>
        </div>
    </div>

    <asp:LinkButton ID="btnSalvar" CssClass="btn btn-primary" OnClick="BtnSalvar_Click" Text="Salvar" runat="server" />
    <asp:Label ID="lblMensagem" runat="server" CssClass="pl-4" ForeColor="Red" />

    <script>                
        $("[id$=txtHoraInicio]").mask("99:99").numeric();
        $("[id$=txtHoraAlmoco]").mask("99:99").numeric();
        $("[id$=txtHoraRetorno]").mask("99:99").numeric();
        $("[id$=txtHoraSaida]").mask("99:99").numeric();
    </script>
</asp:Content>
