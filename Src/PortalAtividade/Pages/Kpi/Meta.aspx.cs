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
    public partial class Meta : BasePage
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
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Meta1).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Meta2).DscPergunta;            

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Meta1);
            txtAnalise1.Text = analise.DscAnalise;
            txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Meta2).DscAnalise;

            // Obtem as metas do mês realizadas no mês anterior para análise
            ltlPergunta2.Text += " - " + UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(1).Month);
            ltlPerguntaAnterior.Text = "Meta estabelecida para " + UtilBo.GetInstance.ObterNomeMes(dataConsulta.Month);
            txtAnaliseAnterior.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta.AddMonths(-1), ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Meta2).DscAnalise;
            phAnterior.Visible = !string.IsNullOrEmpty(txtAnaliseAnterior.Text);

            if (ucFiltro.Gestor == UsuarioAD)
                PermitirAlteracao(!analise.Concluido);
            else
                PermitirAlteracao(false);
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise1.Enabled = habilita;
            txtAnalise2.Enabled = habilita;
            btnSalvar.Visible = habilita;
            btnSalvarEnviar.Visible = habilita;
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
                Grupo = ucFiltro.Gestor,
                Concluido = concluido
            };

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Meta1, txtAnalise1.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Meta2, txtAnalise2.Text, analise);

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