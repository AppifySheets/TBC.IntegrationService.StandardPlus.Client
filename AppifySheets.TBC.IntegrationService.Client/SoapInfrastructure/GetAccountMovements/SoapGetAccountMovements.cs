using System.Diagnostics.CodeAnalysis;
using AppifySheets.Immutable.BankIntegrationTypes;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.GetAccountMovements;

[UsedImplicitly]
public record SoapGetAccountMovements(Period Period, int PageIndex)
    : SoapBase(PerformedActionSoapEnvelope.TBCServiceAction.GetAccountMovements)
{
    [StringSyntax(StringSyntaxAttribute.Xml)]
    public override string SoapXml
        => $"""
                            <myg:GetAccountMovementsRequestIo>
                                <myg:accountMovementFilterIo>
                                    <myg:pager>
                                        <myg:pageIndex>{PageIndex}</myg:pageIndex>
                                        <myg:pageSize>700</myg:pageSize>
                                    </myg:pager>
                                    <myg:periodFrom>{Period.From:yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'}</myg:periodFrom>
                                    <myg:periodTo>{Period.Till:yyyy-MM-dd'T'HH:mm:ss.fffffff'Z'}</myg:periodTo>
                            </myg:accountMovementFilterIo>
                        </myg:GetAccountMovementsRequestIo>
            """;
}