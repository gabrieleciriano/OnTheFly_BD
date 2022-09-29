using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Voo
    {
        public string IdVoo { get; set; } //V0000
        public string Destino { get; set; }
        public DateTime DataVoo { get; set; } // Data 8 dígitos + 4 dígitos da hora
        public DateTime DataCadastro { get; set; }
        public int AssentosOcupados { get; set; } //fazer um contador p nao deixar passar da qt que o aviao suporta
        public char Situacao { get; set; } //A Ativo ou C Cancelado
        public Aeronave InscricaoAeronave { get; set; }
        public CompanhiaAerea CNPJ { get; set; }
        public ConexaoBD db;
        ConexaoBD banco = new ConexaoBD();
        CompanhiaAerea ca = new CompanhiaAerea();

        public Voo()
        {
          
        }
        public void CadastrarVoo(SqlConnection conexaosql)
        {
            // idvoo 
            //if (AssentosOcupados.Count > 9999)
            //{
            //    Console.WriteLine("Limite de Voos atingidos!");
            //    return;
            //}
            //this.IdVoo = "V" + (listaVoos.Count() + 1).ToString("0000");

            Console.WriteLine("**COMPANHIAS AEREAS DISPONÍVEIS PARA CADASTRAR O VOO**");
            Console.Clear();
            Console.WriteLine("\n");
            string sql = $"SELECT CNPJ, RazaoSocial, DataAbertura, UltimoVoo, DataCadastro, Situacao  FROM dbo.CompanhiaAerea WHERE Situacao = ('A');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"CNPJ: {reader.GetString(0)}");
                    Console.WriteLine($"Razão Social: {reader.GetString(1)}");
                    Console.WriteLine($"Data de Abertura: {reader.GetDateTime(2).ToShortDateString()}");
                    Console.WriteLine($"Data e Hora do último voo: {reader.GetDateTime(3)}");
                    Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
                    Console.WriteLine($"Situação: {reader.GetString(5)}");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a visualização...");
            Console.ReadKey();
            Console.WriteLine("Informe o CNPJ da companhia aerea (sem caracteres especiais) que deseja cadastrar o voo: ");
            ca.CNPJ = Console.ReadLine();

            //1 aeronave opera um voo por vez entao ela nao pode operar varios voos?
            Console.WriteLine("**LISTA DE AERONAVES DISPONÍVEIS**");
            Console.Clear();
            Console.WriteLine("\n");
            sql = $"SELECT Inscricao,Capacidade,UltimaVenda,DataCadastro,Situacao,CompanhiaAerea  FROM dbo.Aeronave WHERE Situacao = ('A');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
             cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Inscricao: {reader.GetString(0)}");
                    Console.WriteLine($"Capacidade: {reader.GetInt32(1)}");
                    Console.WriteLine($"Data da última venda de passagem: {reader.GetDateTime(2)}");
                    Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(3)}");
                    Console.WriteLine($"Situação: {reader.GetString(4)}");
                    Console.WriteLine($"CNPJ da Companhia Aerea que a Aeronave pertence: {reader.GetString(5)}");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização...");
            Console.ReadKey();
            Console.WriteLine("Informe a INSCRIÇÃO do Aeroporto que irá operar esse voo: [Ex: GRU]");
            InscricaoAeronave = Console.ReadLine();


            Console.WriteLine("**LISTA DE AEROPORTO DE DESTINO DISPONÍVEIS**");
            Console.Clear();
            Console.WriteLine("\n");
            sql = $"SELECT Sigla FROM dbo.Iatas";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Sigla: {reader.GetString(0)}");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a visualização...");
            Console.ReadKey();
          
            //Data e hora do voo
            Console.Write("Informe a data de partida do voo: ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data de partida do voo: ");
            }

            Console.Write("Informe a hora de partida do voo: ");
            DateTime horaVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out horaVoo))
            {
                Console.Write("Informe a hora de partida do voo: ");
            }

           

            //INFORMAR QUAL AERONAVE SERA VINCULADA COM ESSE VOOO
        //    Console.Write("Informe qual Aeronave pertence a este Voo: ");
        //    InscricaoAeronave = Console.Read
        //    string insc = Console.ReadLine();

        //    foreach (Aeronave item in listaAeronaves)
        //    {
        //        if (item.Situacao == 'A')
        //        {
        //            if (item.Inscricao == insc)
        //                this.Aeronave = item;
        //        }
        //    }
        //}




    }
}
