using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class CheckListSistemaDao : BaseDao
    {
        /// <summary> 
        /// Selecione o checklist do dia por grupo
        /// </summary> 
        /// <param name="login">Login do Analista</param>
        /// <param name="tipo">Manhã ou Noite</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<CheckListSistemaInfo> ConsultarCheckListSistema(string login, int tipo)
        {
            var listaCheckList = new List<CheckListSistemaInfo>();
            CheckListSistemaInfo checklist;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@Tipo", SqlDbType.Int)};

            parms[0].Value = login;
            parms[1].Value = tipo;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT " +
                                                                    "CL.CodItem," +
                                                                    "CodCheckList, " +
                                                                    "Sistema, " +
                                                                    "Modulo, " +
                                                                    "Descricao, " +
                                                                    "CL.Grupo, " +
                                                                    "Nome, " +
                                                                    "CodStatus, " +
                                                                    "Observacao " +
                                                                    "FROM CheckListSistema CL " +
                                                                    "INNER JOIN OperadorGrupo ON OperadorGrupo.Grupo = CL.Grupo " +
                                                                    "LEFT OUTER JOIN CheckListSistemaDia Dia ON Dia.CodItem = CL.CodItem AND DataItem = CONVERT(VARCHAR(10), GETDATE(), 120) AND Dia.Tipo = @Tipo " +
                                                                    "LEFT OUTER JOIN Operador ON Operador.LoginAd = Dia.LoginAd " +
                                                                    "WHERE " +
                                                                    "OperadorGrupo.LoginAd = @Login AND " +
                                                                    "CL.Tipo IN (0, @Tipo) " +
                                                                    "ORDER BY CL.Grupo, CL.CodItem", parms))
            {
                while (dataReader.Read())
                {
                    checklist = new CheckListSistemaInfo
                    {
                        CodItem = Convert.ToInt32(dataReader["CodItem"]),
                        CodCheckList = Convert.ToInt32(dataReader["CodCheckList"] == DBNull.Value ? 0 : dataReader["CodCheckList"]),
                        Sistema = dataReader["Sistema"].ToString(),
                        Modulo = dataReader["Modulo"].ToString(),
                        Descricao = dataReader["Descricao"].ToString(),                       
                        Grupo = dataReader["Grupo"].ToString(),
                        Responsavel = dataReader["Nome"].ToString(),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"] == DBNull.Value ? 0 : dataReader["CodStatus"]),
                        Observacao = dataReader["Observacao"].ToString()
                    };
                    listaCheckList.Add(checklist);
                }
            }
            return listaCheckList;
        }

        /// <summary> 
        /// Selecione o checklist do dia de todos os grupos
        /// </summary> 
        /// <param name="login">Login do Analista</param>
        /// <param name="tipo">Manhã ou Noite</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<CheckListSistemaInfo> ConsultarCheckListSistemaCompleto(int tipo)
        {
            var listaCheckList = new List<CheckListSistemaInfo>();
            CheckListSistemaInfo checklist;

            SqlParameter[] parms =
            {                
                new SqlParameter("@Tipo", SqlDbType.Int)};

            parms[0].Value = tipo;

            using (var dataReader = ExecuteReader(CommandType.Text, "SELECT " +
                                                                    "CL.CodItem," +
                                                                    "CodCheckList, " +
                                                                    "Sistema, " +
                                                                    "Modulo, " +
                                                                    "Descricao, " +
                                                                    "CL.Grupo, " +
                                                                    "Nome, " +
                                                                    "CodStatus, " +
                                                                    "Observacao " +
                                                                    "FROM CheckListSistema CL " +                                                                    
                                                                    "LEFT OUTER JOIN CheckListSistemaDia Dia ON Dia.CodItem = CL.CodItem AND DataItem = CONVERT(VARCHAR(10), GETDATE(), 120) AND Dia.Tipo = @Tipo " +
                                                                    "LEFT OUTER JOIN Operador ON Operador.LoginAd = Dia.LoginAd " +
                                                                    "WHERE " +                                                                    
                                                                    "CL.Tipo IN (0, @Tipo) " +
                                                                    "ORDER BY CL.Grupo, CL.CodItem", parms))
            {
                while (dataReader.Read())
                {
                    checklist = new CheckListSistemaInfo
                    {
                        CodItem = Convert.ToInt32(dataReader["CodItem"]),
                        CodCheckList = Convert.ToInt32(dataReader["CodCheckList"] == DBNull.Value ? 0 : dataReader["CodCheckList"]),
                        Sistema = dataReader["Sistema"].ToString(),
                        Modulo = dataReader["Modulo"].ToString(),
                        Descricao = dataReader["Descricao"].ToString(),
                        Grupo = dataReader["Grupo"].ToString(),
                        Responsavel = dataReader["Nome"].ToString(),
                        CodStatus = Convert.ToInt32(dataReader["CodStatus"] == DBNull.Value ? 0 : dataReader["CodStatus"]),
                        Observacao = dataReader["Observacao"].ToString()
                    };
                    listaCheckList.Add(checklist);
                }
            }
            return listaCheckList;
        }

        /// <summary> 
        /// Inclui um novo registro no checklist diário do sistema
        /// </summary> 
        /// <param name="checklist">Dados do Checklist</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirCheckList(CheckListSistemaInfo checklist)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodItem", SqlDbType.Int),
                new SqlParameter("@Tipo", SqlDbType.Int),
                new SqlParameter("@DataItem", SqlDbType.DateTime),
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@CodStatus", SqlDbType.Int),
                new SqlParameter("@Observacao", SqlDbType.VarChar, 100)
            };
            parms[0].Value = checklist.CodItem;
            parms[1].Value = checklist.Tipo;
            parms[2].Value = checklist.DataItem;
            parms[3].Value = checklist.LoginAd;
            parms[4].Value = checklist.CodStatus;

            if (string.IsNullOrEmpty(checklist.Observacao))
                parms[5].Value = DBNull.Value;
            else
                parms[5].Value = checklist.Observacao;

            ExecuteNonQuery(CommandType.Text, "INSERT INTO CheckListSistemaDia " +
                                            "(CodItem, Tipo, DataItem, LoginAd, CodStatus, Observacao, DataAlteracao) " +
                                            "VALUES (@CodItem, @Tipo, @DataItem, @LoginAd, @CodStatus, @Observacao, getdate())", parms);
        }

        /// <summary> 
        /// Altera um registro no checklist diário do sistema
        /// </summary> 
        /// <param name="checklist">Dados do Checklist</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarCheckList(CheckListSistemaInfo checklist)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodCheckList", SqlDbType.Int),                
                new SqlParameter("@LoginAd", SqlDbType.VarChar, 8),
                new SqlParameter("@CodStatus", SqlDbType.Int)
            };
            parms[0].Value = checklist.CodCheckList;
            parms[1].Value = checklist.LoginAd;
            parms[2].Value = checklist.CodStatus;                       

            ExecuteNonQuery(CommandType.Text, "UPDATE CheckListSistemaDia SET " +
                                            "LoginAd = @LoginAd, " +
                                            "CodStatus = @CodStatus, " +
                                            "DataAlteracao = getdate() " +
                                            "WHERE CodCheckList = @CodCheckList", parms);
        }
    }
}