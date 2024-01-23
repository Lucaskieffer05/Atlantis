using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Atlantis.Models;
using System.Transactions;
using System.Windows.Controls;

namespace Atlantis.DataBase
{
    internal class BD
    {
        private readonly string _conexion;
        public string Conexion { get { return _conexion; } }

        public BD() {
            _conexion = @"Server=DESKTOP-3EJ63U7; Database=ATLANTIS; Trusted_Connection=true;";
        }
        internal ObservableCollection<PagoModel> ObtenerClientes(int id_anio)
        {
            ObservableCollection<PagoModel> lst = new ObservableCollection<PagoModel>();
            string query = "SELECT cliente.id_cliente, cliente.dni, cliente.NomApell, cliente.fechaNacimiento, " +
                    "pago.enero, pago.febrero, pago.marzo, pago.abril, pago.mayo, pago.junio, pago.julio, " +
                    "pago.agosto, pago.septiembre, pago.octubre, pago.noviembre, pago.diciembre, pago.seguro, anio.anio " +
                    "FROM cliente " +
                    "INNER JOIN pago ON cliente.id_cliente = pago.id_cliente " +
                    "INNER JOIN anio ON pago.id_anio = anio.id_anio " +
                    "WHERE anio.id_anio=" + id_anio +
                    "ORDER BY cliente.NomApell;";
            using (SqlConnection cn = new SqlConnection(Conexion)) {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query,cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    ClienteModel cliente = new ClienteModel()
                    {
                        Id_cliente = (int)reader["id_cliente"],
                        NomApell = (string)reader["NomApell"],
                        dni = (string)reader["dni"],
                        fechaNacimiento = reader["fechaNacimiento"] == DBNull.Value ? null : (string)reader["fechaNacimiento"]
                    };


                    AnioModel anio = new AnioModel()
                    {
                        Id_anio = id_anio,
                        Anio = (string)reader["anio"]
                    };

                    lst.Add(new PagoModel()
                    {
                        Id_cliente = (int)reader["id_cliente"],
                        Id_anio = id_anio,
                        Enero = reader["enero"] == DBNull.Value ? "-" : (string)reader["enero"],
                        Febrero = reader["febrero"] == DBNull.Value ? "-" : (string)reader["febrero"],
                        Marzo = reader["marzo"] == DBNull.Value ? "-" : (string)reader["marzo"],
                        Abril = reader["abril"] == DBNull.Value ? "-" : (string)reader["abril"],
                        Mayo = reader["mayo"] == DBNull.Value ? "-" : (string)reader["mayo"],
                        Junio = reader["junio"] == DBNull.Value ? "-" : (string)reader["junio"],
                        Julio = reader["julio"] == DBNull.Value ? "-" : (string)reader["julio"],
                        Agosto = reader["agosto"] == DBNull.Value ? "-" : (string)reader["agosto"],
                        Septiembre = reader["septiembre"] == DBNull.Value ? "-" : (string)reader["septiembre"],
                        Octubre = reader["octubre"] == DBNull.Value ? "-" : (string)reader["octubre"],
                        Noviembre = reader["noviembre"] == DBNull.Value ? "-" : (string)reader["noviembre"],
                        Diciembre = reader["diciembre"] == DBNull.Value ? "-" : (string)reader["diciembre"],
                        Seguro = reader["seguro"] == DBNull.Value ? "0" : (string)reader["seguro"],
                        Cliente = cliente,
                        Anio = anio
                    });
                }
                reader.Close();
                cn.Close();
            }
            return lst;

        }
        internal void AgregarCliente(PagoModel pago, int id_anio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    cn.Open();

                    // Insertar el cliente
                    string queryCliente = "INSERT INTO cliente (NomApell, dni, fechaNacimiento) " +
                                          "VALUES (@NomApell, @dni, @fechaNacimiento);" +
                                          "SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdCliente = new SqlCommand(queryCliente, cn);
                    cmdCliente.Parameters.AddWithValue("@NomApell", pago.Cliente.NomApell);
                    cmdCliente.Parameters.AddWithValue("@dni", pago.Cliente.dni);
                    cmdCliente.Parameters.AddWithValue("@fechaNacimiento", (object)pago.Cliente.fechaNacimiento ?? DBNull.Value);

                    int idCliente = Convert.ToInt32(cmdCliente.ExecuteScalar()); // Obtener el id del cliente insertado

                    // Insertar el pago
                    string queryPago = "INSERT INTO pago (enero, febrero, marzo, abril, mayo, junio, julio, agosto, septiembre, octubre, noviembre, diciembre, seguro, id_cliente, id_anio) " +
                                       "VALUES (@Enero, @Febrero, @Marzo, @Abril, @Mayo, @Junio, @Julio, @Agosto, @Septiembre, @Octubre, @Noviembre, @Diciembre, @Seguro, @idCliente, @idAnio);";

                    SqlCommand cmdPago = new SqlCommand(queryPago, cn);
                    cmdPago.Parameters.AddWithValue("@idCliente", idCliente);
                    cmdPago.Parameters.AddWithValue("@idAnio", id_anio);
                    cmdPago.Parameters.AddWithValue("@Enero", (object)pago.Enero ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Febrero", (object)pago.Febrero ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Marzo", (object)pago.Marzo ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Abril", (object)pago.Abril ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Mayo", (object)pago.Mayo ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Junio", (object)pago.Junio ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Julio", (object)pago.Julio ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Agosto", (object)pago.Agosto ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Septiembre", (object)pago.Septiembre ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Octubre", (object)pago.Octubre ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Noviembre", (object)pago.Noviembre ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Diciembre", (object)pago.Diciembre ?? DBNull.Value);
                    cmdPago.Parameters.AddWithValue("@Seguro", (object)pago.Seguro ?? DBNull.Value);

                    cmdPago.ExecuteNonQuery();
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al agregar el cliente: " + ex.Message);
            }
        }

