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
    public partial class FiltroGrupo : System.Web.UI.UserControl
    {
        public event EventHandler ConsultarFiltro;

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

        public void CarregarFiltro(List<string> listaGrupo)
        {            
            CarregarGrupos(listaGrupo);
        }

        private void EfetuarConsultaFiltro(EventArgs e)
        {   
            ConsultarFiltro?.Invoke(ConsultarFiltro, e);
        }        

        private void CarregarGrupos(List<string> listaGrupo)
        {            
            if (listaGrupo.Count() > 1)
            {
                phGrupo.Visible = true;

                ddlGrupo.DataSource = listaGrupo.OrderBy(p => p).ToList();
                ddlGrupo.DataBind();

                ddlGrupo.Items.Insert(0, new ListItem("Todos", "0"));
            }
            else
            {
                phGrupo.Visible = false;
            }
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