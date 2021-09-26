using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System.Windows;
using Workflows.Shared.Contracts;

namespace Workflows.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IPluginService _pluginService = Ioc.Default.GetRequiredService<IPluginService>();


        public MainWindow()
        {
            InitializeComponent();

        }
    }
}
