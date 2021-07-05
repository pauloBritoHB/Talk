using Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace dmaintestes
{
    public class ClienteFake
    {
        public Cliente ObterClienteValidoSemId()
            => new Cliente()
            {
                CPF = 222222,
                Nascimento = DateTime.Parse("20/02/1980"),
                Nome = "Conan",
                SobreNome = "O Brarbaro",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

        public Cliente ObterClienteValidoComId()
            => new Cliente()
            {
                CPF = 111112,
                Id = new Guid(),
                Nascimento = DateTime.Parse("20/02/1985"),
                Nome = "Tomas",
                SobreNome = "O Gato",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

        public Cliente ObterClienteInvalido()
            => new Cliente()
            {
                CPF = 33333,
                Id = new Guid(),
                Nascimento = DateTime.Parse("20/02/1800"),
                Nome = "Ze",
                SobreNome = "Só zé",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

        public List<string> ObterListaErros()
            => new List<String> { "CPF inválido, numero tem que ser par",
                                  "Nome tem que conter no mínimo 3 letras",
                                  "Sobrenome tem que conter no mínimo 3 letras",
                                  "Cliente tem que ter mais de 17 anos"};
    }
}
