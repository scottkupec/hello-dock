# Hello, Dock!

A simple "Hello, World" example using Dock.Avalonia.

Demonstrates a simple use of Dock.Avalonia to help me understand how to integrate it into larger apps.

Key work for adding Dock.Avalonia to an existing Avalonia app and hosting docked windows:
1. Add Dock.Avalonia and Dock.Model.Mvvm packages.
1. Update App.axaml to include DockFluentTheme
1. Create a View and associated ViewModel for the window content to be hosted as a docked window.
   1. Tool style windows should derive from Dock.Model.Mvvm.Controls.Tool
   1. Document style windows should derive from Dock.Model.Mvvm.Controls.Document
1. Don't skip registering the DataTemplate in App.axaml.
   1. Alternately, add a custom ViewLocator that can do the resolution.
1. Create a factory class based on Dock.Model.Mvvm.Factory.
1. In the View hosting the Dock control, add
   `<dock:DockControl x:Name="Dock" />`
1. In the code-behind for the view, assign the layout created by the factory to Dock.Layout.
