using Dominio.Entidades;
using Dominio.Tools;
using System;

namespace Aplicacao.Interfaces
{
    public interface IClienteService
    {
        Result<Cliente> CriarNovoCliente(Cliente cliente);
        Result<Cliente> AlterarStatusCliente(Guid id);
        Result<Cliente> DeletarCliente(Guid id);
        Result<Cliente> ObterClientePorId(Guid id);
        Result<Cliente> ObterTodosClientes();
    }
}
