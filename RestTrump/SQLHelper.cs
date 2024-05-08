using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace RestTrump
{
    public static class SQLHelper
    {

        private static string _connectionString = Properties.Settings.Default.appCadenaConexionHuesped;

        internal static string queryInvenFill = @"SELECT   codigo, descr, precio, precio1, precio2, precio3, tiva FROM inven";
        internal static string queryInvenFillByCodigo = @"SELECT codigo, descr, precio, precio1, precio2, precio3, tiva, precio2_o, precio3_o, barra FROM inven WHERE (barra = {0})";
        internal static string queryInvenFillByDescr = @"SELECT    codigo, descr, precio, precio1, precio2, precio3, tiva FROM inven WHERE (descr LIKE {0})";
        internal static string queryInvenFillByProductosNuevos = @"SELECT    codigo, descr, precio, precio1, precio2 FROM inven WHERE (precio2_o = 0) OR (precio3_o = 0)";
        internal static string queryInvenUpdateQueryByCodigo = @"UPDATE inven SET precio2=@precio2  precio =@precio  WHERE (codigo = @codigo)";
        internal static string queryGruinvFill = @"SELECT descr, codigo FROM gruinv WHERE (NOT (codigo = ' '))";
        internal static string querydpto_cajasFill = @"SELECT codigo, descr FROM dpto_cajas";
        internal static string queryTaxFill = @"SELECT tipo, valor, nombre, nomencla  FROM tax";

        public static DataTable getGruinv() =>
            getDatatable<SqlConnection>(queryGruinvFill);

        public static DataTable getdpto_cajas() =>
            getDatatable<SqlConnection>(querydpto_cajasFill);

        public static DataTable getTax() =>
            getDatatable<SqlConnection>(queryTaxFill);

        public static DataTable getInvenProductosNuevos() =>
            getDatatable<SqlConnection>(queryInvenFillByProductosNuevos);

        public static DataTable getInvenByDescr(string descr) =>
              getDatatable<SqlConnection>(string.Format(queryInvenFillByDescr, descr));

        public static DataTable getInvenByCodigo(string Codigo) =>
                  getDatatable<SqlConnection>(string.Format(queryInvenFillByCodigo,Codigo));
        public static DataTable getInven() =>
            getDatatable<SqlConnection>(queryInvenFill);

        /// <summary>
        /// Actualiza Inventario
        /// </summary>
        /// <param name="precio2_o"></param>
        /// <param name="precio3_o"></param>
        /// <param name="precio"></param>
        /// <param name="original_codigo"></param>
        /// <returns></returns>
        public static int UpdateInvenByCodigo(string precio2_o, string precio3_o, string precio, string original_codigo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand cmd = new SqlCommand(queryInvenUpdateQueryByCodigo, connection))
                {
                    //if (Properties.Settings.Default.esPrecio3) cmd.Parameters.AddWithValue("precio2", precio2_o);
                    cmd.Parameters.AddWithValue("precio2_o", precio2_o);
                    cmd.Parameters.AddWithValue("precio3_o", precio3_o);
                    cmd.Parameters.AddWithValue("precio", precio);
                    cmd.Parameters.AddWithValue("codigo", original_codigo);
                    connection.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
                return 0;
            }
        }


        /// <summary>
        /// Devuelve una Datatable de una Conexion
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <example>
        /// dt = Read1<MySqlConnection>(query); // ~9800ms
        /// dt = Read2<MySqlConnection, MySqlDataAdapter>(query); // ~2300ms</example>
        /// <returns></returns>
        private static DataTable getDatatable<T>(string query) where T : IDbConnection, new()
        {
            try
            {
                using (var conn = new T())
                {
                    using (var cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection.ConnectionString = _connectionString;
                        cmd.Connection.Open();
                        var table = new DataTable();
                        table.Load(cmd.ExecuteReader());
                        //MessageBox.Show("Tabla rows:" + table.Rows.Count.ToString());
                        return table;
                    }
                }
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
                return null;
            }

        }

        /// <summary>
        /// Devuelve una Datatable d euna conexion
        /// </summary>
        /// <typeparam name="S"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        private static DataTable GetDataTable<S, T>(string query) where S : IDbConnection, new()
                                                   where T : IDbDataAdapter, IDisposable, new()
        {
            try
            {
                using (var conn = new S())
                {
                    using (var da = new T())
                    {
                        using (da.SelectCommand = conn.CreateCommand())
                        {
                            da.SelectCommand.CommandText = query;
                            da.SelectCommand.Connection.ConnectionString = _connectionString;
                            DataSet ds = new DataSet(); //conn is opened by dataadapter
                            da.Fill(ds);
                            return ds.Tables[0];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
                return null;
            }
        }


    }
}
