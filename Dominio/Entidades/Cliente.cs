using Dominio.Enums;
using System;

namespace Dominio.Entidades
{
    public class Cliente 
    {
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public Guid Id { get; set; }
        public int CPF { get; set; }
        public StatusClienteEnum Status { get; set; }
        public DateTime Nascimento { get; set; }

    }
}
