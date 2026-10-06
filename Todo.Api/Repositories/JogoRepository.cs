using MongoDB.Bson;
using MongoDB.Driver;
using Todo.Api.Models;

namespace Todo.Api.Repositories;

public class JogoRepository : IJogoRepository
{
    private readonly IMongoCollection<Jogo> _jogos;

    public JogoRepository(IMongoDatabase database)
    {
        _jogos = database.GetCollection<Jogo>("jogos");
    }

    public async Task<IReadOnlyCollection<Jogo>> GetAllAsync() =>
        await _jogos.Find(FilterDefinition<Jogo>.Empty)
            .SortBy(j => j.Titulo)
            .ToListAsync();

    public async Task<Jogo?> GetByIdAsync(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return null;
        return await _jogos.Find(j => j.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Jogo> CreateAsync(Jogo jogo)
    {
        jogo.Id = ObjectId.GenerateNewId().ToString();
        await _jogos.InsertOneAsync(jogo);
        return jogo;
    }

    public async Task<bool> UpdateAsync(string id, Jogo jogo)
    {
        if (!ObjectId.TryParse(id, out _)) return false;

        jogo.Id = id;
        var result = await _jogos.ReplaceOneAsync(j => j.Id == id, jogo);
        return result.MatchedCount > 0;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        if (!ObjectId.TryParse(id, out _)) return false;

        var result = await _jogos.DeleteOneAsync(j => j.Id == id);
        return result.DeletedCount > 0;
    }

    public async Task<IReadOnlyCollection<Jogo>> BuscarAsync(string plataforma, decimal precoMaximo)
    {
        var filter = Builders<Jogo>.Filter.And(
            Builders<Jogo>.Filter.Eq(j => j.Plataforma, plataforma),
            Builders<Jogo>.Filter.Lte(j => j.Preco, precoMaximo)
        );

        return await _jogos.Find(filter)
            .SortBy(j => j.Preco)
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<EstoqueRelatorio>> RelatorioEstoqueAsync()
    {
        var resultado = await _jogos.Aggregate()
            .Group(
                j => j.Plataforma,
                g => new EstoqueRelatorio
                {
                    Plataforma = g.Key,
                    QuantidadeTitulos = g.Count(),
                    ValorTotalInventario = g.Sum(j => j.Preco * j.Estoque)
                })
            .SortBy(r => r.Plataforma)
            .ToListAsync();

        return resultado;
    }
}
