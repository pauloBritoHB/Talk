using Aplicacao.Services;
using Dominio.Entidades;
using Dominio.Interfaces;
using Dominio.Tools;
using FluentAssertions;
using Infra;
using NSubstitute;
using NUnit.Framework;

namespace dmaintestes.Services
{
    [TestFixture]
    public class ClienteServiceTests
    {
        private ClienteService _clienteService;
        private ClienteFake _clienteFake;
        private Cliente _clienteValido, _clienteInvalido;

        [SetUp]
        public void SetUp()
        {
            _clienteFake = new ClienteFake();
            var repositorio = Substitute.For<IClienteRepository>();
            _clienteValido = _clienteFake.ObterClienteValidoSemId();
            _clienteInvalido = _clienteFake.ObterClienteInvalido();

            repositorio.AdicionarCliente(Arg.Is<Cliente>(x => x.Nome == "Zé")).Returns(true);
            repositorio.AdicionarCliente(_clienteInvalido).Returns(false);

            var validator = Substitute.For<IClienteValidator>();
            validator.ValidarCliente(_clienteInvalido).Returns(new Result<Cliente> { IsValid = false, ListaErros = _clienteFake.ObterListaErros()});

            _clienteService = new ClienteService(repositorio, validator);
        }


        [Test]
        public void Deve_Criar_Novo_Cliente_Com_Sucesso()
        {
            //Arrange
            //Act
            var result = _clienteService.CriarNovoCliente(_clienteValido);

            //Assert
            result.IsValid.Should().BeTrue();
            result.ListaErros.Should().BeEmpty();
        }

        [Test]
        public void Deve_Dar_Erro_Ao_Criar_Novo_Cliente()
        {
            //Arrange
            //Act

            var result = _clienteService.CriarNovoCliente(_clienteInvalido);

            //Assert
            result.IsValid.Should().BeFalse();
            result.ListaErros.Should().Contain(_clienteFake.ObterListaErros());
        }

    }
}
