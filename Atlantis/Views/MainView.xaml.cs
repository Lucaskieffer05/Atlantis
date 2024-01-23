using Atlantis.ControlUsuario;
using Atlantis.Models;
using Atlantis.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Data;
using System.Runtime.InteropServices;

namespace Atlantis.Views
{
    /// <summary>
    /// Lógica de interacción para MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {

        public MainView()
        {
            
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener el cliente seleccionado
            PagoModel clienteSeleccionado = (PagoModel)clientesDataGrid.SelectedItem;

            MainViewModel viewModel = DataContext as MainViewModel;
            // Ejecutar el comando en el ViewModel
            if (clienteSeleccionado != null)
            {
                viewModel.ClienteSeleccionado = clienteSeleccionado;
                viewModel.MostrarDetallesCommand.Execute(clienteSeleccionado);
            }
        }

        

    }

}

