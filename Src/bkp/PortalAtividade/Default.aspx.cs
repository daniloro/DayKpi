using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade
{
    public partial class _Default : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarAtividade();
                CarregarPonto();
            }
        }

        private void CarregarAtividade()
        {
            var login = Context.User.Identity.Name;            

            var atividade = AtividadeBo.GetInstance.ObterAtividadeAtual(login);

            if (!string.IsNullOrEmpty(atividade.NroAtividade))
            {
                ltlNroChamado.Text = atividade.NroChamado;
                ltlDscChamado.Text = atividade.DscChamado;                
                ltlNroAtividade.Text = atividade.NroAtividade;
                ltlDscAtividade.Text = atividade.DscAtividade;
                lblDataFinal.Text = atividade.DataFinal.ToString("dd/MM/yyyy");

                if (atividade.DataFinal.Date < DateTime.Today)
                {
                    lblDataFinal.ForeColor = Color.Red;                    
                    lblStatusAtividade.ForeColor = Color.Red;
                    lblStatusAtividade.Text = "Vencido";
                }                
            }
            else
            {
                lblStatusAtividade.Text = "Atenção";
                lblStatusAtividade.ForeColor = Color.Red;
                ltlMsgAtividade.Text = "Nenhuma atividade em andamento";                
                phAtividadeAtual.Visible = false;
            }            
        }

        #region Ponto      

        private void CarregarPonto()
        {
            CurrentSession.Objects = null;

            var ponto = PontoBo.GetInstance.ObterPonto(Context.User.Identity.Name);
            if (ponto.CodPonto > 0)
            {
                CurrentSession.Objects = ponto;

                lbHoraInicio.Text = "Início: " + ponto.HoraInicio.ToString("HH:mm") + "h";
                lbHoraAlmoco.Text = ponto.HoraAlmoco == DateTime.MinValue ? "Almoço &raquo;" : "Almoço: " + ponto.HoraAlmoco.ToString("HH:mm") + "h";
                lbHoraRetorno.Text = ponto.HoraRetorno == DateTime.MinValue ? "Retorno &raquo;" : "Retorno: " + ponto.HoraRetorno.ToString("HH:mm") + "h";
                lbHoraSaida.Text = ponto.HoraSaida == DateTime.MinValue ? "Saída &raquo;" : "Saída: " + ponto.HoraSaida.ToString("HH:mm") + "h";
                
                AlterarBotaoPonto(lbHoraInicio);

                if (ponto.HoraAlmoco == DateTime.MinValue)
                {
                    lbHoraAlmoco.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Almoço?', " + lbHoraAlmoco.ClientID + "); return false;";
                    lbHoraRetorno.OnClientClick = "return false;";
                    lbHoraSaida.OnClientClick = "return false;";
                }
                else
                {
                    AlterarBotaoPonto(lbHoraAlmoco);

                    if (ponto.HoraRetorno == DateTime.MinValue)
                    {
                        lbHoraRetorno.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Retorno do Almoço?', " + lbHoraRetorno.ClientID + "); return false;";                        
                        lbHoraSaida.OnClientClick = "return false;";
                    }
                    else
                    {
                        AlterarBotaoPonto(lbHoraRetorno);

                        if (ponto.HoraSaida == DateTime.MinValue)
                        {
                            lbHoraSaida.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Saída?', " + lbHoraSaida.ClientID + "); return false;";                            
                        }
                        else
                        {
                            AlterarBotaoPonto(lbHoraSaida);
                        }
                    }
                }
            }
            else
            {
                lbHoraInicio.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Deseja realmente incluir o horário de Entrada?', " + lbHoraInicio.ClientID + "); return false;";
                lbHoraAlmoco.OnClientClick = "return false;";
                lbHoraRetorno.OnClientClick = "return false;";
                lbHoraSaida.OnClientClick = "return false;";
            }
        }

        private void AlterarBotaoPonto(LinkButton lb)
        {
            lb.CssClass = "btn btn-outline-secondary btn-lg w-100";
            lb.Enabled = false;
            lb.OnClientClick = "return false;";
        }

        protected void LbHoraInicio_Click(object sender, EventArgs e)
        {
            if (!(CurrentSession.Objects is PontoInfo))
            {
                PontoInfo ponto = new PontoInfo
                {
                    DataPonto = DateTime.Today,
                    HoraInicio = DateTime.Now,
                    LoginAd = Context.User.Identity.Name
                };

                PontoBo.GetInstance.IncluirPonto(ponto);
                CarregarPonto();
            }
        }

        protected void LbHoraAlmoco_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is PontoInfo ponto)
            {
                if (ponto.HoraAlmoco == DateTime.MinValue)
                {
                    ponto.HoraAlmoco = DateTime.Now;

                    PontoBo.GetInstance.AlterarPonto(ponto);
                    CarregarPonto();
                }                
            }
        }

        protected void LbHoraRetorno_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is PontoInfo ponto)
            {
                if (ponto.HoraRetorno == DateTime.MinValue)
                {
                    ponto.HoraRetorno = DateTime.Now;

                    PontoBo.GetInstance.AlterarPonto(ponto);
                    CarregarPonto();
                }
            }
        }

        protected void LbHoraSaida_Click(object sender, EventArgs e)
        {
            if (CurrentSession.Objects is PontoInfo ponto)
            {
                if (ponto.HoraSaida == DateTime.MinValue)
                {
                    ponto.HoraSaida = DateTime.Now;

                    PontoBo.GetInstance.AlterarPonto(ponto);
                    CarregarPonto();
                }
            }
        }

        #endregion
    }
}