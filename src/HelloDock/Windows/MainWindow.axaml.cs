using Avalonia.Controls;
using Avalonia.Input;
using HelloDock.ViewModels;

namespace HelloDock.Windows
{
    internal partial class MainWindow : Window
    {
        public MainWindow() => this.InitializeComponent();

        private async void OnInputKeyDownAsync(object sender, KeyEventArgs eventArgs)
        {
            if (this.DataContext is MainWindowViewModel viewModel)
            {
                Key keyPressed = eventArgs.Key;

                if (keyPressed == Key.Enter)
                {
                    await viewModel.CreateToolAsync().ConfigureAwait(false);
                }
                else if (keyPressed == Key.Escape)
                {
                    viewModel.InputText = string.Empty;
                }
            }
        }
    }
}
