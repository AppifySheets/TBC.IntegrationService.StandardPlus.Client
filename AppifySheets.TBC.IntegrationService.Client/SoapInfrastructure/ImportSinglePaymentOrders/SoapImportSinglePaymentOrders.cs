using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.ImportSinglePaymentOrders;

[UsedImplicitly]
public record SoapImportSinglePaymentOrders(TransferTypeRecord TransferType)
    : SoapBase(PerformedActionSoapEnvelope.TBCServiceAction.ImportSinglePaymentOrders)
{
    [StringSyntax(StringSyntaxAttribute.Xml)]
    public override string SoapXml
        => $"""
            <myg:ImportSinglePaymentOrdersRequestIo>
                <myg:singlePaymentOrder xsi:type="myg:{TransferType.GetType().Name}">
                   <!--optional<myg:singlePaymentRequestId>231</myg:singlePaymentRequestId>-->
                    {TransferType.Is<IRecipient>(b
                        => $"""
                            <myg:creditAccount>
                               <myg:accountNumber>{b.RecipientAccountWithCurrency.BankAccountNumber}</myg:accountNumber>
                               {TransferType.Is<TransferToOwnAccountPaymentOrderIo>(t
                                   => $"<myg:accountCurrencyCode>{b.RecipientAccountWithCurrency.CurrencyV}</myg:accountCurrencyCode>")}
                            </myg:creditAccount>
                            """)}
                      <myg:debitAccount>
                         <myg:accountNumber>{TransferType.SenderAccountWithCurrency.BankAccountNumber}</myg:accountNumber>
                         <myg:accountCurrencyCode>{TransferType.SenderAccountWithCurrency.CurrencyV}</myg:accountCurrencyCode>
                      </myg:debitAccount>
                      <myg:documentNumber>{TransferType.DocumentNumber}</myg:documentNumber>
                      <myg:amount>
                         <myg:amount>{TransferType.Amount}</myg:amount>
                         <myg:currency>{TransferType.SenderAccountWithCurrency.CurrencyV}</myg:currency>
                      </myg:amount>
                      <myg:position>3</myg:position>
                      <myg:additionalDescription>{TransferType.Is<IAdditionalDescription>(t => t.AdditionalDescription)}</myg:additionalDescription>
                      <myg:description>{TransferType.Is<IDescription>(t => t.Description)}</myg:description>
                      {TransferType.Is<IBeneficiaryName>(b => $"<myg:beneficiaryName>{b.BeneficiaryName}</myg:beneficiaryName>")}
                      {TransferType.Is<IBeneficiaryTaxCode>(b => $"<myg:beneficiaryTaxCode>{b.BeneficiaryTaxCode}</myg:beneficiaryTaxCode>")}
                      {TransferType.Is<IBeneficiaryForCurrencyTransfer>(b
                          => $"""
                              <myg:beneficiaryAddress>{b.BeneficiaryAddress}</myg:beneficiaryAddress>
                              <myg:beneficiaryBankCode>{b.BeneficiaryBankCode}</myg:beneficiaryBankCode>
                              <myg:beneficiaryBankName>{b.BeneficiaryBankName}</myg:beneficiaryBankName>
                              <myg:intermediaryBankCode />
                              <myg:intermediaryBankName />
                              <myg:chargeDetails>{b.ChargeDetails}</myg:chargeDetails>
                              """)}
                      {TransferType.Is<ITreasury>(b => $"<myg:treasuryCode>{b.TreasuryCode}</myg:treasuryCode>")}
                </myg:singlePaymentOrder>
            </myg:ImportSinglePaymentOrdersRequestIo>
                                        
            """;
}