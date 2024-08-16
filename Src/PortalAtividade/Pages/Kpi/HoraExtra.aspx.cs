using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;


namespace PortalAtividade.Pages.Kpi
{
    public partial class HoraExtra : BasePage
    {
        private enum ColPonto
        {
            Nome = 0,
            Atraso,
            CinquentaPorcento,
            CemPorcento,
            Noturno
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ucFiltro.CarregarFiltro(UsuarioAD);                            
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarHoraExtra();
        }       

        private void CarregarHoraExtra()
        {
            phConsulta.Visible = true;
            phEdicao.Visible = false;

            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaPonto = PontoBo.GetInstance.ConsultarPontoMensal(ucFiltro.Gestor, dataConsulta);

            gdvPonto.DataSource = listaPonto;
            gdvPonto.DataBind();

            ltlTotal.Text = UtilBo.GetInstance.ObterTempoTotal(listaPonto.Sum(p => p.CinquentaPorcento + p.CemPorcento + p.Noturno));
            ltlTotal50.Text = UtilBo.GetInstance.ObterTempoTotal(listaPonto.Sum(p => p.CinquentaPorcento));
            ltlTotal100.Text = UtilBo.GetInstance.ObterTempoTotal(listaPonto.Sum(p => p.CemPorcento));
            ltlTotalNoturno.Text = UtilBo.GetInstance.ObterTempoTotal(listaPonto.Sum(p => p.Noturno));

            CurrentSession.Objects = listaPonto;
        }

        protected void GdvPonto_PreRender(object sender, EventArgs e)
        {
            if (gdvPonto.Rows.Count > 0)
            {
                gdvPonto.UseAccessibleHeader = true;
                gdvPonto.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvPonto.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvPonto_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PontoMensalInfo ponto = (PontoMensalInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColPonto.Nome].Text = ponto.Nome;
                e.Row.Cells[(int)ColPonto.Atraso].Text = UtilBo.GetInstance.ObterTempoTotal(ponto.Atraso);
                e.Row.Cells[(int)ColPonto.CinquentaPorcento].Text = UtilBo.GetInstance.ObterTempoTotal(ponto.CinquentaPorcento);
                e.Row.Cells[(int)ColPonto.CemPorcento].Text = UtilBo.GetInstance.ObterTempoTotal(ponto.CemPorcento);
                e.Row.Cells[(int)ColPonto.Noturno].Text = UtilBo.GetInstance.ObterTempoTotal(ponto.Noturno);
                ((LinkButton)e.Row.FindControl("btnEditar")).CommandArgument = ponto.LoginAd;
            }
        }

        protected void EditarPonto(object sender, CommandEventArgs e)
        {
            phConsulta.Visible = false;
            phEdicao.Visible = true;

            CarregarDadosPonto(e.CommandArgument.ToString());
        }

        private void CarregarDadosPonto(string loginAd)
        {
            if (CurrentSession.Objects is List<PontoMensalInfo> listaPonto)
            {
                var ponto = listaPonto.Find(p => p.LoginAd == loginAd);

                ltlMes.Text = UtilBo.GetInstance.ObterNomeMes(Convert.ToDateTime(ucFiltro.Mes).Month);
                ltlNome.Text = ponto.Nome;

                txtAtraso.Text = UtilBo.GetInstance.ObterTempoTotal(ponto.Atraso);
                txtCinquentaPorcento.Text = UtilBo.GetInstance.ObterTempoTotal(ponto.CinquentaPorcento);
                txtCemPorcento.Text = UtilBo.GetInstance.ObterTempoTotal(ponto.CemPorcento);
                txtNoturno.Text = UtilBo.GetInstance.ObterTempoTotal(ponto.Noturno);
                txtObservacao.Text = ponto.Observacao;

                CurrentSession.Objects = ponto;
            }
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            CarregarHoraExtra();
        }

        protected void BtnIncluirPonto_Click(object sender, EventArgs e)
        {
            if (!ValidarCadastro())
                return;

            IncluirPonto();
        }

        private bool ValidarCadastro()
        {
            if (string.IsNullOrEmpty(txtAtraso.Text) || !ValidarHorario(txtAtraso.Text))
            {
                RetornarMensagemAviso("Atraso inválido!");
                return false;
            }
            if (string.IsNullOrEmpty(txtCinquentaPorcento.Text) || !ValidarHorario(txtCinquentaPorcento.Text))
            {
                RetornarMensagemAviso("50% inválido!");
                return false;
            }
            if (string.IsNullOrEmpty(txtCemPorcento.Text) || !ValidarHorario(txtCemPorcento.Text))
            {
                RetornarMensagemAviso("100% inválido!");
                return false;
            }
            if (string.IsNullOrEmpty(txtNoturno.Text) || !ValidarHorario(txtNoturno.Text))
            {
                RetornarMensagemAviso("Adicional Noturno inválido!");
                return false;
            }

            return true;
        }

        private bool ValidarHorario(string horario)
        {
            var tempo = horario.Split(':');
            
            if (tempo.Length > 2 || tempo.Length == 0)
                return false;

            foreach (var t in tempo)
            {
                if (t == null)
                    return false;
                else if (!int.TryParse(t, out _))
                    return false;
            }

            return true;
        }
        
        public int ObterTotalMinutos(string horario)
        {
            var tempo = horario.Split(':');
            
            var retorno = Convert.ToInt32(tempo[0]) * 60;

            if (tempo.Length > 1)
                retorno += Convert.ToInt32(tempo[1]);
            
            return retorno;
        }

        private void IncluirPonto()
        {
            if (CurrentSession.Objects is PontoMensalInfo ponto)
            {
                ponto.LoginGestor = UsuarioAD;
                ponto.Atraso = ObterTotalMinutos(txtAtraso.Text);
                ponto.CinquentaPorcento = ObterTotalMinutos(txtCinquentaPorcento.Text);
                ponto.CemPorcento = ObterTotalMinutos(txtCemPorcento.Text);
                ponto.Noturno = ObterTotalMinutos(txtNoturno.Text);
                ponto.Observacao = txtObservacao.Text;

                if (ponto.Atraso + ponto.CinquentaPorcento + ponto.CemPorcento + ponto.Noturno == 0)
                {
                    if (string.IsNullOrEmpty(ponto.Observacao))
                    {
                        RetornarMensagemAviso("Adicione uma observação.");
                        return;
                    }
                }

                if (ponto.CinquentaPorcento + ponto.CemPorcento + ponto.Noturno > 1200) //20 horas
                {
                    if (string.IsNullOrEmpty(ponto.Observacao))
                    {
                        RetornarMensagemAviso("Justifique as horas extras no campo observação.");
                        return;
                    }
                }

                if (ponto.CodPontoMensal > 0)
                {
                    PontoBo.GetInstance.AlterarPontoMensal(ponto);
                }
                else
                {
                    ponto.DataPonto = Convert.ToDateTime(ucFiltro.Mes);                    
                    PontoBo.GetInstance.IncluirPontoMensal(ponto);
                }

                CarregarHoraExtra();
            }
        }
    }
}