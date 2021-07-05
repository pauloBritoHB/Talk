using NSubstitute;
using Aplicacao.Services;
using NUnit.Framework;
using Infra;
using Dominio.Interfaces;
using Dominio.Entidades;
using FluentAssertions;
using Dominio.Tools;
using AplicacaoTestes.Fakes;

namespace AplicacaoTestes.ServicesTestes
{
    [TestFixture]
    public class ClienteServiceTests
    {
        private ClienteService _clienteService;
        private ClienteFake _clienteFake;
        private Cliente _clienteValido, _clienteInvalido;

        [OneTimeSetUp]
        public void Setup()
        {
            _clienteFake = new ClienteFake();

            _clienteValido = _clienteFake.ObterClienteValidoSemId();
            _clienteInvalido = _clienteFake.ObterClienteInvalido();

            var repositorio = Substitute.For<IClienteRepository>();
            repositorio.AdicionarCliente(_clienteValido).Returns(true);
            repositorio.AdicionarCliente(_clienteInvalido).Returns(true);

            var validator = Substitute.For<IClienteValidator>();
            validator.ValidarCliente(_clienteInvalido).Returns(new Result<Cliente> {IsValid = false, ListaErros = _clienteFake.ObterListaErros()});
            
            _clienteService = new ClienteService(repositorio, validator);

            //Arg.Is("algo")
            //Arg.Any<int>(),
        }

        [Test]
        public void Deve_Criar_Novo_Cliente_Com_Sucesso()
        {
            //Arrange
            //var cliente = _clienteFake.ObterClienteValidoSemId();
            var cliente = _clienteValido;

            //Act
            var result =  _clienteService.CriarNovoCliente(cliente);

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