        internal ObservableCollection<AnioModel> ObstenerAnios()
        {

            ObservableCollection<AnioModel> lst = new ObservableCollection<AnioModel>();
            string query = "SELECT * FROM anio";
            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lst.Add(new AnioModel()
                    {
                        Anio = (string)reader["anio"],
                        Id_anio = (int)reader["id_anio"]
                    });
                }
                reader.Close();
                cn.Close();
            }
            return lst;

        }

        internal void EliminarCliente(PagoModel pago, int id_anio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    cn.Open();

                    string queryEliminarCliente = "DELETE FROM pago WHERE id_cliente = @idCliente AND id_anio = @idAnio;";
                    SqlCommand cmdEliminarCliente = new SqlCommand(queryEliminarCliente, cn);
                    cmdEliminarCliente.Parameters.AddWithValue("@idCliente", pago.Id_cliente);
                    cmdEliminarCliente.Parameters.AddWithValue("@idAnio", id_anio);

                    cmdEliminarCliente.ExecuteNonQuery();

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar el pago: " + ex.Message);
            }
        }

        internal void GuardarCambios(PagoModel pago, int id_anio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    cn.Open();

                    // Actualizar el cliente
                    string queryActualizarCliente = "UPDATE cliente SET NomApell = @NomApell, dni = @dni, fechaNacimiento = @fechaNacimiento " +
                                                   "WHERE id_cliente = @idCliente;";

                    SqlCommand cmdActualizarCliente = new SqlCommand(queryActualizarCliente, cn);
                    cmdActualizarCliente.Parameters.AddWithValue("@idCliente", pago.Id_cliente);
                    cmdActualizarCliente.Parameters.AddWithValue("@NomApell", pago.Cliente.NomApell);
                    cmdActualizarCliente.Parameters.AddWithValue("@dni", pago.Cliente.dni);
                    cmdActualizarCliente.Parameters.AddWithValue("@fechaNacimiento", (object)pago.Cliente.fechaNacimiento ?? DBNull.Value);

                    cmdActualizarCliente.ExecuteNonQuery();

                    // Actualizar el pago
                    string queryActualizarPago = "UPDATE pago SET " +
                                                 "enero = @Enero, febrero = @Febrero, marzo = @Marzo, abril = @Abril, mayo = @Mayo, junio = @Junio, " +
                                                 "julio = @Julio, agosto = @Agosto, septiembre = @Septiembre, octubre = @Octubre, " +
                                                 "noviembre = @Noviembre, diciembre = @Diciembre, seguro = @Seguro " +
                                                 "WHERE id_cliente = @idCliente AND id_anio = @idAnio;";

                    SqlCommand cmdActualizarPago = new SqlCommand(queryActualizarPago, cn);
                    cmdActualizarPago.Parameters.AddWithValue("@idCliente", pago.Id_cliente);
                    cmdActualizarPago.Parameters.AddWithValue("@idAnio", id_anio);
                    cmdActualizarPago.Parameters.AddWithValue("@Enero", (object)pago.Enero ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Febrero", (object)pago.Febrero ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Marzo", (object)pago.Marzo ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Abril", (object)pago.Abril ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Mayo", (object)pago.Mayo ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Junio", (object)pago.Junio ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Julio", (object)pago.Julio ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Agosto", (object)pago.Agosto ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Septiembre", (object)pago.Septiembre ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Octubre", (object)pago.Octubre ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Noviembre", (object)pago.Noviembre ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Diciembre", (object)pago.Diciembre ?? DBNull.Value);
                    cmdActualizarPago.Parameters.AddWithValue("@Seguro", pago.Seguro);

                    cmdActualizarPago.ExecuteNonQuery();

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar cambios: " + ex.Message);
            }
        }

        internal ObservableCollection<PagoModel> FiltrarClientes(string filtro, int id_anio)
        {
            ObservableCollection<PagoModel> lst = new ObservableCollection<PagoModel>();
            string query = "SELECT cliente.id_cliente, cliente.dni, cliente.NomApell, cliente.fechaNacimiento, " +
                            "pago.enero, pago.febrero, pago.marzo, pago.abril, pago.mayo, pago.junio, pago.julio, " +
                            "pago.agosto, pago.septiembre, pago.octubre, pago.noviembre, pago.diciembre, pago.seguro, anio.anio " +
                            "FROM cliente " +
                            "INNER JOIN pago ON cliente.id_cliente = pago.id_cliente " +
                            "INNER JOIN anio ON pago.id_anio = anio.id_anio " +
                            "WHERE anio.id_anio = @idAnio AND (cliente.NomApell LIKE @filtro);";

            using (SqlConnection cn = new SqlConnection(Conexion))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(query, cn);
                cmd.Parameters.AddWithValue("@idAnio", id_anio);
                cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%"); // Se agregan comodines para buscar coincidencias parciales
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    ClienteModel cliente = new ClienteModel()
                    {
                        Id_cliente = (int)reader["id_cliente"],
                        NomApell = (string)reader["NomApell"],
                        dni = (string)reader["dni"],
                        fechaNacimiento = reader["fechaNacimiento"] == DBNull.Value ? null : (string)reader["fechaNacimiento"]
                    };

                    AnioModel anio = new AnioModel()
                    {
                        Id_anio = id_anio,
                        Anio = (string)reader["anio"]
                    };

                    lst.Add(new PagoModel()
                    {
                        Id_cliente = (int)reader["id_cliente"],
                        Id_anio = id_anio,
                        Enero = reader["enero"] == DBNull.Value ? "-" : (string)reader["enero"],
                        Febrero = reader["febrero"] == DBNull.Value ? "-" : (string)reader["febrero"],
                        Marzo = reader["marzo"] == DBNull.Value ? "-" : (string)reader["marzo"],
                        Abril = reader["abril"] == DBNull.Value ? "-" : (string)reader["abril"],
                        Mayo = reader["mayo"] == DBNull.Value ? "-" : (string)reader["mayo"],
                        Junio = reader["junio"] == DBNull.Value ? "-" : (string)reader["junio"],
                        Julio = reader["julio"] == DBNull.Value ? "-" : (string)reader["julio"],
                        Agosto = reader["agosto"] == DBNull.Value ? "-" : (string)reader["agosto"],
                        Septiembre = reader["septiembre"] == DBNull.Value ? "-" : (string)reader["septiembre"],
                        Octubre = reader["octubre"] == DBNull.Value ? "-" : (string)reader["octubre"],
                        Noviembre = reader["noviembre"] == DBNull.Value ? "-" : (string)reader["noviembre"],
                        Diciembre = reader["diciembre"] == DBNull.Value ? "-" : (string)reader["diciembre"],
                        Seguro = reader["seguro"] == DBNull.Value ? "0" : (string)reader["seguro"],
                        Cliente = cliente,
                        Anio = anio
                    });
                }
                reader.Close();
                cn.Close();
            }
            return lst;
        }

        internal void ImportarClientes(ObservableCollection<PagoModel> pagos, int id_anio)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(Conexion))
                {
                    cn.Open();

                    foreach (PagoModel pago in pagos)
                    {

                        string query = "INSERT INTO pago (enero, febrero, marzo, abril, mayo, junio, julio, agosto, septiembre, octubre, noviembre, diciembre, seguro, id_cliente, id_anio) " +
                                       "VALUES (@Enero, @Febrero, @Marzo, @Abril, @Mayo, @Junio, @Julio, @Agosto, @Septiembre, @Octubre, @Noviembre, @Diciembre, @Seguro, @idCliente, @idAnio);";

                        SqlCommand cmdPago = new SqlCommand(query, cn);
                        cmdPago.Parameters.AddWithValue("@idCliente", pago.Id_cliente);
                        cmdPago.Parameters.AddWithValue("@idAnio", id_anio);
                        cmdPago.Parameters.AddWithValue("@Enero", (object)pago.Enero ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Febrero", (object)pago.Febrero ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Marzo", (object)pago.Marzo ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Abril", (object)pago.Abril ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Mayo", (object)pago.Mayo ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Junio", (object)pago.Junio ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Julio", (object)pago.Julio ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Agosto", (object)pago.Agosto ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Septiembre", (object)pago.Septiembre ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Octubre", (object)pago.Octubre ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Noviembre", (object)pago.Noviembre ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Diciembre", (object)pago.Diciembre ?? DBNull.Value);
                        cmdPago.Parameters.AddWithValue("@Seguro", (object)pago.Seguro ?? DBNull.Value);

                        cmdPago.ExecuteNonQuery();
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al guardar cambios: " + ex.Message);
            }
        }


    }
}
