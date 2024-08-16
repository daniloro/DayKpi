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
    public partial class AbertoConcluido : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ucFiltro.ConsultarFiltro += CarregarFiltro;

            if (!IsPostBack)
            {
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider) 
                    Response.Redirect("~/Default.aspx", false);
                
                ucFiltro.CarregarFiltro(UsuarioAD);
            }            
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            GerarChartAbertoConcluido();
            CarregarAnalise();
        }        

        private void GerarChartAbertoConcluido()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaAbertoConcluido = RelatorioBo.GetInstance.ConsultarAbertoConcluidoGrupoChart(ucFiltro.Grupo, dataConsulta);

            ucChart.CarregarComboChart(dataConsulta, listaAbertoConcluido);
        }

        private void CarregarAnalise()
        {
            ltlPergunta1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.AbertoConcluido1).DscPergunta;
            ltlPergunta2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.AbertoConcluido2).DscPergunta;            

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.AbertoConcluido1);
            txtAnalise1.Text = analise.DscAnalise;
            txtAnalise2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, ucFiltro.Grupo, (int)TiposInfo.PerguntaKPI.AbertoConcluido2).DscAnalise;

            PermitirAlteracao(!analise.Concluido);
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
                RetornarMensagemAviso("Análise salva com sucesso!");
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

            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.AbertoConcluido1, txtAnalise1.Text, analise);
            AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.AbertoConcluido2, txtAnalise2.Text, analise);                       

            GerarChartAbertoConcluido();
            CarregarAnalise();
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(ucFiltro.Grupo))
            {
                RetornarMensagemAviso("Nenhum grupo selecionado!");
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
            if (string.IsNullOrEmpty(ucFiltro.Grupo))
            {
                RetornarMensagemAviso("Nenhum grupo selecionado!");
                return false;
            }
            if (string.IsNullOrEmpty(txtAnalise1.Text) || string.IsNullOrEmpty(txtAnalise2.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }

        protected void BtnExcel_Click(object sender, EventArgs e)
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaRelatorio = RelatorioBo.GetInstance.ConsultarEmFilaGrupo(ucFiltro.Grupo, dataConsulta, dataConsulta.AddMonths(1));
            listaRelatorio.RemoveAll(p => p.CodStatus == 0 && p.DataAbertura < dataConsulta);
            listaRelatorio = listaRelatorio.Select(p => { p.DataConclusao = p.DataConclusao == DateTime.MinValue ? null : p.DataConclusao; return p; }).OrderBy(p => p.DataAbertura).ToList();

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

            GridView gvRelatorio = new GridView();
            gvRelatorio.DataSource = listaExcel;
            gvRelatorio.DataBind();

            if (listaExcel.Any())
                gvRelatorio.Rows[1].Cells[1].Width = 500; //DscChamado

            GerarExcel(gvRelatorio);
        }
    }
}