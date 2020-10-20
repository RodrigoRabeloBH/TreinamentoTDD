using DatabaseLib;
using System.Data;

namespace RegrasNegocio
{
  public class CalculosFuncionario : ICalculoFuncionarioBase
  {
    private DataTable _funcionarios;
    private double _fatorAumento = 10;
    private double _faixaSalarioMinimo = 1000;
    private double _FaixaSalarioMaximo = 5000;
    public double FatorAumento
    { get => _fatorAumento; set => _fatorAumento = value; }
    public double FaixaSalarioMinimo { get => _faixaSalarioMinimo; set => _faixaSalarioMinimo = value; }
    public double FaixaSalarioMaximo { get => _FaixaSalarioMaximo; set => _FaixaSalarioMaximo = value; }

    public CalculosFuncionario(DataTable funcionarios)
    {
      _funcionarios = funcionarios;
    }
    public double CalculaAumento(DataRow row)
    {
      throw new System.NotImplementedException();
    }

    public double CalculaFatorAumentoAdicional(DataRow row)
    {
      throw new System.NotImplementedException();
    }

    public bool FuncionarioAtivo(DataRow row)
    {
      return row.Field<string>("ATIVO") == "S";
    }

    public bool FuncionarioDentroFaixaFatorAumento(DataRow row)
    {
      return (row.Field<double>("SALARIO") >= _faixaSalarioMinimo) && (row.Field<double>("SALARIO") <= _FaixaSalarioMaximo);
    }
  }
}
