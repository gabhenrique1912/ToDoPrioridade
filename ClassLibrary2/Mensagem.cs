using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary2 
{
    public class Mensagem
    {
        public int Id { get; set; }
        public string? Tarefa { get; set; }
        public string? Descricao { get; set; }
        public int Prioridade { get; set; }

    }
}
