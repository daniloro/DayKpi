using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace PortalAtividade.DataAccess
{
    public sealed class UtilDao : BaseDao
    {
        /// <summary> 
        /// Obtem a Data Hora de Atualização do Sistema
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public DateTime ObterDataAtualizacao()
        {
            var retorno = DateTime.MinValue;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_DATAATUALIZACAO", null))
            {
                if (dataReader.Read())
                {
                    retorno = Convert.ToDateTime(dataReader["DataAtualizacao"]);
                }
            }
            return retorno;
        }        
    }
}