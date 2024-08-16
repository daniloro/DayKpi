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
    public partial class Backlog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarBacklog(string gestor, DateTime dataConsulta)
        {            
            CarregarAnalise(gestor, dataConsulta);

            var listaBacklog = RelatorioBo.GetInstance.ConsultarBacklogChart(gestor, dataConsulta.AddMonths(-11), dataConsulta.AddMonths(1));

            ColumnChart ucColumnChart = (ColumnChart)LoadControl("~/UserControl/Chart/ColumnChart.ascx");
            ucColumnChart.ID = ID;
            ucColumnChart.CarregarColumnChart(dataConsulta, listaBacklog);
            phColumnChart.Controls.Add(ucColumnChart);
        }

        private void CarregarAnalise(string gestor, DateTime dataConsulta)
        {
            ltlBacklog1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Backlog1).DscPergunta;
            ltlBacklogResp1.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, gestor, (int)TiposInfo.PerguntaKPI.Backlog1).DscAnalise ?? "Sem resposta.";
            ltlBacklogResp1.Text = ltlBacklogResp1.Text.Replace(Environment.NewLine, "<br />");

            ltlBacklog2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Backlog2).DscPergunta;
            ltlBacklogResp2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, gestor, (int)TiposInfo.PerguntaKPI.Backlog2).DscAnalise ?? "Sem resposta.";
            ltlBacklogResp2.Text = ltlBacklogResp2.Text.Replace(Environment.NewLine, "<br />");

            ltlBacklog3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Backlog3).DscPergunta;
            ltlBacklogResp3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, gestor, (int)TiposInfo.PerguntaKPI.Backlog3).DscAnalise ?? "Sem resposta.";
            ltlBacklogResp3.Text = ltlBacklogResp3.Text.Replace(Environment.NewLine, "<br />");
        }
    }
}