using System;

namespace PostageCalculator
{
    public class Calculator
    {
        public Money Calculate(int weight, int height, int width, int depth, Currency currency)
        {
            var postageInBaseCurrency = PostageInBaseCurrency(weight, height, width, depth);
            return ConvertCurrency(postageInBaseCurrency, currency);
        }

        private decimal PostageInBaseCurrency(int weight, int height, int width, int depth)
        {
            if (weight <= 60 && height <= 229 && width <= 162 && depth <= 25)
            {
                return 120m;
            }
            if (weight <= 500 && height <= 324 && width <= 229 && depth <= 100)
            {
                return weight*4;
            }
            return Math.Max(weight, height*width*depth/1000m)*6;
        }

        private Money ConvertCurrency(decimal amountInBaseCurrency, Currency currency)
        {
            if (currency == Currency.Gbp)
                return new Money(Currency.Gbp, amountInBaseCurrency);
            if(currency == Currency.Eur)
                return new Money(Currency.Eur, (amountInBaseCurrency + 200) * 1.25m);
            if(currency == Currency.Chf)
                return new Money(Currency.Chf, (amountInBaseCurrency + 200) * 1.36m);
            throw new Exception("Currency not supported");
        }
    }
}
