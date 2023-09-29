# TBC Bank IntegrationService Standard+ C#/net7 Client
Client for TBC Standard+ IntegrationService - https://developers.tbcbank.ge/docs/dbi-overview

## Following services are implemented:
* (Import Single Payments)[https://developers.tbcbank.ge/docs/import-single-payments]
* (Account Movement)[https://developers.tbcbank.ge/docs/account-movement]

### Usage
See the [Demo](AppifySheets.TBC.IntegrationService.Client.DemoConsole/Program.cs)

```csharp
var credentials = new TBCApiCredentials("Username", "Password"); // Obtain API Credentials & Certificate with password from the Bank/Banker
var tbcSoapCaller = new TBCSoapCaller("certificate.pfx", "CertificatePassword", credentials);

var accountMovements = await Worker.GetDeserialized(new GetAccountMovementsDeserializer(tbcSoapCaller,
    new Period(new DateTime(2023, 9, 1), new DateTime(2023, 9, 26))));

var withinBankGel = await Worker
    .GetDeserialized(new ImportSinglePaymentOrdersDeserializer(tbcSoapCaller,
        new SoapImportSinglePaymentOrders(
            new TransferWithinBankPaymentOrderIo("TEST", "TEST",
                BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own GEL account within TBC where funds are being transferred TO"), CurrencyV.GEL).Value)
            {
                DocumentNumber = 123,
                Amount = 0.01m,
                BeneficiaryName = "TEST",
                SenderAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own GEL account within TBC where funds are being transferred FROM"), CurrencyV.GEL).Value
            })));

var withinBankCurrency = await Worker
    .GetDeserialized(new ImportSinglePaymentOrdersDeserializer(tbcSoapCaller,
        new SoapImportSinglePaymentOrders(
            new TransferWithinBankPaymentOrderIo("TEST", "TEST",
                BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own account CURRENCY within TBC where funds are being transferred TO"), CurrencyV.USD).Value)
            {
                DocumentNumber = 123,
                Amount = 0.01m,
                BeneficiaryName = "TEST",
                SenderAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own CURRENCY account within TBC where funds are being transferred FROM"), CurrencyV.USD).Value
            })));
var toAnotherBankGel = await Worker
    .GetDeserialized(new ImportSinglePaymentOrdersDeserializer(tbcSoapCaller, new SoapImportSinglePaymentOrders(new TransferToOtherBankNationalCurrencyPaymentOrderIo(
        BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of a GEL account OUTSIDE TBC where funds are being transferred TO"), CurrencyV.GEL).Value, "TEST", "TEST", "123123123")
    {
        DocumentNumber = 123,
        Amount = 0.01m,
        BeneficiaryName = "TEST",
        SenderAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own GEL account within TBC where funds are being transferred FROM"), CurrencyV.GEL).Value
    })));

var toAnotherBankCurrency = await Worker
    .GetDeserialized(new ImportSinglePaymentOrdersDeserializer(tbcSoapCaller, new SoapImportSinglePaymentOrders(
        new TransferToOtherBankForeignCurrencyPaymentOrderIo("TEST", "TEST",
            "test", "test", "SHA", "TEST",
            BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of a CURRENCY account OUTSIDE TBC where funds are being transferred TO"), CurrencyV.USD).Value)
        {
            DocumentNumber = 123,
            Amount = 100m,
            BeneficiaryName = "TEST",
            SenderAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own CURRENCY account within TBC where funds are being transferred FROM"), CurrencyV.USD).Value
        })));

var toTreasury = await Worker
    .GetDeserialized(new ImportSinglePaymentOrdersDeserializer(tbcSoapCaller,
        new SoapImportSinglePaymentOrders(
            new TreasuryTransferPaymentOrderIo(101001000, "TEST")
            {
                DocumentNumber = 123,
                Amount = 0.01m,
                BeneficiaryName = "TEST",
                SenderAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("IBAN of your own GEL account within TBC where funds to Treasury are being transferred FROM"), CurrencyV.GEL).Value
            })));
```
