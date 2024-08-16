using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Atividade
{
    public partial class Atividade : BasePage
    {
        private enum ColAtividade
        {
            NroChamado = 0,
            DscChamado,
            NroAtividade,
            DscAtividade,
            Operador,
            QtdRepactuacao,            
            DataFinal
        }

        private enum ColHistorico
        {
            Data = 0,
            Tipo,
            Operador,
            DataFinal,
            Observacao
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAtividades();                
                CarregarGrupos();
            }
        }

        private void CarregarAtividades()
        {
            phLista.Visible = true;
            phAtividade.Visible = false;

            var listaAtividade = AtividadeBo.GetInstance.ConsultarAtividadePendente(UsuarioAD);            

            if (ucFiltro.Grupo != "" && ucFiltro.Grupo != "0")
            {
                listaAtividade.RemoveAll(p => p.Grupo != ucFiltro.Grupo);
            }
            CurrentSession.Objects = listaAtividade;

            ltlTotal.Text = listaAtividade.Count.ToString();
            ltlVencido.Text = listaAtividade.FindAll(p => p.DataFinal.Date < DateTime.Today).Count.ToString();
            
            gdvAtividade.DataSource = listaAtividade;
            gdvAtividade.DataBind();
        }

        private void CarregarGrupos()
        {
            if (CurrentSession.Objects is List<AtividadeInfo> listaAtividade)
            {
                ucFiltro.CarregarFiltro(listaAtividade.Select(p => p.Grupo).Distinct().ToList());                
            }            
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarAtividades();
        }        

        protected void GdvAtividade_PreRender(object sender, EventArgs e)
        {
            if (gdvAtividade.Rows.Count > 0)
            {
                gdvAtividade.UseAccessibleHeader = true;
                gdvAtividade.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvAtividade.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvAtividade_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                AtividadeInfo atividade = (AtividadeInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColAtividade.NroChamado].Text = atividade.NroChamado;
                e.Row.Cells[(int)ColAtividade.DscChamado].Text = atividade.DscChamado;
                e.Row.Cells[(int)ColAtividade.NroAtividade].Text = atividade.NroAtividade;
                e.Row.Cells[(int)ColAtividade.DscAtividade].Text = atividade.DscAtividade;
                e.Row.Cells[(int)ColAtividade.Operador].Text = atividade.Operador.Contains(" ") ? atividade.Operador.Substring(0, atividade.Operador.IndexOf(" ")) : "";
                e.Row.Cells[(int)ColAtividade.DataFinal].Text = atividade.DataFinal.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColAtividade.QtdRepactuacao].Text = atividade.QtdRepactuacao.ToString();
                ((LinkButton)e.Row.FindControl("btnDetalhe")).CommandArgument = atividade.NroAtividade;
            }
        }

        protected void ObterAtividade(object sender, CommandEventArgs e)
        {
            string nroAtividade = e.CommandArgument.ToString();

            if (CurrentSession.Objects is List<AtividadeInfo> listaAtividade)
            {
                var atividade = listaAtividade.Find(p => p.NroAtividade == nroAtividade);
                CurrentSession.Objects = atividade;

                phAtividade.Visible = true;
                phLista.Visible = false;

                ltlNroChamado.Text = atividade.NroChamado;
                ltlDscChamado.Text = atividade.DscChamado;
                ltlNroAtividade.Text = atividade.NroAtividade;
                ltlDscAtividade.Text = atividade.DscAtividade;
                ltlQtdRepactuacao.Text = atividade.QtdRepactuacao.ToString();
                ltlOperador.Text = atividade.Operador;
                lblDataFinal.Text = atividade.DataFinal.ToString("dd/MM/yyyy");

                ucHistorico.CarregarHistorico(atividade.NroAtividade);
            }
        }        

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {            
            CarregarAtividades();
        }
    }
}