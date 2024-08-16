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
    public partial class EmFilaMensal : BasePage
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
            GerarChartEmFilaMeses();
            CarregarAnalise();
        }        

        private void GerarChartEmFilaMeses()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaEmFila = RelatorioBo.GetInstance.ConsultarEmFilaGrupoChart(ucFiltro.Grupo, dataConsulta);

            ucChart.CarregarColumnChart(dataConsulta, listaEmFila);
        }

        private void CarregarAnalise()
        {
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila1).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila2).DscPergunta;
            ltlPergunta3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila3).DscPergunta;
            ltlPergunta4.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila4).DscPergunta;
            ltlPergunta5.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila5).DscPergunta;

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.EmFila1);
            txtAnalise1.Text = analise.DscAnalise;
            txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.EmFila2).DscAnalise;
            txtAnalise3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.EmFila3).DscAnalise;
            txtAnalise4.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.EmFila4).DscAnalise;
            txtAnalise5.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.EmFila5).DscAnalise;

            PermitirAlteracao(!analise.Concluido);
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise1.Enabled = habilita;
            txtAnalise2.Enabled = habilita;
            txtAnalise3.Enabled = habilita;
            txtAnalise4.Enabled = habilita;
            txtAnalise5.Enabled = habilita;
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
                Grupo = ucFiltro.Grupo,
                Concluido = concluido
            };

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.EmFila1, txtAnalise1.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.EmFila2, txtAnalise2.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.EmFila3, txtAnalise3.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.EmFila4, txtAnalise4.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.EmFila5, txtAnalise5.Text, analise);

            GerarChartEmFilaMeses();
            CarregarAnalise();
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(ucFiltro.Grupo))
            {
                RetornarMensagemAviso("Nenhum grupo selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) && string.IsNullOrEmpty(txtAnalise2.Text) && string.IsNullOrEmpty(txtAnalise3.Text) && string.IsNullOrEmpty(txtAnalise4.Text) && string.IsNullOrEmpty(txtAnalise5.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }

        private bool ValidarAnaliseEnvio()
        {
            if (string.IsNullOrEmpty(ucFiltro.Grupo))
            {
                RetornarMensagemAviso("Nenhum grupo selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) || string.IsNullOrEmpty(txtAnalise2.Text) || string.IsNullOrEmpty(txtAnalise3.Text) || string.IsNullOrEmpty(txtAnalise4.Text) || string.IsNullOrEmpty(txtAnalise5.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }        

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes).AddMonths(1);
            var listaRelatorio = RelatorioBo.GetInstance.ConsultarEmFilaGrupo(ucFiltro.Grupo, dataConsulta, dataConsulta);
            listaRelatorio = listaRelatorio.Select(p => { p.DataConclusao = p.DataConclusao == DateTime.MinValue ? null: p.DataConclusao; return p; }).ToList();

            var listaExcel = listaRelatorio.Select(p => new {
                p.NroChamado,
                p.DscChamado,
                p.TipoChamado,
                p.NroAtividade,
                p.Grupo,
                p.Operador,
                p.DataAbertura,
                p.DataFinal,
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