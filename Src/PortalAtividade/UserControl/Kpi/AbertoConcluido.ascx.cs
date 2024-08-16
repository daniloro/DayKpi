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
    public partial class AbertoConcluido : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarAbertoConcluido(string grupo, DateTime dataConsulta)
        {            
            ltlGrupo.Text = grupo;
            CarregarAnalise(grupo, dataConsulta);

            var listaAbertoConcluido = RelatorioBo.GetInstance.ConsultarAbertoConcluidoGrupoChart(grupo, dataConsulta);

            ComboChart ucComboChart = (ComboChart)LoadControl("~/UserControl/Chart/ComboChart.ascx");
            ucComboChart.ID = ID;
            ucComboChart.CarregarComboChart(dataConsulta, listaAbertoConcluido);
            phComboChart.Controls.Add(ucComboChart);
        }        

        private void CarregarAnalise(string grupo, DateTime dataConsulta)
        {            
            ltlAbertoConcluido1.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.AbertoConcluido1).DscPergunta;
            ltlAbertoConcluidoResp1.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, grupo, (int)TiposInfo.PerguntaKPI.AbertoConcluido1).DscAnalise ?? "Sem resposta.";
            ltlAbertoConcluidoResp1.Text = ltlAbertoConcluidoResp1.Text.Replace(Environment.NewLine, "<br />");

            ltlAbertoConcluido2.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.AbertoConcluido2).DscPergunta;
            ltlAbertoConcluidoResp2.Text = AnaliseKPIBo.GetInstance.ObterAnaliseGrupo(dataConsulta, grupo, (int)TiposInfo.PerguntaKPI.AbertoConcluido2).DscAnalise ?? "Sem resposta.";
            ltlAbertoConcluidoResp2.Text = ltlAbertoConcluidoResp2.Text.Replace(Environment.NewLine, "<br />");
        }
    }
}