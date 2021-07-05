using Aplicacao.Services;
using Dominio.Entidades;
using Dominio.Tools;
using Dominio.Validador;
using Infra;
using System;
using System.Linq;

namespace TestesUnitarios
{
    class Program
    {
        static void Main(string[] args)
        {
            var validator = new ClienteValidator();
            var repository = new ClienteRepository();
            var servico = new ClienteService(repository, validator);

            var resposta = 1;

            while (resposta != 0)
            {
                Menu();

                resposta = Int32.Parse(Console.ReadLine());

                switch (resposta)
                {
                    case 1:
                        Buscar();
                        break;
                    case 2:
                        Cadastrar();
                        break;
                    case 3:
                        Alterar();
                        break;
                    case 4:
                        Deletar();
                        break;
                    case 5:
                        BuscarPorId();
                        break;
                    case 0:
                        Console.WriteLine("\tFinalizado");
                        break;
                    default:
                        Console.WriteLine("\tOpção inválida");
                        break;
                }

                Console.WriteLine("\n\n");
                Console.WriteLine("\ttecle para continuar!!!");
                Console.ReadLine();

                Console.Clear();
            }


            void Buscar()
            {
                var result = servico.ObterTodosClientes();
                if (!result.IsValid)
                    MostrarMensagensErro(result);
                else
                {
                    Console.WriteLine("\t|                 Id                   |         Nome\t\t|       CPF\t\t|\tStatus\t|\tNascimento");
                    foreach (var c in result.Dados)
                    {

                        Console.Write("\t| " + c.Id + " ");
                        Console.Write("|    " + c.Nome + " " + c.SobreNome + "\t");
                        Console.Write("|    " + c.CPF + "\t\t");
                        Console.Write("|\t" + c.Status + "\t|");
                        Console.Write("\t" + c.Nascimento);
                        Console.WriteLine("");
                    }
                    Console.WriteLine("\n\n");
                }
            }

            void Cadastrar()
            {
                Console.Write("\tNome: ");
                var nome = Console.ReadLine();

                Console.Write("\tSobrenome: ");
                var sobrenome = Console.ReadLine();

                Console.Write("\tNascimento [ dd/mm/aaaa ]: ");
                var nascimento = DateTime.Parse(Console.ReadLine());

                Console.Write("\tCPF: ");
                var cpf = Int32.Parse(Console.ReadLine());

                var cliente = new Cliente
                {
                    Nome = nome,
                    SobreNome = sobrenome,
                    Nascimento = nascimento,
                    CPF = cpf,
                    Status = Dominio.Enums.StatusClienteEnum.ATIVO
                };

                var result = servico.CriarNovoCliente(cliente);

                if (!result.IsValid)
                    MostrarMensagensErro(result);
                else
                    Console.Write("\tCadastrado com sucesso");
            }

            void Alterar()
            {
                Console.Write("\tInforme id do cliente a ser alterado: ");
                var id = Guid.Parse(Console.ReadLine());
                var response = servico.AlterarStatusCliente(id);
                if (response.IsValid)
                    Console.Write("\tAlterado com sucesso!! ");
                else
                    MostrarMensagensErro(response);
            }

            void BuscarPorId()
            {
                Console.Write("\tInforme id do cliente a ser pesquisado: ");
                var id = Guid.Parse(Console.ReadLine());
                var result = servico.ObterClientePorId(id);

                if (!result.IsValid)
                {
                    MostrarMensagensErro(result);
                    return;
                }
                var c = result.Dados.First();
                Console.WriteLine("\n\n\t|                 Id                   |         Nome\t\t|       CPF\t\t|\tStatus\t|\tNascimento");
                Console.Write("\t| " + c.Id + " ");
                Console.Write("|    " + c.Nome + " " + c.SobreNome + "\t\t");
                Console.Write("|    " + c.CPF + "\t\t");
                Console.Write("|\t" + c.Status + "\t|");
                Console.Write("\t" + c.Nascimento);
                Console.WriteLine("");
            }

            void Deletar()
            {
                Console.Write("\tInforme id do cliente a ser deletado: ");
                var id = Guid.Parse(Console.ReadLine());
                var result = servico.DeletarCliente(id);
                if (result.IsValid)
                    Console.Write("\tDeletado com sucesso!! ");
                else
                    MostrarMensagensErro(result);
            }

            void Menu()
            {
                Console.WriteLine("\tComeçando a brincadeira\n");
                Console.WriteLine("\tEscolha uma das opções:");
                Console.WriteLine("\t[1] Buscar todos clientes");
                Console.WriteLine("\t[2] Cadastrar novo cliente");
                Console.WriteLine("\t[3] Alterar status cliente");
                Console.WriteLine("\t[4] Deletar cliente");
                Console.WriteLine("\t[5] Buscar cliente");
                Console.WriteLine("\t[0] Sair\n\n");
            }
        }

        private static void MostrarMensagensErro(Result<Cliente> result)
        {
            Console.WriteLine("\n\n=========================================");
            foreach (var mensagem in result.ListaErros)
            {
                Console.WriteLine(mensagem);
            }
            Console.WriteLine("=========================================\n\n");
        }
    }
}
