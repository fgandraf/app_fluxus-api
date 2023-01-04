using System;

namespace Fluxus.Api.Entities
{

    public class Fatura
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public double SubtotalOs { get; set; }
        public double SubtotalDeslocamento { get; set; }
        public double Total { get; set; }
    }


}
