using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using wcfRESTful.Model;
using System.Runtime.Serialization;
using static wcfRESTful.ExchangeRates;


namespace wcfRESTful
{
    public partial class ExchangeRates
    {
        private YappeEntities yappeEntities = new YappeEntities();
        [DataContract]
        public class JsonResponse<T>
        {
            [DataMember]
            public string Message { get; set; }

            [DataMember]
            public T Data { get; set; }
        }
        [DataContract]
        public class ExchangeRatesB
        {
            [DataMember]
            public string SourceCurrency { get; set; }
            [DataMember]
            public string TargetCurrency { get; set; }
            [DataMember]
            public decimal Rate { get; set; }
            [DataMember]
            public decimal Amount { get; set; }
            
        }

        [DataContract]
        public class ExchangeRatesC
        {
            [DataMember]
            public int Id { get; set; }
            [DataMember]
            public decimal Rate { get; set; }
            
        }

        [DataContract]
        public class Login
        {
            [DataMember]
            public int username { get; set; }
            [DataMember]
            public decimal password { get; set; }

        }


        public List<Model.ExchangeRates> ListaExchangeRates
        {
            get { return yappeEntities.ExchangeRates.ToList(); }
        }
        public Model.ExchangeRates InsertExchangeRate(ExchangeRatesB exchangeRateB)
        {
            Model.ExchangeRates exchangeRate = new Model.ExchangeRates();
            exchangeRate.SourceCurrency = exchangeRateB.SourceCurrency;
            exchangeRate.TargetCurrency = exchangeRateB.TargetCurrency;
            exchangeRate.Rate = exchangeRateB.Rate;
            exchangeRate.Amount = exchangeRateB.Amount;
            exchangeRate.AmountWithExchangeRate = exchangeRateB.Rate * exchangeRateB.Amount;
            yappeEntities.ExchangeRates.Add(exchangeRate);
            yappeEntities.SaveChanges();
            return exchangeRate;
        }

        public Model.ExchangeRates UpdateExchangeRate(ExchangeRatesC exchangeRatesC)
        {
            var existingExchangeRate = yappeEntities.ExchangeRates.Find(exchangeRatesC.Id);
            if (existingExchangeRate != null)
            {
                existingExchangeRate.Rate = exchangeRatesC.Rate;
                existingExchangeRate.AmountWithExchangeRate = existingExchangeRate.Amount * exchangeRatesC.Rate;
                yappeEntities.SaveChanges();
                return existingExchangeRate;
            }
            else
            {
                throw new Exception("Exchange rate not found.");
            }
        }
        public void DeleteExchangeRate(int id)
        {
            var exchangeRate = yappeEntities.ExchangeRates.Find(id);
            if (exchangeRate != null)
            {
                yappeEntities.ExchangeRates.Remove(exchangeRate);
                yappeEntities.SaveChanges();
            }
            else
            {
                throw new Exception("Exchange rate not found.");
            }
        }

    }
}