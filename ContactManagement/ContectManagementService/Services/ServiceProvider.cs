using Microsoft.Extensions.Configuration;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Remoting;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Client;
using ServiceFabric.Remoting.CustomHeaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagementService.Services
{
    public class ServiceProvider<TService> where TService: IService
    {
        public TService Service { get; }

        public ServiceProvider(IEnumerable<IConfigurationSection> configurationSections)
        {
            var serviceUrl = configurationSections.First(x => x.Key == typeof(TService).Name).Value;
            var uri = new Uri(serviceUrl);

            //Service = ServiceProxy.Create<TService>(uri);

            var proxyFactory = new ServiceProxyFactory(handler =>
                new ExtendedServiceRemotingClientFactory(
                    new FabricTransportServiceRemotingClientFactory(remotingCallbackMessageHandler: handler), new CustomHeaders()));

            Service = proxyFactory.CreateServiceProxy<TService>(uri);
        }
    }
}
