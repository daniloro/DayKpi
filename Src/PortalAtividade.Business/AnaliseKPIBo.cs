using System;
using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class AnaliseKPIBo
    {
        private static readonly AnaliseKPIDao AnaliseKPIDao = new AnaliseKPIDao();

        #region " Singleton "

        private static AnaliseKPIBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static AnaliseKPIBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new AnaliseKPIBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Obtem uma pergunta do questionario
        /// </summary> 
        /// <param name="codPergunta">Codigo da Pergunta</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AnaliseKPIInfo ObterPergunta(int codPergunta)
        {            
            return AnaliseKPIDao.ObterPergunta(codPergunta);
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
            var analise =  AnaliseKPIDao.ObterAnaliseGrupo(dataAnalise, grupo, codPergunta);
            analise.DscAnalise = ObterAnalise(grupo, analise.DscAnalise);            
            return analise;
        }

        /// <summary> 
        /// Obtem uma analise do questionario
        /// </summary>
        /// <param name="dataAnalise">Data da Análise</param>
        /// <param name="loginAd">Login do Gestor</param>
        /// <param name="codPergunta">Codigo da Pergunta</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AnaliseKPIInfo ObterAnaliseGestor(DateTime dataAnalise, string loginAd, int codPergunta)
        {
            var analise = AnaliseKPIDao.ObterAnaliseGestor(dataAnalise, loginAd, codPergunta);
            analise.DscAnalise = ObterAnalise(loginAd, analise.DscAnalise);
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
            var analise = AnaliseKPIDao.ObterAnaliseOperador(dataAnalise, loginOperador, codPergunta);
            analise.DscAnalise = ObterAnalise(loginOperador, analise.DscAnalise);
            return analise;
        }

        /// <summary> 
        /// Obtem a análise descriptografada
        /// </summary>
        /// <param name="concatenado">string utilizada para concatenar a criptografia</param>
        /// <param name="analise">analise criptografada</param>
        /// <returns>analise descriptografada</returns> 
        /// <remarks></remarks>
        private string ObterAnalise(string concatenado, string analise)
        {
            string retorno = null;
            if (!string.IsNullOrEmpty(analise))
            {
                retorno = CriptografiaBo.Decrypt(analise);
                retorno = retorno.Remove(0, concatenado.Length);                
            }
            return retorno;
        }

        /// <summary> 
        /// Inclui uma analise
        /// </summary>
        /// <param name="nroGmud">Número da GMUD</param>
        /// <param name="codSistema">Codigo do Sistema</param>
        /// <param name="versao">Versao do Sistema</param>
        /// <returns></returns> 
        /// <remarks></remarks>  
        public int IncluirAnalise(AnaliseKPIInfo analise)
        {
            return AnaliseKPIDao.IncluirAnalise(analise);
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
            AnaliseKPIDao.AlterarAnalise(analise);
        }

        /// <summary> 
        /// Salva a Análise (Incluir ou Alterar)
        /// </summary>
        /// <param name="codPergunta">Codigo da Pergunta</param>
        /// <param name="dscAnalise">Descrição da Analise</param>
        /// <param name="analise">Dados da Análise</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public void SalvarAnalise(int codPergunta, string dscAnalise, AnaliseKPIInfo analise)
        {            
            AnaliseKPIInfo analiseBase;
            string concatenado;

            if (!string.IsNullOrEmpty(analise.LoginOperador))
            {
                analiseBase = ObterAnaliseOperador(analise.DataAnalise, analise.LoginOperador, codPergunta);
                concatenado = analise.LoginOperador;
            }
            else if (!string.IsNullOrEmpty(analise.Grupo))
            {
                analiseBase = ObterAnaliseGrupo(analise.DataAnalise, analise.Grupo, codPergunta);
                concatenado = analise.Grupo;
            }
            else
            {
                analiseBase = ObterAnaliseGestor(analise.DataAnalise, analise.LoginAd, codPergunta);
                concatenado = analise.LoginAd;
            }

            if (analiseBase.CodAnalise == 0)
            {
                if (!string.IsNullOrEmpty(dscAnalise))
                {                    
                    analise.CodPergunta = codPergunta;
                    analise.DscAnalise = CriptografiaBo.Encrypt(concatenado + dscAnalise);                   

                    analise.CodAnalise = IncluirAnalise(analise);
                }
            }
            else if (dscAnalise != analiseBase.DscAnalise || analise.Concluido)
            {
                analise.CodAnalise = analiseBase.CodAnalise;
                analise.DscAnalise = CriptografiaBo.Encrypt(concatenado + dscAnalise);              

                AlterarAnalise(analise);
            }
        }
    }
}