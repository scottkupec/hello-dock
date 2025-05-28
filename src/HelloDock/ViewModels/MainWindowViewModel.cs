using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Core;

namespace HelloDock.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        public IDock DockLayout { get; }

        public MainWindowViewModel()
        {
            DockFactory factory = new(
                new DockedToolViewModel("hello-id", "Hello Tool", "Hello"),
                new DockedToolViewModel("dock-id", "Dock Tool", "Dock"));

            this.DockLayout = factory.CreateLayout();
            factory.InitLayout(this.DockLayout);
        }
    }
}
