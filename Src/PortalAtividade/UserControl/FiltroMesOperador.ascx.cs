using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.UserControl
{
    public partial class FiltroMesOperador : System.Web.UI.UserControl
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
        public string Operador
        {
            get
            {
                return ddlOperador.SelectedValue;
            }
            set
            {
                ddlOperador.SelectedValue = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CarregarFiltro(string login, int tipoOperador)
        {
            CarregarMeses();
            CarregarOperador(login, tipoOperador);

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

        private void CarregarOperador(string login, int tipoOperador)
        {
            if (tipoOperador >= (int)TiposInfo.TipoOperador.Lider)
            {
                phOperador.Visible = true;

                var listaOperador = OperadorBo.GetInstance.ConsultarOperadorGestor(login);

                ddlOperador.DataSource = listaOperador;
                ddlOperador.DataTextField = "Nome";
                ddlOperador.DataValueField = "LoginAd";
                ddlOperador.DataBind();
            }
            else
            {
                phOperador.Visible = false;
            }
        }

        protected void DdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }

        protected void DdlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }
    }
}