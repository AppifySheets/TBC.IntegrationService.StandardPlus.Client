using System.Threading.Tasks;
using AppifySheets.TBC.IntegrationService.Client.SoapInfrastructure;
using CSharpFunctionalExtensions;

namespace AppifySheets.TBC.IntegrationService.Client.TBC_Services;

public static class Worker
{
    public static Task<Result<TDeserializeInto>> GetDeserialized<TSoapBase, TDeserializeInto>(SoapBaseWithDeserializer<TDeserializeInto, TSoapBase> soapBaseWithDeserializer)
        where TSoapBase : SoapBase => soapBaseWithDeserializer.GetDeserialized();
}