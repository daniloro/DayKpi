using System;
using System.Web.UI;
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

            }

            base.OnLoad(e);
        }

        private void AtualizarSessao()
        {
            CurrentSession = (SessionManager)Session["CurrentSession"];
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
    }    
}