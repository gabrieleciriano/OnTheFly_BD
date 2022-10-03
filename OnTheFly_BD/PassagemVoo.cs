using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class PassagemVoo
    {
        public string IdPassagem { get; set; } //PA0000
        public DateTime DataUltimaOp { get; set; }
        public float Valor { get; set; }
        public char Situacao { get; set; }
        //deveria passar o IdVoo como Voo IdVoo ?
        public string IdVoo { get; set; }
        public PassagemVoo()
        {

        }
        public PassagemVoo(string idvoo)
        {
            //int id = GeradorDeId();
            //this.IdPassagem = "PA" + id.ToString();
            //DataUltimaOp = DateTime.Now;
            //Valor = 500;
            //Situacao = 'L';
            //IdVoo = idvoo;
        }
        public void CadastrarPassagem(SqlConnection conexaosql)
        {
            Aeronave aero = new Aeronave();
            //gerando uma passagem com random
            Console.WriteLine("");
            int id = GeradorDeId();
            this.IdPassagem = "PA" + id.ToString();
            //LISTA COM OS VOOS ATIVOS PARA GERAR A PASSAGEM
            Console.WriteLine("Informe o id do voo");
            Console.WriteLine("***VOOS ATIVOS***");
            Console.WriteLine("\n");
            string sql = $"SELECT IdVoo,IdAeronave,DataVoo,DataCadastro,Destino,AssentosOcupados,Situacao  FROM dbo.Voo WHERE Situacao=('A');";
            ConexaoBD db = new ConexaoBD();
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
                    Console.WriteLine("------------------------------------------------------------");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização...");
            Console.ReadKey();
            Console.WriteLine("\nInforme o ID do voo da passagem [Ex: V000]: ");
            string voo = Console.ReadLine();
            Voo v = new Voo();
            v.IdVoo = voo;
            DataUltimaOp = DateTime.Now;
            Console.Write("Informe o valor  padrão das passagens desse voo R$: ");
            Valor = float.Parse(Console.ReadLine());
            if (Valor > 9999.99 || Valor < 0)
            {
                Console.WriteLine("Valor das passagem excedeu o limite permitido!");
            }

            string escolha;
            do
            {
                Console.WriteLine("Deseja pagar as passagens nesse exato momento? [S/N]");
                Console.WriteLine("Se não desejar pagar as passagens agora, mas, gostaria de reservá-las, por favor, informe [R]");
                escolha = Console.ReadLine().ToUpper();
            } while (escolha != "S" && escolha != "N" && escolha != "R");
            if (escolha == "S")
            {
                Situacao = 'P';
            }
            else if (escolha == "R")
            {
                Console.WriteLine("As passagens ficarão reservadas até o momento do pagamento!");
                Situacao = 'R';
            }
            else
            {
                Situacao = 'L';
            }
            //fazer o vinculo da quantidade de assentos do voo com a qt de passagens
            Console.WriteLine($"O Id dessa passagem é: {IdPassagem} ");
            Console.WriteLine("Deseja realmente efetuar esse cadastro ? Informe [1 - SIM 2 - NÃO]: ");
            int opc = int.Parse(Console.ReadLine());
            do
            {
                if (opc == 1)
                {
                    try
                    {
                        
                        sql = $"INSERT INTO dbo.Passagem (IdPassagem, IdVoo, DataUltimaOp, Valor, Situacao) VALUES ('{IdPassagem}', '{IdVoo}', '{DataUltimaOp}', '{Valor}', '{Situacao}');";
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
                    Console.WriteLine("CADASTRO DE VOO CANCELADO!");
            } while (opc != 1 && opc != 2);
        }
        public void EditarPassagem(SqlConnection conexaosql)
        {


        }
        public void VisualizarPassagemEspecifica(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o ID da PASSAGEM para localizar o Cadastro SEM caracteres especiais [PA0000]:");
            this.IdPassagem = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO
            Console.WriteLine("Deseja visualizar os dados desse voo específico?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    //Console.Clear();
                    Console.WriteLine("***DADOS DA PASSAGEM***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT IdPassagem,IdVoo,DataUltimaOp,Valor,Situacao  FROM dbo.Voo WHERE IdPassagem=('{this.IdPassagem}');";
                    ConexaoBD db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id da Passagem: {reader.GetString(0)}");
                            Console.WriteLine($"Id do Voo: {reader.GetString(1)}");
                            Console.WriteLine($"Data da última operação: {reader.GetDateTime(2)}");
                            Console.WriteLine($"Valor: {reader.GetFloat(3)}");
                            Console.WriteLine($"Situação: {reader.GetString(4)}");
                            Console.WriteLine("------------------------------------------------------------");

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
        public void VisualizarPassagenslivres(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro de TODAS as passagens livres?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    //Console.Clear();
                    Console.WriteLine("***PASSAGENS LIVRES***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT IdPassagem,IdVoo,DataUltimaOp,Valor,Situacao  FROM dbo.Passagem WHERE Situacao=('L');";
                    ConexaoBD db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id da Passagem: {reader.GetString(0)}");
                            Console.WriteLine($"Id do Voo: {reader.GetString(1)}");
                            Console.WriteLine($"Data da última operação: {reader.GetDateTime(2)}");
                            Console.WriteLine($"Valor: {reader.GetFloat(3)}");
                            Console.WriteLine($"Situação: {reader.GetString(4)}");
                            Console.WriteLine("------------------------------------------------------------");
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
        public void VisualizarPassagensCanceladas(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro de TODAS as passagens canceladas?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    //Console.Clear();
                    Console.WriteLine("***PASSAGENS LIVRES***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT IdPassagem,IdVoo,DataUltimaOp,Valor,Situacao  FROM dbo.Passagem WHERE Situacao=('C');";
                    ConexaoBD db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id da Passagem: {reader.GetString(0)}");
                            Console.WriteLine($"Id do Voo: {reader.GetString(1)}");
                            Console.WriteLine($"Data da última operação: {reader.GetDateTime(2)}");
                            Console.WriteLine($"Valor: {reader.GetFloat(3)}");
                            Console.WriteLine($"Situação: {reader.GetString(4)}");
                            Console.WriteLine("------------------------------------------------------------");
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
        public void VisualizarPassagensReservadas(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro de TODAS as passagens canceladas?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    //Console.Clear();
                    Console.WriteLine("***PASSAGENS LIVRES***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT IdPassagem,IdVoo,DataUltimaOp,Valor,Situacao  FROM dbo.Passagem WHERE Situacao=('R');";
                    ConexaoBD db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Id da Passagem: {reader.GetString(0)}");
                            Console.WriteLine($"Id do Voo: {reader.GetString(1)}");
                            Console.WriteLine($"Data da última operação: {reader.GetDateTime(2)}");
                            Console.WriteLine($"Valor: {reader.GetFloat(3)}");
                            Console.WriteLine($"Situação: {reader.GetString(4)}");
                            Console.WriteLine("------------------------------------------------------------");
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
        public void DeletarPassagemEspecifica()
        {

        }
        public void DeletarTodasPassagens()
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


