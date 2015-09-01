using System;
using System.Reflection;
using Editable.Host.Infrastructure;
using FluentAssertions;
using Microsoft.CodeAnalysis;
using Moq;
using Xunit;

namespace Editable.Host.Compilation.Tests
{
    public class PluginCompilerTests
    {
        [Fact]
        public void Ctor_GivenEmptyDependency_ShouldThrowException()
        {
            ((Action) (() => new PluginCompiler(null)))
                .ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Should().Be("assemblyReferenceProvider");
            ((Action)(() => new PluginCompiler(Mock.Of<IAssemblyReferenceProvider>())))
                .ShouldNotThrow();
        }

        [Fact]
        public void CompileFrom_GivenEmptyCode_ShouldThrowException()
        {
            var compiler = new PluginCompiler(Mock.Of<IAssemblyReferenceProvider>());

            compiler.Invoking(c => c.CompileFrom(null)).ShouldThrow<ArgumentException>();
            compiler.Invoking(c => c.CompileFrom(string.Empty)).ShouldThrow<ArgumentException>();
        }

        [Fact]
        public void CompileFrom_GivenCompilableSourceCode_ShouldProvideGeneratedAssembly()
        {
            const string sourceCode = @"namespace Test { public class Foo { public void Bar() { System.Console.WriteLine(""Bar."");} } }";
            var mscorlibReference = MetadataReference.CreateFromFile(typeof (int).Assembly.Location);
            var compiler = new PluginCompiler(Mock.Of<IAssemblyReferenceProvider>(provider 
                => provider.CollectMetadataReferences(It.IsAny<Assembly>()) == mscorlibReference.AsEnumerable()));

            var result = compiler.CompileFrom(sourceCode);
            result.Should().NotBeNull();
            var assembly = Assembly.Load(result);

            assembly.Should().DefineType("Test", "Foo").And.Reference(typeof(string).Assembly);
        }

        [Fact]
        public void CompileFrom_GivenSourceCodeWithErrors_ShouldThrowException()
        {
            const string sourceCode = @"namespace Test { public class Foo { public void Bar() { System.Console.WriteLine(""Bar."") } } }";
            var mscorlibReference = MetadataReference.CreateFromFile(typeof(int).Assembly.Location);
            var compiler = new PluginCompiler(Mock.Of<IAssemblyReferenceProvider>(provider
                => provider.CollectMetadataReferences(It.IsAny<Assembly>()) == mscorlibReference.AsEnumerable()));

            compiler.Invoking(c => c.CompileFrom(sourceCode)).ShouldThrow<PluginCompilationException>();
        }
    }
}
