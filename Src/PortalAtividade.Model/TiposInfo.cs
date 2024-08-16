
namespace PortalAtividade.Model
{
    public class TiposInfo
    {
        public enum Equipe
        {
            DesenvolvimentoI = 1,
            DesenvolvimentoII = 2,
            DesenvolvimentoIII = 3,
            BusinessI = 4,
            Redes = 5,
            BusinessII = 6,
            DesenvolvimentoIV = 7,
            BusinessIII = 8
        }

        public enum TipoEquipe
        {
            Desenvolvimento = 1,
            Business = 2,
            Redes = 3
        }

        public enum TipoOperador
        {
            Analista = 1,
            Lider = 2,
            Adm = 3
        }

        public enum TipoAvaliacao
        {
            AutoDesenvolvimento = 1,
            LiderDesenvolvimento = 2
        }

        public enum Planejador
        {
            Espec = 1,
            ValidaEspec = 2,
            AprovaEspec = 3,
            Definicao = 4,
            ComiteDev = 5,
            Dev = 6,
            CodeReview = 7,
            ValidaDev = 8,
            Merge = 9,
            Hml = 10,
            AprovaHml = 11,
            PosGmud = 12
        }

        public enum PerguntaKPI
        {
            Objetivo = 1,
            Organograma = 2,
            Missao = 3,
            Meta1 = 4,
            Meta2 = 5,
            Backlog1 = 6,
            Backlog2 = 7,
            Backlog3 = 8,
            EmFila1 = 9,
            EmFila2 = 10,
            EmFila3 = 11,
            EmFila4 = 12,
            EmFila5 = 13,
            AbertoConcluido1 = 14,
            AbertoConcluido2 = 15,           
            Atendimento1 = 16,
            Atendimento2 = 17,
            Atendimento3 = 18,
            Atendimento4 = 19,
            Performance1 = 20,
            Performance2 = 21,
            Performance3 = 22,
            Performance4 = 23,
            Gmud = 24
        }
    }
}