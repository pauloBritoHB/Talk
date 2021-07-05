using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Validador;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dmaintestes.Validador
{
    [TestFixture]
    public class ClienteValidatorTests
    {

        private IClienteValidator _clienteValidator;

        [OneTimeSetUp]
        public void Setup()
        {
            _clienteValidator = new ClienteValidator();
        }

        [TestCaseSource(nameof(CamposDateTestCaseData))]
        public void Deve_Validar_Cliente(List<string> listaErro, bool EhValido, Cliente cliente)
        {
            //arrange
            var validator = new ClienteValidator();

            //Act
            var result = validator.ValidarCliente(cliente);

            //Arrange
            result.IsValid.Should().Be(EhValido);

            if (EhValido)
                result.ListaErros.Should().BeEmpty();
            else
                result.ListaErros.Should().Contain(listaErro);
        }

        public static IEnumerable<TestCaseData> CamposDateTestCaseData
        {
            get
            {
                yield return new TestCaseData(new List<string>() { }, true,
                    new Cliente() { CPF = 111112, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1985"), Nome = "Tomas", SobreNome = "O Gato", Status = Dominio.Enums.StatusClienteEnum.ATIVO })
                    .SetName("Nao_Deve_Criticar_Ao_Validar_Cliente_Valido_V2");

                yield return new TestCaseData(new List<string> { "CPF inválido, numero tem que ser par" }, false,
                    new Cliente() { CPF = 111111, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1985"), Nome = "Tomas", SobreNome = "O Gato", Status = Dominio.Enums.StatusClienteEnum.ATIVO })
                    .SetName("Deve_Criticar_Ao_Validar_Cliente_Com_Cpf_Invalido_V2");

                yield return new TestCaseData(new List<string> { "Nome tem que conter no mínimo 3 letras" }, false,
                    new Cliente() { CPF = 111112, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1985"), Nome = "To", SobreNome = "O Gato", Status = Dominio.Enums.StatusClienteEnum.ATIVO })
                    .SetName("Deve_Criticar_Ao_Validar_Cliente_Com_Nome_Invalido_V2");

                yield return new TestCaseData(new List<string> { "Sobrenome tem que conter no mínimo 3 letras" }, false,
                    new Cliente() { CPF = 111112, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/1985"), Nome = "Tomas", SobreNome = "O", Status = Dominio.Enums.StatusClienteEnum.ATIVO })
                    .SetName("Deve_Criticar_Ao_Validar_Cliente_Com_SobreNome_Invalido_V2");

                yield return new TestCaseData(new List<string> { "Cliente tem que ter mais de 17 anos" }, false,
                    new Cliente() { CPF = 111112, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/2020"), Nome = "Tomas", SobreNome = "O Gato", Status = Dominio.Enums.StatusClienteEnum.ATIVO })
                    .SetName("Deve_Criticar_Ao_Validar_Cliente_Com_Idade_Invalida_V2");

                yield return new TestCaseData(ObterListaErros(), false,
                    new Cliente() { CPF = 111111, Id = Guid.NewGuid(), Nascimento = DateTime.Parse("20/02/2020"), Nome = "To", SobreNome = "O", Status = Dominio.Enums.StatusClienteEnum.ATIVO })
                    .SetName("Deve_Criticar_Ao_Validar_Cliente_Com_Todos_Campos_Invalidos_V2");
            }
        }

        private static List<string> ObterListaErros()
           => new List<String> { "CPF inválido, numero tem que ser par",
                                  "Nome tem que conter no mínimo 3 letras",
                                  "Sobrenome tem que conter no mínimo 3 letras",
                                  "Cliente tem que ter mais de 17 anos"};
    }
}
