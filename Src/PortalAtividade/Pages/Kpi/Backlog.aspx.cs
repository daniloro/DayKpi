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
    public partial class Backlog : BasePage
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
            GerarChartBacklog();
            CarregarAnalise();            
        }

        private void GerarChartBacklog()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaBacklog = RelatorioBo.GetInstance.ConsultarBacklogChart(ucFiltro.Gestor, dataConsulta.AddMonths(-11), dataConsulta.AddMonths(1));

            ucChart.CarregarColumnChart(dataConsulta, listaBacklog);
        }

        private void CarregarAnalise()
        {
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Backlog1).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Backlog2).DscPergunta;
            ltlPergunta3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Backlog3).DscPergunta;

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Backlog1);
            txtAnalise1.Text = analise.DscAnalise;
            txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Backlog2).DscAnalise;
            txtAnalise3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Backlog3).DscAnalise;

            if (ucFiltro.Gestor == UsuarioAD)
                PermitirAlteracao(!analise.Concluido);
            else
                PermitirAlteracao(false);
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise1.Enabled = habilita;
            txtAnalise2.Enabled = habilita;
            txtAnalise3.Enabled = habilita;
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
                Concluido = concluido
            };

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Backlog1, txtAnalise1.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Backlog2, txtAnalise2.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Backlog3, txtAnalise3.Text, analise);

            GerarChartBacklog();
            CarregarAnalise();
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(ucFiltro.Gestor))
            {
                RetornarMensagemAviso("Nenhum gestor selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) && string.IsNullOrEmpty(txtAnalise2.Text) && string.IsNullOrEmpty(txtAnalise3.Text))
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
            if (string.IsNullOrEmpty(txtAnalise1.Text) || string.IsNullOrEmpty(txtAnalise2.Text) || string.IsNullOrEmpty(txtAnalise3.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes).AddMonths(1);
            var listaRelatorio = RelatorioBo.GetInstance.ConsultarBacklog(ucFiltro.Gestor, dataConsulta, dataConsulta);
            listaRelatorio = listaRelatorio.Select(p => { p.DataConclusao = p.DataConclusao == DateTime.MinValue ? null : p.DataConclusao; return p; }).ToList();

            var listaExcel = listaRelatorio.Select(p => new {
                p.NroChamado,
                p.DscChamado,
                p.DscPlanejador,
                p.Grupo,
                p.Operador,
                p.DataAbertura,
                p.DataConclusao
            }).ToList();

            GridView gvRelatorio = new GridView();
            gvRelatorio.DataSource = listaExcel;
            gvRelatorio.DataBind();

            if (listaExcel.Any())
                gvRelatorio.Rows[0].Cells[1].Width = 500; //DscChamado

            GerarExcel(gvRelatorio);
        }
    }
}