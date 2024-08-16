<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Planejamento.aspx.cs" Inherits="PortalAtividade.Pages.Atividade.Planejamento" %>
<%@ Register Src="~/UserControl/FiltroMesGestorOperador.ascx" TagPrefix="uc1" TagName="FiltroMesGestorOperador" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Planejamento Semanal</h2>
    
    <uc1:FiltroMesGestorOperador ID="ucFiltro" EsconderMes="true" OnConsultarFiltro="CarregarFiltro" runat="server" />
    <br />
    <h5>
        <asp:Literal runat="server" ID="ltlSemana" /></h5>
    <p>Informe o seu planejamento para esta semana.</p>
    
    <asp:ListView ID="lvwPlanejamento" OnItemDataBound="LvwPlanejamento_ItemDataBound" runat="server">
        <LayoutTemplate>
            <thead>                
            </thead>
            <tbody>
                <ul>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </ul>
            </tbody>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="pb-2">
                <asp:Literal runat="server" ID="ltlDescricao" />&nbsp;
                <asp:LinkButton ID="btnExcluir" OnCommand="ExcluirPlanejamento" ToolTip="Excluir este Planejamento" runat="server" >
                    Excluir
                </asp:LinkButton>
                <asp:Image ID="imgOk" ImageUrl="~/Content/images/visto.png" Visible="false" ToolTip="OK" runat="server" />
            </li>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="row pb-2">
                <div class="col-md-12">
                    <h6 style="color: red">Nenhum planejamento realizado por enquanto</h6>
                </div>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>
    <asp:LinkButton ID="btnNovo" CssClass="btn btn-primary mr-2" OnClick="BtnNovo_Click" Text="(+) Planejamento" runat="server" />
    <br />

    <asp:PlaceHolder ID="phNovo" Visible="false" runat="server">
        <div class="row">
            <div class="col-sm-12 col-md-6 col-lg-3">
                <div class="form-group">                    
                    <label>Tipo Planejamento</label>
                    <asp:DropDownList runat="server" ID="ddlTipoPlanejamento" OnSelectedIndexChanged="DdlTipoPlanejamento_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control">
                        <asp:ListItem Text="Conclusão da Atividade" Value="1" Selected="True" />
                        <asp:ListItem Text="Andamento na Atividade" Value="2" />
                        <asp:ListItem Text="Outros - Campo Aberto" Value="3" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-12 col-md-6 col-lg-2">
                <div class="form-group">
                    <label>NroAtividade</label>
                    <asp:TextBox runat="server" ID="txtNroAtividade" MaxLength="14" CssClass="form-control" />
                </div>
            </div>            
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="form-group">
                    <label>Descrição</label>
                    <asp:TextBox runat="server" ID="txtDscPlanejamento" TextMode="MultiLine" Height="120px" CssClass="form-control" />
                </div>
            </div>
        </div>
        <asp:LinkButton ID="btnCancelar" CssClass="btn btn-outline-primary mr-2" OnClick="BtnCancelar_Click" Text="Cancelar" runat="server" />
        <asp:LinkButton ID="lbIncluirPlanejamento" CssClass="btn btn-primary" OnClick="BtnIncluirPlanejamento_Click" Text="Incluir &raquo;" runat="server" />
    </asp:PlaceHolder>

    <br /><br /><hr />
    <h5>
        <asp:Literal runat="server" ID="ltlSemanaAnterior" /></h5>
    <p>Verifique se o que foi planejado foi seguido.</p>
    
    <asp:ListView ID="lvwPlanejamentoAnterior" OnItemDataBound="LvwPlanejamentoAnterior_ItemDataBound" runat="server">
        <LayoutTemplate>
            <thead>                
            </thead>
            <tbody>
                <ul>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </ul>
            </tbody>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="pb-2">
                <asp:Literal runat="server" ID="ltlDescricao" />&nbsp;
                <asp:Image ID="imgOk" ImageUrl="~/Content/images/visto.png" Visible="false" ToolTip="OK" runat="server" />
                <asp:Image ID="imgNok" ImageUrl="~/Content/images/delete.gif" Visible="false" ToolTip="NOK" runat="server" />
            </li>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="row pb-2">
                <div class="col-md-12">
                    <h6 style="color: red">Nenhum planejamento realizado</h6>
                </div>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>

    <br />
    <h6>
        Atuações sem Planejamento</h6>
    
    <asp:ListView ID="lvwAtuacao" OnItemDataBound="LvwAtuacao_ItemDataBound" runat="server">
        <LayoutTemplate>
            <thead>                
            </thead>
            <tbody>
                <ul>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </ul>
            </tbody>
        </LayoutTemplate>
        <ItemTemplate>
            <li class="pb-2">
                <asp:Literal runat="server" ID="ltlDescricao" />                
            </li>
        </ItemTemplate>
        <EmptyDataTemplate>
            <div class="row pb-2">
                <div class="col-md-12">
                    <h6 style="color: green">Nenhuma atuação sem planejamento na semana.</h6>
                </div>
            </div>
        </EmptyDataTemplate>
    </asp:ListView>

    <script>
        $("[id$=txtNroAtividade]").numeric({ allow: "RA\\ " });

        //$("[id$=ddlTipoPlanejamento]").change(function () {
        //    var tipoPlanejamento = this.value;

        //    if (tipoPlanejamento == 3) {
        //        $("[id$=txtNroAtividade]").attr("disabled", "disabled"); 
        //    }
        //    else {
        //        $("[id$=txtNroAtividade]").removeAttr("disabled"); 
        //    }    
        //});
    </script>
</asp:Content>
