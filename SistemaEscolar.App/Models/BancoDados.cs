using SistemaEscolar.App.Enums;

namespace SistemaEscolar.App.Models;

public static class BancoDados
{
    private static readonly List<Aluno> _alunos = new()
    {
        new Aluno
        {
            Id = 1,
            Nome = "Gustavo Dimas Nascimento de Souza Silva",
            Idade = 17,
            Escolaridade = Escolaridade.Medio,
            DataCadastro = new DateTime(2026, 1, 1),
            DataAtualizacao = null
        }
    };

    public static List<Aluno> ObterLista()
    {
        return _alunos.ToList();
    }

    public static Aluno? ObterPorId(ulong id)
    {
        return _alunos.FirstOrDefault(aluno => aluno.Id == id);
    }

    public static void Adicionar(Aluno aluno)
    {
        var ultimoId = _alunos.Count == 0 ? 0UL : _alunos.Max(item => item.Id);

        aluno.Id = ultimoId + 1;
        aluno.DataCadastro = DateTime.Now;
        aluno.DataAtualizacao = null;

        _alunos.Add(aluno);
    }

    public static bool Atualizar(Aluno alunoAtualizado)
    {
        var aluno = _alunos.FirstOrDefault(item => item.Id == alunoAtualizado.Id);
        if (aluno is null)
        {
            return false;
        }

        aluno.Nome = alunoAtualizado.Nome;
        aluno.Idade = alunoAtualizado.Idade;
        aluno.Escolaridade = alunoAtualizado.Escolaridade;
        aluno.DataAtualizacao = DateTime.Now;

        return true;
    }

    public static bool Remover(ulong id)
    {
        var aluno = _alunos.FirstOrDefault(item => item.Id == id);
        if (aluno is null)
        {
            return false;
        }

        return _alunos.Remove(aluno);
    }
}