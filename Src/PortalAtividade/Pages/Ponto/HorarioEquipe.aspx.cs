using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Globalization;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Ponto
{
    public partial class HorarioEquipe : BasePage
    {
        private string mensagemErro = "";

        private enum ColPonto
        {
            Nome = 0,
            HEMensal,
            HEUltimoDia,
            Manual,
            Pendente
        }

        private enum ColPontoDetalhe
        {
            DataPonto = 0,
            DiaSemana,
            HoraInicio,
            HoraAlmoco,
            HoraRetorno,
            HoraSaida
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ucFiltro.CarregarFiltro(UsuarioAD, true, true);                
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarHoraEquipe();
        }

        private void CarregarHoraEquipe()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaPonto = RelatorioBo.GetInstance.ConsultarPontoMensal(UsuarioAD, dataConsulta, dataConsulta.AddMonths(1), ucFiltro.Grupo);

            gdvPonto.DataSource = listaPonto;
            gdvPonto.DataBind();            
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
                RelPontoMensalInfo ponto = (RelPontoMensalInfo)e.Row.DataItem;                

                e.Row.Cells[(int)ColPonto.Nome].Text = ponto.Nome;
                e.Row.Cells[(int)ColPonto.HEMensal].Text = UtilBo.GetInstance.ObterTempoTotal(ponto.HEMensal);
                e.Row.Cells[(int)ColPonto.HEUltimoDia].Text = UtilBo.GetInstance.ObterTempoTotal(ponto.HEUltimoDia);                
                e.Row.Cells[(int)ColPonto.Manual].Text = ponto.PontoManual.ToString();
                e.Row.Cells[(int)ColPonto.Pendente].Text = (ponto.PontoPendente + ponto.PontoIncompleto).ToString();
                ((LinkButton)e.Row.FindControl("btnDetalhe")).CommandArgument = ponto.LoginAd;

                if (ponto.HEMensal >= 1800) // 30 horas
                {
                    e.Row.Cells[(int)ColPonto.HEMensal].ForeColor = Color.Red;
                }
                else if (ponto.HEMensal < 0)
                {
                    e.Row.Cells[(int)ColPonto.HEMensal].ForeColor = Color.Red;
                }
                if (ponto.HEUltimoDia < 0)
                {
                    e.Row.Cells[(int)ColPonto.HEUltimoDia].ForeColor = Color.Red;
                }
            }            
        }

        protected void GdvPonto_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WebControl wc = (WebControl)e.CommandSource;
            GridViewRow row = (GridViewRow)wc.NamingContainer;

            var loginAd = ((LinkButton)row.FindControl("btnDetalhe")).CommandArgument;
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var listaPonto = PontoBo.GetInstance.ConsultarPontoMensalOperador(loginAd, dataConsulta, dataConsulta.AddMonths(1));
                        
            CurrentSession.Objects = listaPonto;            

            phDetalhe.Visible = true;
            phLista.Visible = false;

            ltlNome.Text = row.Cells[(int)ColPonto.Nome].Text;
            ltlPeriodo.Text = dataConsulta.ToString("dd/MM/yyyy") + " à " + dataConsulta.AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");

            lbSalvar.Visible = false;
            gdvPontoDetalhe.DataSource = listaPonto;
            gdvPontoDetalhe.DataBind();
        }        

        protected void GdvPontoDetalhe_PreRender(object sender, EventArgs e)
        {
            if (gdvPontoDetalhe.Rows.Count > 0)
            {
                gdvPontoDetalhe.UseAccessibleHeader = true;
                gdvPontoDetalhe.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvPontoDetalhe.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvPontoDetalhe_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                PontoInfo ponto = (PontoInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColPontoDetalhe.DataPonto].Text = ponto.DataPonto.ToString("dd/MM");
                e.Row.Cells[(int)ColPontoDetalhe.DiaSemana].Text = UtilBo.GetInstance.ObterNomeSemana((int)ponto.DataPonto.DayOfWeek);

                if (ponto.HoraInicio != DateTime.MinValue)
                {
                    e.Row.Cells[(int)ColPontoDetalhe.HoraInicio].Text = ponto.HoraInicio.ToString("HH:mm");
                    e.Row.Cells[(int)ColPontoDetalhe.HoraInicio].Font.Bold = ponto.HoraInicioManual;
                }
                if (ponto.HoraAlmoco != DateTime.MinValue)
                {
                    e.Row.Cells[(int)ColPontoDetalhe.HoraAlmoco].Text = ponto.HoraAlmoco.ToString("HH:mm");
                    e.Row.Cells[(int)ColPontoDetalhe.HoraAlmoco].Font.Bold = ponto.HoraAlmocoManual;
                }
                if (ponto.HoraRetorno != DateTime.MinValue)
                {
                    e.Row.Cells[(int)ColPontoDetalhe.HoraRetorno].Text = ponto.HoraRetorno.ToString("HH:mm");
                    e.Row.Cells[(int)ColPontoDetalhe.HoraRetorno].Font.Bold = ponto.HoraRetornoManual;
                }
                if (ponto.HoraSaida != DateTime.MinValue)
                {
                    e.Row.Cells[(int)ColPontoDetalhe.HoraSaida].Text = ponto.HoraSaida.ToString("HH:mm");
                    e.Row.Cells[(int)ColPontoDetalhe.HoraSaida].Font.Bold = ponto.HoraSaidaManual;
                }
                else
                    lbSalvar.Visible = true;

                if (ponto.DataPonto.DayOfWeek == DayOfWeek.Saturday || ponto.DataPonto.DayOfWeek == DayOfWeek.Sunday)
                {
                    e.Row.Cells[(int)ColPontoDetalhe.DiaSemana].ForeColor = Color.Red;
                }

                e.Row.Attributes.Add("CodPonto", ponto.CodPonto.ToString());
            }
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is List<PontoInfo> listaPonto)
            {
                foreach (GridViewRow row in gdvPontoDetalhe.Rows)
                {
                    var ponto = listaPonto.Find(p => p.CodPonto == Convert.ToInt32(row.Attributes["CodPonto"]));

                    if (ponto.CodPonto == 0)
                    {
                        ponto.HoraInicio = VerificarHorario(((TextBox)row.FindControl("txtInicio")).Text, ponto.DataPonto, DateTime.MinValue);
                        ponto.HoraManual = ponto.HoraInicio;
                        IncluirPonto(ponto, 1);
                    }
                    if (ponto.HoraInicio != DateTime.MinValue && ponto.HoraAlmoco == DateTime.MinValue)
                    {
                        ponto.HoraAlmoco = VerificarHorario(((TextBox)row.FindControl("txtAlmoco")).Text, ponto.DataPonto, ponto.HoraInicio);
                        ponto.HoraManual = ponto.HoraAlmoco;
                        IncluirPonto(ponto, 2);                        
                    }
                    if (ponto.HoraAlmoco != DateTime.MinValue && ponto.HoraRetorno == DateTime.MinValue)
                    {
                        ponto.HoraRetorno = VerificarHorario(((TextBox)row.FindControl("txtRetorno")).Text, ponto.DataPonto, ponto.HoraAlmoco);
                        ponto.HoraManual = ponto.HoraRetorno;
                        IncluirPonto(ponto, 3);                        
                    }
                    if (ponto.HoraRetorno != DateTime.MinValue && ponto.HoraSaida == DateTime.MinValue)
                    {
                        ponto.HoraSaida = VerificarHorario(((TextBox)row.FindControl("txtSaida")).Text, ponto.DataPonto, ponto.HoraRetorno);
                        ponto.HoraManual = ponto.HoraSaida;
                        IncluirPonto(ponto, 4);
                    }
                }

                lbSalvar.Visible = false;
                gdvPontoDetalhe.DataSource = listaPonto;
                gdvPontoDetalhe.DataBind();

                if (!string.IsNullOrEmpty(mensagemErro))
                {
                    RetornarMensagemAviso(mensagemErro);                    
                }                
            }
        }

        private DateTime VerificarHorario(string horario, DateTime dataPonto, DateTime horarioAnterior)
        {
            if (string.IsNullOrEmpty(horario))
            {                
                return DateTime.MinValue;
            }

            DateTime horaManual;

            try
            {
                horario = dataPonto.ToString("yyyy/MM/dd ") + horario;
                horaManual = DateTime.Parse(horario, CultureInfo.InvariantCulture);
            }
            catch
            {
                mensagemErro += "Horário inválido!</br>";
                return DateTime.MinValue;
            }

            if (horaManual < horarioAnterior)
            {
                mensagemErro += "Horário deve ser maior que o horário anterior!</br>";
                return DateTime.MinValue;
            }

            return horaManual;
        }

        private void IncluirPonto(PontoInfo ponto, int tipoPonto)
        {
            if (ponto.HoraManual == DateTime.MinValue)
                return;

            if (tipoPonto == 1)
                PontoBo.GetInstance.IncluirPonto(ponto);
            else
                PontoBo.GetInstance.AlterarPonto(ponto);

            ponto.TipoPonto = tipoPonto;            
            PontoBo.GetInstance.IncluirPontoManual(ponto);
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            phDetalhe.Visible = false;
            phLista.Visible = true;
            CarregarHoraEquipe();
        }
    }
}