using Atlantis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atlantis.Views;
using System.Windows.Input;
using Atlantis.DataBase;

namespace Atlantis.ViewModels
{
    internal class AgregarClienteViewModel : ViewModelBase
    {
        private ICommand? _agregarClienteCommand;
        private PagoModel? _pago;
        private readonly BD dataBase;
        private int _id_anio;
        private string _errorDNI;
        private string _errorNomApell;
        public Action CloseAction { get; set; }
        public AgregarClienteViewModel(int id_anio)
        {
            dataBase = new BD();
            _id_anio = id_anio;
            Pago = new PagoModel();
        }

        public string ErrorNomApell
        {
            get { return _errorNomApell; }
            set
            {
                if (_errorNomApell != value)
                {
                    _errorNomApell = value;
                    OnPropertyChanged(nameof(ErrorNomApell));
                }
            }
        }

        public string ErrorDNI
        {
            get { return _errorDNI; }
            set
            {
                if(_errorDNI != value)
                {
                    _errorDNI = value;
                    OnPropertyChanged(nameof(ErrorDNI));
                }
            }
        }

        public PagoModel Pago{ 
            get => _pago; 
            set => _pago = value; 
        }
        public ICommand? AgregarClienteCommand {
            get { 
                if( _agregarClienteCommand == null)
                {
                    _agregarClienteCommand = new ViewModelCommand(AgregarCliente);
                }
                return _agregarClienteCommand;
            }
            set => _agregarClienteCommand = value; 
        }

        private void AgregarCliente(object? parametro)
        {
            if (!string.IsNullOrWhiteSpace(Pago.Cliente.dni) && !string.IsNullOrWhiteSpace(Pago.Cliente.NomApell))
            {
                dataBase.AgregarCliente(Pago, _id_anio);
                CloseAction?.Invoke();
            }
            else
            {
                ErrorDNI = "";
                ErrorNomApell = "";
                if (string.IsNullOrWhiteSpace(Pago.Cliente.dni)) { ErrorDNI = "* DNI no válido"; }
                if (string.IsNullOrWhiteSpace(Pago.Cliente.NomApell)) { ErrorNomApell = "* Nombre y Apellido no válido"; }
            }
        }

    }
}
