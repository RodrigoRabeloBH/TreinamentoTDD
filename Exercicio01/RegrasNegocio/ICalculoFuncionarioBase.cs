using System.Data;

namespace RegrasNegocio
{
  public interface ICalculoFuncionarioBase
  {
    double FatorAumento { get; set; }
    double FaixaSalarioMinimo { get; set; }
    double FaixaSalarioMaximo { get; set; }
    double CalculaAumento(DataRow row);
    bool FuncionarioAtivo(DataRow row);
    double CalculaFatorAumentoAdicional(DataRow row);
    bool FuncionarioDentroFaixaFatorAumento(DataRow row);
  }
}
