using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalAtividade.Model;
using PortalAtividade.Business;

namespace PortalAtividade.Pages
{
    public partial class CheckListSistema : BasePage
    {
        private enum ColCheckList
        {
            Sistema = 0,
            Modulo,
            Descricao,
            Responsavel,
            CodStatus
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarCheckList();
            }
        }

        private void CarregarCheckList()
        {
            txtObservacao.Text = "";

            var login = Context.User.Identity.Name;
            int tipo = 1; // checklist da manhã
            if (DateTime.Now.Hour > 16)
                tipo = 2; // checklist da noite            
            
            if (!chkTodos.Checked)
                login = null;
                
            var listaCheckList = CheckListSistemaBo.GetInstance.ConsultarCheckListSistema(login, tipo);

            CurrentSession.Objects = listaCheckList;

            phLista.Visible = true;
            phObservacao.Visible = false;
            gdvCheckList.DataSource = listaCheckList;
            gdvCheckList.DataBind();
        }

        protected void GdvCheckList_PreRender(object sender, EventArgs e)
        {
            if (gdvCheckList.Rows.Count > 0)
            {
                gdvCheckList.UseAccessibleHeader = true;
                gdvCheckList.HeaderRow.TableSection = TableRowSection.TableHeader;
                gdvCheckList.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void GdvCheckList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckListSistemaInfo checklist = (CheckListSistemaInfo)e.Row.DataItem;

                e.Row.Cells[(int)ColCheckList.Sistema].Text = checklist.Sistema;
                e.Row.Cells[(int)ColCheckList.Modulo].Text = checklist.Modulo;
                e.Row.Cells[(int)ColCheckList.Descricao].Text = checklist.Descricao;

                if (!string.IsNullOrEmpty(checklist.Observacao))
                    e.Row.Cells[(int)ColCheckList.Descricao].Text = checklist.Descricao + "</br> <strong>" + checklist.Observacao + "</strong>";

                if (!string.IsNullOrEmpty(checklist.Responsavel))
                {
                    e.Row.Cells[(int)ColCheckList.Responsavel].Text = checklist.Responsavel.Contains(" ") ? checklist.Responsavel.Substring(0, checklist.Responsavel.IndexOf(" ")) : checklist.Responsavel;
                }
                else
                {
                    e.Row.Cells[(int)ColCheckList.Responsavel].Text = checklist.Grupo;
                }

                if (checklist.CodCheckList == 0)
                {
                    var btnObservacao = (LinkButton)e.Row.FindControl("btnObservacao");                    
                    btnObservacao.CommandArgument = checklist.CodItem.ToString();
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("btnObservacao")).Visible = false;

                    if (checklist.CodStatus == 0)
                    {
                        e.Row.BackColor = System.Drawing.Color.Yellow;                        
                    }                    
                }

                if (checklist.CodStatus == 0)
                {
                    var btnConfirmar = (LinkButton)e.Row.FindControl("btnConfirmar");
                    btnConfirmar.OnClientClick = "DayMensagens.mostraMensagemConfirmacao('Confirmação', 'Tudo certo com este item?', " + btnConfirmar.ClientID + "); return false;";
                    btnConfirmar.CommandArgument = checklist.CodItem.ToString();

                    ((Image)e.Row.FindControl("imgVisto")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Row.FindControl("btnConfirmar")).Visible = false;
                    ((Image)e.Row.FindControl("imgVisto")).Visible = true;
                }
            }
        }

        protected void ConfirmarCheckList(object sender, CommandEventArgs e)
        {
            int codItem = Convert.ToInt32(e.CommandArgument);

            if (CurrentSession.Objects is List<CheckListSistemaInfo> listaCheckList)
            {
                var checklist = listaCheckList.Find(p => p.CodItem == codItem);
                checklist.CodStatus = 1;
                SalvarCheckList(checklist);
            }     
        }

        private void SalvarCheckList(CheckListSistemaInfo checklist)
        {
            checklist.LoginAd = User.Identity.Name;            
            checklist.DataItem = DateTime.Today;
            checklist.Tipo = DateTime.Now.Hour > 16 ? 2 : 1; // checklist da manhã ou noite

            if (checklist.CodCheckList == 0)
            {
                CheckListSistemaBo.GetInstance.IncluirCheckList(checklist);
            }
            else
            {
                CheckListSistemaBo.GetInstance.AlterarCheckList(checklist);
            }

            CarregarCheckList();
        }

        protected void IncluirObservacao(object sender, CommandEventArgs e)
        {
            int codItem = Convert.ToInt32(e.CommandArgument);

            if (CurrentSession.Objects is List<CheckListSistemaInfo> listaCheckList)
            {
                var checklist = listaCheckList.Find(p => p.CodItem == codItem);
                CurrentSession.Objects = checklist;

                phObservacao.Visible = true;
                phLista.Visible = false;

                ltlSistema.Text = checklist.Sistema;
                ltlModulo.Text = checklist.Modulo;
                ltlDescricao.Text = checklist.Descricao;
            }
        }

        protected void BtnVoltar_Click(object sender, EventArgs e)
        {
            CarregarCheckList();
        }

        protected void BtnSalvar_Click(object sender, EventArgs e)
        {
            if (ValidarAtividade())
                SalvarCheckList();
        }

        private bool ValidarAtividade()
        {
            if (string.IsNullOrEmpty(txtObservacao.Text) || txtObservacao.Text.Length < 5)
            {
                RetornarMensagemAviso("Adicione uma observação!");
                return false;
            }

            return true;
        }

        private void SalvarCheckList()
        {
            if (CurrentSession.Objects is CheckListSistemaInfo checklist)
            {
                checklist.Observacao = txtObservacao.Text;                
                checklist.CodStatus = 0;
                SalvarCheckList(checklist);                
            }
        }

        protected void ChkTodos_CheckedChanged(object sender, EventArgs e)
        {
            CarregarCheckList();
        }
    }
}