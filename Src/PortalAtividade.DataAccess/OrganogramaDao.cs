using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class OrganogramaDao : BaseDao
    {
        /// <summary> 
        /// Lista o organograma do login selecionado
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OrganogramaInfo> ConsultarOrganograma(string login)
        {
            var listaOrganograma = new List<OrganogramaInfo>();
            OrganogramaInfo organograma;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ORGANOGRAMA", parms))
            {
                while (dataReader.Read())
                {
                    organograma = new OrganogramaInfo
                    {
                        Nivel = Convert.ToInt32(dataReader["Nivel"]),
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Gestor = dataReader["Gestor"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        Grupo = dataReader["Grupo"].ToString()
                    };
                    listaOrganograma.Add(organograma);
                }
            }
            return listaOrganograma;
        }

        /// <summary> 
        /// Lista o organograma do login e periodo selecionado
        /// </summary>
        /// <param name="dataOrganograma">Data do Organograma</param> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<OrganogramaInfo> ConsultarOrganogramaPeriodo(DateTime dataOrganograma, string login)
        {
            var listaOrganograma = new List<OrganogramaInfo>();
            OrganogramaInfo organograma;

            SqlParameter[] parms =
            {
                new SqlParameter("@DataOrganograma", SqlDbType.DateTime),
                new SqlParameter("@Login", SqlDbType.VarChar, 8)};
            parms[0].Value = dataOrganograma;
            parms[1].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_ORGANOGRAMA_PERIODO", parms))
            {
                while (dataReader.Read())
                {
                    organograma = new OrganogramaInfo
                    {
                        Nivel = Convert.ToInt32(dataReader["Nivel"]),
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Gestor = dataReader["Gestor"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        Grupo = dataReader["Grupo"].ToString()
                    };
                    listaOrganograma.Add(organograma);
                }
            }
            return listaOrganograma;
        }

        /// <summary> 
        /// Obtem os dados do Gestor
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public OrganogramaInfo ObterGestor(string login)
        {
            OrganogramaInfo organograma = null;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar)};
            parms[0].Value = login;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_GESTOR", parms))
            {
                if (dataReader.Read())
                {
                    organograma = new OrganogramaInfo
                    {
                        LoginAd = dataReader["LoginAd"].ToString(),
                        Nome = dataReader["Nome"].ToString(),
                        Gestor = dataReader["Gestor"].ToString(),
                        NomeGestor = dataReader["NomeGestor"].ToString()
                    };
                }
            }
            return organograma;
        }

        /// <summary> 
        /// Altera o gestor
        /// </summary> 
        /// <param name="login">Login do Operador</param>
        /// <param name="gestor">Login do Gestor</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarGestor(string login, string gestor)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@Gestor", SqlDbType.VarChar, 8)
            };
            parms[0].Value = login;

            if (string.IsNullOrEmpty(gestor))
                parms[1].Value = DBNull.Value;
            else
                parms[1].Value = gestor;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_GESTOR", parms);
        }

        /// <summary> 
        /// Salva o organograma do Gestor daquele Periodo
        /// </summary> 
        /// <param name="organograma">Dados do Organograma</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public void IncluirOrganograma(OrganogramaInfo organograma)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@DataOrganograma", SqlDbType.DateTime),
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@Nome", SqlDbType.VarChar, 50),
                new SqlParameter("@Grupo", SqlDbType.VarChar, 50),
                new SqlParameter("@Gestor", SqlDbType.VarChar, 8),
                new SqlParameter("@NomeGestor", SqlDbType.VarChar, 50),
                new SqlParameter("@CodAnalise", SqlDbType.Int),
                new SqlParameter("@DataUltimaAlteracao", SqlDbType.DateTime)
            };
            parms[0].Value = organograma.DataOrganograma;
            parms[1].Value = organograma.LoginAd;
            parms[2].Value = organograma.Nome;

            if (string.IsNullOrEmpty(organograma.Grupo))
                parms[3].Value = DBNull.Value;
            else
                parms[3].Value = organograma.Grupo;

            parms[4].Value = organograma.Gestor;
            parms[5].Value = organograma.NomeGestor;
            parms[6].Value = organograma.CodAnalise;
            parms[7].Value = organograma.DataUltimaAlteracao;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_ORGANOGRAMA", parms);
        }
    }
}