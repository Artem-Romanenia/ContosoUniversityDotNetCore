using MediatR.Extensions.Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ContosoUniversity.Api
{
    public class GenericConvention : MediatrMvcConvention
    {
        public static string DeriveFeatureFolderName(ControllerModel model)
        {
            var @namespace = model.ControllerType.IsGenericType ? model.ControllerType.GenericTypeArguments[0].Namespace : model.ControllerType.Namespace;
            var result = @namespace.Split('.')
                .SkipWhile(s => s != "Features")
                .Aggregate("", Path.Combine);

            return result;
        }

        protected override string ProvideControllerName(Type requestType)
        {
            return requestType.Namespace.Split('.').Last();
        }

        protected override string ProvideActionName(Type requestType, MethodInfo action)
        {
            return requestType.DeclaringType.Name;
        }
    }
}
