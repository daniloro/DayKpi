<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroGrupo.ascx.cs" Inherits="PortalAtividade.UserControl.FiltroGrupo" %>
<asp:PlaceHolder ID="phGrupo" runat="server">
    <div class="row">
        <div class="col-sm-6 col-md-6 col-lg-4">
            <div class="form-group">
                <label>Grupo</label>
                <asp:DropDownList runat="server" ID="ddlGrupo" CssClass="form-control"
                    OnSelectedIndexChanged="DdlGrupo_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>
    </div>
</asp:PlaceHolder>
