using System;
using Editable.Host.Infrastructure;
using Editable.Host.Resolver.NullObject;
using Editable.Infrastructure;
using JetBrains.Annotations;
using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Syntax;

namespace Editable.Host.Resolver
{
    public class ModuleRegistration : NinjectModule
    {
        public override void Load()
        {
            Bind<IPluginLocator>().To<SingleImplementationPluginLocator>().InSingletonScope();
            Bind<IVoidMethodPlugin>().ToProvider<VoidMethodPluginProvider>();
            Bind(typeof(IVoidMethodPlugin<>)).ToMethod(
                ctx =>
                    ((IProvider)ctx.Kernel.Get(typeof(VoidMethodPluginProvider<>)
                        .MakeGenericType(ctx.GenericArguments))).Create(ctx));
            Bind(typeof (IMethodPlugin<>)).ToMethod(
                ctx =>
                    ((IProvider) ctx.Kernel.Get(typeof (MethodPluginProvider<>)
                        .MakeGenericType(ctx.GenericArguments))).Create(ctx));
            Bind(typeof(IMethodPlugin<,>)).ToMethod(
                ctx =>
                    ((IProvider)ctx.Kernel.Get(typeof(MethodPluginProvider<,>)
                        .MakeGenericType(ctx.GenericArguments))).Create(ctx));
            Bind(typeof (IMethodPlugin<,>)).ToProvider(typeof (MethodPluginProvider<,>));
            Bind<IPluginFactory>().ToFactory().InSingletonScope();
        }
    }

    internal abstract class PluginProvider<T> : Provider<T>
    {
        protected abstract Type DefaultPluginType { get; }

        private Type LocateTargetImplementation(IResolutionRoot resolutionRoot)
        {
            return resolutionRoot.Get<IPluginLocator>().LocateImplementation(typeof (T));
        }

        protected override T CreateInstance(IContext context)
        {
            return (T) context.Kernel.Get(LocateTargetImplementation(context.Kernel) ?? DefaultPluginType);
        }
    }

    [UsedImplicitly]
    internal class VoidMethodPluginProvider : PluginProvider<IVoidMethodPlugin>
    {
        protected override Type DefaultPluginType => typeof (DefaultVoidMethodPlugin);
    }

    [UsedImplicitly]
    internal class VoidMethodPluginProvider<T> : PluginProvider<IVoidMethodPlugin<T>>
    {
        protected override Type DefaultPluginType => typeof (DefaultVoidMethodPlugin<T>);
    }

    [UsedImplicitly]
    internal class MethodPluginProvider<TOut> : PluginProvider<IMethodPlugin<TOut>>
    {
        protected override Type DefaultPluginType => typeof (DefaultMethodPlugin<TOut>);
    }

    [UsedImplicitly]
    internal class MethodPluginProvider<TIn, TOut> : PluginProvider<IMethodPlugin<TIn, TOut>>
    {
        protected override Type DefaultPluginType => typeof (DefaultMethodPlugin<TIn, TOut>);
    }
}
