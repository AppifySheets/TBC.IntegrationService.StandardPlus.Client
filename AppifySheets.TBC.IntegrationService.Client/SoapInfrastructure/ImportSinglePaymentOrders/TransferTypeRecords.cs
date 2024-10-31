using AppifySheets.Immutable.BankIntegrationTypes;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.ImportSinglePaymentOrders;

public record TransferWithinBankPaymentOrderIo : TransferTypeRecord, IDescription, IAdditionalDescription, IRecipient
{
    public string? AdditionalDescription { get; init; }
    public required string? Description { get; init; }
    public required BankAccountWithCurrencyV RecipientAccountWithCurrency { get; init; }
}

public record TransferToOtherBankForeignCurrencyPaymentOrderIo(
    string AdditionalDescription,
    string BeneficiaryAddress,
    string BeneficiaryBankCode,
    string BeneficiaryBankName,
    string ChargeDetails,
    string Description,
    BankAccountWithCurrencyV RecipientAccountWithCurrency)
    : TransferTypeRecord, IRecipient, IAdditionalDescription, IDescription, IBeneficiaryForCurrencyTransfer;

public record TransferToOtherBankNationalCurrencyPaymentOrderIo(BankAccountWithCurrencyV RecipientAccountWithCurrency, string Description, string AdditionalDescription, string BeneficiaryTaxCode)
    : TransferTypeRecord, IRecipient, IDescription, IAdditionalDescription, IBeneficiaryTaxCode;

public record TreasuryTransferPaymentOrderIo(long TreasuryCode, string AdditionalDescription)
    : TransferTypeRecord, ITreasury, IAdditionalDescription;

[UsedImplicitly]
public record TransferToOwnAccountPaymentOrderIo(BankAccountWithCurrencyV RecipientAccountWithCurrency, string Description) : TransferTypeRecord, IRecipient, IDescription;