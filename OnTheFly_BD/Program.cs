using System;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Passageiro p = new Passageiro();
            ConexaoBD db = new ConexaoBD();
            SqlConnection conexaosql = new SqlConnection(db.Caminho());
            p.CadastrarPassageiro(conexaosql);
        }
    }
}
