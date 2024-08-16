<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroMesGrupo.ascx.cs" Inherits="PortalAtividade.UserControl.FiltroMesGrupo" %>
<div class="row">
    <div class="col-sm-12 col-md-12 col-lg-4">
        <div class="form-group">
            <label>Mês</label>
            <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control"
                OnSelectedIndexChanged="DdlMes_SelectedIndexChanged" AutoPostBack="true" />
        </div>
    </div>
    <asp:PlaceHolder ID="phGrupo" runat="server">        
        <div class="col-sm-6 col-md-6 col-lg-4">
            <div class="form-group">
                <label>Grupo</label>
                <asp:DropDownList runat="server" ID="ddlGrupo" CssClass="form-control"
                    OnSelectedIndexChanged="DdlGrupo_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>        
    </asp:PlaceHolder>
</div>