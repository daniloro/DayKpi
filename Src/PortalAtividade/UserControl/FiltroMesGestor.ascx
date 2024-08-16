<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroMesGestor.ascx.cs" Inherits="PortalAtividade.UserControl.FiltroMesGestor" %>
<div class="row">
    <div class="col-sm-12 col-md-4 col-lg-4">
        <div class="form-group">
            <label>Mês</label>
            <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control"
                OnSelectedIndexChanged="DdlMes_SelectedIndexChanged" AutoPostBack="true" />
        </div>
    </div>
    <asp:PlaceHolder ID="phEquipe" runat="server">        
        <div class="col-sm-6 col-md-6 col-lg-4">
            <div class="form-group">
                <label>Equipes</label>
                <asp:DropDownList runat="server" ID="ddlGestor" CssClass="form-control"
                    OnSelectedIndexChanged="DdlGestor_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>        
    </asp:PlaceHolder>
</div>