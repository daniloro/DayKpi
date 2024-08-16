using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using PortalAtividade.Business;

namespace PortalAtividade.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {                
                txtLogin.Focus();
                EsconderMenu();
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid && OperadorBo.GetInstance.ObterOperador(txtLogin.Text) != null)
            {
                try
                {   
                    var auth = new LdapAuthentication();

                    if (auth.IsAuthenticated("daycoval", txtLogin.Text, txtSenha.Text))
                    {
                        FormsAuthentication.RedirectFromLoginPage(txtLogin.Text.ToLower(), false);
                    }
                    else
                    {
                        phErrorMessage.Visible = true;
                        litMensagem.Text = "Tentativa de login inválida!";
                    }
                }
                catch (Exception ex)
                {
                    litMensagem.Text = "Tentativa de login inválida! " + ex.Message;
                }
            }
            else
            {
                litMensagem.Text = "Usuário não cadastrado!";
            }
        }

        private void EsconderMenu()
        {
            ((HtmlControl)Master.FindControl("conteudoNavbarSuportado")).Visible = false;
        }
    }
}