<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FiltroMesGestorOperador.ascx.cs" Inherits="PortalAtividade.UserControl.FiltroMesGestorOperador" %>
<div class="row">
    <asp:PlaceHolder ID="phMes" runat="server">
        <div class="col-sm-12 col-md-12 col-lg-4">
            <div class="form-group">
                <label>Mês</label>
                <asp:DropDownList runat="server" ID="ddlMes" CssClass="form-control"
                    OnSelectedIndexChanged="DdlMes_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phEquipe" runat="server">        
        <div class="col-sm-6 col-md-12 col-lg-4">
            <div class="form-group">
                <label>Equipes</label>
                <asp:DropDownList runat="server" ID="ddlGestor" CssClass="form-control"
                    OnSelectedIndexChanged="DdlGestor_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>        
    </asp:PlaceHolder>
    <asp:PlaceHolder ID="phGrupo" runat="server">        
        <div class="col-sm-6 col-md-12 col-lg-4">
            <div class="form-group">
                <label>Operador</label>
                <asp:DropDownList runat="server" ID="ddlOperador" CssClass="form-control"
                    OnSelectedIndexChanged="DdlOperador_SelectedIndexChanged" AutoPostBack="true" />
            </div>
        </div>        
    </asp:PlaceHolder>
</div>