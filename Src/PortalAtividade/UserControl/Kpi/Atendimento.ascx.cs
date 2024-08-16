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
    public partial class Atendimento : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarAtendimento(string login, DateTime dataConsulta)
        {
            ltlOperador.Text = OperadorBo.GetInstance.ObterOperador(login).Nome;
            CarregarAnalise(login, dataConsulta);
            
            var listaConcluido = RelatorioBo.GetInstance.ConsultarConcluidoOperadorChart(login, dataConsulta);

            ColumnChart ucColumnChart = (ColumnChart)LoadControl("~/UserControl/Chart/ColumnChart.ascx");
            ucColumnChart.ID = ID;
            ucColumnChart.CarregarColumnChart(dataConsulta, listaConcluido);
            phColumnChart.Controls.Add(ucColumnChart);            
        }

        private void CarregarAnalise(string login, DateTime dataConsulta)
        {
            ltlAtendimento1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento1).DscPergunta;
            ltlAtendimentoResp1.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Atendimento1).DscAnalise ?? "Sem resposta.";
            ltlAtendimentoResp1.Text = ltlAtendimentoResp1.Text.Replace(Environment.NewLine, "<br />");

            ltlAtendimento2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento2).DscPergunta;
            ltlAtendimentoResp2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Atendimento2).DscAnalise ?? "Sem resposta.";
            ltlAtendimentoResp2.Text = ltlAtendimentoResp2.Text.Replace(Environment.NewLine, "<br />");

            ltlAtendimento3.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento3).DscPergunta;
            ltlAtendimentoResp3.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Atendimento3).DscAnalise ?? "Sem resposta.";
            ltlAtendimentoResp3.Text = ltlAtendimentoResp3.Text.Replace(Environment.NewLine, "<br />");

            ltlAtendimento4.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Atendimento4).DscPergunta;
            ltlAtendimentoResp4.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Atendimento4).DscAnalise ?? "Sem resposta.";
            ltlAtendimentoResp4.Text = ltlAtendimentoResp4.Text.Replace(Environment.NewLine, "<br />");
        }
    }
}