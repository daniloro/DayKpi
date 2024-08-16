<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AvaliacaoAtividade.aspx.cs" Inherits="PortalAtividade.Pages.AvaliacaoAtividade" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">Avaliação</h2>
    <h3>Efetue a avaliação das atividades concluídas</h3>
    <%--<p></p>--%>

    <asp:PlaceHolder runat="server" ID="phLista">
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
                    <asp:BoundField HeaderText="Operador" />
                    <asp:BoundField HeaderText="Data Conclusão" />
                    <asp:TemplateField HeaderText="" ItemStyle-Width="50px" ItemStyle-Wrap="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditar" CssClass="btn btn-outline-primary" OnCommand="EditarAtividade" Text="Avaliar" runat="server" />
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

        <div class="container pt-4 pb-2">
            <div class="row">
                <div class="col-md-6">
                    <div class="form-group">
                        <label>Complexidade</label>
                        <asp:DropDownList runat="server" ID="ddlComplexidade" CssClass="form-control">
                            <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                            <asp:ListItem Text="Bug ou Pequena Melhoria" Value="2" />
                            <asp:ListItem Text="Melhoria no Sistema" Value="4" />
                            <asp:ListItem Text="Pequeno Projeto" Value="6" />
                            <asp:ListItem Text="Projeto Médio" Value="8" />
                            <asp:ListItem Text="Projeto Grande" Value="10" />                            
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="form-group">
                        <label><asp:Literal runat="server" ID="ltlNotaPo" Text="Product Owner - Senso de Dono" /></label>
                        <asp:DropDownList runat="server" ID="ddlNotaPo" CssClass="form-control" >
                            <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                            <asp:ListItem Text="Melhorar muito" Value="2" />
                            <asp:ListItem Text="Melhorar um pouco" Value="4" />
                            <asp:ListItem Text="Ok" Value="6" />
                            <asp:ListItem Text="Bom" Value="8" />
                            <asp:ListItem Text="Muito bom" Value="10" />
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <asp:PlaceHolder ID="phDesenvolvedor" runat="server">
                <asp:UpdatePanel ID="uppNegocio" UpdateMode="Conditional" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Analista de Negócio</label>
                                    <asp:DropDownList runat="server" ID="ddlAnalistaNegocio" CssClass="form-control"
                                        OnSelectedIndexChanged="DdlAnalistaNegocio_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Não teve analista de Negócio" Value="0" />
                                        <asp:ListItem Text="Alan Mendes da Silva" Value="1" />
                                        <asp:ListItem Text="Alessandra Alves Teixeira Igne" Value="2" />
                                        <asp:ListItem Text="Ana Paula Lima de Oliveira Vieira" Value="3" />
                                        <asp:ListItem Text="Ana Paula Sobral Ramos" Value="4" />
                                        <asp:ListItem Text="André Luiz de Souza" Value="5" />
                                        <asp:ListItem Text="Andre Rodrigues dos Santos" Value="6" />
                                        <asp:ListItem Text="Clayton Roberto Abreu" Value="7" />
                                        <asp:ListItem Text="Danilo Caliman Mendes" Value="8" />
                                        <asp:ListItem Text="Danilo da Silva Bezerra" Value="9" />
                                        <asp:ListItem Text="Denise Aparecida de Sousa Sá" Value="10" />
                                        <asp:ListItem Text="Elisa Alves de Lima" Value="11" />
                                        <asp:ListItem Text="Felipe Mendes Silva" Value="12" />
                                        <asp:ListItem Text="Fernanda Amaral Schorch" Value="13" />
                                        <asp:ListItem Text="Fernando Bertolini" Value="14" />
                                        <asp:ListItem Text="Flavio Ricardo de Souza Simoes" Value="15" />
                                        <asp:ListItem Text="Graziele Moreira da Nobrega" Value="16" />
                                        <asp:ListItem Text="Henrique de Oliveira Pinheiro" Value="17" />
                                        <asp:ListItem Text="Herbert Francisco Pereira" Value="18" />
                                        <asp:ListItem Text="Jeferson de Jesus Pereira Leite" Value="19" />
                                        <asp:ListItem Text="Johnny Eder Teixeira Meneses" Value="20" />
                                        <asp:ListItem Text="Julia Generoso Mendes" Value="21" />
                                        <asp:ListItem Text="Julio Cesar Alves Ribeiro da Costa" Value="22" />
                                        <asp:ListItem Text="Lucas Trovo Parra" Value="23" />
                                        <asp:ListItem Text="Luciana Ramos Rodrigues" Value="24" />
                                        <asp:ListItem Text="Marcel Shinzato" Value="25" />
                                        <asp:ListItem Text="Marcos Alexandre Lemes Guilherme" Value="26" />
                                        <asp:ListItem Text="Marilene Vieira Ferreira" Value="27" />
                                        <asp:ListItem Text="Mayra Sonia Vieira Dias Salemo" Value="28" />
                                        <asp:ListItem Text="Michele Henrique Nogueira" Value="29" />
                                        <asp:ListItem Text="Michele Silva Santana" Value="30" />
                                        <asp:ListItem Text="Oscar Ryo Toyoda" Value="31" />
                                        <asp:ListItem Text="Rafael de Oliveira" Value="32" />
                                        <asp:ListItem Text="Raphael Publio de Oliveira" Value="33" />
                                        <asp:ListItem Text="Regiane Gonçalves dos Santos Vieira" Value="34" />
                                        <asp:ListItem Text="Robson da Silva" Value="35" />
                                        <asp:ListItem Text="Rodrigo da Silva Zeferino" Value="36" />
                                        <asp:ListItem Text="Rodrigo Rosa de Oliveira" Value="37" />
                                        <asp:ListItem Text="Tamires de Moraes Colla" Value="38" />
                                        <asp:ListItem Text="Thiago Jorge Restivo" Value="39" />
                                        <asp:ListItem Text="Wellington Iranil da Silva" Value="40" />
                                        <asp:ListItem Text="Welton Ronald da Silva Junior" Value="41" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Especificação</label>
                                    <asp:DropDownList runat="server" ID="ddlNotaEspec" CssClass="form-control" >
                                        <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Não possui especificação" Value="0" />
                                        <asp:ListItem Text="Pode melhorar muito" Value="2" />
                                        <asp:ListItem Text="Pode melhorar um pouco" Value="4" />
                                        <asp:ListItem Text="Atende" Value="6" />
                                        <asp:ListItem Text="Especificação Boa" Value="8" />
                                        <asp:ListItem Text="Superou a expectavia" Value="10" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Acompanhamento</label>
                                    <asp:DropDownList runat="server" ID="ddlNotaNegocio" CssClass="form-control" >
                                        <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                                        <asp:ListItem Text="Não teve analista de negócio" Value="0" />
                                        <asp:ListItem Text="Só abriu o chamado e validou" Value="2" />
                                        <asp:ListItem Text="Demora para tirar dúvidas por estar em outro chamado" Value="4" />
                                        <asp:ListItem Text="Atende" Value="6" />
                                        <asp:ListItem Text="Fez reunião de início de projeto e acompanhou durante o dev" Value="8" />
                                        <asp:ListItem Text="Parceria do início ao fim" Value="10" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </asp:PlaceHolder>

            <asp:PlaceHolder ID="phLider" runat="server">
                <div class="row">
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Qualidade (Code Review)</label>
                            <asp:DropDownList runat="server" ID="ddlNotaQualidade" CssClass="form-control" >
                                <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                                <asp:ListItem Text="Muitos itens com problema - Refazer" Value="2" />
                                <asp:ListItem Text="Alguns itens com problema - Corrigir" Value="4" />
                                <asp:ListItem Text="Ok, mas sugeri melhoria pontual" Value="6" />
                                <asp:ListItem Text="Code Review Ok" Value="8" />
                                <asp:ListItem Text="Fez mais do que o esperado" Value="10" />
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="form-group">
                            <label>Performance - Produtividade</label>
                            <asp:DropDownList runat="server" ID="ddlNotaPerformance" CssClass="form-control" >
                                <asp:ListItem Text="Selecione" Value="-1" Selected="True" />
                                <asp:ListItem Text="Gastou muito tempo na atividade" Value="2" />
                                <asp:ListItem Text="Gastou mais tempo do que o esperado" Value="4" />
                                <asp:ListItem Text="Entregou no prazo acordado" Value="6" />
                                <asp:ListItem Text="Entregou no prazo acordado - Prazo apertado" Value="8" />
                                <asp:ListItem Text="Surpreendeu no tempo de entrega" Value="10" />
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>

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
        $("[id$=txtObservacao]").alphanumeric({ allow: ".,- '" });

    </script>
</asp:Content>
