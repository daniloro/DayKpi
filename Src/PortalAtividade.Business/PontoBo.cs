using System;
using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class PontoBo
    {
        private static readonly PontoDao PontoDao = new PontoDao();

        #region " Singleton "

        private static PontoBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static PontoBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new PontoBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem os dados do Ponto do dia
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public PontoInfo ObterPonto(string login)
        {
            return PontoDao.ObterPonto(login);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPonto(PontoInfo ponto)
        {
            PontoDao.IncluirPonto(ponto);
        }

        /// <summary> 
        /// Altera um registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarPonto(PontoInfo ponto)
        {
            PontoDao.AlterarPonto(ponto);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela Ponto
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPontoManual(PontoInfo ponto)
        {
            PontoDao.IncluirPontoManual(ponto);
        }

        /// <summary> 
        /// Consultar lista de ponto
        /// </summary>
        /// <param name="codEquipe">Codigo da Equipe</param>
        /// <param name="dataPonto">Data do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PontoInfo> ConsultarPonto(string login, DateTime dataPonto)
        {
            return PontoDao.ConsultarPonto(login, dataPonto);
        }

        /// <summary> 
        /// Consultar o detalhe do Ponto Mensal do Operador
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataInicio">Data Inicial</param>
        /// <param name="dataFim">Data Final</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PontoInfo> ConsultarPontoMensalOperador(string login, DateTime dataInicio, DateTime dataFim)
        {
            return PontoDao.ConsultarPontoMensalOperador(login, dataInicio, dataFim);
        }

        /// <summary> 
        /// Consultar o resumo do Ponto Mensal da Equipe
        /// </summary> 
        /// <param name="login">Login</param> 
        /// <param name="dataMes">Data da Consulta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<PontoMensalInfo> ConsultarPontoMensal(string login, DateTime dataMes)
        {
            return PontoDao.ConsultarPontoMensal(login, dataMes);
        }

        /// <summary> 
        /// Inclui um novo registro na tabela PontoMensal
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirPontoMensal(PontoMensalInfo ponto)
        {
            PontoDao.IncluirPontoMensal(ponto);
        }

        /// <summary> 
        /// Altera um registro na tabela PontoMensal
        /// </summary> 
        /// <param name="ponto">Dados do Ponto</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void AlterarPontoMensal(PontoMensalInfo ponto)
        {
            PontoDao.AlterarPontoMensal(ponto);
        }
    }
}
