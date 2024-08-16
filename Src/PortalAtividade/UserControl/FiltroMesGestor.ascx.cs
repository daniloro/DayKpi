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
    public partial class FiltroMesGestor : System.Web.UI.UserControl
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
        public string Gestor
        {
            get
            {
                return ddlGestor.SelectedValue;
            }
            set
            {
                ddlGestor.SelectedValue = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void CarregarFiltro(string login)
        {
            CarregarMeses();
            CarregarGestores(login);

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

        private void CarregarGestores(string login)
        {
            var listaEquipe = OperadorBo.GetInstance.ConsultarOperadorGestorLider(login);

            ddlGestor.DataSource = listaEquipe;
            ddlGestor.DataTextField = "Nome";
            ddlGestor.DataValueField = "LoginAd";
            ddlGestor.DataBind();

            ddlGestor.SelectedValue = login;
            phEquipe.Visible = listaEquipe.Count > 1;
        }

        protected void DdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }

        protected void DdlGestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }
    }
}