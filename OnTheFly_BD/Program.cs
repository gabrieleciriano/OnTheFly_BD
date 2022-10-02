using System;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class Program
    {
        //static Aeronave a = new Aeronave();

        //static Voo voo = new Voo();
        static void Main(string[] args)
        {

            MenuPrincipal();

            //a.CadastrarAeronave(conexaosql);
            //a.EditarAeronave(conexaosql);
            //a.VisualizarAeronaveEspecifica(conexaosql);
            // a.VisualizarAeronavesAtivas(conexaosql);
            //voo.CadastrarVoo(conexaosql);
            //voo.VisualizarVooEspecifico(conexaosql);
            //voo.VisualizarVoosAtivos(conexaosql);
            // voo.VisualizarVoosCancelados(conexaosql);
            //voo.DeletarVooEspecifico();
            //voo.DeletarTodosVoos();

        }
        public static void MenuPrincipal()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>AEROPORTO ON THE FLY<<<");
                Console.WriteLine("0 - Sair do Menu Principal");
                Console.WriteLine("1 - Menu Passageiro");
                Console.WriteLine("2 - Menu Companhia Aerea");
                Console.WriteLine("3 - Menu Aeronave");
                Console.WriteLine("4 - Menu Voo");
                Console.WriteLine("5 - Menu Passagens");
                Console.WriteLine("6 - Menu Venda");
                Console.WriteLine("7 - Menu Item Venda");
                Console.WriteLine("\n>>Informe o que deseja acessar...");
                opc = int.Parse(Console.ReadLine());
                if (opc < 0 || opc > 7)
                    Console.WriteLine("OPÇÃO INVÁLIDA! Informe um número válido para acessar o menu:");
                else
                {
                    Console.Clear();
                    switch (opc)
                    {
                        case 0:
                            Console.WriteLine("SAINDO...");
                            Environment.Exit(0);
                            break;
                        case 1:
                            MenuPassageiro();
                            break;

                        case 2:

                            break;

                        case 3:

                            break;

                        default:
                            Console.WriteLine("OPÇÃO INVÁLIDA! Informe uma das opções segundo o menu!");
                            break;
                    }
                }
            } while (opc != 0);
        }
        public static void MenuPassageiro()
        {

            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>MENU PASSAGEIRO<<<");
                Console.WriteLine("1 - Voltar ao Menu Principal");
                Console.WriteLine("2 - Cadastrar Passageiro");
                Console.WriteLine("3 - Buscar cadastro específico");
                Console.WriteLine("4 - Visualizar todos os cadastros ativos");
                Console.WriteLine("5 - Visualizar todos os cadastros inativos");
                Console.WriteLine("6 - Editar dados do cadastro");
                Console.WriteLine("7 - Deletar passageiro específico");
                Console.WriteLine("8 - Deletar cadastro de todos os passageiros");
                Console.WriteLine("\n>>Informe o que deseja acessar...");
                opc = int.Parse(Console.ReadLine());
                if (opc <= 0 || opc > 8)
                    Console.WriteLine("OPÇÃO INVÁLIDA! Informe um número válido para acessar o menu:");
                else
                {
                    Console.Clear();
                    switch (opc)
                    {
                        case 1:
                            MenuPrincipal();

                            break;

                        case 2:
                            Passageiro p = new Passageiro();
                            ConexaoBD db = new ConexaoBD();
                            SqlConnection conexaosql = new SqlConnection(db.Caminho());
                            p.CadastrarPassageiro(conexaosql);
                            break;

                        case 3:
                            p = new Passageiro();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.VisualizarPassageiro(conexaosql);
                            break;

                        case 4:
                            p = new Passageiro();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.VisualizarPassageiroAtivo(conexaosql);
                            break;

                        case 5:
                            //visualizar tds inativos
                            break;

                        case 6:
                            p = new Passageiro();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.EditarPassageiro(conexaosql);
                            break;

                        case 7:
                            p = new Passageiro();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.DeletarPassageiroEspecifico(conexaosql);
                            break;

                        case 8:
                            p = new Passageiro();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.DeletarTodosPassageiros(conexaosql);
                            break;

                        default:
                            Console.WriteLine("OPÇÃO INVÁLIDA! Informe uma das opções segundo o menu!");
                            break;
                    }
                }
            } while (opc <= 0 || opc > 8);
        }
        public static void MenuCompAerea()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>MENU COMPANHIA AEREA<<<");
                Console.WriteLine("1 - Voltar ao Menu Principal");
                Console.WriteLine("2 - Cadastrar Companhia Aerea");
                Console.WriteLine("3 - Buscar cadastro específico");
                Console.WriteLine("4 - Visualizar todos os cadastros ativos");
                Console.WriteLine("5 - Visualizar todos os cadastros inativos");
                Console.WriteLine("6 - Editar dados do cadastro");
                Console.WriteLine("7 - Deletar companhia aerea específica");
                Console.WriteLine("8 - Deletar cadastro de todas as companhias");
                Console.WriteLine("\n>>Informe o que deseja acessar...");
                opc = int.Parse(Console.ReadLine());
                if (opc <= 0 || opc > 8)
                    Console.WriteLine("OPÇÃO INVÁLIDA! Informe um número válido para acessar o menu:");
                else
                {
                    Console.Clear();
                    switch (opc)
                    {
                        case 1:
                            MenuPrincipal();
                            break;

                        case 2:
                            CompanhiaAerea ca = new CompanhiaAerea();
                            ConexaoBD db = new ConexaoBD();
                            SqlConnection conexaosql = new SqlConnection(db.Caminho());
                            ca.CadastroCompanhiaAerea(conexaosql);
                            break;

                        case 3:
                            ca = new CompanhiaAerea();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            ca.VisualizarCompanhiaEspecifica(conexaosql);
                            break;

                        case 4:
                            ca = new CompanhiaAerea();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            ca.VizualizarCompanhiaAtiva(conexaosql);
                            break;

                        case 5:
                            //visualizar tds inativos
                            break;

                        case 6:
                            ca = new CompanhiaAerea();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            ca.EditarCompahiaAerea(conexaosql);
                            break;

                        case 7:
                            ca = new CompanhiaAerea();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            ca.DeletarCompAereaEspecifica(conexaosql);
                            break;

                        case 8:
                            ca = new CompanhiaAerea();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            ca.DeletarTodasCompanhias(conexaosql);
                            break;

                        default:
                            Console.WriteLine("OPÇÃO INVÁLIDA! Informe uma das opções segundo o menu!");
                            break;
                    }
                }
            } while (opc <= 0 || opc > 8);
        }
        public static void MenuAeronave()
        {

        }
        public static void MenuVoo()
        {

        }
    }
}
