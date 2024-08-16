using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class OperadorDao : BaseDao
    {
        /// <summary> 
        /// Obtem os dados do Operador
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public OperadorInfo ObterOperador(string login)
        {
            OperadorInfo operador = null;            

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_OPERADOR", parms))
            {
                if (dataReader.Read())
                {
                    operador = new OperadorInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),                        
                        TipoOperador = Convert.ToInt32(dataReader["TipoOperador"]),
                        CodTipoEquipe = Convert.ToInt32(dataReader["CodTipoEquipe"])
                    };                    
                }
            }
            return operador;
        }        

        /// <summary> 
        /// Consulta todos Operadores do Gestor
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarOperadorGestor(string login)
        {
            var listaOperador = new List<OperadorInfo>();
            OperadorInfo operador;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_OPERADOR_GESTOR", parms))
            {
                while (dataReader.Read())
                {
                    operador = new OperadorInfo
                    {   
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        Email = dataReader["Email"].ToString(),
                        TipoOperador = Convert.ToInt32(dataReader["TipoOperador"]),
                        Grupo = dataReader["Grupo"].ToString()
                    };
                    listaOperador.Add(operador);
                }
            }
            return listaOperador;
        }

        /// <summary> 
        /// Consulta todos Operadores Lider do Gestor
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarOperadorGestorLider(string login)
        {
            var listaOperador = new List<OperadorInfo>();
            OperadorInfo operador;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_OPERADOR_GESTOR_LIDER", parms))
            {
                while (dataReader.Read())
                {
                    operador = new OperadorInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        CodTipoEquipe = Convert.ToInt32(dataReader["CodTipoEquipe"])
                    };
                    listaOperador.Add(operador);
                }
            }
            return listaOperador;
        }

        /// <summary> 
        /// Consulta todos Lideres do Sistema
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarOperadorLider()
        {
            var listaOperador = new List<OperadorInfo>();
            OperadorInfo operador;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_OPERADOR_LIDER", null))
            {
                while (dataReader.Read())
                {
                    operador = new OperadorInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        CodTipoEquipe = Convert.ToInt32(dataReader["CodTipoEquipe"])
                    };
                    listaOperador.Add(operador);
                }
            }
            return listaOperador;
        }

        /// <summary> 
        /// Consulta todos Grupos do TopDesk do Operador
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OperadorInfo> ConsultarGruposOperador(string login)
        {
            var listaOperador = new List<OperadorInfo>();
            OperadorInfo operador;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_GRUPO_OPERADOR", parms))
            {
                while (dataReader.Read())
                {
                    operador = new OperadorInfo
                    {                        
                        Grupo = dataReader["Grupo"].ToString()
                    };
                    listaOperador.Add(operador);
                }
            }
            return listaOperador;
        }
    }
}