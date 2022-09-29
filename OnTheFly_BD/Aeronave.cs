using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Aeronave
    {
        public String Inscricao { get; set; }
        public int Capacidade { get; set; }
        public DateTime UltimaVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public CompanhiaAerea CompanhiaAerea { get; set; }
        public ConexaoBD db;
        ConexaoBD banco = new ConexaoBD();
        CompanhiaAerea ca = new CompanhiaAerea();

        public Aeronave()
        {

        }
        public String SufixoAeronave()
        {
            string sufixo;
            bool aux;
            do
            {
                Console.Write("Informe as 3 últimas letras da inscrição da aeronave em MAIÚSCULO!: ");
                sufixo = Console.ReadLine();
                aux = VerificarSufixo(sufixo);
                if (!aux) Console.WriteLine("SUFIXO INVÁLIDO");
            } while (sufixo.Length != 3 || !aux);
            return sufixo.ToUpper();
        }
        public bool VerificarSufixo(String sufixo)
        {
            int i;
            for (i = 0; i < 3; i++)
            {
                char aux = sufixo[i];
                if (Char.IsLetter(aux))
                {
                    return true;
                }
                else return false;
            }
            return true;
        }
        public String SelecionarPrefixo()
        {
            int prefixo;
            do
            {
                Console.WriteLine("Informe o prefixo da aeronave\n1 - PP\n2 - PT\n3 - PR\n4 - PS\n5 - BR\n0 - Sair");
                int.TryParse(Console.ReadLine(), out prefixo);
                switch (prefixo)
                {
                    case 1:
                        return "PP";
                    case 2:
                        return "PT";
                    case 3:
                        return "PR";
                    case 4:
                        return "PS";
                    case 5:
                        return "BR";
                    default:
                        Console.WriteLine("PREFIXO INVÁLIDO");
                        break;
                }
            } while (prefixo <= 0 && prefixo > 5);
            return "0";
        }
        public void CadastrarAeronave(SqlConnection conexaosql)
        {
            Console.Clear();
            Console.WriteLine(">>>CADASTRO DE AERONAVE<<<");

            string prefixo = SelecionarPrefixo();
            if (prefixo == "0")
            {
                Console.WriteLine("CADASTRAMENTO CANCELADO, Pressione ENTER para continuar...");
                Console.ReadKey();
                Console.Clear();
                return;
            }
            Inscricao = prefixo + "-" + SufixoAeronave();
            do
            {
                Console.WriteLine("Informe a capacidade de pessoas que a Aeronave comporta: ");
                Capacidade = int.Parse(Console.ReadLine());
            } while (Capacidade < 0 || Capacidade > 999);

            UltimaVenda = DateTime.Now;
            DataCadastro = DateTime.Now;
            do
            {
                Console.WriteLine("Situação: [A - Ativo I - Inativo]");
                Situacao = char.Parse(Console.ReadLine());
            } while (Situacao != 'A' && Situacao != 'I');

            Console.Clear();
            Console.WriteLine("***LISTA DE COMPANHIAS AEREAS ATIVAS***");
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
            Console.WriteLine("\nAperte ENTER para finalizar a visualização");
            Console.ReadKey();

            Console.WriteLine("\n");
            Console.Write("Informe o CNPJ da Companhia Aérea a qual essa Aeronave Pertence: ");
            ca.CNPJ = Console.ReadLine();
            Console.WriteLine("Deseja realmente efetuar esse cadastro ? Informe [1 - SIM 2 - NÃO]: ");
            int escolha;
            do
            {
                escolha = int.Parse(Console.ReadLine());
                if (escolha == 1)
                {
                    try
                    {
                        sql = $"INSERT INTO dbo.Aeronave (Inscricao, Capacidade, UltimaVenda, DataCadastro, Situacao, CompanhiaAerea) VALUES ('{Inscricao}', '{Capacidade}', '{UltimaVenda}', '{DataCadastro}', '{Situacao}', '{ca.CNPJ}');";
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
                    Console.WriteLine("CADASTRO DE AERONAVE CANCELADO!");
            } while (escolha != 1 && escolha != 2);
        }
        public void EditarAeronave(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe a INSCRIÇÃO da Aeronave para localizar o Cadastro [XX-XXX]:");
            this.Inscricao = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO

            Console.WriteLine("***AERONAVE CADASTRADA***");

            string sql = $"SELECT Inscricao,Capacidade,UltimaVenda,DataCadastro,Situacao,CompanhiaAerea  FROM dbo.Aeronave WHERE Inscricao=('{this.Inscricao}');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
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
            //Console.Clear();
            int op;
            do
            {
                Console.WriteLine(">>> EDITAR CADASTRO AERONAVE<<<");
                Console.WriteLine("0 - SAIR");
                Console.WriteLine("1 - CAPACIDADE");
                Console.WriteLine("2 - DATA DA ÚLTIMA VENDA DE PASSAGEM");
                Console.WriteLine("3 - DATA DO CADASTRO DA AERONAVE");
                Console.WriteLine("4 - SITUAÇÃO DO CADASTRO ");
                //Console.WriteLine("5- COMPANHIA AEREA QUE A AERONAVE PERTENCE");
                Console.WriteLine("\nEscolha entre as opções, o/os dados que deseja editar no Cadastro: ");
                op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 0:
                        Console.WriteLine("Aperte ENTER para sair...");
                        Console.ReadKey();
                        break;
                    case 1:
                        do
                        {
                            Console.WriteLine("Informe a capacidade de pessoas que a Aeronave comporta: ");
                            this.Capacidade = int.Parse(Console.ReadLine());
                        } while (Capacidade < 0 || Capacidade > 999);
                        sql = $"UPDATE dbo.Aeronave SET Capacidade='{this.Capacidade}' WHERE Inscricao='{this.Inscricao}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("CADASTRO ATUALIZADO COM SUCESSO!");
                        Console.ReadKey();
                        break;

                    case 2:
                        do
                        {
                            Console.Write("Informe a data da última venda de passagem [mês/dia/ano] : ");
                            this.UltimaVenda = DateTime.Parse(Console.ReadLine());

                            if (this.UltimaVenda > DateTime.Today)
                            {
                                Console.WriteLine("A data do último voo não pode ser maior que a data atual!");

                            }
                        } while (this.UltimaVenda > DateTime.Today);
                        sql = $"UPDATE dbo.Aeronave SET UltimaVenda='{this.UltimaVenda}' WHERE Inscricao='{this.Inscricao}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("CADASTRO ATUALIZADO COM SUCESSO!");
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
                                sql = $"UPDATE dbo.Aeronave SET DataCadastro='{dt}' WHERE Inscricao='{this.Inscricao}';";
                                db = new ConexaoBD();
                                db.Connection(conexaosql, sql);
                                Console.WriteLine("CADASTRO ATUALIZADO COM SUCESSO!");
                                Console.ReadKey();
                            }
                        }
                        break;

                    case 4:
                        do
                        {
                            Console.WriteLine("Informe a SITUAÇÃO do cadastro [A - Ativo, I - Inativo]: ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"UPDATE dbo.Aeronave SET Situacao='{Situacao}' WHERE Inscricao='{this.Inscricao}';";
                            db = new ConexaoBD();
                            db.Connection(conexaosql, sql);
                            Console.WriteLine("CADASTRO ATUALIZADO COM SUCESSO!");
                            Console.ReadKey();
                        } while (Situacao != 'A' && Situacao != 'I');
                        break;

                    case 5:
                        break;

                    default:
                        Console.WriteLine("\nINFORME UMA DAS OPÇÕES DO MENU!");
                        break;
                }
            } while (op < 0 && op < 5);
        }
        public void VisualizarAeronaveEspecifica(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe a INSCRIÇÃO da Aeronave para localizar o Cadastro [XX-XXX]:");
            this.Inscricao = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO

            Console.WriteLine("Deseja visualizar os dados dessa Aeronave específica?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    Console.Clear();
                    Console.WriteLine("***DADOS DO CADASTRO DA AERONAVE***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT Inscricao,Capacidade,UltimaVenda,DataCadastro,Situacao,CompanhiaAerea  FROM dbo.Aeronave WHERE Inscricao=('{this.Inscricao}');";
                    db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
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
                    Console.WriteLine("\nAperte ENTER para finalizar a localização");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("A localização do cadastro foi cancelada!");
                }
            } while (opc != 1 && opc != 2);
        }
        public void VisualizarAeronavesAtivas(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro ATIVO de TODAS as Aeronaves?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    Console.Clear();
                    Console.WriteLine("***DADOS DOS CADASTROS DAS AERONAVES***");
                    Console.WriteLine("\n");
                    string sql = $"SELECT Inscricao,Capacidade,UltimaVenda,DataCadastro,Situacao,CompanhiaAerea  FROM dbo.Aeronave WHERE Situacao = ('A');";
                    db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
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
                    Console.WriteLine("\nAperte ENTER para finalizar a localização");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("A localização do cadastro foi cancelada!");
                }
            } while (opc != 1 && opc != 2);
        }
        //vizualizar aeronaves inativas
        public void DeletarAeronaveEspecifica(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe a INSCRIÇÃO da Aeronave para localizar o Cadastro [XX-XXX]:");
            this.Inscricao = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("***DADOS DO CADASTRO DA AERONAVE***");
            Console.WriteLine("\n");
            string sql = $"SELECT Inscricao,Capacidade,UltimaVenda,DataCadastro,Situacao,CompanhiaAerea  FROM dbo.Aeronave WHERE Inscricao=('{this.Inscricao}');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
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
            Console.WriteLine("\nDeseja realmente DELETAR esse cadastro ? Não será possível repensar essa ação...");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    sql = $"DELETE FROM dbo.Aeronave WHERE Inscricao=('{this.Inscricao}');";
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
        public void DeletarTodasAeronaves(SqlConnection conexaosql)
        {
            Console.WriteLine("***DELETAR CADASTRO DE AERONAVES***");
            Console.WriteLine("\n");
            string sql = $"SELECT Inscricao,Capacidade,UltimaVenda,DataCadastro,Situacao,CompanhiaAerea  FROM dbo.Aeronave WHERE Situacao = ('A');";
            db = new ConexaoBD();
            db.Select(conexaosql, sql);
            SqlCommand cmd = new SqlCommand(sql, conexaosql);
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
            Console.WriteLine("\nAperte ENTER para finalizar a visualização...");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("\nDeseja realmente DELETAR TODOS os CADASTROS? Não será possível repensar essa ação... ");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    sql = $"DELETE FROM dbo.Aeronave";
                    db = new ConexaoBD();
                    db.Connection(conexaosql, sql);
                    cmd = new SqlCommand(sql, conexaosql);
                    Console.WriteLine("\nCADASTROS DELETADOS COM SUCESSO!");
                    Console.WriteLine("Pressione ENTER para sair...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Exclusão de cadastros CANCELADA!!");
                }
            } while (opc != 1 && opc != 2);
        }
    }
}

