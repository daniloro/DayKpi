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
    public partial class FiltroMesGrupo : System.Web.UI.UserControl
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
        public string Grupo
        {
            get
            {
                return ddlGrupo.SelectedValue;
            }
            set
            {
                ddlGrupo.SelectedValue = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
        }       

        public void CarregarFiltro(string login, bool principal, bool todos)
        {
            CarregarMeses();
            CarregarGrupos(login, principal, todos);

            EfetuarConsultaFiltro(new EventArgs());            
        }

        public void CarregarFiltro(string login)
        {
            CarregarFiltro(login, false, false);
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

        private string ObterMesReferencia(DateTime dataConsulta)
        {
            var retorno = UtilBo.GetInstance.ObterNomeMes(dataConsulta.AddMonths(1).Month);
            retorno += " - " + dataConsulta.ToString("dd/MM/yyyy") + " à " + dataConsulta.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");
            return retorno;
        }

        private void CarregarGrupos(string login, bool principal, bool todos)
        {
            List<OperadorInfo> listaGrupo;

            if (principal)
                listaGrupo = OperadorBo.GetInstance.ConsultarOperadorGestor(login); //Consulta os grupos principais que possuem operadores
            else
                listaGrupo = OperadorBo.GetInstance.ConsultarGruposOperador(login);

            if (listaGrupo.Any())
            {
                ddlGrupo.DataSource = listaGrupo.Select(p => p.Grupo).Distinct();
                ddlGrupo.DataBind();

                if (todos)
                    ddlGrupo.Items.Insert(0, new ListItem("Todos", "0"));
            }
            phGrupo.Visible = listaGrupo.Count > 1;
        }

        protected void DdlMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }

        protected void DdlGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            EfetuarConsultaFiltro(e);
        }        
    }
}