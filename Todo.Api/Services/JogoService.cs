using Todo.Api.Contracts;
using Todo.Api.Models;
using Todo.Api.Repositories;

namespace Todo.Api.Services;

public interface IJogoService
{
    Task<IReadOnlyCollection<Jogo>> GetAllAsync();
    Task<Jogo?> GetByIdAsync(string id);
    Task<Jogo> CreateAsync(CreateJogoRequest request);
    Task<bool> UpdateAsync(string id, UpdateJogoRequest request);
    Task<bool> DeleteAsync(string id);
    Task<IReadOnlyCollection<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo);
    Task<IReadOnlyCollection<EstoqueRelatorio>> RelatorioEstoqueAsync();
}

public class JogoService(IJogoRepository repository) : IJogoService
{
    public Task<IReadOnlyCollection<Jogo>> GetAllAsync() =>
        repository.GetAllAsync();

    public Task<Jogo?> GetByIdAsync(string id) =>
        repository.GetByIdAsync(id);

    public Task<Jogo> CreateAsync(CreateJogoRequest request) =>
        repository.CreateAsync(new Jogo
        {
            Titulo = request.Titulo.Trim(),
            Plataforma = request.Plataforma.Trim(),
            Genero = request.Genero.Trim(),
            Preco = request.Preco,
            AnoLancamento = request.AnoLancamento,
            Estoque = request.Estoque
        });

    public Task<bool> UpdateAsync(string id, UpdateJogoRequest request) =>
        repository.UpdateAsync(id, new Jogo
        {
            Id = id,
            Titulo = request.Titulo.Trim(),
            Plataforma = request.Plataforma.Trim(),
            Genero = request.Genero.Trim(),
            Preco = request.Preco,
            AnoLancamento = request.AnoLancamento,
            Estoque = request.Estoque
        });

    public Task<bool> DeleteAsync(string id) =>
        repository.DeleteAsync(id);

    public Task<IReadOnlyCollection<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo) =>
        repository.BuscarAsync(plataforma.Trim(), precoMaximo);

    public Task<IReadOnlyCollection<EstoqueRelatorio>> RelatorioEstoqueAsync() =>
        repository.RelatorioEstoqueAsync();
}
