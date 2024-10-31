# TBC Bank IntegrationService Standard+ C#/net8 Client
Service Documentation by the TBC Bank is here - https://developers.tbcbank.ge/docs/dbi-overview

## Following services are implemented:
* [Import Single Payments](https://developers.tbcbank.ge/docs/import-single-payments)
* [Account Movement](https://developers.tbcbank.ge/docs/account-movement)

### Usage
See the [Demo](AppifySheets.TBC.IntegrationService.Client.DemoConsole/Program.cs)

```csharp
var credentials = new TBCApiCredentials("Username", "Password"); // Obtain API Credentials & Certificate with password from the Bank/Banker
var tbcApiCredentialsWithCertificate = new TBCApiCredentialsWithCertificate(credentials, "TBCIntegrationService.pfx", "XR_w;,64");

var tbcSoapCaller = new TBCSoapCaller(tbcApiCredentialsWithCertificate);

var accountMovements =
    await GetAccountMovementsHelper.GetAccountMovement(new Period(new DateTime(2023, 9, 1), new DateTime(2023, 9, 26)), tbcSoapCaller);

var checkStatus = await tbcSoapCaller.GetDeserialized(new GetPaymentOrderStatusRequestIo(1632027071));

var ownAccountGEL = BankAccountWithCurrencyV.Create(new BankAccountV("GE31TB7467936080100003"), CurrencyV.GEL).Value;
var ownAccountUSD = BankAccountWithCurrencyV.Create(new BankAccountV("GE47TB7467936170100001"), CurrencyV.USD).Value;

var transferTypeRecordSpecific = new TransferTypeRecordSpecific
{
    DocumentNumber = 123,
    Amount = 0.01m,
    BeneficiaryName = "TEST",
    SenderAccountWithCurrency = ownAccountGEL,
    Description = "TEST"
};

var withinBankGel2 = await tbcSoapCaller.GetDeserialized(new ImportSinglePaymentOrdersRequestIo(
    new TransferWithinBankPaymentOrderIo
    {
        RecipientAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("GE86TB1144836120100002"), CurrencyV.GEL).Value,
        TransferTypeRecordSpecific = transferTypeRecordSpecific
    }));

var withinBankCurrency = await tbcSoapCaller.GetDeserialized(new ImportSinglePaymentOrdersRequestIo(
    new TransferWithinBankPaymentOrderIo
    {
        TransferTypeRecordSpecific = transferTypeRecordSpecific with
        {
            SenderAccountWithCurrency = ownAccountUSD
        },
        RecipientAccountWithCurrency = BankAccountWithCurrencyV.Create(new BankAccountV("GE86TB1144836120100002"), CurrencyV.USD).Value,
    }));

var toAnotherBankGel = await tbcSoapCaller.GetDeserialized(
    new ImportSinglePaymentOrdersRequestIo(
        new TransferToOtherBankNationalCurrencyPaymentOrderIo(
            BankAccountWithCurrencyV.Create(new BankAccountV("GE33BG0000000263255500"), CurrencyV.GEL).Value, "123123123")
        {
            TransferTypeRecordSpecific = transferTypeRecordSpecific
        }));

var toAnotherBankCurrencyGood = await tbcSoapCaller.GetDeserialized(
    new ImportSinglePaymentOrdersRequestIo(
        new TransferToOtherBankForeignCurrencyPaymentOrderIo("test", "test", "SHA", "TEST",
            BankAccountWithCurrencyV.Create(new BankAccountV("GE33BG0000000263255500"), CurrencyV.USD).Value)
        {
            TransferTypeRecordSpecific = transferTypeRecordSpecific with { SenderAccountWithCurrency = ownAccountUSD }
        }));

var toAnotherBankCurrencyBad = await tbcSoapCaller.GetDeserialized(
    new ImportSinglePaymentOrdersRequestIo(
        new TransferToOtherBankForeignCurrencyPaymentOrderIo("test", "test", "SHA", "TEST",
            BankAccountWithCurrencyV.Create(new BankAccountV("GE33BG0000000263255500"), CurrencyV.USD).Value)
        {
            TransferTypeRecordSpecific = transferTypeRecordSpecific with { SenderAccountWithCurrency = ownAccountUSD }
        }));

var toChina = await tbcSoapCaller.GetDeserialized(
    new ImportSinglePaymentOrdersRequestIo(
        new TransferToOtherBankForeignCurrencyPaymentOrderIo( "China",
            // "ICBKCNBJSZN", "INDUSTRIAL AND COMMERCIAL BANK OF CHINA SHENZHEN BRANCH", "SHA", "Invoice(LZSK202311028)",
            "ICBKCNBJSZN", "INDUSTRIAL AND COMMERCIAL BANK OF CHINA SHENZHEN BRANCH", "SHA",
            BankAccountWithCurrencyV.Create(new BankAccountV("4000109819100186641"), CurrencyV.USD).Value)
        {
            TransferTypeRecordSpecific = transferTypeRecordSpecific with
            {
                SenderAccountWithCurrency = ownAccountUSD,
                BeneficiaryName = "Shenzhen Shinekoo Supply Chain Co.,Ltd"
            }
        }));

var toTreasury = await tbcSoapCaller.GetDeserialized(
    new ImportSinglePaymentOrdersRequestIo(
        new TreasuryTransferPaymentOrderIo(101001000)
            { TransferTypeRecordSpecific = transferTypeRecordSpecific }));
```
