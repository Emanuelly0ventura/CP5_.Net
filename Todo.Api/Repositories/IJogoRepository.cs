using Todo.Api.Models;

namespace Todo.Api.Repositories;

public interface IJogoRepository
{
    Task<IReadOnlyCollection<Jogo>> GetAllAsync();
    Task<Jogo?> GetByIdAsync(string id);
    Task<Jogo> CreateAsync(Jogo jogo);
    Task<bool> UpdateAsync(string id, Jogo jogo);
    Task<bool> DeleteAsync(string id);
    Task<IReadOnlyCollection<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo);
    Task<IReadOnlyCollection<EstoqueRelatorio>> RelatorioEstoqueAsync();
}
