using SistemaEscolar.App.Enums;

namespace SistemaEscolar.App.Models;

public class Aluno
{
    public ulong Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public uint Idade { get; set; }
    public Escolaridade Escolaridade { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; } = null;
}