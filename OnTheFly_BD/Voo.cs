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
        public DateTime DataVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public int AssentosOcupados { get; set; }
        public char Situacao { get; set; } //A Ativo ou C Cancelado
        public Aeronave InscricaoAeronave { get; set; }
        public CompanhiaAerea CNPJ { get; set; }
        //public ConexaoBD db;
        //ConexaoBD banco = new ConexaoBD();
        //CompanhiaAerea ca = new CompanhiaAerea();
        //Aeronave a = new Aeronave();
        //Voo v = new Voo();
        public Voo()
        {

        }
        public void CadastrarVoo(SqlConnection conexaosql)
        {

            int idvoo = GeradorDeId();
            this.IdVoo = "V" + idvoo.ToString();
            Console.Clear();
            Console.WriteLine("**LISTA DE AERONAVES DISPONÍVEIS**");
            Console.WriteLine("\n");
            string sql = $"SELECT Inscricao,Capacidade,DataCadastro,CompanhiaAerea,Situacao FROM dbo.Aeronave WHERE Situacao = ('A');";
            ConexaoBD db = new ConexaoBD();
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
            Console.WriteLine("Informe a Aeronave que irá operar este voo: ");
            string aero = Console.ReadLine();
            Console.WriteLine("**LISTA DE AEROPORTO DE DESTINO DISPONÍVEIS**");
            sql = $"SELECT Sigla FROM dbo.Iatas";
            // db = new ConexaoBD();
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
            DataVoo = DateTime.Parse(Console.ReadLine());
            if (DataVoo <= DateTime.Now)
            {
                Console.WriteLine("Informe uma data válida para a partida do voo!");
            }

            //DateTime dataVoo;
            //while (!DateTime.TryParse(Console.ReadLine(), out dataVoo))
            //{
            //    Console.Write("Informe a data e a hora de partida do voo:  ");
            //}
            //if (dataVoo <= DateTime.Now)
            //{
            //    Console.WriteLine("Informe uma data válida para a partida do voo!");
            //}
            //else
            //{
            //    DataVoo = dataVoo;
            //}

            DataCadastro = DateTime.Now;
            AssentosOcupados = 0;
            do
            {
                Console.WriteLine("Situação: [A - Ativo ou C - Cancelado]");
                Situacao = char.Parse(Console.ReadLine());
            } while (Situacao != 'A' && Situacao != 'I');

            //Mostrando o ID voo na tela...
            Console.WriteLine($"O Id desse voo é: {IdVoo}");

            Console.WriteLine("Deseja realmente efetuar esse cadastro ? Informe [1 - SIM 2 - NÃO]: ");
            int escolha;
            do
            {
                escolha = int.Parse(Console.ReadLine());
                if (escolha == 1)
                {
                    try
                    {
                        //Console.WriteLine("Informe o valor das passagens desse voo: ");
                        //float valor = int.Parse(Console.ReadLine());
                        //PassagemVoo passagem = new PassagemVoo(IdVoo);
                        sql = $"INSERT INTO dbo.Voo (IdVoo, IdAeronave, DataVoo, DataCadastro, Destino, AssentosOcupados, Situacao) VALUES ('{IdVoo}', '{aero}', '{DataVoo}', '{DataCadastro}', '{destino}', '{AssentosOcupados}', '{Situacao}');";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("O CADASTRO FOI EFETUADO COM SUCESSO!");
                        Console.ReadKey();
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
        public void EditarVoo(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o ID DO VOO para localizar o Cadastro [V-0000]:");
            this.IdVoo = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO
            //Console.Clear();
            Console.WriteLine("***DADOS DO CADASTRO DO VOO***");
            Console.WriteLine("\n");
            string sql = $"SELECT IdVoo,IdAeronave,DataVoo,DataCadastro,Destino,AssentosOcupados,Situacao  FROM dbo.Voo WHERE IdVoo=('{this.IdVoo}');";
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
            int opc = -1;
            do
            {

                Console.WriteLine(">>> EDITAR CADASTRO VOO<<<");
                Console.WriteLine("0 - SAIR");
                Console.WriteLine("1 - AERONAVE");
                Console.WriteLine("2 - DATA DO VOO");
                Console.WriteLine("3 - DATA DO CADASTRO DO VOO");
                Console.WriteLine("4 - DESTINO");
                Console.WriteLine("5 - QUANTIDADE ASSENTOS OCUPADOS");
                Console.WriteLine("6 - SITUAÇÃO DO CADASTRO ");
                Console.WriteLine("\nEscolha entre as opções, o/os dados que deseja editar no Cadastro: ");
                int op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 0:
                        Console.WriteLine("Aperte ENTER para sair...");
                        Console.ReadKey();
                        break;
                    case 1:
                        Console.WriteLine("**LISTA DE AERONAVES DISPONÍVEIS**");
                        Console.WriteLine("\n");
                        sql = $"SELECT Inscricao,Capacidade,DataCadastro,CompanhiaAerea,Situacao FROM dbo.Aeronave WHERE Situacao = ('A');";
                        db = new ConexaoBD();
                        db.Select(conexaosql, sql);
                        cmd = new SqlCommand(sql, conexaosql);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine($"Id Aeronave: {reader.GetString(0)}");
                                Console.WriteLine($"Capacidade: {reader.GetInt32(1)}");
                                Console.WriteLine($"Data em que a Aeronave foi cadastrada: {reader.GetDateTime(2)}");
                                Console.WriteLine($"CNPJ da Companhia Aerea que a Aeronave pertence: {reader.GetString(3)}");
                                Console.WriteLine($"Situação: {reader.GetString(4)}");
                                Console.WriteLine("------------------------------------------------------------");
                            }
                        }
                        conexaosql.Close();
                        Console.WriteLine("\nAperte ENTER para finalizar a localização...");
                        Console.ReadKey();
                        Console.Clear();
                        Console.WriteLine("Informe a Aeronave que irá operar este voo: ");
                        string aero = Console.ReadLine();
                        sql = $"UPDATE dbo.Voo SET IdAeronave='{aero}' WHERE IdVoo='{this.IdVoo}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("\nCADASTRO ATUALIZADO COM SUCESSO!");
                        Console.ReadKey();
                        break;

                    case 2:
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
                        sql = $"UPDATE dbo.Voo SET DataVoo='{DataVoo}' WHERE IdVoo='{this.IdVoo}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("\nCADASTRO ATUALIZADO COM SUCESSO!");
                        Console.ReadKey();
                        break;

                    case 3:
                        DateTime dt;
                        Console.WriteLine("\nInforme a DATA DO CADASTRO: ");
                        dt = DateTime.Parse(Console.ReadLine());
                        while (!DateTime.TryParse(Console.ReadLine(), out dt))
                        {
                            if (dt > DateTime.Now)
                            {
                                Console.WriteLine("A data do cadastro não pode ser maior que a data atual!");
                            }
                            else
                            {
                                dt.ToString("dd/MM/yyyy");
                                sql = $"UPDATE dbo.Voo SET DataCadastro='{dt}' WHERE IdVoo='{this.IdVoo}';";
                                db = new ConexaoBD();
                                db.Connection(conexaosql, sql);
                                Console.WriteLine("\nCADASTRO ATUALIZADO COM SUCESSO!");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case 4:
                        //destino
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
                        sql = $"UPDATE dbo.Voo SET Destino='{Destino}' WHERE IdVoo='{this.IdVoo}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("\nCADASTRO ATUALIZADO COM SUCESSO!");
                        Console.ReadKey();

                        break;
                    case 5:
                        //assentos ocupados
                        break;
                    case 6:
                        do
                        {
                            Console.WriteLine("Informe a SITUAÇÃO do voo [A - Ativo ou C - Cancelado]: ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"UPDATE dbo.Voo SET Situacao='{Situacao}' WHERE IdVoo='{this.IdVoo}';";
                            db = new ConexaoBD();
                            db.Connection(conexaosql, sql);
                            Console.WriteLine("\nCADASTRO ATUALIZADO COM SUCESSO!");
                            Console.ReadKey();
                        } while (Situacao != 'A' && Situacao != 'C');
                        break;

                    default:
                        Console.WriteLine("\nINFORME UMA DAS OPÇÕES DO MENU!");
                        break;
                }
            } while (opc < 0 && opc < 6);
        }
        public void VisualizarVooEspecifico(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o ID DO VOO para localizar o Cadastro SEM caracteres especiais [V0000]:");
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
                }
                else
                {
                    Console.WriteLine("A localização do cadastro foi cancelada!");
                }
            } while (opc != 1 && opc != 2);
        }
        public void VisualizarVoosAtivos(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro ATIVO de TODOS os Voos?");
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
                }
                else
                {
                    Console.WriteLine("A localização do cadastro foi cancelada!");
                }
            } while (opc != 1 && opc != 2);
        }
        public void VisualizarVoosCancelados(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar TODOS os Voos que foram cancelados?");
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
                    string sql = $"SELECT IdVoo,IdAeronave,DataVoo,DataCadastro,Destino,AssentosOcupados,Situacao  FROM dbo.Voo WHERE Situacao=('C');";
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
                            Console.WriteLine("--------------------------------------------------------");
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
        public void DeletarTodosVoos(SqlConnection conexaosql)
        {
            Console.WriteLine("***DELETAR CADASTRO DE VOOS***");
            Console.WriteLine("\n");
            //Console.Clear();
            string sql = $"SELECT IdVoo,IdAeronave,DataVoo,DataCadastro,Destino,AssentosOcupados,Situacao  FROM dbo.Voo WHERE IdVoo=('{this.IdVoo}');";
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
            Console.WriteLine("\nDeseja realmente DELETAR TODOS os cadastros? Não será possível repensar essa ação...");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    sql = $"DELETE FROM dbo.Voo";
                    db = new ConexaoBD();
                    db.Connection(conexaosql, sql);
                    cmd = new SqlCommand(sql, conexaosql);
                    Console.WriteLine("\nCADASTRO DELETADO COM SUCESSO!");
                    Console.WriteLine("Pressione ENTER para sair...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nExclusão do cadastro CANCELADA!!");
                }
            } while (opc != 1 && opc != 2);
        }
        public void DeletarVooEspecifico(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o ID do Voo para localizar o Cadastro SEM caracteres especiais [V0000]:");
            this.IdVoo = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("***DADOS DO CADASTRO DO VOO***");
            Console.WriteLine("\n");
            //Console.Clear();
            string sql = $"SELECT IdVoo,IdAeronave,DataVoo,DataCadastro,Destino,AssentosOcupados,Situacao  FROM dbo.Voo WHERE IdVoo=('{this.IdVoo}');";
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
            Console.WriteLine("\nDeseja realmente DELETAR esse cadastro ? Não será possível repensar essa ação...");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    sql = $"DELETE FROM dbo.Voo WHERE IdVoo=('{this.IdVoo}');";
                    db = new ConexaoBD();
                    db.Connection(conexaosql, sql);
                    cmd = new SqlCommand(sql, conexaosql);
                    Console.WriteLine("CADASTRO DELETADO COM SUCESSO!");
                    Console.WriteLine("Pressione ENTER para sair...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Exclusão do cadastro CANCELADA!!");
                }
            } while (opc != 1 && opc != 2);
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