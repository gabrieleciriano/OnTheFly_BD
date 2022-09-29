using System;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Passageiro p = new Passageiro();
            CompanhiaAerea ca = new CompanhiaAerea();
            Aeronave a = new Aeronave();
            ConexaoBD db = new ConexaoBD();
            Voo voo = new Voo();
            SqlConnection conexaosql = new SqlConnection(db.Caminho());
            //p.CadastrarPassageiro(conexaosql);
            // p.EditarPassageiro(conexaosql);
            //p.VisualizarPassageiro(conexaosql);
            //p.VisualizarPassageiroAtivo(conexaosql);
            //p.DeletarPassageiroEspecifico(conexaosql);
            //p.DeletarTodosPassageiros(conexaosql);
            //ca.CadastroCompanhiaAerea(conexaosql);
            //ca.VisualizarCompanhiaEspecifica(conexaosql);
            //ca.VizualizarCompanhiaAtiva(conexaosql);
            //ca.DeletarCompAereaEspecifica(conexaosql);
            //ca.DeletarTodasCompanhias(conexaosql);
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
    }
}
