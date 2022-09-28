using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Passageiro
    {
        public String CPF { get; set; } //CHAVE com 11 dígitos
        public String Nome { get; set; } // < 50 digitos
        public string DataNascimento { get; set; }
        public char Sexo { get; set; } //M F N
        public DateTime UltimaCompra { get; set; } //no cadastro, data atual string p converter
        public DateTime DataCadastro { get; set; } //talvez colocar como string pra converter
        public char Situacao { get; set; } //A - Ativo I - Inativo
        public ConexaoBD db;
        
        //Nao se pode deletar nenhum cadastro e sim inativar para nao aparecer na lista 
        public Passageiro()
        {

        }
        ConexaoBD banco = new ConexaoBD();

        //Já com o insert
        public void CadastrarPassageiro(SqlConnection conexaosql)
        {
            Console.WriteLine(">>>CADASTRO DE PASSAGEIRO<<<");
            do
            {
                Console.WriteLine("Informe o número de seu Cadastro de Pessoas Físicas (CPF) sem caracteres especiais: ");
                CPF = Console.ReadLine();
                if (ValidarCpf(CPF) == false)
                {
                    Console.WriteLine("NÚMERO DO CPF INVÁLIDO! INFORME UM CPF VÁLIDO");
                    Console.ReadKey();
                }

            } while (ValidarCpf(CPF) == false && CPF.Length < 11 || CPF.Length > 11);

            do
            {
                Console.WriteLine("Informe o Nome [Máximo 50 digítos]: ");
                Nome = Console.ReadLine();
                if (Nome.Length > 50)
                {
                    Console.WriteLine("\nInforme um nome que contenha até 50 caracteres!");

                }
                else
                    break;
            } while (Nome.Length > 50);

            DateTime datanasc;
            Console.Write("Informe a Data de Nascimento: ");
            while (!DateTime.TryParse(Console.ReadLine(), out datanasc))
            {
                if (datanasc > DateTime.Now)
                {
                    Console.WriteLine("Informe uma data de nascimento válida!");
                }
                else
                    Console.Write("Informe a Data de Nascimento: ");
            }
            DataNascimento = datanasc.ToString("dd/MM/yyyy");

            while (Sexo != 'M' && Sexo != 'F' && Sexo != 'N')
            {
                Console.WriteLine("Informe o sexo: [M - Masculino, F - Feminino, N - Não desejo informar] : ");
                Sexo = char.Parse(Console.ReadLine().ToUpper());
                if (Sexo != 'M' && Sexo != 'F' && Sexo != 'N')
                {
                    Console.WriteLine("OPÇÃO INVÁLIDA! INFORME (M, F OU N) ");
                }
            }
            UltimaCompra = DateTime.Now; //data atual do sistema
            DataCadastro = DateTime.Now; //data atual do sistema
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
                        string sql = $"INSERT INTO dbo.Passageiro (CPF, Nome, DataNascimento, Sexo, Data_Cadastro, UltimaCompra, Situacao) VALUES ('{CPF}', '{Nome}', '{DataNascimento}', '{Sexo}', '{DataCadastro}', '{UltimaCompra}', '{Situacao}');";
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
                    Console.WriteLine("CADASTRO DE PASSAGEIRO CANCELADO!");
            } while (escolha != 1 && escolha != 2);
        }

        public void VisualizarPassageiro(SqlConnection conexaosql)
        {
            //SELECT DE UM PASSAGEIRO ESPECIFICO
            Console.WriteLine("Informe o CPF para localizar o cadastro: ");
            this.CPF = Console.ReadLine();
            Console.WriteLine("Deseja localizar os dados do cadastro de um passageiro específico?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    string sql = $"SELECT CPF,Nome,DataNascimento,Sexo,Data_Cadastro,UltimaCompra,Situacao  FROM dbo.Passageiro WHERE CPF=('{this.CPF}');";
                    db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Nome: {reader.GetString(1)}");
                            Console.WriteLine($"CPF: {reader.GetString(0)}");
                            Console.WriteLine($"Data de Nascimento: {reader.GetDateTime(2).ToShortDateString()}");
                            Console.WriteLine($"Sexo: {reader.GetString(3)}");
                            Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
                            Console.WriteLine($"Data da Última Compra: {reader.GetDateTime(5).ToShortDateString()}");
                            Console.WriteLine($"Situação: {reader.GetString(6)}");
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
        public void VisualizarPassageiroAtivo(SqlConnection conexaosql)
        {
            Console.WriteLine("Deseja visualizar o cadastro ATIVO de todos os passageiros?");
            Console.WriteLine("Informe [1 - SIM, 2 - NÃO]: ");
            int opc;
            do
            {
                opc = int.Parse(Console.ReadLine());
                if (opc == 1)
                {
                    Console.Clear();
                    string sql = $"SELECT CPF, Nome, DataNascimento, Sexo, Data_Cadastro, UltimaCompra, Situacao  FROM dbo.Passageiro WHERE Situacao = ('A');";
                    db = new ConexaoBD();
                    db.Select(conexaosql, sql);
                    SqlCommand cmd = new SqlCommand(sql, conexaosql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"Nome: {reader.GetString(1)}");
                            Console.WriteLine($"CPF: {reader.GetString(0)}");
                            Console.WriteLine($"Data de Nascimento: {reader.GetDateTime(2).ToShortDateString()}");
                            Console.WriteLine($"Sexo: {reader.GetString(3)}");
                            Console.WriteLine($"Data do Cadastro: {reader.GetDateTime(4).ToShortDateString()}");
                            Console.WriteLine($"Data da Última Compra: {reader.GetDateTime(5).ToShortDateString()}");
                            Console.WriteLine($"Situação: {reader.GetString(6)}");
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
        //Já com o update
        public void EditarPassageiro(SqlConnection conexaosql)
        {
            string sql;
            Console.WriteLine("Informe o CPF para localizar o Cadastro:");
            this.CPF = Console.ReadLine(); //TRATAR ERROS SE INFORMAR UM QUE NAO FOI CADASTRADO
            int op;
            do
            {
                Console.WriteLine(">>> EDITAR CADASTRO <<<");
                Console.WriteLine("0 - SAIR");
                Console.WriteLine("1 - NOME");
                Console.WriteLine("2 - DATA DE NASCIMENTO");
                Console.WriteLine("3 - SEXO");
                Console.WriteLine("4 - DATA DO CADASTRO");
                Console.WriteLine("5 - DATA DA ÚLTIMA COMPRA");
                Console.WriteLine("6 - SITUAÇÃO DO CADASTRO ");
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
                            Console.WriteLine("Informe o Nome [Máximo 50 digítos]: ");
                            Nome = Console.ReadLine();
                            if (Nome.Length > 50)
                            {
                                Console.WriteLine("\nInforme um nome que contenha até 50 caracteres!");

                            }
                            else
                            {
                                sql = $"UPDATE dbo.Passageiro SET Nome='{Nome}' WHERE CPF='{this.CPF}';";
                                db = new ConexaoBD();
                                db.Connection(conexaosql, sql);
                                Console.ReadKey();
                            }
                        } while (Nome.Length > 50);
                        break;

                    case 2:
                        DateTime datanasc;
                        Console.Write("\nInforme a Data de Nascimento: ");
                        while (!DateTime.TryParse(Console.ReadLine(), out datanasc))
                        {
                            if (datanasc > DateTime.Now)
                            {
                                Console.WriteLine("\nInforme uma data de nascimento válida!");
                            }
                            else
                                Console.Write("\nInforme a Data de Nascimento: ");
                        }
                        DataNascimento = datanasc.ToString("dd/MM/yyyy");
                        sql = $"UPDATE dbo.Passageiro SET DataNascimento='{DataNascimento}' WHERE CPF='{this.CPF}';";
                        db = new ConexaoBD();
                        db.Connection(conexaosql, sql);
                        Console.ReadKey();
                        break;

                    case 3:
                        while (Sexo != 'M' && Sexo != 'F' && Sexo != 'N')
                        {
                            Console.WriteLine("\nInforme o sexo: [M - Masculino, F - Feminino, N - Não desejo informar] : ");
                            Sexo = char.Parse(Console.ReadLine().ToUpper());
                            if (Sexo != 'M' && Sexo != 'F' && Sexo != 'N')
                            {
                                Console.WriteLine("OPÇÃO INVÁLIDA! INFORME (M, F OU N) ");
                            }
                            else
                            {
                                sql = $"UPDATE dbo.Passageiro SET Sexo='{Sexo}' WHERE CPF='{this.CPF}';";
                                //db = new ConexaoBD();
                                db.Connection(conexaosql, sql);
                                Console.ReadKey();
                            }
                        }
                        break;

                    case 4:
                        DateTime dt;
                        Console.WriteLine("\nInforme a DATA DO CADASTRO: ");
                        DataCadastro = DateTime.Parse(Console.ReadLine());
                        while (!DateTime.TryParse(Console.ReadLine(), out dt))
                        {
                            dt.ToString("dd/MM/yyyy");
                            sql = $"UPDATE dbo.Passageiro SET Data_Cadastro='{dt}' WHERE CPF='{this.CPF}';";
                            db = new ConexaoBD();
                            db.Connection(conexaosql, sql);
                            Console.ReadKey();
                        }
                        break;

                    case 5:
                        DateTime data;
                        Console.WriteLine("\nInforme a Data da Última Compra: ");
                        UltimaCompra = DateTime.Parse(Console.ReadLine());
                        while (!DateTime.TryParse(Console.ReadLine(), out data))
                        {
                            data.ToString("dd/MM/yyyy");
                            sql = $"UPDATE dbo.Passageiro SET UltimaCompra='{UltimaCompra}' WHERE CPF='{this.CPF}';";
                            db = new ConexaoBD();
                            db.Connection(conexaosql, sql);
                            Console.ReadKey();
                        }

                        break;
                    case 6:
                        do
                        {
                            Console.WriteLine("Informe a SITUAÇÃO do cadastro correta (A - Ativo, I - Inativo): ");
                            Situacao = char.Parse(Console.ReadLine());
                            sql = $"UPDATE dbo.Passageiro SET Situacao='{Situacao}' WHERE CPF='{this.CPF}';";
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

        public static bool ValidarCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf, digito;
            int soma, resto;

            //Formatando para deixar o CPF somente com os números, sem caracteres especiais
            cpf = cpf.ToLower().Trim();
            cpf = cpf.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            cpf = cpf.Replace("+", "").Replace("*", "").Replace(",", "").Replace("?", "");
            cpf = cpf.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "");
            cpf = cpf.Replace("%", "").Replace("¨", "").Replace("&", "").Replace("(", "");
            cpf = cpf.Replace("=", "").Replace("[", "").Replace("]", "").Replace(")", "");
            cpf = cpf.Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "");
            cpf = cpf.Replace("<", "").Replace(">", "").Replace("ç", "").Replace("Ç", "");


            //Se o CPF for informado vazio
            if (cpf.Length == 0)
                return false;

            //Se a quantidade de dígitos for diferente do permitido (11)
            if (cpf.Length != 11)
                return false;

            //Se os números informados forem todos iguais
            switch (cpf)
            {

                case "00000000000":

                    return false;

                case "11111111111":

                    return false;

                case "22222222222":

                    return false;

                case "33333333333":

                    return false;

                case "44444444444":

                    return false;

                case "55555555555":

                    return false;

                case "66666666666":

                    return false;

                case "77777777777":

                    return false;

                case "88888888888":

                    return false;

                case "99999999999":

                    return false;
            }

            tempCpf = cpf.Substring(0, 9);

            //Calculo para gerar um número de CPF válido
            soma = 0;
            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
        public bool cpfAlreadyExists(SqlConnection conn, string CPF)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT CPF FROM dbo.Passageiro WHERE CPF = @CPF;";
            cmd.Connection = conn;
            cmd.Parameters.AddWithValue("@CPF", CPF);

            bool cpfAlreadyExists = false;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        cpfAlreadyExists = false;
                    }

                    else
                    {
                        cpfAlreadyExists = true;
                    }
                }
            }
            return cpfAlreadyExists;
        }

    }
}



