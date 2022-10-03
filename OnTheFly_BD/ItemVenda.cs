using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class ItemVenda
    {
        public int IdItemVenda { get; set; }
        public int IdVenda { get; set; }
        public float ValorUnitario { get; set; }
       

        public void CadastrarItemVenda(SqlConnection conexaosql)
        {
            
            for (int i = 1; i <= 999; i++)
            {
                i.ToString();
                IdVenda = i;
            }
            this.IdVenda.ToString();
            Console.WriteLine("Informe o ID da passagem que deseja realizar a venda: ");
            string idpassagem = Console.ReadLine();
            Console.WriteLine("***DADOS DA PASSAGEM***");
            Console.WriteLine("\n");
            string sql = $"SELECT IdPassagem,IdVoo,DataUltimaOp,Valor,Situacao  FROM dbo.Passagem WHERE IdPassagem=('{idpassagem}');";
            ConexaoBD db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Id da Passagem: {reader.GetString(0)}");
                    Console.WriteLine($"Valor: {reader.GetFloat(3)}");
                    Console.WriteLine("------------------------------------------------------------");

                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização...");
            Console.ReadKey();
            Console.WriteLine("Informe o valor da passagem acima: ");
            this.ValorUnitario = float.Parse(Console.ReadLine());
            Console.WriteLine($"Id Item Venda: {IdItemVenda} ");
            Console.WriteLine("Deseja realmente efetuar essa benda ? Informe [1 - SIM 2 - NÃO]: ");
            int opc = int.Parse(Console.ReadLine());
            do
            {
                if (opc == 1)
                {
                    try
                    {

                        sql = $"INSERT INTO dbo.ItemVenda (IdItemVenda, IdVenda, ValorUnitario) VALUES ('{IdItemVenda}', '{IdVenda}', '{ValorUnitario}');";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("O CADASTRO FOI EFETUADO COM SUCESSO!");
                        Console.ReadKey();
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Inválido!");
                        Console.ReadKey();
                    }
                }
                else
                    Console.WriteLine("CADASTRO CANCELADO!");
            } while (opc != 1 && opc != 2);
        }
    }
}
