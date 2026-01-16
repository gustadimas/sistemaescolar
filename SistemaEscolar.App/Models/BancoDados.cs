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

    public static List<Aluno> Listar()
    {
        return _alunos
            .OrderByDescending(aluno => aluno.DataCadastro)
            .ToList();
    }

    public static Aluno? Listar(ulong id)
    {
        return _alunos.FirstOrDefault(aluno => aluno.Id == id);
    }

    public static void Adicionar(Aluno aluno)
    {
        try
        {
            var ultimoId = _alunos.Count == 0 ? 0UL : _alunos.Max(item => item.Id);

            aluno.Id = ultimoId + 1;
            aluno.DataCadastro = DateTime.Now;
            aluno.DataAtualizacao = null;

            _alunos.Add(aluno);
        }
        catch (Exception ex)
        {
            throw new Exception(
                "Ocorreu um erro não tratado. Por favor, contate o time de sistemas!",
                ex);
        }
    }

    public static bool Atualizar(Aluno alunoAtualizado)
    {
        try
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
        catch (Exception ex)
        {
            throw new Exception(
                "Ocorreu um erro não tratado. Por favor, contate o time de sistemas!",
                ex);
        }
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