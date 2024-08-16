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
    public partial class Performance : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider)
                    Response.Redirect("~/Default.aspx", false);
                
                CarregarPerguntas();
                ucFiltro.CarregarFiltro(UsuarioAD);                
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarAnalise();
        }

        private void CarregarPerguntas()
        {
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Performance1).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Performance2).DscPergunta;
            ltlPergunta3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Performance3).DscPergunta;
            ltlPergunta4.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Performance4).DscPergunta;
        }

        private void CarregarAnalise()
        {
            if (!string.IsNullOrEmpty(ucFiltro.Operador))
            {
                var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);

                var listaAtividade = AvaliacaoAtividadeBo.GetInstance.ConsultarAtividadePonderacaoMes(ucFiltro.Operador, dataConsulta, dataConsulta.AddMonths(1));
                var ponderacao = listaAtividade.Sum(p => p.Ponderacao);
                ltlPonderacao.Text = listaAtividade.Sum(p => p.Ponderacao).ToString();
                ltlPonderacaoTotal.Text = listaAtividade.Sum(p => p.PonderacaoTotal).ToString();

                var analise = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Performance1);            
            
                ddlAnalise1.SelectedValue = analise.DscAnalise;
                txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Performance2).DscAnalise;
                txtAnalise3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Performance3).DscAnalise;
                txtAnalise4.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Performance4).DscAnalise;

                if (ucFiltro.Gestor == UsuarioAD && ucFiltro.Operador != ucFiltro.Gestor)
                    PermitirAlteracao(!analise.Concluido);
                else
                    PermitirAlteracao(false);
            }
            else
                PermitirAlteracao(false);
        }

        private void PermitirAlteracao(bool habilita)
        {
            ddlAnalise1.Enabled = habilita;
            txtAnalise2.Enabled = habilita;
            txtAnalise3.Enabled = habilita;
            txtAnalise4.Enabled = habilita;
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
                LoginOperador = ucFiltro.Operador,
                Concluido = concluido
            };

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Performance1, ddlAnalise1.SelectedValue, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Performance2, txtAnalise2.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Performance3, txtAnalise3.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Performance4, txtAnalise4.Text, analise);
            
            CarregarAnalise();
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(ucFiltro.Operador))
            {
                RetornarMensagemAviso("Nenhum operador selecionado!");
                return false;
            }
            if (ddlAnalise1.SelectedValue == "0" && string.IsNullOrEmpty(txtAnalise2.Text) && string.IsNullOrEmpty(txtAnalise3.Text) && string.IsNullOrEmpty(txtAnalise4.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }

        private bool ValidarAnaliseEnvio()
        {
            if (string.IsNullOrEmpty(ucFiltro.Operador))
            {
                RetornarMensagemAviso("Nenhum operador selecionado!");
                return false;
            }
            if (ddlAnalise1.SelectedValue == "0" || string.IsNullOrEmpty(txtAnalise2.Text) || string.IsNullOrEmpty(txtAnalise3.Text) || string.IsNullOrEmpty(txtAnalise4.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }
    }
}