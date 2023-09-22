using AppifySheets.TBC.IntegrationService.Client.TBC_Services;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.ImportSinglePaymentOrders;

public record ImportSinglePaymentOrdersDeserializer(TBCSoapCaller TBCSoapCaller, SoapImportSinglePaymentOrders ImportSinglePaymentOrders)
    : SoapBaseWithDeserializer<ImportSinglePaymentOrdersResponseIo, SoapImportSinglePaymentOrders>(TBCSoapCaller)
{
    protected override SoapImportSinglePaymentOrders GetSoapBase() => ImportSinglePaymentOrders;
}