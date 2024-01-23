using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Models
{
    public class ClienteModel{
        private int _id_cliente;
        private string _NomApell;
        private string _dni;
        private string? _fechaNacimiento;

        public ClienteModel()
        {
            _fechaNacimiento = null;
        }

        public int Id_cliente { 
            get => _id_cliente; 
            set{
                if (_id_cliente != value)
                {
                    _id_cliente = value;
                }
            } 
        }
        public string NomApell { 
            get => _NomApell;
            set {
                if (_NomApell != value)
                {
                    _NomApell = value;
                }
            } 
        }
        public string dni { 
            get => _dni;
            set {
                if (_dni != value)
                {
                    _dni = value;
                }
            } 
        }
        public string fechaNacimiento { 
            get => _fechaNacimiento;
            set {
                if (_fechaNacimiento != value)
                {
                    _fechaNacimiento = value;
                }
            } 
        }
    }
}