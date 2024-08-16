using System.DirectoryServices;

namespace PortalAtividade
{
    public class LdapAuthentication
    {
        private string _dominio = ".";
        private string _usuario = "";
        private string _senha = "";

        /// <summary>
        /// Domínio
        /// </summary>
        public string Dominio
        {
            set { _dominio = value; }
        }

        /// <summary>
        /// Login do usuário
        /// </summary>
        public string Usuario
        {
            set { _usuario = value; }
        }

        /// <summary>
        /// Senha 
        /// </summary>
        public string Senha
        {
            set { _senha = value; }
        }

        /// <summary>
        /// Verifica se a infromação login e senha estão corretas
        /// </summary>
        /// <returns>True = Usuário autenticado</returns>
        public bool IsAuthenticated()
        {
            var domainAndUsername = _dominio + "\\" + _usuario;
            var entry = new DirectoryEntry("", domainAndUsername, _senha);
            try
            {
                var search = new DirectorySearcher(entry) { Filter = "(SAMAccountName=" + _usuario + ")" };
                search.PropertiesToLoad.Add("cn");
                search.PropertiesToLoad.Add("SAMAccountName");

                if (search.FindOne() == null)
                    return false;

                //_path = result.Path;
                //_filterAttribute = (String)result.Properties["cn"][0]; // Nome
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Verifica se a infromação login e senha estão corretas
        /// </summary>
        /// <param name="username">Login</param>
        /// <param name="pwd">Senha</param>
        /// <returns>True = Usuário autenticado</returns>
        public bool IsAuthenticated(string username, string pwd)
        {
            _usuario = username;
            _senha = pwd;

            return IsAuthenticated();
        }

        /// <summary>
        /// Verifica se a infromação login e senha estão corretas
        /// </summary>
        /// <param name="domain">Domínio</param>
        /// <param name="username">Login</param>
        /// <param name="pwd">Senha</param>
        /// <returns>True = Usuário autenticado</returns>
        public bool IsAuthenticated(string domain, string username, string pwd)
        {
            _dominio = domain;
            _usuario = username;
            _senha = pwd;

            return IsAuthenticated();
        }
    }
}