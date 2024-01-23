using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Models
{
    class AnioModel
    {
        private int _id_anio;
        private string _anio;

        public int Id_anio { 
            get => _id_anio;
            set { 
                if (_id_anio != value)
                {
                    _id_anio = value;
                }
            } 
        }
        public string Anio { 
            get => _anio;
            set {
                if (value != _anio)
                {
                    _anio = value;
                }
            } 
        }
    }
}
