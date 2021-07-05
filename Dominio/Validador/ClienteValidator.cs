using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Tools;
using System;

namespace Dominio.Validador
{
    public class ClienteValidator : IClienteValidator
    {
        private const int IDADE_MINIMA = 17;

        public Result<Cliente> ValidarCliente(Cliente cliente)
        {
            var result = new Result<Cliente>();

            if(cliente.CPF % 2 != 0)
                result.ListaErros.Add("CPF inválido, numero tem que ser par");

            if ((DateTime.Now.Year - cliente.Nascimento.Year) < IDADE_MINIMA)
                result.ListaErros.Add("Cliente tem que ter mais de " + IDADE_MINIMA + " anos");

            if (cliente.Nome.Length <= 3)
                result.ListaErros.Add("Nome tem que conter no mínimo 3 letras");

            if (cliente.SobreNome.Length <= 3)
                result.ListaErros.Add("Sobrenome tem que conter no mínimo 3 letras");

            if (result.ListaErros.Count > 0)
                result.IsValid = false;

            return result;
        }
    }
}
