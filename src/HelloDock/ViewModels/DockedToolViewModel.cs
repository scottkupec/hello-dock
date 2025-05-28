using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Mvvm.Controls;

namespace HelloDock.ViewModels
{
    /// <summary>Hosts a <see cref="TextBox"/> as a dockable window.</summary>
    // If this is changed to derive from Document, then change DockFactory.CreateLayout()
    // to create DocumentDocks where it currently creates ToolDocks.
    internal partial class DockedToolViewModel : Tool
    {
        [ObservableProperty]
        private string content;

        public DockedToolViewModel(string id, string title, string text)
        {
            this.content = text;

            this.Title = title;
            this.Id = id;

            // If we let all the windows close, we can't get them back. In production, we
            // need to investigate how we'd allow them to be re-opened. For this sample,
            // we just disable closing of the controls.
            this.CanClose = false;

            // These are the defaults, but we set them explicitly for clarity.
            this.CanFloat = true;
            this.CanPin = true;
        }
    }
}
