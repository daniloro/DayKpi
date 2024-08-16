<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PortalAtividade.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2 class="pt-4"><%: Title %></h2>

    <div class="row">
        <div class="col-md-8">
            <section id="loginForm">
                <div class="form-horizontal">
                    <h4>KPI Desenvolvimento</h4>
                    <hr />
                    
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtLogin" CssClass="col-md-2 control-label">Login</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtLogin" CssClass="form-control" MaxLength="8" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLogin"
                                CssClass="text-danger" ErrorMessage="Digite o Login" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="txtSenha" CssClass="col-md-2 control-label">Senha</asp:Label>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" ID="txtSenha" TextMode="Password" CssClass="form-control" MaxLength="18" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSenha" CssClass="text-danger" ErrorMessage="Digite a Senha" />
                        </div>
                    </div>
                    <%--<div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Remember me?</asp:Label>
                            </div>
                        </div>
                    </div>--%>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Log in" CssClass="btn btn-outline-primary" />
                        </div>
                    </div>

                    <asp:PlaceHolder runat="server" ID="phErrorMessage" Visible="false">
                        <div class="form-group">
                            <div class="col-md-offset-2 col-md-10">
                                <p class="text-danger">
                                    <asp:Literal runat="server" ID="litMensagem" />
                                </p>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
                                
            </section>
        </div>
        
    </div>
</asp:Content>
