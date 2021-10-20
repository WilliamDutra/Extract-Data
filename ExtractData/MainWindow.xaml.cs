using ExtractData.Domain.Interfaces;
using ExtractData.Domain.Services;
using ExtractData.UI;
using ExtractData.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExtractData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var serviceCollection = new ServiceCollection();
            Startup.Register(serviceCollection);
            var Mysql = Startup.Container.GetService<IMySql>();

            DataContext = new MainWindowViewModel(Mysql);
            
        }
    }
}
