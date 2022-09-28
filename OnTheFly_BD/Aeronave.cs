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
            Console.WriteLine("Informe o CNPJ sem caracteres especiais para localizar o Cadastro:");
            this.CNPJ = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO

            Console.WriteLine("***CADASTRO DE COMPANHIA AEREA***");

            string sql = $"SELECT CNPJ,RazaoSocial,DataAbertura,UltimoVoo,DataCadastro,Situacao  FROM dbo.CompanhiaAerea WHERE CNPJ=('{this.CNPJ}');";
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
                    Console.WriteLine($"Data do último voo operado: {reader.GetDateTime(3)}");
                    Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
                    Console.WriteLine($"Situação: {reader.GetString(5)}");
                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização");
            Console.ReadKey();
            //Console.Clear();
            int op;
            do
            {
                Console.WriteLine(">>> EDITAR CADASTRO COMPANHIA AEREA <<<");
                Console.WriteLine("0 - SAIR");
                Console.WriteLine("1 - NOME [Razão Social]");
                Console.WriteLine("2 - DATA DE ABERTURA");
                Console.WriteLine("3 - ÚLTIMO VOO");
                Console.WriteLine("4 - DATA DO CADASTRO");
                Console.WriteLine("5 - SITUAÇÃO DO CADASTRO ");
                Console.WriteLine("\nEscolha entre as opções, o/os dados que deseja editar no Cadastro: ");
                op = int.Parse(Console.ReadLine());
                switch (op)
                {
                    case 0:
                        Console.WriteLine("Aperte ENTER para sair!");
                        Console.ReadKey();
                        break;
                    case 1:
                        do
                        {
                            Console.WriteLine("Informe a Razão Social [Máximo 50 digítos]: ");
                            this.RazaoSocial = Console.ReadLine();
                            if (this.RazaoSocial.Length > 50)
                            {
                                Console.WriteLine("\nInforme um nome que contenha até 50 caracteres!");

                            }
                            else
                            {
                                sql = $"UPDATE dbo.CompahiaAerea SET RazaoSocial='{this.RazaoSocial}' WHERE CNPJ='{this.CNPJ}';";
                                db = new ConexaoBD();
                                db.Connection(conexaosql, sql);
                                Console.ReadKey();
                            }
                        } while (this.RazaoSocial.Length > 50);
                        break;

                    case 2:
                        TimeSpan result;
                        do
                        {
                            Console.Write("Informe a data de abertura da Companhia [mês/dia/ano] : ");
                            DataAbertura = DateTime.Parse(Console.ReadLine());

                            result = DateTime.Now - DataAbertura;

                            if (result.Days / 30 < 6)
                            {
                                Console.WriteLine($"A companhia tem {result.Days / 30} meses");
                                Console.WriteLine("\nO tempo é insufiente para finalizar o cadastro!");
                                Console.WriteLine("Pressione ENTER para informar uma data válida!.");
                                Console.ReadKey();
                            }
                        } while (result.Days / 30 < 6);
                        sql = $"UPDATE dbo.CompanhiaAerea SET DataAbertura='{DataAbertura}' WHERE CNPJ='{this.CNPJ}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.ReadKey();
                        break;

                    case 3:
                        do
                        {
                            Console.Write("Informe a data e a hora do último voo operado [mês/dia/ano] : ");
                            UltimoVoo = DateTime.Parse(Console.ReadLine());

                            if (UltimoVoo > DateTime.Now)
                            {
                                Console.WriteLine("A data do último voo não pode ser maior que a data atual!");

                            }
                        } while (UltimoVoo > DateTime.Now);
                        sql = $"UPDATE dbo.CompanhiaAerea SET UltimoVoo='{UltimoVoo}' WHERE CNPJ='{this.CNPJ}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.ReadKey();
                        break;

                    case 4:
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
                                sql = $"UPDATE dbo.Passageiro SET Data_Cadastro='{dt}' WHERE CNPJ='{this.CNPJ}';";
                                db = new ConexaoBD();
                                db.Connection(conexaosql, sql);
                                Console.ReadKey();
                            }
                        }
                        break;

                    case 5:
                        do
                        {
                            Console.WriteLine("Informe a SITUAÇÃO do cadastro [A - Ativo, I - Inativo]: ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"UPDATE dbo.CompanhiaAerea SET Situacao='{Situacao}' WHERE CNPJ='{this.CNPJ}';";
                            db = new ConexaoBD();
                            db.Connection(conexaosql, sql);
                            Console.ReadKey();
                        } while (Situacao != 'A' && Situacao != 'I');
                        break;
                    default:
                        Console.WriteLine("\nINFORME UMA DAS OPÇÕES DO MENU!");
                        break;
                }
            } while (op < 0 && op < 5);
        }


    }
}
