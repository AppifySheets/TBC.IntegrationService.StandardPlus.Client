using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.GetPaymentOrderStatus;

[UsedImplicitly]
public record SoapGetPaymentOrderStatus(int SinglePaymentId)
    : SoapBase(PerformedActionSoapEnvelope.TBCServiceAction.GetPaymentOrderStatus)
{
    [StringSyntax(StringSyntaxAttribute.Xml)]
    public override string SoapXml
        => $"""
            <myg:GetPaymentOrderStatusRequestIo>
            	<myg:singlePaymentId>{SinglePaymentId}</myg:singlePaymentId>
            </myg:GetPaymentOrderStatusRequestIo>
            """;
}