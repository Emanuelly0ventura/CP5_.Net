using Microsoft.AspNetCore.Mvc;
using Todo.Api.Contracts;
using Todo.Api.Services;

namespace Todo.Api.Controllers;

[ApiController]
[Route("api/jogos")]
public class JogosController(IJogoService service, ILogger<JogosController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAll()
    {
        var jogos = await service.GetAllAsync();
        return Ok(jogos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetById(string id)
    {
        var jogo = await service.GetByIdAsync(id);
        return jogo is null
            ? NotFound(new { message = "Jogo não encontrado." })
            : Ok(jogo);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateJogoRequest request)
    {
        var jogo = await service.CreateAsync(request);
        logger.LogInformation("Jogo {JogoId} criado: {Titulo}", jogo.Id, jogo.Titulo);

        return CreatedAtAction(nameof(GetById), new { id = jogo.Id }, jogo);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(string id, UpdateJogoRequest request)
    {
        var updated = await service.UpdateAsync(id, request);

        if (!updated)
            return NotFound(new { message = "Jogo não encontrado." });

        return Ok(await service.GetByIdAsync(id));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var deleted = await service.DeleteAsync(id);

        return deleted
            ? NoContent()
            : NotFound(new { message = "Jogo não encontrado." });
    }

    [HttpGet("busca")]
    public async Task<ActionResult> Buscar(
        [FromQuery] string plataforma,
        [FromQuery] decimal precoMaximo)
    {
        if (string.IsNullOrWhiteSpace(plataforma))
            return BadRequest(new { message = "Informe a plataforma." });

        var jogos = await service.BuscarAsync(plataforma, precoMaximo);
        return Ok(jogos);
    }

    [HttpGet("relatorio-estoque")]
    public async Task<ActionResult> RelatorioEstoque()
    {
        var relatorio = await service.RelatorioEstoqueAsync();
        return Ok(relatorio);
    }
}
