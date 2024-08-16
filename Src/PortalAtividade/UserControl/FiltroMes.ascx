<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroMes.ascx.cs" Inherits="PortalAtividade.UserControl.FiltroMes" %>
<div class="row">
    <div class="col-sm-12 col-md-12 col-lg-4">
        <div class="form-group">
            <label>Mês</label>
            <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control"
                OnSelectedIndexChanged="DdlMes_SelectedIndexChanged" AutoPostBack="true" />
        </div>
    </div>    
</div>