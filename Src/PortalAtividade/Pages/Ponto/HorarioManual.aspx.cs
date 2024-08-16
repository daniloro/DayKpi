using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Business;
using PortalAtividade.Model;

namespace PortalAtividade.Pages.Ponto
{
    public partial class HorarioManual : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarPonto();
            }
        }

        private void CarregarPonto()
        {
            txtHoraInicio.Visible = false;
            txtHoraAlmoco.Visible = false;
            txtHoraRetorno.Visible = false;
            txtHoraSaida.Visible = false;

            var ponto = PontoBo.GetInstance.ObterPonto(UsuarioAD);

            if (VerificarHorario(ponto.HoraInicio, txtHoraInicio, lblHoraInicio))
            {
                if (VerificarHorario(ponto.HoraAlmoco, txtHoraAlmoco, lblHoraAlmoco))
                {
                    if (VerificarHorario(ponto.HoraRetorno, txtHoraRetorno, lblHoraRetorno))
                    {
                        if (VerificarHorario(ponto.HoraSaida, txtHoraSaida, lblHoraSaida))
                        {
                            btnSalvar.Visible = false;
                        }
                    }
                }
            }
        }

        private bool VerificarHorario(DateTime horario, TextBox edicao, Label descricao)
        {
            bool retorno = false;

            if (horario != DateTime.MinValue)
            {
                descricao.Visible = true;
                descricao.Text = horario.ToString("HH:mm") + "h";
                retorno = true;                
            }
            else
            {
                descricao.Visible = false;
                edicao.Visible = true;
                edicao.Focus();
            }
            return retorno;
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            lblMensagem.Text = "";

            var ponto = PontoBo.GetInstance.ObterPonto(UsuarioAD);

            if (ponto.HoraInicio == DateTime.MinValue)
            {
                ponto.TipoPonto = 1;
                ponto.HoraInicio = VerificarHoraManual(ponto, txtHoraInicio.Text);

                if (ponto.HoraInicio != DateTime.MinValue)
                {                    
                    SalvarPonto(ponto);
                }
            }
            else if (ponto.HoraAlmoco == DateTime.MinValue)
            {
                ponto.TipoPonto = 2;
                ponto.HoraAlmoco = VerificarHoraManual(ponto, txtHoraAlmoco.Text);

                if (ponto.HoraAlmoco != DateTime.MinValue)
                {                    
                    SalvarPonto(ponto);
                }
            }
            else if (ponto.HoraRetorno == DateTime.MinValue)
            {
                ponto.TipoPonto = 3;
                ponto.HoraRetorno = VerificarHoraManual(ponto, txtHoraRetorno.Text);

                if (ponto.HoraRetorno != DateTime.MinValue)
                {                    
                    SalvarPonto(ponto);
                }
            }
            else if (ponto.HoraSaida == DateTime.MinValue)
            {
                ponto.TipoPonto = 4;
                ponto.HoraSaida = VerificarHoraManual(ponto, txtHoraSaida.Text);

                if (ponto.HoraSaida != DateTime.MinValue)
                {                    
                    SalvarPonto(ponto);
                }
            }
        }

        private void SalvarPonto(PontoInfo ponto)
        {
            ponto.LoginAd = UsuarioAD;

            if (ponto.TipoPonto == 1)
            {                
                ponto.DataPonto = DateTime.Today;
                PontoBo.GetInstance.IncluirPonto(ponto);                
            }
            else
            {
                PontoBo.GetInstance.AlterarPonto(ponto);                
            }

            ponto.HoraManual = DateTime.Now;
            PontoBo.GetInstance.IncluirPontoManual(ponto);

            CarregarPonto();
        }

        private DateTime VerificarHoraManual(PontoInfo ponto, string horario)
        {
            if (string.IsNullOrEmpty(horario))
            {
                lblMensagem.Text = "Digite o horário!";
                return DateTime.MinValue;
            }

            try
            {
                ponto.HoraManual = DateTime.ParseExact(horario, "HH:mm", CultureInfo.InvariantCulture);
            }
            catch
            {                
                lblMensagem.Text = "Horário inválido!";
                return DateTime.MinValue;
            }

            if ((ponto.TipoPonto == 2 && ponto.HoraManual < ponto.HoraInicio) || (ponto.TipoPonto == 3 && ponto.HoraManual < ponto.HoraAlmoco) || (ponto.TipoPonto == 4 && ponto.HoraManual < ponto.HoraRetorno))
            {
                lblMensagem.Text = "O horário não pode ser menor que o horário anterior!";
                ponto.HoraManual = DateTime.MinValue;
            }            

            return ponto.HoraManual;
        }        
    }
}