using Atlantis.ViewModels;
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
using System.Windows.Shapes;

namespace Atlantis.Views
{
    /// <summary>
    /// Lógica de interacción para AgregarClienteVIew.xaml
    /// </summary>
    public partial class AgregarClienteVIew : Window
    {
        public AgregarClienteVIew(int idAnioSeleccionado)
        {
            InitializeComponent();

            AgregarClienteViewModel viewmodel = new AgregarClienteViewModel(idAnioSeleccionado);
            viewmodel.CloseAction = this.Close;

            DataContext = viewmodel;

            //Owner = Application.Current.MainWindow;
           // Owner.IsEnabled = false;

            //Closed += AgregarClienteView_Closed;
        }
        //private void AgregarClienteView_Closed(object sender, System.EventArgs e)
        //{
            // Habilitar la ventana principal al cerrar la nueva ventana
        //    Owner.IsEnabled = true;
        //}

        private void CerrarVentana(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana actual
            Close();
        }
    }
}
