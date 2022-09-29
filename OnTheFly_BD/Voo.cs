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
        Aeronave a = new Aeronave();

        public Voo()
        {

        }
        public void CadastrarVoo(SqlConnection conexaosql)
        {

            int idvoo = GeradorDeId();
            this.IdVoo = "V" + "-"+idvoo.ToString();

            //Console.WriteLine("**COMPANHIAS AEREAS DISPONÍVEIS PARA CADASTRAR O VOO**");
            //Console.Clear();
            //Console.WriteLine("\n");
            //string sql = $"SELECT CNPJ, RazaoSocial, DataAbertura, UltimoVoo, DataCadastro, Situacao  FROM dbo.CompanhiaAerea WHERE Situacao = ('A');";
            //db = new ConexaoBD();
            //db.Select(conexaosql, sql);
            //SqlCommand cmd = new SqlCommand(sql, conexaosql);
            //using (SqlDataReader reader = cmd.ExecuteReader())
            //{
            //    while (reader.Read())
            //    {
            //        Console.WriteLine($"CNPJ: {reader.GetString(0)}");
            //        Console.WriteLine($"Razão Social: {reader.GetString(1)}");
            //        Console.WriteLine($"Data de Abertura: {reader.GetDateTime(2).ToShortDateString()}");
            //        Console.WriteLine($"Data e Hora do último voo: {reader.GetDateTime(3)}");
            //        Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
            //        Console.WriteLine($"Situação: {reader.GetString(5)}");
            //    }
            //}
            //conexaosql.Close();
            //Console.WriteLine("\nAperte ENTER para finalizar a visualização...");
            //Console.ReadKey();
            //Console.WriteLine("Informe o CNPJ da companhia aerea (sem caracteres especiais) que deseja cadastrar o voo: ");
            //ca.CNPJ = Console.ReadLine();

            //1 aeronave opera um voo por vez entao ela nao pode operar varios voos?
            Console.Clear();
            Console.WriteLine("**LISTA DE AERONAVES DISPONÍVEIS**");
            Console.WriteLine("\n");
            string sql = $"SELECT Inscricao,Capacidade,DataCadastro,CompanhiaAerea,Situacao FROM dbo.Aeronave WHERE Situacao = ('A');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"Id Aeronave: {reader.GetString(0)}");
                    Console.WriteLine($"Capacidade: {reader.GetInt32(1)}");
                    Console.WriteLine($"Data em que a Aeronave foi cadastrada: {reader.GetDateTime(2)}");
                    Console.WriteLine($"CNPJ da Companhia Aerea que a Aeronave pertence: {reader.GetString(3)}");
                    Console.WriteLine($"Situação: {reader.GetString(4)}");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Informe a Aeronave que irá operar este voo: ");
            string aero = Console.ReadLine();
            Console.WriteLine("**LISTA DE AEROPORTO DE DESTINO DISPONÍVEIS**");
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
            Console.WriteLine("Informe o destino desse voo [Ex:GRU]:");
            string destino = Console.ReadLine();
            //Data e hora do voo
            Console.Write("Informe a data e hora de partida do voo [dd/mm/aaaa hh:mm]: ");
            DateTime dataVoo;
            while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            {
                Console.Write("Informe a data e a hora de partida do voo:  ");
            }
            if (dataVoo <= DateTime.Now)
            {
                Console.WriteLine("Informe uma data válida para a partida do voo!");
            }
            else
            {
                DataVoo = dataVoo;
            }

            DataCadastro = DateTime.Now;
            //Console.WriteLine("Quantidades de Assentos Ocupados:");
            AssentosOcupados = 0;

            do
            {
                Console.WriteLine("Situação: [A - Ativo C - Cancelado]");
                Situacao = char.Parse(Console.ReadLine());
            } while (Situacao != 'A' && Situacao != 'I');

            Console.WriteLine("Deseja realmente efetuar esse cadastro ? Informe [1 - SIM 2 - NÃO]: ");
            int escolha;
            do
            {
                escolha = int.Parse(Console.ReadLine());
                if (escolha == 1)
                {
                    try
                    {
                        sql = $"INSERT INTO dbo.Voo (IdVoo, IdAeronave, DataVoo, DataCadastro, Destino, AssentosOcupados, Situacao) VALUES ('{IdVoo}', '{aero}', '{DataVoo}', '{DataCadastro}', '{destino}', '{AssentosOcupados}', '{Situacao}');";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("O CADASTRO FOI EFETUADO COM SUCESSO!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Inválido!");
                    }
                }
                else
                    Console.WriteLine("CADASTRO DE VOO CANCELADO!");
            } while (escolha != 1 && escolha != 2);
        }
        public void VisualizarVooEspecifico(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o ID DO VOO para localizar o Cadastro [V-0000]:");
            this.IdVoo = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO
            Console.WriteLine("Deseja visualizar os dados desse voo específico?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    //Console.Clear();
                    Console.WriteLine("***DADOS DO CADASTRO DO VOO***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT IdVoo,IdAeronave,DataVoo,DataCadastro,Destino,AssentosOcupados,Situacao  FROM dbo.Voo WHERE IdVoo=('{this.IdVoo}');";
                    db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"IdVoo: {reader.GetString(0)}");
                            Console.WriteLine($"Inscrição da Aeronave: {reader.GetString(1)}");
                            Console.WriteLine($"Data do voo: {reader.GetDateTime(2)}");
                            Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(3)}");
                            Console.WriteLine($"IATA do Destino: {reader.GetString(4)}");
                            Console.WriteLine($"Quantidade de Assentos Ocupados: {reader.GetInt32(5)}");
                            Console.WriteLine($"Situação: {reader.GetString(6)}");
                        }
                    }
                    conexaosql.Close();
                    Console.WriteLine("\nAperte ENTER para finalizar a localização...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("A localização do cadastro foi cancelada!");
                }
            } while (opc != 1 && opc != 2);
        }

        public void VisualizarVoosAtivos()
        {

        }
        public int GeradorDeId()
        {
            Random rand = new Random();
            int[] numero = new int[100];
            int aux = 0;
            for (int k = 0; k < numero.Length; k++)
            {
                int rnd = 0;
                do
                {
                    rnd = rand.Next(1000, 9999);
                } while (numero.Contains(rnd));
                numero[k] = rnd;
                aux = numero[k];
            }
            return aux;
        }
    }
}
