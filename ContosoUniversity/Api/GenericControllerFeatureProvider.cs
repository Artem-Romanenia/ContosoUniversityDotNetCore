using MediatR;
using MediatR.Extensions.Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ContosoUniversity.Api
{
    public class GenericControllerFeatureProvider : MediatrMvcFeatureProvider
    {
        public GenericControllerFeatureProvider(IServiceCollection services, Action<Settings> applySettings = null) : base(services, null, applySettings)
        {
        }

        protected override Type ProvideGenericControllerType(Type requestType)
        {
            if (requestType.Name == "Query") return typeof(GenericGetController<,>);
            else if (requestType.Name == "Command") return requestType.GetInterfaces().Any(i => i == typeof(IRequest)) ? typeof(GenericPostController<>) : typeof(GenericPostController<,>);
            else throw new Exception("Unknown request.");
        }
    }
}
