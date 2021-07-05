using Dominio.Entidades;
using System;
using System.Collections.Generic;

namespace Infra
{
    public interface IClienteRepository
    {
        IEnumerable<Cliente> GetAll();
        Cliente GetById(Guid id);
        Boolean AdicionarCliente(Cliente cliente);
        Boolean ALterarCliente(Cliente cliente);
        Boolean RemoverCliente(Guid id);
    }
}
