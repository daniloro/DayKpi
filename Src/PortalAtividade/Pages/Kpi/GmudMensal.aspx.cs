using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Kpi
{
    public partial class GmudMensal : BasePage
    {
        private enum ColGmud
        {
            NroGmud = 0,
            Sistema,
            Versao,
            Descricao,
            Data,
            QtdChamado
        }

        private enum ColGmudDetalhe
        {
            NroChamado = 0,
            DscChamado
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                mwGmud.SetActiveView(vwLista);
                
                CarregarPerguntaKPI();
                ucFiltro.CarregarFiltro(UsuarioAD);                              
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarGmud();
            CarregarAnalise();
        }        

        private void CarregarGmud()
        {
            DateTime dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaGmud = GmudBo.GetInstance.ConsultarGmudPeriodo(ucFiltro.Gestor, dataConsulta, dataConsulta.AddMonths(1));

            gdvGmud.DataSource = listaGmud;
            gdvGmud.DataBind();

            ltlTotal.Text = listaGmud.Count().ToString();
            ltlTotalChamado.Text = listaGmud.Sum(p => p.QtdChamado).ToString();

            ltlGmudProgramada.Text = listaGmud.Count(p => !p.Versao.ToUpper().Contains("EMERGENCIAL") && (!p.Versao.Contains(".") || p.Versao.Substring(p.Versao.LastIndexOf(".") + 1, 1) == "0")).ToString();
            ltlGmudEmergencial.Text = listaGmud.Count(p => p.Versao.ToUpper().Contains("EMERGENCIAL") || (p.Versao.Contains(".") && p.Versao.Substring(p.Versao.LastIndexOf(".") + 1, 1) != "0")).ToString();

            CurrentSession.Objects = listaGmud;
        }

        protected void GdvGmud_PreRender(object sender, EventArgs e)
        {
            if (gdvGmud.Rows.Count > 0)
            {
                gdvGmud.UseAccessibleHeader = true;
                gdvGmud.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvGmud.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvGmud_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GmudInfo gmud = (GmudInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColGmud.NroGmud].Text = gmud.NroGmud;
                e.Row.Cells[(int)ColGmud.Sistema].Text = gmud.NomeSistema;
                e.Row.Cells[(int)ColGmud.Versao].Text = gmud.Versao;
                e.Row.Cells[(int)ColGmud.Descricao].Text = gmud.DscGmud;
                e.Row.Cells[(int)ColGmud.Data].Text = gmud.DataGmud.ToString("dd/MM/yyyy");
                e.Row.Cells[(int)ColGmud.QtdChamado].Text = gmud.QtdChamado.ToString();
                ((LinkButton)e.Row.FindControl("btnDetalhe")).CommandArgument = gmud.NroGmud;
            }
        }

        protected void ObterGmud(object sender, CommandEventArgs e)
        {
            var nroGmud = e.CommandArgument.ToString();

            if (CurrentSession.Objects is List<GmudInfo> listaGmud)
            {
                CarregarChamadosGmud(listaGmud.Find(p => p.NroGmud == nroGmud));
            }
        }

        private void CarregarChamadosGmud(GmudInfo gmud)
        {
            var listaGmud = GmudBo.GetInstance.ConsultarChamadoGmud(ucFiltro.Gestor, gmud.VersionId);

            mwGmud.SetActiveView(vwDetalhe);
            ltlTitulo.Text = "Dados da GMUD";

            ltlNroGmud.Text = gmud.NroGmud;
            ltlDscGmud.Text = gmud.DscGmud;
            ltlDataGmud.Text = gmud.DataGmud.ToString("dd/MM/yyyy");

            gdvGmudDetalhe.DataSource = listaGmud;
            gdvGmudDetalhe.DataBind();
        }

        protected void GdvGmudDetalhe_PreRender(object sender, EventArgs e)
        {
            if (gdvGmudDetalhe.Rows.Count > 0)
            {
                gdvGmudDetalhe.UseAccessibleHeader = true;
                gdvGmudDetalhe.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvGmudDetalhe.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvGmudDetalhe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GmudInfo gmud = (GmudInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColGmudDetalhe.NroChamado].Text = gmud.NroChamado;
                e.Row.Cells[(int)ColGmudDetalhe.DscChamado].Text = gmud.DscChamado;

                if (!gmud.ChamadoEquipe)                
                    e.Row.ForeColor = System.Drawing.Color.LightGray;                
            }
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            mwGmud.SetActiveView(vwLista);
            CarregarGmud();
            ltlTitulo.Text = "GMUD Período";
        }

        private void CarregarPerguntaKPI()
        {
            ltlPergunta.Text = AnaliseKPIBo.GetInstance.ObterPergunta((int)TiposInfo.PerguntaKPI.Gmud).DscPergunta;
        }

        private void CarregarAnalise()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);

            var analise = AnaliseKPIBo.GetInstance.ObterAnaliseGestor(dataConsulta, ucFiltro.Gestor, (int)TiposInfo.PerguntaKPI.Gmud);
            txtAnalise.Text = analise.DscAnalise;
            
            if (ucFiltro.Gestor == UsuarioAD)            
                PermitirAlteracao(!analise.Concluido);            
            else
                PermitirAlteracao(false);
        }

        private void PermitirAlteracao(bool habilita)
        {
            txtAnalise.Enabled = habilita;            
            btnSalvar.Visible = habilita;
            btnSalvarEnviar.Visible = habilita;
        }        

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            SalvarAnalise(false);
        }

        protected void BtnSalvarEnviar_Click(object sender, EventArgs e)
        {
            SalvarAnalise(true);
        }

        private void SalvarAnalise(bool concluido)
        {
            if (ValidarAnalise())
            {
                AnaliseKPIInfo analise = new AnaliseKPIInfo
                {
                    DataAnalise = Convert.ToDateTime(ucFiltro.Mes),
                    LoginAd = UsuarioAD,
                    Concluido = concluido
                };

                AnaliseKPIBo.GetInstance.SalvarAnalise((int)TiposInfo.PerguntaKPI.Gmud, txtAnalise.Text, analise);
                CarregarAnalise();
            }            
        }

        private bool ValidarAnalise()
        {
            if (string.IsNullOrEmpty(txtAnalise.Text))
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }
            return true;
        }        
    }
}