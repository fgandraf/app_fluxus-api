using System;

namespace Fluxus.Api.Entities
{

    public class Os
    {
        public long Id { get; set; }
        public string Titulo { get; set; }
        public string Referencia { get; set; }
        public string Agencia { get; set; }
        public string ProfissionalId { get; set; }
        public string AtividadeId { get; set; }
        public string ValorAtividade { get; set; }
        public string ValorDeslocamento { get; set; }
        public string ClienteNome { get; set; }
        public string Cidade { get; set; }
        public string ContatoNome { get; set; }
        public string ContatoTelefone { get; set; }
        public string Coordenada { get; set; }
        public string Status { get; set; }
        public string Observacoes { get; set; }
        public long FaturaId { get; set; }
        public string DataOrdem { get; set; }
        public DateTime Prazo { get; set; }
        public string DataPendente { get; set; }
        public string DataVistoria { get; set; }
        public string DataConcluida { get; set; }
        public bool Siopi { get; set; }
    }


}
