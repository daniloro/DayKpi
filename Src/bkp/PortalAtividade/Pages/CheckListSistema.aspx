<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckListSistema.aspx.cs" Inherits="PortalAtividade.Pages.CheckListSistema" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="pt-4">CheckList</h2>    

    <div class="row">
        <div class="col-md-10">
            <h3>Diário dos Sistemas</h3>
        </div>
        <div class="col-md-2 align-self-end">
            <asp:CheckBox runat="server" ID="chkTodos" CssClass="align-bottom" Text="&nbsp;Mostrar Todos" 
                OnCheckedChanged="ChkTodos_CheckedChanged" AutoPostBack="true" />
        </div>
    </div>

    <asp:PlaceHolder runat="server" ID="phLista">
        <div class="table-responsive">
            <asp:GridView ID="gdvCheckList" CssClass="display table table-condensed table-striped" runat="server" AutoGenerateColumns="False"
                GridLines="None" OnPreRender="GdvCheckList_PreRender" OnRowDataBound="GdvCheckList_RowDataBound" EnableViewState="True">
                <EmptyDataTemplate>
                    <div class="box-alerta-emptyData">
                        <div class="text-center">
                            <p>Nenhuma ocorrência encontrada para esta consulta!</p>
                        </div>
                    </div>
                </EmptyDataTemplate>
                <Columns>
                    <asp:BoundField HeaderText="Sistema" />
                    <asp:BoundField HeaderText="Módulo" />
                    <asp:BoundField HeaderText="Descrição" />
                    <asp:BoundField HeaderText="Responsável" /> 
                    <asp:TemplateField HeaderText="" ItemStyle-Wrap="False">
                        <ItemTemplate>                            
                            <asp:LinkButton ID="btnConfirmar" OnCommand="ConfirmarCheckList" runat="server" >
                                <asp:Image ImageUrl="~/Content/images/check.png" runat="server" />
                            </asp:LinkButton>
                            <asp:Image ID="imgVisto" ImageUrl="~/Content/images/visto.png" Visible="false" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="" ItemStyle-Wrap="False">
                        <ItemTemplate>                            
                            <asp:LinkButton ID="btnObservacao" OnCommand="IncluirObservacao" runat="server" >
                                <asp:Image ImageUrl="~/Content/images/delete.gif" runat="server" />
                            </asp:LinkButton>                            
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:PlaceHolder>

    <asp:PlaceHolder runat="server" ID="phObservacao">
        <hr />

        <div class="form-group row mb-2">
            <div class="col-md-6 font-weight-bold">
                <asp:Literal runat="server" ID="ltlSistema" />
            </div>
            <div class="col-md-6">
                <asp:Literal runat="server" ID="ltlModulo" />
            </div>
        </div>
        <div class="form-group row mb-2">
            <div class="col-md-12">
                <asp:Literal runat="server" ID="ltlDescricao" />
            </div>            
        </div>  

        <div class="container pt-4 pb-2">            
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
