using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Core;
using Dock.Model.Mvvm.Controls;

namespace HelloDock.ViewModels
{
    internal partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private IDock dockLayout;

        [ObservableProperty]
        private string? errorMessage;

        [ObservableProperty]
        private string? inputText;

        public MainWindowViewModel()
        {
            DockFactory factory = new(
                new DockedToolViewModel("hello-id", "Hello Tool", "Hello"),
                new DockedToolViewModel("dock-id", "Dock Tool", "Dock"));

            this.DockLayout = factory.CreateLayout();
            factory.InitLayout(this.DockLayout);
        }

        public Task CreateToolAsync()
        {
            if (!string.IsNullOrWhiteSpace(this.InputText))
            {
                this.ErrorMessage = string.Empty;

                string id = Guid.NewGuid().ToString();
                DockedToolViewModel newTool = new(id, $"Dynamic Tool {id}", this.InputText);

                if (this.DockLayout is IDock rootDock)
                {
                    IDock? toolsHost = FindFirstToolDock(rootDock);

                    if (toolsHost is { })
                    {
                        toolsHost.VisibleDockables ??= new List<IDockable>();
                        toolsHost.VisibleDockables.Add(newTool);
                        toolsHost.ActiveDockable = newTool;

                        this.ErrorMessage = $"Added to {toolsHost.Id}";
                    }
                    else
                    {
                        ToolDock newToolDock = new()
                        {
                            Id = $"{id}-dock",
                            Title = "More Tools",
                            Dock = DockMode.Right,
                            IsCollapsable = true,
                            Alignment = Alignment.Right,
                            VisibleDockables = [newTool],
                            ActiveDockable = newTool,
                        };

                        this.DockLayout.VisibleDockables!.Add(newToolDock);
                        this.DockLayout.ActiveDockable = newToolDock;

                        this.ErrorMessage = "Created a new ToolDock";
                    }
                }
            }
            else
            {
                this.ErrorMessage = "Some text must be provided.";
            }

            this.InputText = string.Empty;
            return Task.CompletedTask;
        }

        // We could also check dock.Id if we need to add to a specific dock.
        private static IDock? FindFirstToolDock(IDockable dockable)
        {
            if (dockable is ToolDock dock)
            {
                return dock;
            }

            if (dockable is IDock childDock && childDock.VisibleDockables is { })
            {
                foreach (IDockable child in childDock.VisibleDockables)
                {
                    IDock? found = FindFirstToolDock(child);

                    if (found is { })
                    {
                        return found;
                    }
                }
            }

            return null;
        }
    }
}
