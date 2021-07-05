using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infra
{
    public class ClienteRepository : IClienteRepository
    {
        private static List<Cliente> _clientes;

        public ClienteRepository()
        {
            _clientes = new List<Cliente>();
            PopularLista();
        }

        public bool AdicionarCliente(Cliente cliente)
        {
            cliente.Id = Guid.NewGuid();
            _clientes.Add(cliente);
            return true;
        }

        public bool ALterarCliente(Cliente cliente)
        {
            var clienteBanco = _clientes.FirstOrDefault(x => x.Id == cliente.Id);

            if (clienteBanco == null)
                return false;

            _clientes.Remove(clienteBanco);
            _clientes.Add(cliente);

            return true;
        }

        public IEnumerable<Cliente> GetAll()
           => _clientes;

        public Cliente GetById(Guid id)
            => _clientes.FirstOrDefault(x => x.Id == id);

        public bool RemoverCliente(Guid id)
        {
            var clienteBanco = _clientes.FirstOrDefault(x => x.Id == id);

            if (clienteBanco == null)
                return false;

            _clientes.Remove(clienteBanco);
            return true;
        }

        private void PopularLista()
        {
            if(_clientes.Count == 0)
            {
                var c1 = new Cliente() { CPF = 111112, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1985"), Nome = "Tomas", SobreNome = "O Gato", Status = Dominio.Enums.StatusClienteEnum.ATIVO };
                var c2 = new Cliente() { CPF = 222222, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1998"), Nome = "Mickey", SobreNome = "Mouse", Status = Dominio.Enums.StatusClienteEnum.ATIVO };
                var c3 = new Cliente() { CPF = 333332, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1997"), Nome = "Rambo", SobreNome = "O cara", Status = Dominio.Enums.StatusClienteEnum.ATIVO };
                _clientes.Add(c1);
                _clientes.Add(c2);
                _clientes.Add(c3);
            }
        }
    }
}
