using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class AnaliseKPIDao : BaseDao
    {
        /// <summary> 
        /// Obtem uma pergunta do questionario
        /// </summary> 
        /// <param name="codPergunta">Codigo da Pergunta</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AnaliseKPIInfo ObterPergunta(int codPergunta)
        {
            var pergunta = new AnaliseKPIInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@CodPergunta", SqlDbType.Int)};
            parms[0].Value = codPergunta;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_PERGUNTA_KPI", parms))
            {
                if (dataReader.Read())
                {
                    pergunta.CodPergunta = Convert.ToInt32(dataReader["CodPergunta"]);
                    pergunta.DscPergunta = dataReader["DscPergunta"].ToString();                    
                }
            }
            return pergunta;
        }

        /// <summary> 
        /// Obtem uma analise do questionario
        /// </summary>
        /// <param name="dataAnalise">Data da Análise</param>
        /// <param name="grupo">Grupo</param>
        /// <param name="codPergunta">Codigo da Pergunta</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AnaliseKPIInfo ObterAnaliseGrupo(DateTime dataAnalise, string grupo, int codPergunta)
        {
            var analise = new AnaliseKPIInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@DataAnalise", SqlDbType.DateTime),
                new SqlParameter("@Grupo", SqlDbType.VarChar, 50),
                new SqlParameter("@CodPergunta", SqlDbType.Int)};
            parms[0].Value = dataAnalise;
            parms[1].Value = grupo;
            parms[2].Value = codPergunta;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_ANALISE_KPI_GRUPO", parms))
            {
                if (dataReader.Read())
                {
                    analise.CodAnalise = Convert.ToInt32(dataReader["CodAnalise"]);
                    analise.DataAnalise = Convert.ToDateTime(dataReader["DataAnalise"]);
                    analise.Grupo = dataReader["Grupo"].ToString();
                    analise.CodPergunta = Convert.ToInt32(dataReader["CodPergunta"]);
                    analise.DscAnalise = dataReader["DscAnalise"].ToString();
                    analise.Concluido = Convert.ToBoolean(dataReader["Concluido"]);
                }
            }
            return analise;
        }

        /// <summary> 
        /// Obtem uma analise do questionario
        /// </summary>
        /// <param name="dataAnalise">Data da Análise</param>
        /// <param name="login">Login do Gestor</param>
        /// <param name="codPergunta">Codigo da Pergunta</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AnaliseKPIInfo ObterAnaliseGestor(DateTime dataAnalise, string login, int codPergunta)
        {
            var analise = new AnaliseKPIInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@DataAnalise", SqlDbType.DateTime),
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@CodPergunta", SqlDbType.Int)};
            parms[0].Value = dataAnalise;
            parms[1].Value = login;
            parms[2].Value = codPergunta;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_ANALISE_KPI_GESTOR", parms))
            {
                if (dataReader.Read())
                {
                    analise.CodAnalise = Convert.ToInt32(dataReader["CodAnalise"]);
                    analise.DataAnalise = Convert.ToDateTime(dataReader["DataAnalise"]);
                    analise.Grupo = dataReader["Grupo"].ToString();
                    analise.CodPergunta = Convert.ToInt32(dataReader["CodPergunta"]);
                    analise.DscAnalise = dataReader["DscAnalise"].ToString();
                    analise.Concluido = Convert.ToBoolean(dataReader["Concluido"]);
                }
            }
            return analise;
        }

        /// <summary> 
        /// Obtem uma analise por operador
        /// </summary>
        /// <param name="dataAnalise">Data da Análise</param>
        /// <param name="loginOperador">Login do Operador</param>
        /// <param name="codPergunta">Codigo da Pergunta</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AnaliseKPIInfo ObterAnaliseOperador(DateTime dataAnalise, string loginOperador, int codPergunta)
        {
            var analise = new AnaliseKPIInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@DataAnalise", SqlDbType.DateTime),
                new SqlParameter("@LoginOperador", SqlDbType.VarChar, 8),
                new SqlParameter("@CodPergunta", SqlDbType.Int)};
            parms[0].Value = dataAnalise;
            parms[1].Value = loginOperador;
            parms[2].Value = codPergunta;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_ANALISE_KPI_OPERADOR", parms))
            {
                if (dataReader.Read())
                {
                    analise.CodAnalise = Convert.ToInt32(dataReader["CodAnalise"]);
                    analise.DataAnalise = Convert.ToDateTime(dataReader["DataAnalise"]);
                    analise.LoginOperador = dataReader["LoginOperador"].ToString();
                    analise.CodPergunta = Convert.ToInt32(dataReader["CodPergunta"]);
                    analise.DscAnalise = dataReader["DscAnalise"].ToString();
                    analise.Concluido = Convert.ToBoolean(dataReader["Concluido"]);
                }
            }
            return analise;
        }

        /// <summary> 
        /// Inclui uma analise
        /// </summary>
        /// <param name="analise">Dados da analise</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public int IncluirAnalise(AnaliseKPIInfo analise)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@DataAnalise", SqlDbType.DateTime),
                new SqlParameter("@Grupo", SqlDbType.VarChar, 50),
                new SqlParameter("@LoginOperador", SqlDbType.VarChar, 8),
                new SqlParameter("@CodPergunta", SqlDbType.Int),
                new SqlParameter("@DscAnalise", SqlDbType.VarChar),
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@Concluido", SqlDbType.Bit)};
            parms[0].Value = analise.DataAnalise;

            if (!string.IsNullOrEmpty(analise.Grupo))
                parms[1].Value = analise.Grupo;
            else
                parms[1].Value = DBNull.Value;

            if (!string.IsNullOrEmpty(analise.LoginOperador))
                parms[2].Value = analise.LoginOperador;
            else
                parms[2].Value = DBNull.Value;

            parms[3].Value = analise.CodPergunta;
            parms[4].Value = analise.DscAnalise;
            parms[5].Value = analise.LoginAd;
            parms[6].Value = analise.Concluido;

            var codAnalise = ExecuteScalar(CommandType.StoredProcedure, "P_INCLUIR_ANALISE_KPI", parms);
            return Convert.ToInt32(codAnalise);
        }

        /// <summary> 
        /// Altera uma analise
        /// </summary>
        /// <param name="codAnalise">Codigo da Analise</param>
        /// <param name="dscAnalise">Descrição da Analise</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public void AlterarAnalise(AnaliseKPIInfo analise)
        {
            SqlParameter[] parms =
            {                
                new SqlParameter("@CodAnalise", SqlDbType.Int),
                new SqlParameter("@DscAnalise", SqlDbType.VarChar),
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@Concluido", SqlDbType.Bit)};            
            parms[0].Value = analise.CodAnalise;
            parms[1].Value = analise.DscAnalise;
            parms[2].Value = analise.LoginAd;
            parms[3].Value = analise.Concluido;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_ANALISE_KPI", parms);
        }        
    }
}