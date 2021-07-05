using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Validador;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DominioTestes.ValidadorTestes
{
    [TestFixture]
    public class ClienteValidatorTestes
    {
        private IClienteValidator _clienteValidator;

        [OneTimeSetUp]
        public void Setup()
        {
            _clienteValidator = new ClienteValidator();
        }

        [Test]
        public void Nome_Do_Teste()
        {
            //Arrange

            //Act

            //Assert
        }

        //=====================================================================================================================//
        //=====================================================================================================================//
        [Test]
        public void Nao_Deve_Criticar_Ao_Validar_Cliente_Valido()
        {
            //Arrange
            var validator = new ClienteValidator();

            var cliente = new Cliente()
            {
                CPF = 111112,
                Id = Guid.NewGuid(),
                Nascimento = DateTime.Parse("20/02/1985"),
                Nome = "Tomas",
                SobreNome = "O Gato",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

            //Act
            var result = validator.ValidarCliente(cliente);

            //Assert Nunit
            Assert.IsTrue(result.IsValid);
            Assert.IsEmpty(result.ListaErros);

            //Assert Fluent
            result.IsValid.Should().BeTrue();
            result.ListaErros.Should().BeEmpty();
        }
        //=====================================================================================================================//
        //=====================================================================================================================//


        [Test]
        public void Nao_Deve_Criticar()
        {
            //Arrange           
            var cliente = new Cliente()
            {
                CPF = 111112,
                Id = Guid.NewGuid(),
                Nascimento = DateTime.Parse("20/02/1985"),
                Nome = "Tomas",
                SobreNome = "O Gato",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

            //Act
            var result = _clienteValidator.ValidarCliente(cliente);

            //Assert Nunit
            Assert.IsTrue(result.IsValid);
            Assert.IsEmpty(result.ListaErros);

            //Assert Fluent
            result.IsValid.Should().BeTrue();
            result.ListaErros.Should().BeEmpty();
        }

        //=====================================================================================================================//
        //=====================================================================================================================//
        [Test]
        public void Deve_Criticar_Ao_Validar_Cliente_Cpf_Invalido()
        {
            //Arrange
            var validator = new ClienteValidator();

            var cliente = new Cliente()
            {
                CPF = 111111,
                Id = Guid.NewGuid(),
                Nascimento = DateTime.Parse("20/02/1985"),
                Nome = "Tomas",
                SobreNome = "O Gato",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

            //Act
            var result = validator.ValidarCliente(cliente);

            //Assert Nunit
            Assert.IsFalse(result.IsValid);
            Assert.That(result.ListaErros, Has.Exactly(1).EqualTo("CPF inválido, numero tem que ser par"));
            Assert.LessOrEqual(result.ListaErros.Count(), 5);  //Cuidado ao testar

            //Assert Fluent
            result.IsValid.Should().BeFalse();
            result.ListaErros.Should().NotBeNullOrEmpty().And.Contain("CPF inválido, numero tem que ser par");// não pega quando tem mais de um erro
            result.ListaErros.Single().Should().Be("CPF inválido, numero tem que ser par");    // melhor forma
        }
        //=====================================================================================================================//
        //=====================================================================================================================//



        //=====================================================================================================================//
        //=====================================================================================================================//
        [Test]
        public void Deve_Criticar_Ao_Validar_Cliente_Com_Todos_Campos_Invalidos()
        {
            //Arrange
            var validator = new ClienteValidator();

            var cliente = new Cliente()
            {
                CPF = 111111,
                Id = Guid.NewGuid(),
                Nascimento = DateTime.Parse("20/02/2018"),
                Nome = "To",
                SobreNome = "O",
                Status = Dominio.Enums.StatusClienteEnum.ATIVO
            };

            var listaErros = new List<string> {
                "CPF inválido, numero tem que ser par",
                "Nome tem que conter no mínimo 3 letras",
                "Sobrenome tem que conter no mínimo 3 letras",
                "Cliente tem que ter mais de 17 anos"};          //Não importa a ordem da lista

            //Act
            var result = validator.ValidarCliente(cliente);

            //Assert
            result.IsValid.Should().BeFalse();

            //desnecessario com a validação abaixo
            result.ListaErros.Count().Should().Be(4);

            result.ListaErros.Should().Contain(listaErros);

            result.ListaErros.Should().Contain("CPF inválido, numero tem que ser par",
                                                "Nome tem que conter no mínimo 3 letras",
                                                "Sobrenome tem que conter no mínimo 3 letras",
                                                "Cliente tem que ter mais de 17 anos");
        }
        //=====================================================================================================================//
        //=====================================================================================================================//



        //[TestCaseSource(nameof(CamposDateTestCaseData))]
        //public void Deve_Validar_Cliente()
        //{
        //    //arrange


        //    //Act


        //    //Arrange

        //}

        //public static IEnumerable<TestCaseData> CamposDateTestCaseData
        //{
        //    get
        //    {
        //        yield return new TestCaseData();
        //        yield return new TestCaseData();
        //    }
        //}





        //=====================================================================================================================//
        //=====================================================================================================================//
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
        //=====================================================================================================================//
        //=====================================================================================================================//

        private static List<string> ObterListaErros()
            => new List<String> { "CPF inválido, numero tem que ser par",
                                  "Nome tem que conter no mínimo 3 letras",
                                  "Sobrenome tem que conter no mínimo 3 letras",
                                  "Cliente tem que ter mais de 17 anos"};

    }
}
