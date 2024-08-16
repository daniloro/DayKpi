<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PortalAtividade._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron pb-2 pt-4">
        <h2>
            <asp:Label runat="server" ID="lblStatusAtividade" Text="Regular" /></h2>
        <p class="lead">
            <asp:Literal runat="server" ID="ltlMsgAtividade" Text="Última atualização" />
             - 24/08/2020 08:05h.
        </p>

        <asp:PlaceHolder runat="server" ID="phAtividadeAtual">
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
                    Data Estimada
                </div>
                <div class="col-md-10">
                    <asp:Label runat="server" ID="lblDataFinal" />
                </div>
            </div>
            <%--<div class="lista-dados">
                <ul>                    
                    <li>
                        <label>
                            <asp:Literal runat="server" ID="ltlNroAtividade" /></label>
                        <span>
                            <asp:Literal runat="server" ID="ltlDscAtividade" /></span>
                    </li>
                    <li>
                        <label>Data Estimada</label>
                        <span>
                            <asp:Label runat="server" ID="lblDataFinal" /></span>
                    </li>
                </ul>
            </div>--%>
            <%--<p class="pt-4"><asp:LinkButton runat="server" ID="lbConfirmarAtividade" CssClass="btn btn-primary btn-lg" Text="Confirmar &raquo;" /></p>--%>
        </asp:PlaceHolder>

        <div class="row pt-4 pb-2">
            <div class="col-md-3">
                <asp:LinkButton runat="server" ID="lbHoraInicio" CssClass="btn btn-secondary btn-lg w-100" OnClick="LbHoraInicio_Click" Text="Início &raquo;" />
            </div>
            <div class="col-md-3">
                <asp:LinkButton runat="server" ID="lbHoraAlmoco" CssClass="btn btn-secondary btn-lg w-100" OnClick="LbHoraAlmoco_Click" Text="Almoço &raquo;" />
            </div>
            <div class="col-md-3">
                <asp:LinkButton runat="server" ID="lbHoraRetorno" CssClass="btn btn-secondary btn-lg w-100" OnClick="LbHoraRetorno_Click" Text="Retorno &raquo;" />
            </div>
            <div class="col-md-3">
                <asp:LinkButton runat="server" ID="lbHoraSaida" CssClass="btn btn-secondary btn-lg w-100" OnClick="LbHoraSaida_Click" Text="Saída &raquo;" />
            </div>
        </div>
    </div>        

    <div class="row">
        <div class="col-md-4">
            <h2>Atividades</h2>
            <p>
                Atividades que tiveram as datas de conclusão prorrogadas. Justifique o motivo.<br />
                Você possui 5 atividades pendentes. 
            </p>
            <p>
                <asp:LinkButton runat="server" ID="lbAtividades" CssClass="btn btn-outline-primary" Text="Justificar &raquo;" PostBackUrl="~/Pages/Atividade.aspx" />               
            </p>
        </div>
        <div class="col-md-4">
            <h2>Avaliação</h2>
            <p>
                Efetuar avaliação semanal e avaliação das atividades concluídas.
                Consultar as avaliações.
            </p>
            <p>
                <asp:LinkButton runat="server" ID="lbAvaliacao" CssClass="btn btn-outline-primary" Text="Ver mais &raquo;" PostBackUrl="~/Pages/AvaliacaoAtividade.aspx" />                
            </p>
        </div>
        <div class="col-md-4">
            <h2>CheckList</h2>
            <p>
                Efetuar checklist diário.<br />
                Início da jornada - Horário Limite: 08:25h<br />
                Final da jornada - Horário Limite: 19:15h
            </p>
            <p>
                <asp:LinkButton runat="server" ID="lbCheckList" CssClass="btn btn-outline-primary" Text="Efetuar &raquo;" PostBackUrl="~/Pages/CheckListSistema.aspx" />                
            </p>
        </div>
    </div>

    <script>
        //$("[id$=lbHoraInicio]").prop('disabled', true);
        //$("[id$=lbHoraAlmoco]").prop('disabled', true);
        //DayMensagens.mostraMensagem("Aviso", "Horário de Início já foi cadastrado!");

        //$(document).ready(function () {
        //    if ($("[id$=lbHoraInicio]").text() !== "Início »") {
        //        $("[id$=lbHoraAlmoco]").prop('disabled', true);

        //        if($("[id$=lbHoraAlmoco]").hasClass("btn-secondary"))
        //            $("[id$=lbHoraAlmoco]").removeClass("btn-secondary").addClass("btn-outline-secondary");
        //    }
        //});
        
        //$("[id$=lbHoraInicio]").click(function () {
        //    debugger;
        //    if ($(this).text() === "Início »") {
        //        DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Entrada?', $(this));
        //        return false;                
        //    }
        //    else {                
        //        return false;
        //    }
        //});
        
    </script>

</asp:Content>
