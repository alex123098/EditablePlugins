using System;
using Ninject;
using Ninject.Activation;
using Ninject.Syntax;

namespace Editable.Host.Resolver
{
    public static class ResolutionRootExtensions
    {
        public static IBindingWhenInNamedWithOrOnSyntax<object> ToOpenGenericProvider(
            this IBindingToSyntax<object> @this,
            Type providerType)
        {
            if (@this == null)
            {
                throw new ArgumentNullException(nameof(@this));
            }
            if (providerType == null)
            {
                throw new ArgumentNullException(nameof(providerType));
            }
            if (!providerType.IsGenericTypeDefinition)
            {
                throw new ArgumentException("Provider type should be open generic type definition.", nameof(providerType));
            }
            return @this.ToMethod(
                ctx =>
                    CreateProviderFromOpenGeneric(ctx, providerType));
        }

        private static object CreateProviderFromOpenGeneric(IContext ctx, Type providerType)
        {
            var provider = (IProvider) ctx.Kernel.Get(providerType.MakeGenericType(ctx.GenericArguments));
            return provider.Create(ctx);
        }
    }
}
