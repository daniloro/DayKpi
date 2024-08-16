using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalAtividade.Model
{
    public class SessionManager
    {
        public string SessionId { get; set; }

        public SessionManager(string sessionId)
        {
            SessionId = sessionId;
        }

        public OperadorInfo Usuario { get; set; }
        
        public object Objects { get; set; }
        public object Filtro { get; set; }
        public string Redirect { get; set; }
    }
}