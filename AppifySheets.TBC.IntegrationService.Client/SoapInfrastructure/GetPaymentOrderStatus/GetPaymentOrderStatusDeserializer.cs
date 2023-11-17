using AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.ImportSinglePaymentOrders;
using AppifySheets.TBC.IntegrationService.Client.TBC_Services;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.GetPaymentOrderStatus;

public record GetPaymentOrderStatusDeserializer(TBCSoapCaller TBCSoapCaller, SoapGetPaymentOrderStatus SoapGetPaymentOrderStatus)
    : SoapBaseWithDeserializer<GetPaymentOrderStatusResponseIo, SoapGetPaymentOrderStatus>(TBCSoapCaller)
{
    protected override SoapGetPaymentOrderStatus GetSoapBase() => SoapGetPaymentOrderStatus;
}