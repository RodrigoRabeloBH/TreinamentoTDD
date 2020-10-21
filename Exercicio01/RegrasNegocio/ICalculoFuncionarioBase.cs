using System;
using System.Data;

namespace RegrasNegocio
{
  public interface ICalculoFuncionarioBase
  {
    double FatorAumento { get; set; }
    double CalculaSalarioComAumento(Pessoa funcionario);
  }
}
