using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Atividade
{
    public partial class EventoProblema : BasePage
    {
        private enum ColEvento
        {            
            DataEvento = 0,
            Sistema,
            TipoEvento,
            TituloEvento,
            NroChamado
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {                
                ucFiltro.CarregarFiltro(UsuarioAD);
                CarregarCombos();
            }
        }
        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarEvento();
        }

        private void CarregarCombos()
        {
            var listaSistema = SistemaBo.GetInstance.ConsultarSistemaOperador(UsuarioAD);
            ddlSistema.DataSource = listaSistema;
            ddlSistema.DataTextField = "NomeSistema";
            ddlSistema.DataValueField = "CodSistema";
            ddlSistema.DataBind();
            ddlSistema.Items.Insert(0, new ListItem("Selecione", "0"));

            var listaTipoEvento = EventoProblemaBo.GetInstance.ConsultarTipoEvento();
            ddlTipoEvento.DataSource = listaTipoEvento;
            ddlTipoEvento.DataTextField = "DscTipoEvento";
            ddlTipoEvento.DataValueField = "CodTipoEvento";
            ddlTipoEvento.DataBind();
            ddlTipoEvento.Items.Insert(0, new ListItem("Selecione", "0"));
        }

        private void CarregarEvento()
        {
            phConsulta.Visible = true;
            phEdicao.Visible = false;

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaEvento = EventoProblemaBo.GetInstance.ConsultarEvento(ucFiltro.Gestor, dataConsulta, dataConsulta.AddMonths(1));

            gdvEvento.DataSource = listaEvento;
            gdvEvento.DataBind();
        }

        protected void GdvEvento_PreRender(object sender, EventArgs e)
        {
            if (gdvEvento.Rows.Count > 0)
            {
                gdvEvento.UseAccessibleHeader = true;
                gdvEvento.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvEvento.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvEvento_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                EventoProblemaInfo evento = (EventoProblemaInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColEvento.TipoEvento].Text = evento.DscTipoEvento;
                e.Row.Cells[(int)ColEvento.Sistema].Text = evento.NomeSistema;
                e.Row.Cells[(int)ColEvento.DataEvento].Text = evento.DataEvento.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColEvento.TituloEvento].Text = evento.TituloEvento;
                e.Row.Cells[(int)ColEvento.NroChamado].Text = evento.NroChamado;
                ((LinkButton)e.Row.FindControl("btnEditar")).CommandArgument = evento.CodEvento.ToString();
            }
        }

        protected void EditarEvento(object sender, CommandEventArgs e)
        {
            phConsulta.Visible = false;
            phEdicao.Visible = true;

            CarregarDadosEvento(Convert.ToInt32(e.CommandArgument));
        }

        private void CarregarDadosEvento(int codEvento)
        {
            var evento = EventoProblemaBo.GetInstance.ObterEvento(codEvento);

            if (evento.CodEvento > 0)
            {
                ddlSistema.SelectedValue = evento.CodSistema.ToString();
                ddlTipoEvento.SelectedValue = evento.CodTipoEvento.ToString();
                txtDataEvento.Text = evento.DataEvento.ToString("dd/MM/yyyy");
                txtEvento.Text = evento.TituloEvento;
                txtDescricao.Text = evento.DscEvento;
                txtCorrecao.Text = evento.Correcao;

                txtNroChamado.Text = evento.NroChamado;
                txtNroChamadoCorrecao.Text = evento.NroCorrecao;

                lblDscChamado.Text = ChamadoBo.GetInstance.ObterChamado(evento.NroChamado).DscChamado;
                lblDscChamadoCorrecao.Text = ChamadoBo.GetInstance.ObterChamado(evento.NroCorrecao).DscChamado;

                CurrentSession.Objects = evento;
            }
        }

        protected void BtnIncluir_Click(object sender, EventArgs e)
        {
            phConsulta.Visible = false;
            phEdicao.Visible = true;

            ddlSistema.SelectedValue = "0";
            ddlTipoEvento.SelectedValue = "0";
            txtDataEvento.Text = "";
            txtEvento.Text = "";
            txtDescricao.Text = "";
            txtCorrecao.Text = "";

            txtNroChamado.Text = "";
            txtNroChamadoCorrecao.Text = "";

            lblDscChamado.Text = "";
            lblDscChamadoCorrecao.Text = "";

            CurrentSession.Objects = new EventoProblemaInfo();
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            CarregarEvento();
        }

        protected void BtnAlterar_Click(object sender, EventArgs e)
        {
            if (!ValidarCadastro())
                return;

            AlterarEvento();
        }

        private bool ValidarCadastro()
        {
            if (ddlSistema.SelectedValue == "0")
            {
                RetornarMensagemAviso("Selecione o sistema!");
                return false;
            }
            if (ddlTipoEvento.SelectedValue == "0")
            {
                RetornarMensagemAviso("Selecione o tipo do evento!");
                return false;
            }            
            if (string.IsNullOrEmpty(txtDataEvento.Text))
            {
                RetornarMensagemAviso("Digite a data do evento!");
                return false;
            }
            DateTime.TryParse(txtDataEvento.Text, out DateTime dataEvento);
            if (dataEvento == DateTime.MinValue)
            {
                RetornarMensagemAviso("Digite a data do evento!");
                return false;
            }
            if (string.IsNullOrEmpty(txtEvento.Text))
            {
                RetornarMensagemAviso("Digite um título para o evento!");
                return false;
            }
            if (string.IsNullOrEmpty(txtDescricao.Text))
            {
                RetornarMensagemAviso("Digite uma descrição do evento!");
                return false;
            }
            if (string.IsNullOrEmpty(txtCorrecao.Text))
            {
                RetornarMensagemAviso("Digite como foi feito a correção do evento.");
                return false;
            }

            return true;
        }

        private void AlterarEvento()
        {
            if (CurrentSession.Objects is EventoProblemaInfo evento)
            {
                evento.LoginAd = UsuarioAD;
                evento.TituloEvento = txtEvento.Text.Trim();
                evento.DscEvento = txtDescricao.Text.Trim();
                evento.Correcao = txtCorrecao.Text.Trim();
                evento.NroChamado = txtNroChamado.Text.Trim();
                evento.NroCorrecao = txtNroChamadoCorrecao.Text.Trim();

                evento.CodTipoEvento = Convert.ToInt32(ddlTipoEvento.SelectedValue);
                evento.CodSistema = Convert.ToInt32(ddlSistema.SelectedValue);
                evento.DataEvento = Convert.ToDateTime(txtDataEvento.Text.Trim());

                if (evento.CodEvento > 0)
                {
                    EventoProblemaBo.GetInstance.AlterarEvento(evento);
                }
                else
                {
                    EventoProblemaBo.GetInstance.IncluirEvento(evento);
                }

                CarregarEvento();
            }
        }
    }
}