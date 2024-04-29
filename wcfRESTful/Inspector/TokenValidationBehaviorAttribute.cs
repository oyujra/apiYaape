using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Web;

namespace wcfRESTful.Inspector
{
    public class TokenValidationBehaviorAttribute : Attribute, IServiceBehavior
    {
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase,
                                         System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            foreach (ChannelDispatcherBase channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = channelDispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                {
                    foreach (var endpointDispatcher in channelDispatcher.Endpoints)
                    {
                        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new TokenValidationInspector());
                    }
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}