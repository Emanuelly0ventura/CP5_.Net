namespace Todo.Api.Models;

public class EstoqueRelatorio
{
    public string Plataforma { get; set; } = string.Empty;
    public int QuantidadeTitulos { get; set; }
    public decimal ValorTotalInventario { get; set; }
}
