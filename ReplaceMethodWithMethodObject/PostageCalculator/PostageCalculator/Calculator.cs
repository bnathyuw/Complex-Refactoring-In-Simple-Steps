using System;

namespace PostageCalculator
{
    public class Calculator
    {
        private const int MaximumSmallWeight = 60;
        private const int MaximumSmallHeight = 229;
        private const int MaximumSmallWidth = 162;
        private const int MaximumSmallDepth = 25;
        private const int MaximumMediumWeight = 500;
        private const int MaximumMediumHeight = 324;
        private const int MaximumMediumWidth = 229;
        private const int MaximumMediumDepth = 100;
        private const decimal SmallPackagePostage = 120m;
        private const int MediumPackageMultiplier = 4;
        private const int LargePackageMultiplier = 6;
        private const int CurrencyConversionCharge = 200;
        private const decimal GbpToEur = 1.25m;
        private const decimal GbpToChf = 1.36m;
        private const decimal MillilitresInLitre = 1000m;

        public Money Calculate(int weight, int height, int width, int depth, Currency currency)
        {
            var postageInBaseCurrency = PostageInBaseCurrency(weight, height, width, depth);
            return ConvertCurrency(postageInBaseCurrency, currency);
        }

        private decimal PostageInBaseCurrency(int weight, int height, int width, int depth)
        {
            if (weight <= MaximumSmallWeight && height <= MaximumSmallHeight && width <= MaximumSmallWidth && depth <= MaximumSmallDepth)
            {
                return SmallPackagePostage;
            }
            if (weight <= MaximumMediumWeight && height <= MaximumMediumHeight && width <= MaximumMediumWidth && depth <= MaximumMediumDepth)
            {
                return weight*MediumPackageMultiplier;
            }
            return Math.Max(weight, Volume(height, width, depth))*LargePackageMultiplier;
        }

        private static decimal Volume(int height, int width, int depth)
        {
            return height*width*depth/MillilitresInLitre;
        }

        private Money ConvertCurrency(decimal amountInBaseCurrency, Currency currency)
        {
            if (currency == Currency.Gbp)
                return new Money(Currency.Gbp, amountInBaseCurrency);
            if(currency == Currency.Eur)
                return new Money(Currency.Eur, (amountInBaseCurrency + CurrencyConversionCharge) * GbpToEur);
            if(currency == Currency.Chf)
                return new Money(Currency.Chf, (amountInBaseCurrency + CurrencyConversionCharge) * GbpToChf);
            throw new Exception("Currency not supported");
        }
    }
}
