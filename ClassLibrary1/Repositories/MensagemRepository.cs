using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Repositories
{
    public class MensagemRepository : IMensagemRepository
    {
        private readonly SqlContext sqlContext;
        public MensagemRepository()
        {
            sqlContext = new SqlContext();
        }

        public Mensagem AddMensagem(Mensagem mensagem) 
        {
            var result = sqlContext.Mensagens.Add(mensagem);
            sqlContext.SaveChanges();
            return result.Entity; // Retorna a mensagem persistida
        }

        public bool DeleteMensagem(int id)
        {
            var mensagem = sqlContext.Mensagens.Find(id);
            sqlContext.Mensagens.Remove(mensagem);
            sqlContext.SaveChanges();
            return true;
        }

        public List<Mensagem> GetAll()
        {
            return sqlContext.Mensagens.ToList();
        }

        public Mensagem GetMensagemById(int id)
        {
            return sqlContext.Mensagens.Find(id);
        }

        public void UpdateMensagem(int id, Mensagem mensagem)
        {
            var existente = sqlContext.Mensagens.Find(id);
            if (existente == null) return;
            
            //desse jeito abaixo n atualiza o campo id e atualiza só os campos abaixo, evitando dar bug
            existente.Tarefa = mensagem.Tarefa;
            existente.Descricao = mensagem.Descricao;
            existente.Prioridade = existente.Prioridade;

            sqlContext.SaveChanges();
        }

    }
}
