using System;
using System.Collections.Generic;
using Dock.Avalonia.Controls;
using Dock.Model.Controls;
using Dock.Model.Core;
using Dock.Model.Mvvm;
using Dock.Model.Mvvm.Controls;

namespace HelloDock.ViewModels
{
    internal class DockFactory : Factory
    {
        private readonly DockedToolViewModel toolViewModel1;

        private readonly DockedToolViewModel toolViewModel2;

        public DockFactory(DockedToolViewModel viewModel1, DockedToolViewModel viewModel2)
        {
            this.toolViewModel1 = viewModel1;
            this.toolViewModel2 = viewModel2;
        }

        public IDock? Layout { get; private set; }

        private IRootDock? rootDock;

        public override IRootDock CreateLayout()
        {
            ToolDock leftToolDock = new()
            {
                Id = "LeftToolDock",
                Title = "Some Tools",
                Dock = DockMode.Left,
                IsCollapsable = true,
                Alignment = Alignment.Left,
                VisibleDockables = [this.toolViewModel1],
                ActiveDockable = this.toolViewModel1,
            };

            ToolDock rightToolDock = new()
            {
                Id = "RightToolDock",
                Title = "More Tools",
                Dock = DockMode.Right,
                IsCollapsable = true,
                Alignment = Alignment.Right,
                VisibleDockables = [this.toolViewModel2],
                ActiveDockable = this.toolViewModel2,
            };

            this.Layout = new ProportionalDock()
            {
                Orientation = Orientation.Horizontal,
                VisibleDockables = this.CreateList<IDockable>
                (
                    leftToolDock,
                    new ProportionalDockSplitter(),
                    rightToolDock
                )
            };

            this.rootDock = this.CreateRootDock();
            this.rootDock.Id = "Root";
            this.rootDock.IsCollapsable = true;
            this.rootDock.VisibleDockables = [this.Layout];
            this.rootDock.ActiveDockable = this.Layout;
            this.rootDock.DefaultDockable = this.Layout;

            return this.rootDock;
        }

        public override void InitLayout(IDockable layout)
        {
            this.HostWindowLocator = new Dictionary<string, Func<IHostWindow?>>
            {
                [nameof(IDockWindow)] = () => new HostWindow(),
            };

            base.InitLayout(layout);
        }
    }
}
