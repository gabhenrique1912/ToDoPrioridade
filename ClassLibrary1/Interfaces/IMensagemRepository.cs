using ClassLibrary2;

public interface IMensagemRepository
{
    Mensagem GetMensagemById(int id);
    bool AddMensagem(Mensagem mensagem);
    List<Mensagem> GetAll();
    void UpdateMensagem(int id, Mensagem mensagem);
    bool DeleteMensagem(int id);
}