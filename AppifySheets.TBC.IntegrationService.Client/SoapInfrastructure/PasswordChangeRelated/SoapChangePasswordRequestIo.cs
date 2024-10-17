using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure.PasswordChangeRelated;

[UsedImplicitly]
public record SoapChangePasswordRequestIo(string NewPassword)
    : SoapBase(PerformedActionSoapEnvelope.TBCServiceAction.ChangePassword)
{
    [StringSyntax(StringSyntaxAttribute.Xml)]
    public override string SoapXml
        => $"""
            <myg:ChangePasswordRequestIo>
               <myg:newPassword>{NewPassword}</myg:newPassword>
             </myg:ChangePasswordRequestIo>
            """;
}