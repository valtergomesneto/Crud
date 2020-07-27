using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace AgendaCrud.Data.Factory
{
    public class ConnectionString
    {
        private readonly DataSet _dSet = new DataSet();

        private string StringConnection()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["StringConnection"].ToString();

        }
        //Receber os parametros para qualquer execução de insert, update, e delete.
        public bool Executar(string sql, List<MySqlParameter> param = null)
        {
            MySqlConnection con = new MySqlConnection(StringConnection());
            MySqlCommand cmd = new MySqlCommand();

            con.Open();

            cmd.CommandText = sql;
            cmd.Connection = con;

            if (param != null)
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }

            var linha = cmd.ExecuteNonQuery();

            con.Close();

            if (linha > 0) { return true; } else { return false; };
        }

        //Receber os  paramentro para qualquer consulta
        public DataSet Read(string sql, List<MySqlParameter> param = null)
        {
            MySqlConnection con = new MySqlConnection(StringConnection());
            MySqlCommand cmd = new MySqlCommand();

            con.Open();

            cmd.CommandText = sql;
            cmd.Connection = con;

         

            if (param != null)
                foreach (var p in param)
                {
                    cmd.Parameters.Add(p);
                }

            MySqlDataAdapter adapt = new MySqlDataAdapter(cmd);

            adapt.Fill(_dSet);

            con.Close();
           
            return _dSet;
        }
    }
}
