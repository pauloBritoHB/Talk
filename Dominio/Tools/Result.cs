using System.Collections.Generic;

namespace Dominio.Tools
{
    public class Result<T>
    {
        public bool IsValid = true;
        public List<string> ListaErros = new List<string>();
        public List<T> Dados = new List<T>();
    }
}
