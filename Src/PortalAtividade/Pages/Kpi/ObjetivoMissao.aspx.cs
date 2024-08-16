using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business; 

namespace PortalAtividade.Pages.Kpi
{
    public partial class ObjetivoMissao : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider)
                    Response.Redirect("~/Default.aspx", false);
                
                ucFiltro.CarregarFiltro(UsuarioAD);
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {            
            CarregarAnalise();
        }

        private void CarregarAnalise()
        {
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Objetivo).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Missao).DscPergunta;            

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Objetivo);
            txtAnalise1.Text = analise.DscAnalise;
            txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Missao).DscAnalise;

            if (ucFiltro.Gestor == UsuarioAD)
                PermitirAlteracao(!analise.Concluido);
            else
                PermitirAlteracao(false);

            // Se não estiver preenchido, preenche com a analise do mês anterior
            VerificarAnaliseAnterior(dataConsulta, txtAnalise1, (int)TiposInfo.PerguntaKPI.Objetivo);
            VerificarAnaliseAnterior(dataConsulta, txtAnalise2, (int)TiposInfo.PerguntaKPI.Missao);
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise1.Enabled = habilita;
            txtAnalise2.Enabled = habilita;
            btnSalvar.Visible = habilita;
            btnSalvarEnviar.Visible = habilita;
        }

        private void VerificarAnaliseAnterior(DateTime dataConsulta, TextBox txtAnalise, int codPergunta)
        {
            if (string.IsNullOrEmpty(txtAnalise.Text))
            {
                txtAnalise.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta.AddMonths(-1), ucFiltro.Gestor, codPergunta).DscAnalise;
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarAnalise())
            {
                SalvarAnalise(false);
            }            
        }

        protected void BtnSalvarEnviar_Click(object sender, EventArgs e)
        {
            if (ValidarAnaliseEnvio())
            {
                SalvarAnalise(true);
            }            
        }

        private void SalvarAnalise(bool concluido)
        {
            AnaliseKPIInfo analise = new AnaliseKPIInfo
            {
                DataAnalise = Convert.ToDateTime(ucFiltro.Mes),
                LoginAd = UsuarioAD,                
                Concluido = concluido
            };

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Objetivo, txtAnalise1.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Missao, txtAnalise2.Text, analise);
            
            CarregarAnalise();
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(ucFiltro.Gestor))
            {
                RetornarMensagemAviso("Nenhum gestor selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) && string.IsNullOrEmpty(txtAnalise2.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }

        private bool ValidarAnaliseEnvio()
        {
            if (string.IsNullOrEmpty(ucFiltro.Gestor))
            {
                RetornarMensagemAviso("Nenhum gestor selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) || string.IsNullOrEmpty(txtAnalise2.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }        
    }
}