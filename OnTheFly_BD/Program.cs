using System;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class Program
    {
        static void Main(string[] args)
        {

            MenuPrincipal();

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
                            MenuCompAerea();
                            break;

                        case 3:
                            MenuAeronave();
                            break;
                        case 4:
                            MenuVoo();
                            break;

                        case 5:
                            MenuPassagem();
                            break;

                        case 6:
                            MenuVenda();
                            break;

                        case 7:
                            MenuItemVenda();
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
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>MENU AERONAVE<<<");
                Console.WriteLine("1 - Voltar ao Menu Principal");
                Console.WriteLine("2 - Cadastrar Aeronave");
                Console.WriteLine("3 - Buscar cadastro específico");
                Console.WriteLine("4 - Visualizar todos os cadastros ativos");
                Console.WriteLine("5 - Visualizar todos os cadastros inativos");
                Console.WriteLine("6 - Editar dados do cadastro");
                Console.WriteLine("7 - Deletar aeronave específica");
                Console.WriteLine("8 - Deletar cadastro de todas as aeronaves");
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
                            Aeronave a = new Aeronave();
                            ConexaoBD db = new ConexaoBD();
                            SqlConnection conexaosql = new SqlConnection(db.Caminho());
                            a.CadastrarAeronave(conexaosql);
                            break;

                        case 3:
                            a = new Aeronave();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            a.VisualizarAeronaveEspecifica(conexaosql);
                            break;

                        case 4:
                            a = new Aeronave();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            a.VisualizarAeronavesAtivas(conexaosql);
                            break;

                        case 5:
                            //visualizar tds inativos
                            break;

                        case 6:
                            a = new Aeronave();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            a.EditarAeronave(conexaosql);
                            break;

                        case 7:
                            a = new Aeronave();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            a.DeletarAeronaveEspecifica(conexaosql);
                            break;

                        case 8:
                            a = new Aeronave();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            a.DeletarTodasAeronaves(conexaosql);
                            break;

                        default:
                            Console.WriteLine("OPÇÃO INVÁLIDA! Informe uma das opções segundo o menu!");
                            break;
                    }
                }
            } while (opc <= 0 || opc > 8);
        }
        public static void MenuVoo()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>MENU VOO<<<");
                Console.WriteLine("1 - Voltar ao Menu Principal");
                Console.WriteLine("2 - Cadastrar Voo");
                Console.WriteLine("3 - Buscar cadastro específico");
                Console.WriteLine("4 - Visualizar todos os voos ativos");
                Console.WriteLine("5 - Visualizar todos os voos cancelados");
                Console.WriteLine("6 - Editar dados do cadastro");
                Console.WriteLine("7 - Deletar voo específico");
                Console.WriteLine("8 - Deletar cadastro de todos os voos");
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
                            Voo voo = new Voo();
                            ConexaoBD db = new ConexaoBD();
                            SqlConnection conexaosql = new SqlConnection(db.Caminho());
                            voo.CadastrarVoo(conexaosql);
                            break;

                        case 3:
                            voo = new Voo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            voo.VisualizarVooEspecifico(conexaosql);
                            break;

                        case 4:
                            voo = new Voo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            voo.VisualizarVoosAtivos(conexaosql);
                            break;

                        case 5:
                            voo = new Voo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            voo.VisualizarVoosCancelados(conexaosql);
                            break;

                        case 6:
                            voo = new Voo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            voo.EditarVoo(conexaosql);
                            break;

                        case 7:
                            voo = new Voo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            voo.DeletarVooEspecifico(conexaosql);
                            break;

                        case 8:
                            voo = new Voo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            voo.DeletarTodosVoos(conexaosql);
                            break;

                        default:
                            Console.WriteLine("OPÇÃO INVÁLIDA! Informe uma das opções segundo o menu!");
                            break;
                    }
                }
            } while (opc <= 0 || opc > 8);
        }
        public static void MenuPassagem()
        {
            int opc = 0;
            do
            {
                Console.Clear();
                Console.WriteLine(">>>MENU PASSAGEM<<<");
                Console.WriteLine("1 - Voltar ao Menu Principal");
                Console.WriteLine("2 - Cadastrar Passagem");
                Console.WriteLine("3 - Buscar cadastro específico");
                Console.WriteLine("4 - Visualizar todas as passagens livres");
                Console.WriteLine("5 - Visualizar todas as passagens reservadas");
                Console.WriteLine("6 - Visualizar todas as passagens pagas");
                Console.WriteLine("7 - Editar dados da passagem");
                Console.WriteLine("8 - Deletar passagem específica");
                Console.WriteLine("9 - Deletar todas as passagens");
                Console.WriteLine("\n>>Informe o que deseja acessar...");
                opc = int.Parse(Console.ReadLine());
                if (opc <= 0 || opc > 9)
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
                            PassagemVoo p = new PassagemVoo();
                            ConexaoBD db = new ConexaoBD();
                            SqlConnection conexaosql = new SqlConnection(db.Caminho());
                            p.CadastrarPassagem(conexaosql);
                            break;

                        case 3:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.VisualizarPassagemEspecifica(conexaosql);
                            break;

                        case 4:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.VisualizarPassagensLivres(conexaosql);
                            break;

                        case 5:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.VisualizarPassagensReservadas(conexaosql);
                            break;

                        case 6:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.VisualizarPassagensPagas(conexaosql);
                            break;

                        case 7:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.EditarPassagem(conexaosql);
                            break;

                        case 8:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.DeletarPassagemEspecifica(conexaosql);
                            break;

                        case 9:
                            p = new PassagemVoo();
                            db = new ConexaoBD();
                            conexaosql = new SqlConnection(db.Caminho());
                            p.DeletarTodasPassagens(conexaosql);
                            break;

                        default:
                            Console.WriteLine("OPÇÃO INVÁLIDA! Informe uma das opções segundo o menu!");
                            break;
                    }
                }
            } while (opc <= 0 || opc > 9);
        }
        public static void MenuVenda()
        {

        }
        public static void MenuItemVenda()
        {

        }
    }
}
