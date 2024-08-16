using System;
using System.Linq;
using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;

namespace PortalAtividade.Business
{
    public class AvaliacaoAtividadeBo
    {
        private static readonly AvaliacaoAtividadeDao AvaliacaoAtividadeDao = new AvaliacaoAtividadeDao();

        #region " Singleton "

        private static AvaliacaoAtividadeBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static AvaliacaoAtividadeBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new AvaliacaoAtividadeBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Calcula o valor total da Ponderação da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        private void CalcularPonderacao(AvaliacaoAtividadeInfo atividade)
        {
            atividade.PonderacaoTotal = atividade.Ponderacao + atividade.NotaAvaliacao;

            if (atividade.QtdRepactuacaoSemJustificativa <= atividade.QtdConfirmacao)
            {                
                atividade.PonderacaoTotal *= atividade.QtdConfirmacao + 1 - atividade.QtdRepactuacaoSemJustificativa;
            }
        }

        /// <summary> 
        /// Obtem os dados da Ponderação da Equipe
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadePonderacaoMesEquipe(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaAtividade = AvaliacaoAtividadeDao.ConsultarAtividadePonderacaoMesEquipe(login, dataInicio, dataFim);

            foreach (var atividade in listaAtividade)
            {
                CalcularPonderacao(atividade);
            }
            return listaAtividade;
        }

        /// <summary> 
        /// Obtem os dados da Atividade
        /// </summary> 
        /// <param name="nroAtividade">Nro da Atividade</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AvaliacaoAtividadeInfo> ConsultarAtividadePonderacaoMes(string login, DateTime dataInicio, DateTime dataFim)
        {
            var listaAtividade = AvaliacaoAtividadeDao.ConsultarAtividadePonderacaoMes(login, dataInicio, dataFim);

            foreach (var atividade in listaAtividade)
            {
                CalcularPonderacao(atividade);
            }
            return listaAtividade;
        }

        // <summary> 
        /// Obtem os detalhes da Atividade
            /// </summary> 
        /// <param name="nroAtividade">Número da Atividade</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public AvaliacaoAtividadeInfo ConsultarAtividade(string nroAtividade)
        {
            var atividade = AvaliacaoAtividadeDao.ConsultarAtividade(nroAtividade);

            var listaDestaque = ConsultarAtividadeDestaque(atividade.CodAvaliacao);
            if (listaDestaque.Any())
            {
                atividade.DestaqueGestor = string.Join(", ", listaDestaque.Select(p => p.DscDestaque).ToArray());
                atividade.NotaAvaliacao = listaDestaque.Sum(p => p.Ponderacao);
            }            

            CalcularPonderacao(atividade);

            return atividade;
        }

        /// <summary> 
        /// Inclui um novo registro na AvaliacaoAtividade
        /// </summary> 
        /// <param name="avaliacao">Dados da Avaliação</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public int IncluirAvaliacaoAtividade(AvaliacaoAtividadeInfo atividade)
        {
            return AvaliacaoAtividadeDao.IncluirAvaliacaoAtividade(atividade);
        }

        // <summary> 
        /// Lista os Destaques das Avaliações
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<AtividadeDestaqueInfo> ConsultarDestaque()
        {
            return AvaliacaoAtividadeDao.ConsultarDestaque();
        }

        // <summary> 
        /// Obtem os destaques da avaliação
        /// </summary> 
        /// <param name="codAvaliacao">Código da Avaliação</param>
        /// <returns></returns> 
        /// <remarks></remarks> 
        public List<AtividadeDestaqueInfo> ConsultarAtividadeDestaque(int codAvaliacao)
        {
            return AvaliacaoAtividadeDao.ConsultarAtividadeDestaque(codAvaliacao);
        }

        // <summary> 
        /// Inclui um novo destaque para a Atividade
        /// </summary> 
        /// <param name="avaliacao">Dados do Destaque</param> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public void IncluirAtividadeDestaque(AtividadeDestaqueInfo destaque)
        {
            AvaliacaoAtividadeDao.IncluirAtividadeDestaque(destaque);
        }
    }
}