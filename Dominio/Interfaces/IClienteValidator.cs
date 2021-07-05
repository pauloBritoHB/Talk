using Dominio.Entidades;
using Dominio.Tools;

namespace Dominio.Interfaces
{
    public interface IClienteValidator
    {
        Result<Cliente> ValidarCliente(Cliente cliente);
    }
}
