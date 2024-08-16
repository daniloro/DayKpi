using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.UserControl
{
    public partial class FiltroMes : System.Web.UI.UserControl
    {
        public event EventHandler ConsultarFiltro;        

        public string Mes
        {
            get
            {
                return ddlMes.SelectedValue;
            }
            set
            {
                ddlMes.SelectedValue = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void CarregarFiltro()
        {
            CarregarMeses();            

            EfetuarConsultaFiltro(new EventArgs());
        }

        private void EfetuarConsultaFiltro(EventArgs e)
        {                      
            ConsultarFiltro?.Invoke(ConsultarFiltro, e);
        }

        private void CarregarMeses()
        {
            for (int i = -3; i < 1; i++)
            {
                var dataConsulta = DateTime.Today.AddMonths(i);
                dataConsulta = new DateTime(dataConsulta.Year, dataConsulta.Month, 1);

                ddlMes.Items.Add(new ListItem(UtilBo.GetInstance.ObterNomeMes(dataConsulta.Month), dataConsulta.ToString("dd/MM/yyyy")));
            }
            ddlMes.SelectedIndex = 3;
        }        

        protected void DdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }        
    }
}