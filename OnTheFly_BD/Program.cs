﻿using System;
using System.Data.SqlClient;

namespace OnTheFly_BD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Passageiro p = new Passageiro();
            CompanhiaAerea ca = new CompanhiaAerea();
            ConexaoBD db = new ConexaoBD();
            SqlConnection conexaosql = new SqlConnection(db.Caminho());
            //p.CadastrarPassageiro(conexaosql);
           // p.EditarPassageiro(conexaosql);
            //p.VisualizarPassageiro(conexaosql);
           //p.VisualizarPassageiroAtivo(conexaosql);
            //ca.CadastroCompanhiaAerea(conexaosql);
            //p.DeletarPassageiroEspecifico(conexaosql);
            p.DeletarTodosPassageiros(conexaosql);
        }
    }
}
