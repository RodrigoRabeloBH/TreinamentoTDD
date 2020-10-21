using DatabaseLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading;

namespace RegrasNegocio
{
  public class CalculosFuncionario : ICalculoFuncionarioBase
  {
    private StringBuilder logCalculo;
    private double _fatorAumento = 10;
    private double _faixaSalarioMinimo = 1000;
    private double _FaixaSalarioMaximo = 5000;
    public double FatorAumento
    { get => _fatorAumento; set => _fatorAumento = value; }
    public double FaixaSalarioMinimo { get => _faixaSalarioMinimo; set => _faixaSalarioMinimo = value; }
    public double FaixaSalarioMaximo { get => _FaixaSalarioMaximo; set => _FaixaSalarioMaximo = value; }

    public bool ultimoFuncionarioTeveSalarioAtualizado = false;
    private Func<Dependente, bool> _dependentesPredicate => (d) => (d.Sexo == "F" && d.Nascimento.Month > 7);

    public double CalculaSalarioComAumento(Pessoa funcionario)
    {
      logCalculo = new StringBuilder();
      logCalculo.AppendLine($">   Processando dados do funcionário {funcionario.Id}:{funcionario.Nome}");
      logCalculo.AppendLine($"    Percentual Base: {_fatorAumento}");

      double percentualTotal = CalculaFatorAumentoTotal(funcionario);
      double salarioAntigo = funcionario.Salario;
      double novoSalario = salarioAntigo * percentualTotal;

      logCalculo.AppendLine($"    Novo Salário = ({salarioAntigo}) * ({percentualTotal}) = {novoSalario} [+{(percentualTotal - 1) * 100}%]  ");

      return novoSalario;
    }

    private double CalculaFatorAumentoTotal(Pessoa funcionario)
    {
      double percentualTotal = 0;
      if (FuncionarioAptoAGanharAumento(funcionario))
      {
        percentualTotal = _fatorAumento + CalculaFatorAumentoAdicional(funcionario);
        ultimoFuncionarioTeveSalarioAtualizado = true;
      }
      else
      {
        logCalculo.AppendLine($"    Funcionário {funcionario.Nome}, Salário {funcionario.Salario}, Situacão {(funcionario.Ativo ? "Ativo" : "Inativo")}");
        ultimoFuncionarioTeveSalarioAtualizado = false;
      }
      return (percentualTotal / 100) + 1;
    }

    private double CalculaFatorAumentoAdicional(Pessoa funcionario)
    {
      double percentualAdicional = 0;

      AplicaAumentoDepentes(funcionario, ref percentualAdicional);
      AplicaAumentoAnosTrabalhados(funcionario, ref percentualAdicional);

      return percentualAdicional;
    }

    public void AplicaAumentoDepentes(Pessoa funcionario, ref double percentualAdicional)
    {
      int dependentesMulheres = funcionario.DependentesDentroDaRegra(_dependentesPredicate);
      logCalculo.AppendLine($"    Dependentes do Sexo Feminino nascidas após mês de Julho: {dependentesMulheres}");
      dependentesMulheres = RetornaQuantidadeDeDependentesValidado(dependentesMulheres);
      percentualAdicional += dependentesMulheres;
    }

    public void AplicaAumentoAnosTrabalhados(Pessoa funcionario,ref double percentualAdicional)
    {
      logCalculo.AppendLine($"    Quantidade de anos trabalhados: {funcionario.AnosTrabalhados()}");
      int anosTrabalhados = RetornaValorComBaseNoValorMaximo(10, funcionario.AnosTrabalhados());
      percentualAdicional += anosTrabalhados;
      logCalculo.AppendLine($"    Aplicando limite máximo de anos trabalhados: {anosTrabalhados}");
      logCalculo.AppendLine($"    Percentual Ajustado para anos trabalhados: {percentualAdicional - 1}");
    }

    private int RetornaQuantidadeDeDependentesValidado(int dependentesMulheres)
    {
      if (dependentesMulheres > 0)
      {
        int maxDependentes = RetornaValorComBaseNoValorMaximo(3, dependentesMulheres);
        logCalculo.AppendLine($"    Aplicando limite máximo de dependentes: {maxDependentes}");
        logCalculo.AppendLine($"    Percentual Ajustado para dependentes: ({_fatorAumento}) + ({dependentesMulheres}) = [{_fatorAumento + maxDependentes}]");
        return maxDependentes;
      }
      else
      {
        return dependentesMulheres;
      }
    }

    private bool FuncionarioDentroFaixaFatorAumento(Pessoa funcionario)
    {
      return (funcionario.Salario >= _faixaSalarioMinimo) && (funcionario.Salario <= _FaixaSalarioMaximo);
    }

    private int RetornaValorComBaseNoValorMaximo(int maxValue, int currentValue)
    {
      return currentValue <= maxValue ? currentValue : maxValue;
    }
    private bool FuncionarioAptoAGanharAumento(Pessoa funcionario)
    {
      return FuncionarioDentroFaixaFatorAumento(funcionario) &&
              funcionario.Ativo;
    }

    public string GetLog()
    {
      return logCalculo.ToString();
    }

  }
}
