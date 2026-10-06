using System.ComponentModel.DataAnnotations;

namespace Todo.Api.Contracts;

public class CreateJogoRequest
{
    [Required, StringLength(150, MinimumLength = 1)]
    public string Titulo { get; init; } = string.Empty;

    [Required, StringLength(80)]
    public string Plataforma { get; init; } = string.Empty;

    [Required, StringLength(80)]
    public string Genero { get; init; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Preco { get; init; }

    [Range(1950, 2100)]
    public int AnoLancamento { get; init; }

    [Range(0, int.MaxValue)]
    public int Estoque { get; init; }
}

public class UpdateJogoRequest : CreateJogoRequest
{
}
