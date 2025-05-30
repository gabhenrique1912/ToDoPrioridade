﻿using ClassLibrary2;
using ClassLibrary3;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Channels;

namespace API.Controllers
{
    [ApiController] // Adicione este atributo para habilitar comportamentos comuns da API
    [Route("api/[controller]")] // Define uma rota base para o seu controller (ex: /api/Mensagem)
    public class MensagemController : ControllerBase
    {
        private readonly IMensagemRepository _context;
        private readonly IRabbitMQProducer _rabbitMQProducer;

        public MensagemController(IMensagemRepository context, IRabbitMQProducer rabitMQProducer)
        {
            _context = context;
            _rabbitMQProducer = rabitMQProducer;
        }

        [HttpPost] // Este POST será mapeado para a rota base (/api/Mensagem)

        public ActionResult<Mensagem> AddMensagem(Mensagem mensagem)
        {
            var mensagemPost = _context.AddMensagem(mensagem);
            _rabbitMQProducer.SendProductMessage(mensagemPost);

            return CreatedAtAction(nameof(GetMensagemById), new { id = mensagem.Id }, mensagem); // Usa nameof para evitar erros de string
        }

        [HttpGet]
        public ActionResult<List<Mensagem>> GetAllMensagens() 
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

        [HttpPut("{id}")] // Especifica que este PUT espera um 'id' no path
        public IActionResult UpdateMensagem(int id, Mensagem mensagem)
        {
            var existingMensagem = _context.GetMensagemById(id);
            if (existingMensagem == null)
                return NotFound("Id não encontrado...");
            else
            {
                _context.UpdateMensagem(id, mensagem);
                return Ok("Mensagem atualizada com sucesso...");
            }

        }

        [HttpDelete("{id}")] // Especifica que este DELETE espera um 'id' no path
        public IActionResult DeleteMensagem(int id)
        {
            var mensagem = _context.GetMensagemById(id);
            if (mensagem == null)
            {
                return NotFound("Id não encontrado...");
            }
            else
            {
                _context.DeleteMensagem(id);
                return Ok("Mensagem deletada com sucesso...");
            }

        }

 

        

        

        
    }
}