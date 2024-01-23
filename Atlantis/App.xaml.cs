using System.Configuration;
using System.Data;
using System.Windows;
using Atlantis.Views;

namespace Atlantis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void IniciarAplicacion(object sender, StartupEventArgs e)
        {
            var main = new MainView();
            main.Show();

        }
    }

}
