using Atlantis.DataBase;
using Atlantis.Models;
using Atlantis.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Atlantis.ViewModels
{
    internal class DetalleClienteViewModel : ViewModelBase
    {
        private ICommand? _volvermainCommand;
        private ICommand? _eliminarClienteCommand;
        private ICommand? _guardarcambiosCommand;
        private readonly BD dataBase;
        public event EventHandler<EventArgs> volver;
        public event EventHandler<EventArgs> volverSinCambios;
        private Visibility _mostrarDetalles;
        private string _errornomapell;
        private PagoModel _clienteseleccionado;
        private int _id_anio;
        private string _errorDNI;

        public DetalleClienteViewModel()
        {
            dataBase = new BD();
            _mostrarDetalles = Visibility.Collapsed;
            _errornomapell = "";
            _errorDNI = "";
        }


        public Visibility MostrarDetalles
        {
            get { return _mostrarDetalles; }
            set
            {
                if (_mostrarDetalles != value)
                {
                    _mostrarDetalles = value;
                    OnPropertyChanged(nameof(MostrarDetalles));
                }
            }
        }

        public string ErrorNomApell
        {
            get { return _errornomapell; }
            set
            {
                if (_errornomapell != value)
                {
                    _errornomapell = value;
                    OnPropertyChanged(nameof(ErrorNomApell));
                }
            }
        }

        public string ErrorDNI
        {
            get { return _errorDNI; }
            set
            {
                if (_errorDNI != value)
                {
                    _errorDNI = value;
                    OnPropertyChanged(nameof(ErrorDNI));
                }
            }
        }

        public ICommand VolverMainCommand
        {
            get
            {
                if (_volvermainCommand == null)
                {
                    _volvermainCommand = new ViewModelCommand(VolverMain);
                }
                return _volvermainCommand;
            }
        }

        public ICommand EliminarClienteComand
        {
            get
            {
                if(_eliminarClienteCommand == null)
                {
                    _eliminarClienteCommand = new ViewModelCommand(EliminarCliente);
                }
                return _eliminarClienteCommand;
            }
        }
        public ICommand? GuardarcambiosCommand { 
            get {
                if (_guardarcambiosCommand == null)
                {
                    _guardarcambiosCommand = new ViewModelCommand(GuardarCambios);
                }
                return _guardarcambiosCommand;
            } 
        }


        public PagoModel ClienteSeleccionado { 
            get { return _clienteseleccionado; }
            set {
                _clienteseleccionado = value;
                OnPropertyChanged(nameof(ClienteSeleccionado));
            } 
        }
        public int Id_anio { get => _id_anio; set => _id_anio = value; }


        private void VolverMain(object? parametro)
        {
            ErrorNomApell = "";
            ErrorDNI = "";
            MostrarDetalles = Visibility.Collapsed;
            volverSinCambios?.Invoke(this,EventArgs.Empty);

        }

        private void EliminarCliente(object? parametro)
        {
            // Mostrar un cuadro de diálogo de confirmación
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de que deseas eliminar este cliente?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            // Verificar la respuesta del usuario
            if (result == MessageBoxResult.Yes)
            {
                ErrorNomApell = "";
                ErrorDNI = "";
                dataBase.EliminarCliente(ClienteSeleccionado, Id_anio);
                MostrarDetalles = Visibility.Collapsed;
                volver?.Invoke(this, EventArgs.Empty);
            }
        }

        private void GuardarCambios(object? obj)
        {
            if(ClienteSeleccionado.Cliente.NomApell != "" && ClienteSeleccionado.Cliente.dni != "")
            {
                ErrorNomApell = "";
                ErrorDNI = "";
                dataBase.GuardarCambios(ClienteSeleccionado, Id_anio);
                MostrarDetalles = Visibility.Collapsed;
                volver?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                ErrorNomApell = "";
                ErrorDNI = "";
                if (ClienteSeleccionado.Cliente.dni == "") { ErrorDNI = "* DNI no valido"; }
                if (ClienteSeleccionado.Cliente.NomApell == "") { ErrorNomApell = "* Nombre y Apellido no valido"; }
            }
            
        }

    }
}
