using System;

namespace ExchangerApp.Models
{
    public class HistoryModel : ExchangeModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }
    }
}
