<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroMesOperador.ascx.cs" Inherits="PortalAtividade.UserControl.FiltroMesOperador" %>
<div class="row">
    <div class="col-sm-6 col-md-6 col-lg-4">
        <div class="form-group">
            <label>Mês</label>
            <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control"
                OnSelectedIndexChanged="DdlMes_SelectedIndexChanged" AutoPostBack="true" />
        </div>
    </div>
    <asp:PlaceHolder ID="phOperador" runat="server">
        <div class="col-sm-6 col-md-6 col-lg-4">
            <div class="form-group">
                <label>Operador</label>
                <asp:DropDownList runat="server" ID="ddlOperador" CssClass="form-control"
                    OnSelectedIndexChanged="DdlOperador_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>
    </asp:PlaceHolder>
</div>