using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class ConexaoBD
    {
        string conexao = "Data Source=localhost; Initial Catalog=OntheFly; User Id=sa; Password=MT1860143g;";
        public ConexaoBD()
        {

        }
        public string Caminho()
        {
            return conexao;
        }

        public void Connection(SqlConnection conexaosql, String sql)
        {

            conexaosql.Open();
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Connection = conexaosql;
            cmd.ExecuteNonQuery();
            conexaosql.Close();
        }
        public void Select(SqlConnection conexaosql, String sql)
        {

            conexaosql.Open();
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Connection = conexaosql;
            cmd.ExecuteNonQuery();
        }
    }
}

