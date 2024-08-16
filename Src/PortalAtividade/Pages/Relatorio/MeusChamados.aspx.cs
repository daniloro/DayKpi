using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages.Relatorio
{
    public partial class MeusChamados : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                CarregarMeusChamados();
            }
        }

        private void CarregarMeusChamados()
        {   
            var listaAtividade = RelatorioBo.GetInstance.ConsultarMeusChamados(UsuarioAD);                        

            var retorno = "[";

            if (listaAtividade.Any())
            {
                retorno += "[{label: 'NroChamado', id: 'NroChamado', type: 'string'}, ";
                retorno += "{label: 'DscChamado', id: 'DscChamado', type: 'string'}, ";
                retorno += "{label: 'NroAtividade', id: 'NroAtividade', type: 'string'}, ";
                retorno += "{label: 'DscAtividade', id: 'DscAtividade', type: 'string'}, ";                
                retorno += "{label: 'Operador', id: 'Operador', type: 'string'}, ";                
                retorno += "{label: 'DataFinal', id: 'DataFinal', type: 'string'}], ";

                foreach (var atividade in listaAtividade)
                {                    
                    retorno += "['" + atividade.NroChamado + "', ";
                    retorno += "'" + atividade.DscChamado + "', ";
                    retorno += "'" + atividade.NroAtividade + "', ";
                    retorno += string.Format("'{0}', ", !string.IsNullOrEmpty(atividade.DscPlanejador) ? atividade.DscPlanejador : "Outros");
                    retorno += "'" + atividade.Operador + "', ";
                    retorno += "{ v: '" + atividade.DataFinal.ToString("yyyy/MM/dd") + "', f: '" + string.Format(atividade.DataFinal != DateTime.MinValue ? atividade.DataFinal.ToString("dd/MM/yyyy") : "") + "'}], ";
                }

                retorno += "[{ v: 'Total', p: { className: 'TotalCell' } }, ";

                retorno += "'" + listaAtividade.Count.ToString() + "', ";
                retorno += "'" + "" + "', ";
                retorno += "'" + "" + "', ";
                retorno += "'" + "" + "', ";
                retorno += "'" + "" + "'] ";

                retorno += "]";
            }
            else
            {
                retorno = "[[{label: 'Meus Chamados', id: 'Vazio', type: 'string'}], ['Nenhum registro encontrado!']]";
            }

            ucMeusChamados.CarregarTabela(retorno);
        }
    }
}