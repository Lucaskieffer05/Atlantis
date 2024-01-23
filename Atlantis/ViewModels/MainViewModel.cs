using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Atlantis.DataBase;
using Atlantis.Models;
using Atlantis.Views;
using Xceed.Words.NET;
using Excel = Microsoft.Office.Interop.Excel;
using Range = Microsoft.Office.Interop.Excel.Range;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop;
using ExcelDataReader;
using System.Data;
using System.Runtime.InteropServices;
using System.Collections;
using System.Reflection;
using System.Threading;

namespace Atlantis.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private ICommand? _abrirAgregarClienteCommand;
        private ICommand? _mostrardetallescommand;
        private ICommand? _importarClientesCommand;
        private ICommand? _exportarGrillaCommand;
        private ICommand? _exportarExelCommand;
        private readonly BD dataBase;
        private ObservableCollection<PagoModel> _pagos;
        private PagoModel _pago;
        private ObservableCollection<AnioModel> _anios;
        private AnioModel _anioSeleccionado;
        private PagoModel _clienteSeleccionado;
        private string _buscarcliente;
        private DetalleClienteViewModel _detallesClienteViewModel;


        public MainViewModel()
        {
            dataBase = new BD();
            _pago = new PagoModel();
            _anios = dataBase.ObstenerAnios();
            string nowAnio  = DateTime.Now.Year.ToString();
            AnioModel? anioEncontrado = _anios.FirstOrDefault(anio => anio.Anio == nowAnio);
            _anioSeleccionado = anioEncontrado;
            _pagos = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio);
            _detallesClienteViewModel = new DetalleClienteViewModel();
            DetallesClienteViewModel.volver += volvio;
            DetallesClienteViewModel.volverSinCambios += volvioSinCambios;
            _buscarcliente = "";
        }

        private void volvio(object? sender, EventArgs e)
        {
            ClienteSeleccionado = new PagoModel();
            Pagos = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio);
        }

        private void volvioSinCambios(object? sender, EventArgs e)
        {
            ClienteSeleccionado = new PagoModel();
        }

        public PagoModel Pago {
            get { return _pago; }
            set {
                if (_pago != value) 
                {
                    _pago = value;
                    OnPropertyChanged(nameof(Pago));
                }
            }
        }

        public DetalleClienteViewModel DetallesClienteViewModel
        {
            get { return _detallesClienteViewModel; }
            set
            {
                if (_detallesClienteViewModel != value)
                {
                    _detallesClienteViewModel = value;
                    OnPropertyChanged(nameof(DetallesClienteViewModel));
                }
            }
        }

        public ObservableCollection<PagoModel> Pagos { 
            get { return _pagos; }
            set
            {
                if(_pagos != value)
                {
                    _pagos = value;
                    OnPropertyChanged(nameof(Pagos));
                }
            }
        }
        public PagoModel ClienteSeleccionado { get => _clienteSeleccionado; set => _clienteSeleccionado = value; }

        public ObservableCollection<AnioModel> Anios { 
            get => _anios;
            set { 
                if(_anios != value)
                {
                    _anios = value;
                }
            } 
        }

        public string Buscarcliente { 
            get => _buscarcliente;
            set {
                if (_buscarcliente != value)
                {
                    _buscarcliente = value;
                    if (_buscarcliente != "")
                    {
                        OnPropertyChanged(nameof(Buscarcliente));
                        Pagos = dataBase.FiltrarClientes(Buscarcliente, AnioSeleccionado.Id_anio);
                    }
                    else
                    {
                        OnPropertyChanged(nameof(Buscarcliente));
                        Pagos = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio);
                    }
                }
            }
        }

        public AnioModel AnioSeleccionado { 
            get => _anioSeleccionado;
            set {
                if (_anioSeleccionado != value)
                {
                    _anioSeleccionado = value;
                    OnPropertyChanged(nameof(AnioSeleccionado));
                    Pagos = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio);
                    
                }
            } 
        }

        public ICommand AbrirAgregarClienteCommand
        {
            get
            {
                if (_abrirAgregarClienteCommand == null)
                {
                    _abrirAgregarClienteCommand = new ViewModelCommand(AbrirAgregarCliente);
                }
                return _abrirAgregarClienteCommand;
            }
        }

        public ICommand MostrarDetallesCommand {
            get
            {
                if (_mostrardetallescommand == null)
                {
                    _mostrardetallescommand = new ViewModelCommand(MostrarDetallesClientes);
                }
                return _mostrardetallescommand;
            } 
            
        }

        public ICommand ImportarClientesCommand
        {
            get
            {
                if (_importarClientesCommand == null)
                {
                    _importarClientesCommand = new ViewModelCommand(ImportarClientes);
                }
                return _importarClientesCommand;
            }

        }

        public ICommand ExportarGrillaCommand
        {
            get
            {
                if (_exportarGrillaCommand == null)
                {
                    _exportarGrillaCommand = new ViewModelCommand(ExportarGrilla);
                }
                return _exportarGrillaCommand;
            }

        }

        public ICommand ExportarExelCommand
        {
            get
            {
                if (_exportarExelCommand == null)
                {
                    _exportarExelCommand = new ViewModelCommand(ExportarExcel);
                }
                return _exportarExelCommand;
            }

        }

        private async void ExportarGrilla(object obj)
        {
            if (obj is DataGrid dataGrid)
            {
                var exportar = new ProgresarView();
                exportar.Show();
                string rutaCompleta = "Sin ruta";

                // Obtén el ScrollViewer de la grilla
                var scrollViewer = GetScrollViewer(dataGrid);

                // Obtén el tamaño total de la grilla
                double scrollableHeight = scrollViewer.ScrollableHeight;
                var totalRowCount = dataGrid.Items.Count;

                var filas_visibles = totalRowCount - scrollableHeight;

                // Define la distancia a desplazarse en cada salto
                double saltoTotal = totalRowCount;
                double saltoScroll = filas_visibles;

                // Inicia un bucle para realizar saltos de scroll y tomar capturas
                for (double posicionActual = 0; posicionActual < saltoTotal; posicionActual += saltoScroll)
                {
                    // Establece la posición del ScrollViewer
                    scrollViewer.ScrollToVerticalOffset(posicionActual);

                    // Espera un tiempo para permitir que el ScrollViewer termine de desplazarse
                    await Task.Delay(1500); // Ajusta el tiempo de espera según sea necesario

                    // Captura de pantalla en la posición actual
                    rutaCompleta = CapturarPantalla(dataGrid);
                }
                MessageBox.Show("Captura de pantalla guardada en el escritorio: " + rutaCompleta);
                exportar.Close();
            }
        }
        public static ScrollViewer GetScrollViewer(DependencyObject depObj)
        {
            if (depObj is ScrollViewer scrollViewer)
            {
                return scrollViewer;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = GetScrollViewer(child);
                if (result != null)
                    return result;
            }

            return null;
        }

        private string CapturarPantalla(FrameworkElement ventanaTemporal)
        {
            // Reemplaza 'TuFrameworkElement' con el elemento que deseas capturar
            FrameworkElement elementoACapturar = ventanaTemporal;

            // Crear RenderTargetBitmap con el tamaño del elemento a capturar
            RenderTargetBitmap rtb = new RenderTargetBitmap(
                (int)elementoACapturar.ActualWidth,
                (int)elementoACapturar.ActualHeight,
                90, // dpi X
                75, // dpi Y
                PixelFormats.Pbgra32);

            // Renderizar el elemento a capturar en el RenderTargetBitmap
            rtb.Render(elementoACapturar);

            // Crear un codificador de imagen y guardar la captura en un archivo
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            // Nombre del archivo (puedes cambiarlo según tus necesidades)
            string nombreArchivo = "CapturaPantalla_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".png";

            // Ruta completa del archivo
            string rutaCompleta = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), nombreArchivo);

            // Guardar la captura en el archivo
            using (FileStream fs = File.OpenWrite(rutaCompleta))
            {
                encoder.Save(fs);
            }

            return rutaCompleta;
            
        }


        private void AbrirAgregarCliente(object? parametro)
        {
            AgregarClienteVIew otraVentana = new AgregarClienteVIew(AnioSeleccionado.Id_anio);
            otraVentana.ShowDialog();
            Pagos = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio);

        }

        private void MostrarDetallesClientes(object? parametro)
        {

            if (ClienteSeleccionado != null)
            {
                DetallesClienteViewModel.ClienteSeleccionado = ClienteSeleccionado;
                DetallesClienteViewModel.Id_anio = AnioSeleccionado.Id_anio;
                DetallesClienteViewModel.MostrarDetalles = Visibility.Visible;
            }

        }
        private void ImportarClientes(object obj)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro que quieres importar los clientes del año anterior?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                ObservableCollection<PagoModel> pagados = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio - 1);
                dataBase.ImportarClientes(pagados, AnioSeleccionado.Id_anio);
                Pagos = dataBase.ObtenerClientes(AnioSeleccionado.Id_anio);
            }

            
        }

        private async void ExportarExcel(object obj)
        {
            if (obj is DataGrid dataGrid)
            {
                ProgresarView ventana = new ProgresarView();
                ventana.Show();
                await Task.Delay(1000);


                Excel.Application excel = new Excel.Application();
                Workbook workbook = excel.Workbooks.Add();
                Worksheet sheet = (Worksheet)workbook.Worksheets.Add();
                

                // Obtener los encabezados de las columnas desde el DataGrid
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    Range headerCell = (Range)sheet.Cells[1, i + 1];
                    headerCell.Value = dataGrid.Columns[i].Header;
                    headerCell.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                    headerCell.Borders.LineStyle = XlLineStyle.xlContinuous;
                }

                var itemsSource = dataGrid.ItemsSource as IEnumerable;

                if (itemsSource != null)
                {
                    int row = 2; // Comenzar desde la segunda fila

                    foreach (var item in itemsSource)
                    {
                        if (item is PagoModel fila)
                        {
                            sheet.Cells[row, 1] = fila.Cliente.NomApell;
                            ((Range)sheet.Cells[row, 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                            ((Range)sheet.Cells[row, 1]).ColumnWidth = 25;
                            ((Range)sheet.Cells[row, 1]).Borders.LineStyle = XlLineStyle.xlContinuous;

                            // Crear un array con los valores de los meses
                            object[] meses = {fila.Enero, fila.Febrero, fila.Marzo, fila.Abril, fila.Mayo, fila.Junio, fila.Julio,
                                    fila.Agosto, fila.Septiembre, fila.Octubre, fila.Noviembre, fila.Diciembre};

                            // Iterar sobre los valores del array para llenar las celdas y aplicar formato
                            for (int col = 2; col <= 13; col++)
                            {
                                sheet.Cells[row, col] = meses[col - 2].ToString() == "1" ? "X" : meses[col - 2].ToString();
                                ((Range)sheet.Cells[row, col]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                                ((Range)sheet.Cells[row, col]).Borders.LineStyle = XlLineStyle.xlContinuous;
                            }
                        }
                        row++;
                    }
                }
                ventana.Close();
                excel.Visible = true;
            }
        }

    }
}
