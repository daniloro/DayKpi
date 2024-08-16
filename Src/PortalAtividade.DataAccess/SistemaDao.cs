using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class SistemaDao : BaseDao
    {
        /// <summary> 
        /// Consultar os Sistemas do Operador
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <returns></returns> 
        /// <remarks></remarks>  
        public List<SistemaInfo> ConsultarSistemaOperador(string login)
        {
            var listaSistema = new List<SistemaInfo>();
            SistemaInfo sistema;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_SISTEMA_OPERADOR", parms))
            {
                while (dataReader.Read())
                {
                    sistema = new SistemaInfo
                    {                        
                        CodSistema = Convert.ToInt32(dataReader["CodSistema"]),
                        NomeSistema = dataReader["NomeSistema"].ToString()                        
                    };
                    listaSistema.Add(sistema);
                }
            }
            return listaSistema;
        }
    }
}