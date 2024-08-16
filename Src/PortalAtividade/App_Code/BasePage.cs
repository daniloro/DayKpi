using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade
{
    public class BasePage: Page
    {
        public SessionManager CurrentSession { get; set; }

        protected override void OnLoad(EventArgs e)
        {
            AtualizarSessao();

            if (!IsPostBack)
            {
                AtualizarMenu();
            }

            base.OnLoad(e);
        }

        public string UsuarioAD
        {
            get
            {
                return User.Identity.Name;
            }
        }

        public OperadorInfo Usuario { get {
                return CurrentSession.Usuario;
            }
        }

        private void AtualizarSessao()
        {
            CurrentSession = (SessionManager)Session["CurrentSession"];

            if (CurrentSession.Usuario == null)
            {
                CurrentSession.Usuario = OperadorBo.GetInstance.ObterOperador(UsuarioAD);
            }
        }

        private void AtualizarMenu()
        {
            if (Master != null)
            {                
                if (Usuario.CodTipoEquipe == (int)TiposInfo.TipoEquipe.Redes)
                {
                    ((HtmlControl)Master.FindControl("liAtividade")).Visible = false;
                    ((HtmlControl)Master.FindControl("liKpi")).Visible = false;
                    ((HtmlControl)Master.FindControl("liRelatorio")).Visible = false;
                }
                if (Usuario.TipoOperador == (int)TiposInfo.TipoOperador.Analista)
                {
                    ((HtmlControl)Master.FindControl("lbHorarioEquipe")).Visible = false;                 
                }
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider)
                {
                    ((HtmlControl)Master.FindControl("liKpi")).Visible = false;
                }
                if (Usuario.TipoOperador != (int)TiposInfo.TipoOperador.Adm)
                {
                    ((HtmlControl)Master.FindControl("liAdm")).Visible = false;
                }
            }
        }

        public void RetornarMensagemAviso(string mensagem, string titulo = "Verifique a solicitação.")
        {
            if (!string.IsNullOrEmpty(mensagem))
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Msg", $"DayMensagens.mostraMensagem('{titulo}', '" + mensagem + "');", true);
        }

        public void RetornarMensagemConfirmacao(string mensagem, string botaoId)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Confirma", "DayMensagens.mostraMensagemConfirmacao('Confirmação', '" + mensagem + " Deseja continuar?', " + botaoId + ");", true);
        }        

        #region Excel
        public void GerarExcel(GridView gvRelatorio)
        {
            gvRelatorio.Font.Name = "Tahoma";
            gvRelatorio.Font.Size = 10;

            if (gvRelatorio.Rows.Count + 1 < 65536)
            {
                Response.ClearContent();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AppendHeader("content-disposition", "attachment; filename=Relatorio.xls");
                var tw = new System.IO.StringWriter();
                var hw = new HtmlTextWriter(tw);
                gvRelatorio.RenderControl(hw);
                Response.Write(tw.ToString());
                Response.End();
            }
            else
            {
                RetornarMensagemAviso("Muitas linhas para exportar para o Excel!!!");                
            }
        }

        #endregion
    }
}