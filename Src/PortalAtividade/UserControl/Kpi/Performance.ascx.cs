using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;


namespace PortalAtividade.UserControl.Kpi
{
    public partial class Performance : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void CarregarPerformance(DateTime dataConsulta, string login, string nome)
        {
            ltlOperador.Text = nome;

            ltlNotaPerformance.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Performance1).DscAnalise ?? "Sem resposta.";
            ltlNotaPerformance.Text = ltlNotaPerformance.Text.Replace(Environment.NewLine, "<br />");
            ltlPontosFortes.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Performance2).DscAnalise ?? "Sem resposta.";
            ltlPontosFortes.Text = ltlPontosFortes.Text.Replace(Environment.NewLine, "<br />");
            ltlPontosMelhoria.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Performance3).DscAnalise ?? "Sem resposta.";
            ltlPontosMelhoria.Text = ltlPontosMelhoria.Text.Replace(Environment.NewLine, "<br />");
            ltlOportunidade.Text = AnaliseKPIBo.GetInstance.ObterAnaliseOperador(dataConsulta, login, (int)TiposInfo.PerguntaKPI.Performance4).DscAnalise ?? "Sem resposta.";
            ltlOportunidade.Text = ltlOportunidade.Text.Replace(Environment.NewLine, "<br />");
        }
    }
}