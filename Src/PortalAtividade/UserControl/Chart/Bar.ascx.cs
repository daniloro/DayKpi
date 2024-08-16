using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.UserControl.Chart
{
    public partial class Bar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void CarregarBar(DateTime dataConsulta, List<ChartSeisInfo> listaEmFila)
        {
            if (!listaEmFila.Any())
            {
                Visible = false;
                return;
            }

            Visible = true;
            var grupo = "['Mês', ";
            var mes1 = "['" + UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-5).Month) + "', ";
            var mes2 = "['" + UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-4).Month) + "', ";
            var mes3 = "['" + UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-3).Month) + "', ";
            var mes4 = "['" + UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-2).Month) + "', ";
            var mes5 = "['" + UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(-1).Month) + "', ";
            var mes6 = "['" + UtilBo.GetInstance.ObterNomeMes(dataConsulta.Month) + "', ";

            foreach (var item in listaEmFila)
            {
                grupo += "'" + item.Descricao + "', ";
                mes1 += item.Item1.ToString() + ", ";
                mes2 += item.Item2.ToString() + ", ";
                mes3 += item.Item3.ToString() + ", ";
                mes4 += item.Item4.ToString() + ", ";
                mes5 += item.Item5.ToString() + ", ";
                mes6 += item.Item6.ToString() + ", ";
            }

            grupo = grupo.Remove(grupo.Length - 2) + "],";
            mes1 = mes1.Remove(mes1.Length - 2) + "],";
            mes2 = mes2.Remove(mes2.Length - 2) + "],";
            mes3 = mes3.Remove(mes3.Length - 2) + "],";
            mes4 = mes4.Remove(mes4.Length - 2) + "],";
            mes5 = mes5.Remove(mes5.Length - 2) + "],";
            mes6 = mes6.Remove(mes6.Length - 2) + "]";

            var retorno = "[" + grupo + mes1 + mes2 + mes3 + mes4 + mes5 + mes6 + "]";

            ScriptManager.RegisterStartupScript(Page, this.GetType(), "ScriptBar", $"dadosBar = " + retorno + "; google.charts.setOnLoadCallback(drawBar);", true);
        }
    }
}