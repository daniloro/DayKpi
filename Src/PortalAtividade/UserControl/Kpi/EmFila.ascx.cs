using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;
using PortalAtividade.UserControl.Chart;

namespace PortalAtividade.UserControl.Kpi
{
    public partial class EmFila : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarEmFila(string grupo, DateTime dataConsulta)
        {            
            ltlGrupo.Text = grupo;
            CarregarAnalise(grupo, dataConsulta);

            var listaEmFila = RelatorioBo.GetInstance.ConsultarEmFilaGrupoChart(grupo, dataConsulta);
            
            ColumnChart ucColumnChart = (ColumnChart)LoadControl("~/UserControl/Chart/ColumnChart.ascx");
            ucColumnChart.ID = ID;
            ucColumnChart.CarregarColumnChart(dataConsulta, listaEmFila);
            phColumnChart.Controls.Add(ucColumnChart);                               
        }

        private void CarregarAnalise(string grupo, DateTime dataConsulta)
        {
            ltlEmFila1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila1).DscPergunta;
            ltlEmFilaResp1.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, grupo, (int)TiposInfo.PerguntaKPI.EmFila1).DscAnalise ?? "Sem resposta.";
            ltlEmFilaResp1.Text = ltlEmFilaResp1.Text.Replace(Environment.NewLine, "<br />");

            ltlEmFila2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila2).DscPergunta;
            ltlEmFilaResp2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, grupo, (int)TiposInfo.PerguntaKPI.EmFila2).DscAnalise ?? "Sem resposta.";
            ltlEmFilaResp2.Text = ltlEmFilaResp2.Text.Replace(Environment.NewLine, "<br />");

            ltlEmFila3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.EmFila3).DscPergunta;
            ltlEmFilaResp3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, grupo, (int)TiposInfo.PerguntaKPI.EmFila3).DscAnalise ?? "Sem resposta.";
            ltlEmFilaResp3.Text = ltlEmFilaResp3.Text.Replace(Environment.NewLine, "<br />");
        }
    }
}