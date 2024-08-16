using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class PlanejadorDao : BaseDao
    {
        /// <summary> 
        /// Consulta todos itens do Planejador da Equipe
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PlanejadorInfo> ConsultarPlanejador(string login)
        {
            var listaPlanejador = new List<PlanejadorInfo>();
            PlanejadorInfo planejador;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_PLANEJADOR_EQUIPE", parms))
            {
                while (dataReader.Read())
                {
                    planejador = new PlanejadorInfo
                    {
                        CodPlanejador = Convert.ToInt32(dataReader["CodPlanejador"]),
                        Descricao = dataReader["DscPlanejador"].ToString(),
                        Abreviado = dataReader["Abreviado"].ToString()
                    };
                    listaPlanejador.Add(planejador);
                }
            }
            return listaPlanejador;
        }
    }
}