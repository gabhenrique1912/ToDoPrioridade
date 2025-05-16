using ClassLibrary2;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // Adicione este atributo para habilitar comportamentos comuns da API
    [Route("api/[controller]")] // Define uma rota base para o seu controller (ex: /api/Mensagem)
    public class MensagemController : ControllerBase
    {
        private readonly IMensagemRepository _context;

        public MensagemController(IMensagemRepository context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Mensagem>> GetAllMensagens() // Renomeie para evitar confusão com o GetById
        {
            return _context.GetAll();
        }

        [HttpGet("{id}")] // Especifica que este GET espera um 'id' no path
        public ActionResult<Mensagem> GetMensagemById(int id)
        {
            var mensagem = _context.GetMensagemById(id);

            if (mensagem == null)
            {
                return NotFound();
            }

            return mensagem;
        }

        [HttpPost] // Este POST será mapeado para a rota base (/api/Mensagem)
        public ActionResult<Mensagem> AddMensagem(Mensagem mensagem)
        {
            _context.AddMensagem(mensagem);
            return CreatedAtAction(nameof(GetMensagemById), new { id = mensagem.Id }, mensagem); // Use nameof para evitar erros de string
        }

        [HttpPut("{id}")] // Especifica que este PUT espera um 'id' no path
        public IActionResult UpdateMensagem(int id, Mensagem mensagem)
        {
            if (id != mensagem.Id)
                return BadRequest("Id não encontrado");
            var existingMensagem = _context.GetMensagemById(id);
            if (existingMensagem == null)
                return NotFound("Id não encontrado");
            _context.UpdateMensagem(id, mensagem);
            return NoContent();
        }

        [HttpDelete("{id}")] // Especifica que este DELETE espera um 'id' no path
        public IActionResult DeleteMensagem(int id)
        {
            var mensagem = _context.GetMensagemById(id);
            if (mensagem == null)
                return NotFound("Id não encontrado");

            _context.DeleteMensagem(id);
            return Ok("Mensagem deletada com sucesso");
        }
    }
}