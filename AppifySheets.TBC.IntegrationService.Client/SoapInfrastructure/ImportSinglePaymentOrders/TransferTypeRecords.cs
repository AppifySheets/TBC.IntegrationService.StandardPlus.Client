using AppifySheets.Immutable.BankIntegrationTypes;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.ImportSinglePaymentOrders;

public record TransferWithinBankPaymentOrderIo(string AdditionalDescription, string Description, BankAccountWithCurrencyV RecipientAccountWithCurrency)
    : TransferTypeRecord, IBeneficiaryName, IDescription, IAdditionalDescription, IRecipient;

public record TransferToOtherBankForeignCurrencyPaymentOrderIo(string AdditionalDescription, string BeneficiaryAddress, string BeneficiaryBankCode, string BeneficiaryBankName, 
        string ChargeDetails, string Description, BankAccountWithCurrencyV RecipientAccountWithCurrency)
    : TransferTypeRecord, IRecipient, IAdditionalDescription, IDescription, IBeneficiaryForCurrencyTransfer;

public record TransferToOtherBankNationalCurrencyPaymentOrderIo(BankAccountWithCurrencyV RecipientAccountWithCurrency, string Description, string AdditionalDescription, string BeneficiaryTaxCode)
    : TransferTypeRecord, IRecipient, IDescription, IAdditionalDescription, IBeneficiaryTaxCode;

public record TreasuryTransferPaymentOrderIo(long TreasuryCode, string AdditionalDescription)
    : TransferTypeRecord, ITreasury, IAdditionalDescription;

[UsedImplicitly]
public record TransferToOwnAccountPaymentOrderIo(BankAccountWithCurrencyV RecipientAccountWithCurrency, string Description) : TransferTypeRecord, IRecipient, IDescription;