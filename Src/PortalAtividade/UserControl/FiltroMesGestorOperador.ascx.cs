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
    public partial class FiltroMesGestorOperador : System.Web.UI.UserControl
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
        public bool EsconderMes { get; set; }

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
            if (EsconderMes)
            {
                phMes.Visible = false;
                return;
            }

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

            CarregarOperador(ddlGestor.SelectedValue);
        }

        private void CarregarOperador(string login)
        {
            var listaOperador = OperadorBo.GetInstance.ConsultarOperadorGestor(login);

            ddlOperador.DataSource = listaOperador;
            ddlOperador.DataTextField = "Nome";
            ddlOperador.DataValueField = "LoginAd";
            ddlOperador.DataBind();
        }

        protected void DdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }

        protected void DdlGestor_SelectedIndexChanged(object sender, EventArgs e)
        {
            CarregarOperador(ddlGestor.SelectedValue);

            EfetuarConsultaFiltro(e);
        }

        protected void DdlOperador_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }
    }
}