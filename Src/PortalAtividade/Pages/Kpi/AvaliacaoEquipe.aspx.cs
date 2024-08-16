using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Kpi
{
    public partial class AvaliacaoEquipe : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Usuario.TipoOperador < (int)TiposInfo.TipoOperador.Lider)
                    Response.Redirect("~/Default.aspx", false);                
                
                ucFiltro.CarregarFiltro(UsuarioAD, true, true);
            }
        }

        public void CarregarFiltro(object sender, EventArgs e)
        {
            CarregarResumoMensal();
        }

        private void CarregarResumoMensal()
        {
            var dataConsulta = Convert.ToDateTime(ucFiltro.Mes);
            var grupo = ucFiltro.Grupo;

            var listaOperador = OperadorBo.GetInstance.ConsultarOperadorGestor(UsuarioAD);
            var listaAtividade = AvaliacaoAtividadeBo.GetInstance.ConsultarAtividadePonderacaoMesEquipe(UsuarioAD, dataConsulta, dataConsulta.AddMonths(1));            

            if (grupo != "" && grupo != "0")
            {
                listaAtividade.RemoveAll(p => listaOperador.FindAll(q => q.Grupo != grupo).Select(r => r.LoginAd).Contains(p.LoginAd));                
                listaOperador.RemoveAll(p => p.Grupo != grupo);
            }

            var retorno = "[";

            if (listaOperador.Any())
            {
                retorno += "[{label: 'Nome', id: 'Nome', type: 'string'}, ";
                retorno += "{label: 'Atividades Concluídas', id: 'Atividade', type: 'number'}, ";
                retorno += "{label: 'Destaques nas Atividades', id: 'Destaque', type: 'number'}, ";
                retorno += "{label: 'Confirmação Planejada', id: 'Confirmacao', type: 'number'}, ";
                retorno += "{label: 'Repactuação sem Justificativa', id: 'Repactuacao', type: 'number'}, ";
                retorno += "{label: 'Avaliação de Atividade Pendente', id: 'AvaliacaoPendente', type: 'number'}, ";                
                retorno += "{label: 'Ponderação', id: 'Ponderacao', type: 'number'}], ";

                foreach (var operador in listaOperador)
                {
                    retorno += "['" + operador.Nome + "', ";
                    retorno += (listaAtividade.FindAll(p => p.LoginAd == operador.LoginAd)?.Count() ?? 0).ToString() + ", ";
                    retorno += (listaAtividade.FindAll(p => p.LoginAd == operador.LoginAd)?.Sum(p => p.NotaAvaliacao) ?? 0).ToString() + ", ";
                    retorno += (listaAtividade.FindAll(p => p.LoginAd == operador.LoginAd)?.Sum(p => p.QtdConfirmacao) ?? 0).ToString() + ", ";
                    retorno += (listaAtividade.FindAll(p => p.LoginAd == operador.LoginAd)?.Sum(p => p.QtdRepactuacaoSemJustificativa) ?? 0).ToString() + ", ";
                    retorno += (listaAtividade.FindAll(p => p.LoginAd == operador.LoginAd && p.CodAvaliacaoAuto > 0 && p.CodAvaliacao == 0)?.Count() ?? 0).ToString() + ", ";                    
                    retorno += (listaAtividade.FindAll(p => p.LoginAd == operador.LoginAd)?.Sum(p => p.PonderacaoTotal) ?? 0).ToString() + "], ";
                }

                retorno += "[{ v: 'Total', p: { className: 'TotalCell' } }, ";

                retorno += (listaAtividade?.Count() ?? 0).ToString() + ", ";
                retorno += (listaAtividade?.Sum(p => p.NotaAvaliacao) ?? 0).ToString() + ", ";                
                retorno += (listaAtividade?.Sum(p => p.QtdConfirmacao) ?? 0).ToString() + ", ";
                retorno += (listaAtividade?.Sum(p => p.QtdRepactuacaoSemJustificativa) ?? 0).ToString() + ", ";
                retorno += (listaAtividade.FindAll(p => p.CodAvaliacaoAuto > 0 && p.CodAvaliacao == 0)?.Count() ?? 0).ToString() + ", ";                
                retorno += (listaAtividade?.Sum(p => p.PonderacaoTotal) ?? 0).ToString() + "] ";

                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'Avaliação', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }

            ucAvaliacao.CarregarTabela(retorno);            
        }
    }
}