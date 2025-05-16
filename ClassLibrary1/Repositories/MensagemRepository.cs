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

        public bool AddMensagem(Mensagem mensagem)
        {
            if (mensagem == null) return false;
            sqlContext.Mensagens.Add(mensagem);
            sqlContext.SaveChanges();
            return true;
        }

        public bool DeleteMensagem(int id)
        {
            if (id <= 0) return false;
            var mensagem = sqlContext.Mensagens.Find(id);
            sqlContext.Mensagens.Remove(mensagem);
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
            sqlContext.Mensagens.Update(mensagem);
            sqlContext.SaveChanges();
        }
    }
}
