using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using AppifySheets.TBC.IntegrationService.Client.TBC_Services;
using CSharpFunctionalExtensions;

namespace AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure;

public abstract record SoapBase(PerformedActionSoapEnvelope.TBCServiceAction TBCServiceAction)
{
    [StringSyntax(StringSyntaxAttribute.Xml)] public abstract string SoapXml { get; }

    // public T DeserializeInto<T>(string responseXml)
    // {
    //     
    // }
    //public PerformedActionSoapEnvelope CreateTransferTemplate => TBCIntegrationServiceSoapSource.GetPerformedActionFor(TBCApiCredentials, TBCServiceAction, SoapXml);
}

// ReSharper disable once InconsistentNaming
public abstract record SoapBaseWithDeserializer<TDeserializeInto, TSoapBase>(TBCSoapCaller TBCSoapCaller) where TSoapBase : SoapBase
{
    
    protected abstract TSoapBase GetSoapBase();
    
    public virtual async Task<Result<TDeserializeInto>> GetDeserialized()
    {
        var response = await TBCSoapCaller.CallTBCServiceAsync(GetSoapBase());
        // ReSharper disable once ConvertIfStatementToReturnStatement
        if (response.IsFailure) return response.ConvertFailure<TDeserializeInto>();
        
        return response.Value.DeserializeInto<TDeserializeInto>();
    }
}

public static class DeserializationExtensions
{
    public static Result<T> DeserializeInto<T>(this string str)
        => str.Replace("ns2:", "")
            .Replace(@"<SOAP-ENV:Envelope xmlns:SOAP-ENV=""http://schemas.xmlsoap.org/soap/envelope/""><SOAP-ENV:Header/><SOAP-ENV:Body>", "")
            .Replace(@"</SOAP-ENV:Body></SOAP-ENV:Envelope>", "")
            .XmlDeserializeFromString<T>();

}