<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PortalAtividade._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron pb-2 pt-4">

        <asp:ListView ID="lvwAtividade" OnItemDataBound="LvwAtividade_ItemDataBound" OnItemCommand="LvwAtividade_ItemCommand" runat="server">
            <LayoutTemplate>
                <thead>
                    <div class="row pb-2">
                        <div class="col-md-6">
                            <h2>
                                <asp:Label runat="server" ID="lblStatusAtividade" Text="Confirmação Pendente" /></h2>
                        </div>
                        <div class="col-md-6 text-right">
                            <p class="lead">
                                Última atualização -
                                <asp:Literal runat="server" ID="ltlDataAtualizacao" />h.
                            </p>
                        </div>
                    </div>
                </thead>
                <tbody>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </tbody>                
            </LayoutTemplate>
            <ItemTemplate>
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
                        Data Planejada
                    </div>
                    <div class="col-md-10">
                        <asp:Label runat="server" ID="lblDataFinal" />
                    </div>
                </div>
                <asp:PlaceHolder runat="server" ID="phConfirmacaoAtividade" Visible="false">
                    <div class="row pt-2">
                        <asp:PlaceHolder runat="server" ID="phConfirmarAtividade">
                            <div class="col-md-2">
                                <asp:LinkButton runat="server" ID="lbConfirmarAtividade" CssClass="btn btn-primary btn-lg w-100" Text="Confirmar Data &raquo;" />
                            </div>
                        </asp:PlaceHolder>                        
                        <div class="col-md-2">
                            <asp:LinkButton runat="server" ID="lbAlterarData" CssClass="btn btn-secondary btn-lg w-100" Text="Alterar Data &raquo;" />
                        </div>                        
                        <div class="col-md-2">
                            <asp:LinkButton runat="server" ID="lbAlterarAtividade" CssClass="btn btn-warning btn-lg w-100" Text="Estou em outra Atividade &raquo;" />
                        </div>                        
                    </div>                    
                </asp:PlaceHolder>
                <p class="pb-2">                
            </ItemTemplate>
            <EmptyDataTemplate>
                <div class="row pb-2">
                    <div class="col-md-6">
                        <h2 style="color:red">
                            Nenhuma atividade em andamento</h2>
                    </div>
                    <div class="col-md-6 text-right">
                        <p class="lead">
                            Última atualização -
                            <asp:Literal runat="server" ID="ltlDataAtualizacao" />h.
                        </p>
                    </div>
                </div>
            </EmptyDataTemplate>
        </asp:ListView>

        <asp:PlaceHolder ID="phNovaAtividade" Visible="false" runat="server">
            <div class="row pb-4">
                <div class="col-md-2">
                    <asp:LinkButton runat="server" ID="lbIncluirAtividade" CssClass="btn btn-primary btn-lg w-100" OnCommand="IncluirAtividade" Text="Incluir Atividade &raquo;" />
                </div>
            </div>
        </asp:PlaceHolder>        

        <h2>Planejamento da Semana</h2>
        <div class="row pt-2 pb-4">
            <div class="col-md-2">
                <asp:LinkButton runat="server" ID="lbIncluirPlanejamento" CssClass="btn btn-primary btn-lg w-100" OnClick="IncluirPlanejamento" Text="Incluir &raquo;" />
            </div>
        </div>

        <h2>Acesso</h2>
        <div class="row">
            <div class="col-md-3">
                <asp:RadioButton ID="rbHomeOffice" Text="&nbsp;Home Office" Checked="true" GroupName="HomeOffice" runat="server" />                    
            </div>
            <div class="col-md-3">
                <asp:RadioButton ID="rbPresencial" Text="&nbsp;Presencial" GroupName="HomeOffice" runat="server" />                    
            </div>
        </div>
        <div class="row pt-2 pb-2">
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

    <asp:PlaceHolder ID="phAtalhos" runat="server">
        <div class="row">
            <div class="col-md-4">
                <h2>Atividades</h2>
                <p>
                    Atividades que tiveram as datas de conclusão prorrogadas. Justifique o motivo.<br />
                    <br />
                    <asp:Label runat="server" ID="lblQtdRepactuacaoPendente" Text="Nenhuma atividade pendente." />
                </p>
                <p>
                    <asp:LinkButton runat="server" ID="lbAtividades" CssClass="btn btn-outline-primary" Text="Justificar &raquo;" PostBackUrl="~/Pages/Atividade/AtividadeAlterada.aspx" />
                </p>
            </div>
            <div class="col-md-4" id="divAvaliacao" runat="server">
                <h2>Avaliação</h2>
                <p>
                    Efetuar avaliação mensal e avaliação das atividades concluídas.
                    Consultar as avaliações.
                </p>
                <p>
                    <asp:LinkButton runat="server" ID="lbAvaliacao" CssClass="btn btn-outline-primary" Text="Ver mais &raquo;" PostBackUrl="~/Pages/Atividade/AvaliacaoMensal.aspx" />
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
                    <asp:LinkButton runat="server" ID="lbCheckList" CssClass="btn btn-outline-primary" Text="Efetuar &raquo;" PostBackUrl="~/Pages/CheckList/CheckListSistema.aspx" />
                </p>
            </div>
        </div>
    </asp:PlaceHolder>

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

        $("[id$=lbHoraInicio]").click(function () {

            if ($(this).text() === "Início »") {
                var retorno = '';

                if ($("[id$=rbHomeOffice]").is(":checked")) {
                    retorno = 'em Home Office hoje?';
                }
                else {
                    retorno = 'de forma presencial hoje?';
                }

                DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Irá atuar ' + retorno, $(this));
                return false;
            }
            else {
                return false;
            }            
        });

    </script>
</asp:Content>
