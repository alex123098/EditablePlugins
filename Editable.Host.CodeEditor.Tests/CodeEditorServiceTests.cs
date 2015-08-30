using System;
using Editable.Host.CodeEditor.View;
using Editable.Host.CodeEditor.ViewModel;
using Editable.Host.Infrastructure;
using FluentAssertions;
using Moq;
using Xunit;

namespace Editable.Host.CodeEditor.Tests
{
    public class CodeEditorServiceTests
    {
        [Fact]
        public void Ctor_GivenTheEmptyDepency_ShouldThrowException()
        {
            ((Action) (() => new CodeEditorService(null, null)))
                .ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Should().Be("codeEditorViewModel");
            ((Action)(() => new CodeEditorService(Mock.Of<ICodeEditorViewModel>(), null)))
                .ShouldThrow<ArgumentNullException>()
                .Which.ParamName.Should().Be("windowFactory");
            ((Action)(() => new CodeEditorService(Mock.Of<ICodeEditorViewModel>(), Mock.Of<ICodeEditorWindowFactory>())))
                .ShouldNotThrow();
        }

        [Fact]
        public void EditPluginCode_ShouldReturnChangesMade_WhenChangesWasConfirmed()
        {
            const string initialSource = "namespace test {}";
            const string editedSource = "namespace foo {}";
            var builder = new CodeEditorServiceBuilder()
                .WithDialogReault(CommonDialogResult.Ok);
            Mock.Get(builder.ViewModel).SetupGet(vm => vm.SourceCode).Returns(editedSource);
            var codeEditor = builder.Build();

            var result = codeEditor.EditPluginCode(initialSource);

            result.Should().Be(editedSource);
        }

        [Fact]
        public void EditPluginCode_ShouldReturnInitialVersion_WhenChangesHaveBeenRejected()
        {
            const string initialSource = "namespace test {}";
            const string editedSource = "namespace foo {}";
            var builder = new CodeEditorServiceBuilder()
                .WithDialogReault(CommonDialogResult.Cancel);
            Mock.Get(builder.ViewModel).SetupGet(vm => vm.SourceCode).Returns(editedSource);
            var codeEditor = builder.Build();

            var result = codeEditor.EditPluginCode(initialSource);

            result.Should().Be(initialSource);
        }
    }

    internal class CodeEditorServiceBuilder
    {
        private readonly Mock<IDialogWindow> _windowMock;

        internal ICodeEditorViewModel ViewModel { get; }

        internal ICodeEditorWindowFactory DialogFactory { get; }

        public CodeEditorServiceBuilder()
        {
            ViewModel = Mock.Of<ICodeEditorViewModel>();
            _windowMock = new Mock<IDialogWindow>();
            DialogFactory = Mock.Of<ICodeEditorWindowFactory>(
                factory => factory.CreateDialogFor(It.IsAny<ICodeEditorViewModel>()) == _windowMock.Object);
        }

        public CodeEditorServiceBuilder WithDialogReault(CommonDialogResult result)
        {
            _windowMock.Setup(w => w.Execute()).Returns(result);
            return this;
        }

        public CodeEditorService Build() => new CodeEditorService(ViewModel, DialogFactory);
    }
}
