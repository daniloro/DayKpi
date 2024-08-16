using System;
using System.Data;
using System.Collections.Generic;
using PortalAtividade.Model;
using PortalAtividade.DataAccess;
using System.Linq;

namespace PortalAtividade.Business
{
    public class OrganogramaBo
    {
        private static readonly OrganogramaDao OrganogramaDao = new OrganogramaDao();

        #region " Singleton "

        private static OrganogramaBo _getInstance;
        private static readonly object SyncRoot = new object();
        public static OrganogramaBo GetInstance
        {
            get
            {
                if (_getInstance != null) return _getInstance;
                lock (SyncRoot)
                {
                    _getInstance = new OrganogramaBo();
                }
                return _getInstance;
            }
        }

        #endregion

        /// <summary> 
        /// Lista o organograma do login selecionado
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<OrganogramaInfo> ConsultarOrganograma(string login)
        {
            return OrganogramaDao.ConsultarOrganograma(login);
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
            return OrganogramaDao.ConsultarOrganogramaPeriodo(dataOrganograma, login);
        }

        /// <summary> 
        /// Lista o json organograma do login selecionado
        /// </summary> 
        /// <returns></returns>
        /// <remarks></remarks>
        public string ConsultarOrganogramaJson(List<OrganogramaInfo> listaOrganograma)
        {                      
            var listaResultado = new List<OrganogramaInfo>();            

            var listaGestor = listaOrganograma.FindAll(p => listaOrganograma.Any(q => q.Gestor == p.LoginAd));
            listaGestor.Sort(new GenericComparer<OrganogramaInfo>("Nivel", "DESC"));
            foreach (var gestor in listaGestor)
            {
                gestor.TotalEquipe = listaOrganograma.Count(p => p.Gestor == gestor.LoginAd);
                gestor.TotalEquipe += listaOrganograma.FindAll(p => p.Gestor == gestor.LoginAd).Sum(q => q.TotalEquipe);
                
                if (gestor.Nivel == 1)
                    break;
            }

            var qtdGrupo = listaOrganograma.FindAll(p => p.Nivel >= 1).Select(q => q.Grupo).Distinct().Count();

            foreach (var item in listaOrganograma)
            {
                var nome = item.Nome.Split(' ');
                item.Nome = nome[0] + " " + nome[nome.Length - 1];

                item.GestorAux = item.Gestor;

                if (!listaOrganograma.Exists(p => p.Gestor == item.LoginAd))
                {
                    if (!listaResultado.Exists(p => p.Grupo == item.Grupo && p.Gestor == item.Gestor))
                    {
                        var grupo = new OrganogramaInfo
                        {
                            Nome = item.Grupo.Replace("Desenv_", "").Replace("Desenv - ", ""),
                            LoginAd = $"{item.Grupo}{item.Gestor}",
                            Gestor = item.Gestor,
                            GestorAux = item.Gestor
                        };
                        listaResultado.Add(grupo);
                        item.GestorAux = $"{item.Grupo}{item.Gestor}";
                    }
                    else
                    {
                        if (qtdGrupo > 1) // Se tiver mais de um grupo
                            item.GestorAux = listaResultado.Last(p => p.Grupo == item.Grupo && p.Gestor == item.Gestor && p.IndGestor == false).LoginAd;
                        else
                        {
                            item.GestorAux = $"{item.Grupo}{item.Gestor}";                            
                        }
                    }
                }
                else
                {
                    item.IndGestor = true;
                }
                listaResultado.Add(item);
            }

            var result = "[";

            if (listaOrganograma.Count > 0)
            {
                foreach (var organograma in listaResultado)
                {
                    if (!string.IsNullOrEmpty(organograma.Grupo))
                    {
                        if (organograma.TotalEquipe == 0)
                        {
                            result += "[{ v: '" + organograma.LoginAd + "', f: '" + organograma.Nome + "'}, '" + organograma.GestorAux + "',''],";
                        }
                        else
                        {
                            result += "[{ v: '" + organograma.LoginAd + "', f: '" + organograma.Nome + "<br /><strong>" + organograma.TotalEquipe.ToString() + "</strong>'}, '" + organograma.GestorAux + "',''],";
                        }                        
                    }
                    else //Grupo
                    {                        
                        result += "[{ v: '" + organograma.LoginAd + "', f: '<div class=\"google-visualization-orgchart-nodesel\">" + organograma.Nome + "</div>'}, '" + organograma.GestorAux + "',''],";
                    }
                }
                result = result.Remove(result.Length - 1, 1);
            }            

            result += "]";
            return result;
        }

        /// <summary> 
        /// Obtem os dados do Gestor
        /// </summary> 
        /// <returns></returns> 
        /// <remarks></remarks> 
        public OrganogramaInfo ObterGestor(string login)
        {
            return OrganogramaDao.ObterGestor(login);
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
            OrganogramaDao.AlterarGestor(login, gestor);
        }

        /// <summary> 
        /// Salva o organograma do Gestor daquele Periodo
        /// </summary> 
        /// <param name="login">Login do Operador</param>
        /// <param name="dataOrganograma">Data do Periodo</param>
        /// <param name="codAnalise">Codigo da Analise</param>
        /// <returns></returns> 
        /// <remarks></remarks>
        public void IncluirOrganograma(string login, DateTime dataOrganograma, int codAnalise)
        {
            var lista = ConsultarOrganograma(login);
            var dataUltimaAlteracao = DateTime.Now;

            foreach (var item in lista)
            {
                item.DataOrganograma = dataOrganograma;
                item.CodAnalise = codAnalise;
                item.DataUltimaAlteracao = dataUltimaAlteracao;

                OrganogramaDao.IncluirOrganograma(item);
            }
        }

        /// <summary> 
        /// Lista a equipe do Login Selecionado
        /// </summary> 
        /// <param name="login">Login do Analista</param> 
        /// <returns></returns> 
        /// <remarks></remarks>
        public List<OrganogramaInfo> ConsultarEquipe(DateTime dataConsulta, string login)
        {
            var listaOrganograma = ConsultarOrganogramaPeriodo(dataConsulta, login);
            if (!listaOrganograma.Any())
                listaOrganograma = ConsultarOrganograma(login);

            listaOrganograma.RemoveAll(p => p.Nivel <= 1);

            return listaOrganograma;
        }
    }
}