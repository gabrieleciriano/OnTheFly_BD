using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Venda
    {
        public int IdVenda { get; set; } //CHAVE
        public DateTime DataVenda { get; set; }
        public string CpfPassageiro { get; set; }
        public float ValorTotal { get; set; }
        public void CadastrarVenda(SqlConnection conexaosql)
        {
            //FAZER O ID DA VENDA
            for (int i = 1; i <= 999; i++)
            {
                i.ToString();
                IdVenda = i;
            }
            this.IdVenda.ToString();
            DataVenda = DateTime.Now;
            Console.WriteLine("Informe o CPF do passageiro (que deve já estar cadastrado no sistema) SEM caracteres especiais que deseja realizar essa venda: ");
            this.CpfPassageiro = Console.ReadLine();
            //FAZER UM SELECT DE PASSAGEIRO
            string sql = $"SELECT CPF,Nome,DataNascimento  FROM dbo.Passageiro WHERE CPF=('{this.CpfPassageiro}');";
            ConexaoBD db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Nome: {reader.GetString(1)}");
                    Console.WriteLine($"CPF: {reader.GetString(0)}");
                    Console.WriteLine($"Data de Nascimento: {reader.GetDateTime(2).ToShortDateString()}");
                    Console.WriteLine("--------------------------------------------------------");

                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização");
            Console.ReadKey();

            int qtpassagens;
            do
            {
                //Fazer a verificação da idade para realizar a venda....
                Console.WriteLine("Informe quantas passagens desejam comprar [Máximo 4]: ");
                qtpassagens = int.Parse(Console.ReadLine());
            } while (qtpassagens < 1 && qtpassagens > 4);

            //DAR UM SELECT EM TODAS AS PASSAGENS ATIVAS P ELE ESCOLHER ?
            //Console.Clear();
            Console.WriteLine("***PASSAGENS LIVRES***");
            Console.WriteLine("\n");
            sql = $"SELECT IdPassagem,Valor,Situacao  FROM dbo.Passagem WHERE Situacao=('L');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Id da Passagem: {reader.GetString(0)}");
                    Console.WriteLine($"Valor: {reader.GetFloat(1)}");
                    Console.WriteLine($"Situação: {reader.GetString(2)}");
                    Console.WriteLine("------------------------------------------------------------");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização...");
            Console.ReadKey();
            Console.WriteLine("Por favor, informe o Id da passagem que desejam comprar: ");
            string idpassagem = Console.ReadLine();

            Console.WriteLine($"Valor total da venda: {ValorTotal}");

            //REALIZAR UPDTAES EM CADA TABELA
        }
        
    }
}
