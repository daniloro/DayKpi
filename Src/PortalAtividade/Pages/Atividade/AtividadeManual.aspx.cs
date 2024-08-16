using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Atividade
{
    public partial class AtividadeManual : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarOperador();                
            }
        }

        private void CarregarOperador()
        {
            if (CurrentSession.Objects is string loginAd)
            {
                var operador = OperadorBo.GetInstance.ObterOperador(loginAd);

                ltlOperador.Text = operador.Nome;
            }
        }

        private void CarregarAtividade()
        {
            phConsulta.Visible = true;
            btnSalvar.Visible = true;
            txtDescricao.Text = "";

            var atividade = AtividadeBo.GetInstance.ObterAtividade(txtNroAtividade.Text);

            if (!string.IsNullOrEmpty(atividade.NroAtividade))
            {                
                phDescricao.Visible = true;
                phDescricaoManual.Visible = false;

                ltlNroChamado.Text = atividade.NroChamado;
                ltlDscChamado.Text = atividade.DscChamado;
                ltlNroAtividade.Text = atividade.NroAtividade;
                ltlDscAtividade.Text = atividade.DscAtividade;

                if (atividade.DataFinal > DateTime.Today)                
                    txtDataFinal.Text = atividade.DataFinal.ToString("dd/MM/yyyy");              
                else
                    txtDataFinal.Text = DateTime.Today.ToString("dd/MM/yyyy");
            }
            else
            {
                phDescricao.Visible = false;
                phDescricaoManual.Visible = true;

                if (ChamadoBo.GetInstance.ObterChamado(txtNroAtividade.Text).TipoChamado == "Req. Complexa")
                {
                    ltlNroAtividade.Text = "";                   

                    RetornarMensagemAviso("Digite o número da atividade ao invés do chamado principal!");
                }
                else
                {
                    ltlNroAtividade.Text = txtNroAtividade.Text;
                }
                
                ltlDscAtividade.Text = "Atividade não encontrada no sistema. Incluir a descrição da atividade.";
                txtDataFinal.Text = DateTime.Today.ToString("dd/MM/yyyy");                
            }
        }

        protected void BtnConsultar_Click(object sender, EventArgs e)
        {
            if (ValidarConsulta())
            {
                CarregarAtividade();                
            }
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            Redirecionar();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarAtividade())
            {
                SalvarAtividade();
                Redirecionar();
            }
        }

        private void Redirecionar()
        {
            if (!string.IsNullOrEmpty(CurrentSession.Redirect))
            {
                var pagina = CurrentSession.Redirect;

                CurrentSession.Redirect = null;
                Response.Redirect(pagina, false);
            }
            else
                Response.Redirect("~/Pages/Atividade/AtividadeAndamento.aspx", false);
        }

        private bool ValidarConsulta()
        {
            if (string.IsNullOrEmpty(txtNroAtividade.Text))
            {
                RetornarMensagemAviso("Digite o número da atividade!");
                return false;
            }
            if (txtNroAtividade.Text.Length < 11)
            {
                RetornarMensagemAviso("Número de atividade inválido!");
                return false;
            }
            return true;
        }

        private bool ValidarAtividade()
        {            
            if (ltlNroAtividade.Text.Length < 11)
            {
                RetornarMensagemAviso("Número de atividade inválido!");
                return false;
            }

            if (phDescricaoManual.Visible)
            {
                if (string.IsNullOrEmpty(txtDescricao.Text) || txtDescricao.Text.Length < 3)
                {
                    RetornarMensagemAviso("Adicione uma descrição da atividade!");
                    return false;
                }
            }            

            DateTime.TryParse(txtDataFinal.Text, out DateTime dataFinal);

            if (dataFinal == DateTime.MinValue)
            {
                RetornarMensagemAviso("Digite a data de conclusão!");
                return false;
            }
            else if (dataFinal < DateTime.Today)
            {
                RetornarMensagemAviso("A data de conclusão não pode estar vencida!");
                return false;
            }

            return true;
        }

        private void SalvarAtividade()
        {
            if (CurrentSession.Objects is string loginAd)
            {
                var atividade = new AtividadeInfo
                {
                    LoginAd = loginAd,
                    NroAtividade = ltlNroAtividade.Text,
                    DscAtividade = txtDescricao.Text,                    
                    DataFinal = Convert.ToDateTime(txtDataFinal.Text)
                };
                
                AtividadeBo.GetInstance.IncluirAtividadeManual(atividade);                
            }
        }
    }
}