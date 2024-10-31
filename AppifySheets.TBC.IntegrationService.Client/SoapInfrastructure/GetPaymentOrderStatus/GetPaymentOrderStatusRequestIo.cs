using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.GetPaymentOrderStatus;

[UsedImplicitly]
public record GetPaymentOrderStatusRequestIo(int SinglePaymentId)
    : RequestSoap<GetPaymentOrderStatusResponseIo>
{
    [StringSyntax(StringSyntaxAttribute.Xml)]
    public override string SoapXml
        => $"""
            <myg:GetPaymentOrderStatusRequestIo>
            	<myg:singlePaymentId>{SinglePaymentId}</myg:singlePaymentId>
            </myg:GetPaymentOrderStatusRequestIo>
            """;

    public override TBCServiceAction TBCServiceAction => TBCServiceAction.GetPaymentOrderStatus;
}