using System.ComponentModel.DataAnnotations.Schema;

namespace ExchangerApp.Models
{
    public class ExchangeModel
    {
        public string FirstCurrencyCode { get; set; }

        public float? FirstCurrencyAmount { get; set; }

        public string SecondCurrencyCode { get; set; }

        public float? SecondCurrencyAmount { get; set; }

        [NotMapped]
        public float Rate { get; set; }
    }
}
