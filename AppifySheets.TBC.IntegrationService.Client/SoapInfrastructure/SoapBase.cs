using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using System.Xml;
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
    {
        var doc = new XmlDocument();
        doc.LoadXml(str);
        var bodyNode = doc.SelectSingleNode("//SOAP-ENV:Body", GetNamespaceManager(doc));
        if (bodyNode == null) return Result.Failure<T>("bodyNode was null");
        
        return bodyNode.InnerXml.Replace("ns2:","")
            .XmlDeserializeFromString<T>();

        static XmlNamespaceManager GetNamespaceManager(XmlDocument doc)
        {
            var nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("SOAP-ENV", "http://schemas.xmlsoap.org/soap/envelope/");
            nsManager.AddNamespace("ns2", "http://www.mygemini.com/schemas/mygemini");
            return nsManager;
        }
    }
}