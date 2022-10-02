using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnTheFly_BD
{
    internal class Venda
    {
        public string IdVenda { get; set; } //CHAVE
        public DateTime DataVenda { get; set; }
        public string CpfPassageiro { get; set; }
        public float ValorTotal { get; set; }
        public void CadastrarVenda()
        {


        }

    }
}
