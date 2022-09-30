using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class CompanhiaAerea
    {
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public DateTime DataAbertura { get; set; }
        public DateTime UltimoVoo { get; set; }
        public DateTime DataCadastro { get; set; }
        public char Situacao { get; set; }
        public ConexaoBD db;
        ConexaoBD banco = new ConexaoBD();

        public CompanhiaAerea()
        {

        }
        public void CadastroCompanhiaAerea(SqlConnection conexaosql)
        {
            Console.WriteLine(">>>CADASTRO DE COMPANHIA AEREA<<<");
            //CNPJ
            do
            {
                Console.Write("Informe o seu Cadastro Nacional da Pessoa Jurídica [CNPJ] sem caracteres especiais:  ");
                CNPJ = Console.ReadLine();
                if (ValidarCnpj(CNPJ) == false)
                {
                    Console.WriteLine("\nNÚMERO DE CNPJ INVÁLIDO!");
                    Console.WriteLine("Pressione ENTER para informar novamente...");
                    Console.ReadKey();
                }
            } while (ValidarCnpj(CNPJ) == false);

            //Razão Social
            do
            {
                Console.Write("Informe o nome da Razão Social [Máximo 50 dígitos]:  ");
                RazaoSocial = Console.ReadLine();
                if (RazaoSocial.Length > 50)
                {
                    Console.WriteLine("\nInforme um nome que contenha até 50 caracteres!");
                }
            } while (RazaoSocial.Length > 50);

            //Data de abertura
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
                    Console.WriteLine("Pressione ENTER para informar uma data válida!");
                    Console.ReadKey();
                }
            } while (result.Days / 30 < 6);

            UltimoVoo = DateTime.Now;
            DataCadastro = DateTime.Now;
            do
            {
                Console.WriteLine("Situação: [A - Ativo I - Inativo]");
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
                        string sql = $"INSERT INTO dbo.CompanhiaAerea (CNPJ, RazaoSocial, DataAbertura, UltimoVoo, DataCadastro, Situacao) VALUES ('{CNPJ}', '{RazaoSocial}', '{DataAbertura}', '{UltimoVoo}', '{DataCadastro}', '{Situacao}');";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.WriteLine("O CADASTRO FOI EFETUADO!");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Inválido!");
                    }
                }
                else
                    Console.WriteLine("CADASTRO DE COMPANHIA AEREA CANCELADO!");
            } while (escolha != 1 && escolha != 2);
        }
        public void VisualizarCompanhiaEspecifica(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o CNPJ para localizar o cadastro: ");
            this.CNPJ = Console.ReadLine();
            Console.WriteLine("Deseja visualizar os dados do cadastro de uma companhia aerea específica?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    Console.Clear();
                    Console.WriteLine("***CADASTRO DA COMPANHIA AEREA***");
                    Console.WriteLine("\n");
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
                            Console.WriteLine($"Data e Hora do último voo: {reader.GetDateTime(3)}");
                            Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
                            Console.WriteLine($"Situação: {reader.GetString(5)}");
                            Console.WriteLine("--------------------------------------------------------");

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
        public void VizualizarCompanhiaAtiva(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro ATIVO de TODAS as Companhias Aereas?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    Console.Clear();
                    Console.WriteLine("***CADASTRO DAS COMPANHIAS AEREAS***");
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
                            Console.WriteLine("--------------------------------------------------------");

                        }
                    }
                    conexaosql.Close();
                    Console.WriteLine("\nAperte ENTER para finalizar a visualização");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("\nA visualização dos cadastros foi cancelada!");
                }
            } while (opc != 1 && opc != 2);
        }
        public void EditarCompahiaAerea(SqlConnection conexaosql)
        {
            Console.WriteLine("Informe o CNPJ sem caracteres especiais para localizar o Cadastro:");
            this.CNPJ = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO

            Console.WriteLine("***COMPANHIA AEREA CADASTRADA***");

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
                    Console.WriteLine("--------------------------------------------------------");

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
                            if(dt > DateTime.Now)
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
        public void DeletarCompAereaEspecifica(SqlConnection conexaosql)
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
                    Console.WriteLine("--------------------------------------------------------");

                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a localização...");
            Console.ReadKey();
            Console.WriteLine("\nDeseja realmente DELETAR esse cadastro ?Não será possível repensar essa ação...");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    sql = $"DELETE FROM dbo.CompanhiaAerea WHERE CNPJ=('{this.CNPJ}');";
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
        public void DeletarTodasCompanhias(SqlConnection conexaosql)
        {
            Console.WriteLine("***DELETAR CADASTRO DE PASSAGEIROS***");
            Console.WriteLine("\n");
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
                    Console.WriteLine($"Data do último voo operado: {reader.GetString(3)}");
                    Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
                    Console.WriteLine($"Situação: {reader.GetString(5)}");
                    Console.WriteLine("--------------------------------------------------------");

                }
            }
            conexaosql.Close();
            Console.WriteLine("\nAperte ENTER para finalizar a visualização...");
            Console.ReadKey();
            Console.WriteLine("\nDeseja realmente DELETAR TODOS os CADASTROS? Não será possível repensar essa ação... ");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    sql = $"DELETE FROM dbo.CompanhiaAerea";
                    db = new ConexaoBD();
                    db.Connection(conexaosql, sql);
                    cmd = new SqlCommand(sql, conexaosql);
                    Console.WriteLine("\nCADASTROS DELETADOS COM SUCESSO!");
                    Console.WriteLine("Pressione ENTER para sair");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Exclusão de cadastros CANCELADA!!");
                }
            } while (opc != 1 && opc != 2);
        }
        public static bool ValidarCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            string digito, tempCnpj;

            //limpa caracteres especiais e deixa em minusculo
            cnpj = cnpj.ToLower().Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            cnpj = cnpj.Replace("+", "").Replace("*", "").Replace(",", "").Replace("?", "");
            cnpj = cnpj.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "");
            cnpj = cnpj.Replace("%", "").Replace("¨", "").Replace("&", "").Replace("(", "");
            cnpj = cnpj.Replace("=", "").Replace("[", "").Replace("]", "").Replace(")", "");
            cnpj = cnpj.Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "");
            cnpj = cnpj.Replace("<", "").Replace(">", "").Replace("ç", "").Replace("Ç", "");

            // Se vazio
            if (cnpj.Length == 0)
                return false;

            //Se o tamanho for < 14 então retorna como falso
            if (cnpj.Length != 14)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cnpj)
            {

                case "00000000000000":

                    return false;

                case "11111111111111":

                    return false;

                case "22222222222222":

                    return false;

                case "33333333333333":

                    return false;

                case "44444444444444":

                    return false;

                case "55555555555555":

                    return false;

                case "66666666666666":

                    return false;

                case "77777777777777":

                    return false;

                case "88888888888888":

                    return false;

                case "99999999999999":

                    return false;
            }

            tempCnpj = cnpj.Substring(0, 12);

            //cnpj é gerado a partir de uma função matemática, logo para validar, sempre irá utilizar esse calculo 
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cnpj.EndsWith(digito);

        }
    }
}
