using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using PortalAtividade.Model;

namespace PortalAtividade.DataAccess
{
    public sealed class EventoProblemaDao : BaseDao
    {
        /// <summary> 
        /// Consulta os tipos de Evento
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<EventoProblemaInfo> ConsultarTipoEvento()
        {
            var listaTipoEvento = new List<EventoProblemaInfo>();
            EventoProblemaInfo tipoEvento;            

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_TIPO_EVENTO_PROBLEMA", null))
            {
                while (dataReader.Read())
                {
                    tipoEvento = new EventoProblemaInfo
                    {
                        CodTipoEvento = Convert.ToInt32(dataReader["CodTipoEvento"]),
                        DscTipoEvento = dataReader["DscTipoEvento"].ToString()
                    };
                    listaTipoEvento.Add(tipoEvento);
                }
            }
            return listaTipoEvento;
        }

        /// <summary> 
        /// Consulta os eventos do Periodo
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataInicio">Data de Inicio</param>
        /// <param name="dataFim">Data Final da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<EventoProblemaInfo> ConsultarEvento(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaTipoEvento = new List<EventoProblemaInfo>();
            EventoProblemaInfo tipoEvento;

            SqlParameter[] parms =
            {
                new SqlParameter("@Login", SqlDbType.VarChar, 8),
                new SqlParameter("@DataInicio", SqlDbType.DateTime),
                new SqlParameter("@DataFim", SqlDbType.DateTime)};
            parms[0].Value = login;
            parms[1].Value = dataInicio;
            parms[2].Value = dataFim;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_CONSULTAR_EVENTO_PROBLEMA", parms))
            {
                while (dataReader.Read())
                {
                    tipoEvento = new EventoProblemaInfo
                    {
                        CodEvento = Convert.ToInt32(dataReader["CodEvento"]),
                        CodTipoEvento = Convert.ToInt32(dataReader["CodTipoEvento"]),
                        DscTipoEvento = dataReader["DscTipoEvento"].ToString(),
                        NomeSistema = dataReader["NomeSistema"].ToString(),
                        DataEvento = Convert.ToDateTime(dataReader["DataEvento"]),
                        TituloEvento = dataReader["TituloEvento"].ToString(),
                        NroChamado = dataReader["NroChamado"].ToString()                        
                    };
                    listaTipoEvento.Add(tipoEvento);
                }
            }
            return listaTipoEvento;
        }

        /// <summary> 
        /// Obtem os dados do Evento
        /// </summary> 
        /// <param name="codEvento">Codigo do Evento</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public EventoProblemaInfo ObterEvento(int codEvento)
        {
            var chamado = new EventoProblemaInfo();

            SqlParameter[] parms =
            {
                new SqlParameter("@CodEvento", SqlDbType.Int)};
            parms[0].Value = codEvento;

            using (var dataReader = ExecuteReader(CommandType.StoredProcedure, "P_OBTER_EVENTO_PROBLEMA", parms))
            {
                if (dataReader.Read())
                {
                    chamado.CodEvento = Convert.ToInt32(dataReader["CodEvento"]);
                    chamado.CodTipoEvento = Convert.ToInt32(dataReader["CodTipoEvento"]);
                    chamado.CodSistema = Convert.ToInt32(dataReader["CodSistema"]);
                    chamado.DataEvento = Convert.ToDateTime(dataReader["DataEvento"]);
                    chamado.TituloEvento = dataReader["TituloEvento"].ToString();
                    chamado.DscEvento = dataReader["DscEvento"].ToString();
                    chamado.Correcao = dataReader["Correcao"].ToString();
                    chamado.NroChamado = dataReader["NroChamado"].ToString();
                    chamado.NroCorrecao = dataReader["NroCorrecao"].ToString();
                }
            }
            return chamado;
        }

        /// <summary> 
        /// Inclui um novo registro na tabela EventoProblema
        /// </summary> 
        /// <param name="evento">Dados do Evento</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirEvento(EventoProblemaInfo evento)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodTipoEvento", SqlDbType.Int),
                new SqlParameter("@CodSistema", SqlDbType.Int),
                new SqlParameter("@DataEvento", SqlDbType.DateTime),
                new SqlParameter("@TituloEvento", SqlDbType.VarChar, 100),
                new SqlParameter("@DscEvento", SqlDbType.VarChar, 550),
                new SqlParameter("@DscCorrecao", SqlDbType.VarChar, 550),
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@NroCorrecao", SqlDbType.VarChar, 14),
                new SqlParameter("@Login", SqlDbType.VarChar, 8)
            };
            parms[0].Value = evento.CodTipoEvento;
            parms[1].Value = evento.CodSistema;
            parms[2].Value = evento.DataEvento;
            parms[3].Value = evento.TituloEvento;
            parms[4].Value = evento.DscEvento;
            parms[5].Value = evento.Correcao;
            parms[6].Value = evento.NroChamado;
            parms[7].Value = evento.NroCorrecao;
            parms[8].Value = evento.LoginAd;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_INCLUIR_EVENTO_PROBLEMA", parms);
        }

        /// <summary> 
        /// Altera um registro na tabela EventoProblema
        /// </summary> 
        /// <param name="evento">Dados do Evento</param 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarEvento(EventoProblemaInfo evento)
        {
            SqlParameter[] parms =
            {
                new SqlParameter("@CodEvento", SqlDbType.Int),
                new SqlParameter("@CodTipoEvento", SqlDbType.Int),
                new SqlParameter("@CodSistema", SqlDbType.Int),
                new SqlParameter("@DataEvento", SqlDbType.DateTime),
                new SqlParameter("@TituloEvento", SqlDbType.VarChar, 100),
                new SqlParameter("@DscEvento", SqlDbType.VarChar, 550),
                new SqlParameter("@DscCorrecao", SqlDbType.VarChar, 550),
                new SqlParameter("@NroChamado", SqlDbType.VarChar, 14),
                new SqlParameter("@NroCorrecao", SqlDbType.VarChar, 14),
                new SqlParameter("@Login", SqlDbType.VarChar, 8)
            };
            parms[0].Value = evento.CodEvento;
            parms[1].Value = evento.CodTipoEvento;
            parms[2].Value = evento.CodSistema;
            parms[3].Value = evento.DataEvento;
            parms[4].Value = evento.TituloEvento;
            parms[5].Value = evento.DscEvento;
            parms[6].Value = evento.Correcao;
            parms[7].Value = evento.NroChamado;
            parms[8].Value = evento.NroCorrecao;
            parms[9].Value = evento.LoginAd;

            ExecuteNonQuery(CommandType.StoredProcedure, "P_ALTERAR_EVENTO_PROBLEMA", parms);
        }
    }
}