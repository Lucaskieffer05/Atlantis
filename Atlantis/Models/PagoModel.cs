using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlantis.Models
{
    public class PagoModel
    {
        private ClienteModel _cliente;
        private AnioModel _anio;

        public PagoModel()
        {
            _cliente=new ClienteModel();
            _anio=new AnioModel();
        }

        public int Id_cliente { get; set; }
        public int Id_anio { get; set; }
        public string? Enero { get; set; }
        public string? Febrero { get; set; }
        public string? Marzo { get; set; }
        public string? Abril { get; set; }
        public string? Mayo { get; set; }
        public string? Junio { get; set; }
        public string? Julio { get; set; }
        public string? Agosto { get; set; }
        public string? Septiembre { get; set; }
        public string? Octubre { get; set; }
        public string? Noviembre { get; set; }
        public string? Diciembre { get; set; }
        public string? Seguro { get; set; }
        public ClienteModel Cliente { get => _cliente; set => _cliente = value; }
        internal AnioModel Anio { get => _anio; set => _anio = value; }
    }
}
