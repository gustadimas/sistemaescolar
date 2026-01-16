using SistemaEscolar.App.Enums;
using SistemaEscolar.App.Models;

bool sair = false;

while (!sair)
{
    ExibirMenu();
    var opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            MenuListar();
            break;
        case "2":
            MenuCadastrar();
            break;
        case "3":
            MenuEditar();
            break;
        case "4":
            MenuRemover();
            break;
        case "0":
            sair = true;
            break;
        default:
            Console.WriteLine("Opção Inválida");
            break;
    }

    if (!sair)
    {
        Console.WriteLine();
    }
}

static void ExibirMenu()
{
    Console.WriteLine("-Sistema Escolar-");
    Console.WriteLine("1 - Listar alunos");
    Console.WriteLine("2 - Cadastrar aluno");
    Console.WriteLine("3 - Editar aluno");
    Console.WriteLine("4 - Remover aluno");
    Console.WriteLine("0 - Sair");
    Console.Write("Escolha uma opção: ");
}

static void MenuListar()
{
    Console.WriteLine("1 - Listar todos");
    Console.WriteLine("2 - Listar por ID");
    Console.Write("Escolha uma opção: ");
    var escolha = Console.ReadLine();

    if (escolha == "2")
    {
        var id = LerUlong("Informe o ID do aluno: ");
        var aluno = BancoDados.Listar(id);
        if (aluno is null)
        {
            Console.WriteLine("Não foi encontrado um aluno com o ID informado.");
            return;
        }

        ExibirAluno(aluno);
        return;
    }

    if (escolha != "1")
    {
        Console.WriteLine("Opção Inválida");
        return;
    }

    var alunos = BancoDados.Listar();
    if (alunos.Count == 0)
    {
        Console.WriteLine("Nenhum aluno cadastrado.");
        return;
    }

    foreach (var aluno in alunos)
    {
        ExibirAluno(aluno);
        Console.WriteLine();
    }
}

static void MenuCadastrar()
{
    var nome = LerTexto("Nome: ");
    var idade = LerUint("Idade: ");
    var escolaridade = LerEscolaridade();

    var aluno = new Aluno
    {
        Nome = nome,
        Idade = idade,
        Escolaridade = escolaridade
    };

    try
    {
        BancoDados.Adicionar(aluno);
        Console.WriteLine("Aluno cadastrado com sucesso!");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void MenuEditar()
{
    var id = LerUlong("Informe o ID do aluno: ");
    var nome = LerTexto("Nome: ");
    var idade = LerUint("Idade: ");
    var escolaridade = LerEscolaridade();

    var aluno = new Aluno
    {
        Id = id,
        Nome = nome,
        Idade = idade,
        Escolaridade = escolaridade
    };

    try
    {
        var atualizado = BancoDados.Atualizar(aluno);
        Console.WriteLine(atualizado
            ? "Aluno atualizado com sucesso!"
            : "Não foi encontrado um aluno com o ID informado.");
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
}

static void MenuRemover()
{
    var id = LerUlong("Informe o ID do aluno: ");
    var removido = BancoDados.Remover(id);

    Console.WriteLine(removido
        ? "Aluno removido com sucesso!"
        : "Não foi encontrado um aluno com o ID informado.");
}

static string LerTexto(string rotulo)
{
    while (true)
    {
        Console.Write(rotulo);
        var texto = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(texto))
        {
            return texto.Trim();
        }

        Console.WriteLine("Valor inválido. Tente novamente.");
    }
}

static uint LerUint(string rotulo)
{
    while (true)
    {
        Console.Write(rotulo);
        var texto = Console.ReadLine();
        if (uint.TryParse(texto, out var valor))
        {
            return valor;
        }

        Console.WriteLine("Valor inválido. Tente novamente.");
    }
}

static ulong LerUlong(string rotulo)
{
    while (true)
    {
        Console.Write(rotulo);
        var texto = Console.ReadLine();
        if (ulong.TryParse(texto, out var valor))
        {
            return valor;
        }

        Console.WriteLine("Valor inválido. Tente novamente.");
    }
}

static Escolaridade LerEscolaridade()
{
    while (true)
    {
        Console.WriteLine("Escolaridade:");
        Console.WriteLine("1 - Fundamental");
        Console.WriteLine("2 - Medio");
        Console.WriteLine("3 - Superior");
        Console.WriteLine("4 - PosGraduacao");
        Console.Write("Escolha uma opção: ");

        var texto = Console.ReadLine();
        if (int.TryParse(texto, out var opcao))
        {
            if (opcao >= 1 && opcao <= 4)
            {
                return (Escolaridade)(opcao - 1);
            }
        }

        Console.WriteLine("Opção Inválida");
    }
}

static void ExibirAluno(Aluno aluno)
{
    Console.WriteLine($"ID: {aluno.Id}");
    Console.WriteLine($"Nome: {aluno.Nome}");
    Console.WriteLine($"Idade: {aluno.Idade}");
    Console.WriteLine($"Escolaridade: {aluno.Escolaridade}");
    Console.WriteLine($"Data Cadastro: {aluno.DataCadastro:dd/MM/yyyy HH:mm}");
    Console.WriteLine($"Data Atualizacao: {(aluno.DataAtualizacao.HasValue ? aluno.DataAtualizacao.Value.ToString("dd/MM/yyyy HH:mm") : "null")}");
}