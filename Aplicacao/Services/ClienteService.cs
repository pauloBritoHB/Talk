using Aplicacao.Interfaces;
using Dominio.Entidades;
using Dominio.Enums;
using Dominio.Interfaces;
using Dominio.Tools;
using Infra;
using System;
using System.Linq;

namespace Aplicacao.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteValidator _clienteValidator;

        public ClienteService(IClienteRepository clienteRepository, IClienteValidator clienteValidator)
        {
            _clienteRepository = clienteRepository;
            _clienteValidator = clienteValidator;
        }

        public Result<Cliente> AlterarStatusCliente(Guid id)
        {
            var result = new Result<Cliente>();
            var clienteNoBanco = _clienteRepository.GetById(id);

            if (clienteNoBanco == null)
            {
                result.IsValid = false;
                result.ListaErros.Add("Id informado não encontrado no banco");
                return result;
            }

            clienteNoBanco.Status = clienteNoBanco.Status == StatusClienteEnum.ATIVO ? StatusClienteEnum.INATIVO : StatusClienteEnum.ATIVO;

            var alterado = _clienteRepository.ALterarCliente(clienteNoBanco);

            if (!alterado)
            {
                result.IsValid = false;
                result.ListaErros.Add("Erro ao alterar cliente na base de dados");
            }

            return result;       
        }

        public Result<Cliente> CriarNovoCliente(Cliente cliente)
        {
            var result = _clienteValidator.ValidarCliente(cliente);

            if (!result.IsValid)
                return result;
            
            var adicionado = _clienteRepository.AdicionarCliente(cliente);

            if (!adicionado)
            {
                result.IsValid = false;
                result.ListaErros.Add("Erro ao gravar cliente na base de dados");
            }
            return result;
        }

        public Result<Cliente> DeletarCliente(Guid id)
        {
            var result = new Result<Cliente>();
            result.IsValid = _clienteRepository.RemoverCliente(id);

            if (!result.IsValid)
                result.ListaErros.Add("Erro ao deletar cliente na base de dados");

            return result;
        }

        public Result<Cliente> ObterClientePorId(Guid id)
        {
            var result = new Result<Cliente>();
            var cliente = _clienteRepository.GetById(id);
            if(cliente == null)
            {
                result.IsValid = false;
                result.ListaErros.Add("Id informado não cadastrado na base de dados");
                return result;
            }

            result.Dados.Add(cliente);
            return result;
        }

        public Result<Cliente> ObterTodosClientes()
        {
            var result = new Result<Cliente>();
            var clientes = _clienteRepository.GetAll().ToList();
            if (clientes.Count == 0)
            {
                result.IsValid = false;
                result.ListaErros.Add("Não existem clientes cadastrados na base de dados");
                return result;
            }

            result.Dados = clientes;
            return result;
        }
            
    }
}
