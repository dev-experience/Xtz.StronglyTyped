using System;
using System.Collections.Generic;
using Bogus;
using Xtz.StronglyTyped.BuiltinTypes.Address;
using Xtz.StronglyTyped.BuiltinTypes.Finance;

namespace Xtz.StronglyTyped.BuiltinTypes.Bogus
{
    public class FinanceFakerBuilder : BaseFakerBuilder
    {
        public FinanceFakerBuilder(bool useFakerCache = true)
            : base(useFakerCache)
        {
        }

        /// <summary>
        /// A random account name faker.
        /// </summary>
        /// <remarks>Example: "savings", "checking", "Home Loan", etc.</remarks>
        public Faker<AccountName> BuildAccountNameFaker()
        {
            var result = GetFaker(() => new Faker<AccountName>()
                .CustomInstantiator(f => new AccountName(f.Finance.AccountName())));
            return result;
        }

        /// <summary>
        /// A random account number faker.
        /// </summary>
        /// <param name="length">The length of the account number (default 8).</param>
        public Faker<AccountNumber> BuildAccountNumberFaker(int length = 8)
        {
            var result = GetFaker(() => new Faker<AccountNumber>()
                .CustomInstantiator(f => new AccountNumber(f.Finance.Account(length))), length.ToString());
            return result;
        }

        /// <summary>
        /// A random amount faker (default 0 - 1000).
        /// </summary>
        /// <param name="min">Min value (default 0).</param>
        /// <param name="max">Max value (default 1000).</param>
        /// <param name="decimals">Decimal places (default 2).</param>
        public Faker<Amount> BuildAmountFaker(decimal min = 0, decimal max = 1000, int decimals = 2)
        {
            var cacheKey = $"{min}|{max}|{decimals}";

            var result = GetFaker(() => new Faker<Amount>()
                .CustomInstantiator(f => new Amount(f.Finance.Amount(min, max, decimals))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random Bank Identifier Code (BIC) faker.
        /// </summary>
        public Faker<Bic> BuildBicFaker()
        {
            var result = GetFaker(() => new Faker<Bic>()
                .CustomInstantiator(f => new Bic(f.Finance.Bic())));
            return result;
        }

        /// <summary>
        /// A random currency faker.
        /// </summary>
        public Faker<Currency> BuildCurrencyFaker()
        {
            var result = GetFaker(() => new Faker<Currency>()
                .CustomInstantiator(f =>
                {
                    var bogusCurrency = f.Finance.Currency();
                    return new Currency(
                        new CurrencyName(bogusCurrency.Description),
                        new CurrencyCode(bogusCurrency.Code),
                        !string.IsNullOrWhiteSpace(bogusCurrency.Symbol)
                            ? new CurrencySymbol(bogusCurrency.Symbol)
                            : null
                    );
                }));
            return result;
        }

        /// <summary>
        /// A random International Bank Account Number (IBAN) faker.
        /// </summary>
        /// <param name="formatted">Formatted IBAN containing spaces.</param>
        /// <param name="countryCode">A two letter ISO3166 country code. Throws an exception if the country code is not found or is an invalid length.</param>
        /// <exception cref="KeyNotFoundException">An exception is thrown if the ISO3166 country code is not found.</exception>
        /// <exception cref="ArgumentOutOfRangeException">An exception is thrown if the country code is invalid.</exception>
        public Faker<Iban> BuildIbanFaker(bool formatted = false, CountryCode? countryCode = null)
        {
            var cacheKey = $"{formatted}|{countryCode}";

            var result = GetFaker(() => new Faker<Iban>()
                .CustomInstantiator(f => new Iban(f.Finance.Iban(formatted, countryCode?.ToString()))), cacheKey);
            return result;
        }

        /// <summary>
        /// A random ABA routing number (with valid check digit) faker.
        /// </summary>
        public Faker<RoutingNumber> BuildRoutingNumberFaker()
        {
            var result = GetFaker(() => new Faker<RoutingNumber>()
                .CustomInstantiator(f => new RoutingNumber(f.Finance.RoutingNumber())));
            return result;
        }

        /// <summary>
        /// A random transaction type faker.
        /// </summary>
        /// <remarks>Example: "deposit", "withdrawal", "payment", or "invoice".</remarks>
        public Faker<TransactionType> BuildTransactionTypeFaker()
        {
            var result = GetFaker(() => new Faker<TransactionType>()
                .CustomInstantiator(f => new TransactionType(f.Finance.TransactionType())));
            return result;
        }
    }
}