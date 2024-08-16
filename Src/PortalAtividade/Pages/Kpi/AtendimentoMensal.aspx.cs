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
    public partial class AtendimentoMensal : BasePage
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
            GerarChartAtendimento();
            CarregarAnalise();
        }

        public void GerarChartAtendimento()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            ltlNome.Text = OperadorBo.GetInstance.ObterOperador(ucFiltro.Operador)?.Nome;

            var listaConcluido = RelatorioBo.GetInstance.ConsultarConcluidoOperadorChart(ucFiltro.Operador, dataConsulta);
            ucChart.CarregarColumnChart(dataConsulta, listaConcluido);          
        }

        private void CarregarPerguntas()
        {
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento1).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento2).DscPergunta;
            ltlPergunta3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento3).DscPergunta;
            ltlPergunta4.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento4).DscPergunta;
        }

        private void CarregarAnalise()
        {
            if (!string.IsNullOrEmpty(ucFiltro.Operador))
            {
                var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
                var analise = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Atendimento1);
                txtAnalise1.Text = analise.DscAnalise;
                txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Atendimento2).DscAnalise;
                txtAnalise3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Atendimento3).DscAnalise;
                txtAnalise4.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, ucFiltro.Operador, (int)TiposInfo.PerguntaKPI.Atendimento4).DscAnalise;
                
                if (ucFiltro.Gestor == UsuarioAD && ucFiltro.Operador != ucFiltro.Gestor)                    
                    PermitirAlteracao(!analise.Concluido);                    
                else                    
                    PermitirAlteracao(false);
            }
            else
            {
                PermitirAlteracao(false);
            }
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise1.Enabled = habilita;
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

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Atendimento1, txtAnalise1.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Atendimento2, txtAnalise2.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Atendimento3, txtAnalise3.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Atendimento4, txtAnalise4.Text, analise);

            GerarChartAtendimento();
            CarregarAnalise();
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(ucFiltro.Operador))
            {
                RetornarMensagemAviso("Nenhum operador selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) && string.IsNullOrEmpty(txtAnalise2.Text) && string.IsNullOrEmpty(txtAnalise3.Text) && string.IsNullOrEmpty(txtAnalise4.Text))
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
            if (string.IsNullOrEmpty(txtAnalise1.Text) || string.IsNullOrEmpty(txtAnalise2.Text) || string.IsNullOrEmpty(txtAnalise3.Text) || string.IsNullOrEmpty(txtAnalise4.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }        

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaRelatorio = RelatorioBo.GetInstance.ConsultarConcluidoOperador(ucFiltro.Operador, dataConsulta, dataConsulta.AddMonths(1));

            var listaExcel = listaRelatorio.Select(p => new {
                p.NroChamado,
                p.DscChamado,
                p.TipoChamado,
                p.NroAtividade,
                p.Grupo,
                p.Operador,
                p.DataAbertura,                
                p.DataConclusao
            }).ToList();

            GridView gvRelatorio = new GridView
            {
                DataSource = listaExcel
            };
            gvRelatorio.DataBind();

            if (listaExcel.Any())
                gvRelatorio.Rows[0].Cells[1].Width = 500; //DscChamado

            GerarExcel(gvRelatorio);
        }
    }
}