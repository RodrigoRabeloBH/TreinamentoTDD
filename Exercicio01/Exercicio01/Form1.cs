namespace Exercicio01
{
  using DatabaseLib;
  using System;
  using System.Data;
  using System.Windows.Forms;
  using System.Linq;
  using System.Text;
  using RegrasNegocio;

  public partial class FrmMain : Form
  {
    public FrmMain()
    {
      InitializeComponent();
    }



    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      GeraBancoExemplo.CriaBanco(Manager.GetAccess());
    }
    //"teste", 
    DbManager Manager = new DatabaseLib.SqlServer.SqlServerManager(new ConnectionInfo()
    {
      DatabaseName = "TESTEUNITARIO",
      ServerName = "LOCALHOST",
      UserName = "rm",
      Password = "rm",
      Path = string.Empty
    });

    DataSet dsMain = new DataSet();

    private void CarregaDadosTela()
    {
      dsMain?.Dispose();

      using (var access = Manager.GetAccess())
      {
        access.QueryFill(dsMain, "PESSOA", "SELECT * FROM PESSOA ");
        access.QueryFill(dsMain, "DEPENDENTE", "SELECT * FROM DEPENDENTE ");

        dsMain.Relations.Add(new DataRelation(
          "PESSOA_DEPEND", dsMain.Tables["PESSOA"].Columns["ID"],
          dsMain.Tables["DEPENDENTE"].Columns["PESSOA"],
          true));

      }


      dgvMain.DataSource = dsMain;
      dgvMain.DataMember = "PESSOA";
    }

    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        dgvMain.SuspendLayout();
        CarregaDadosTela();
      }
      finally
      {
        dgvMain.ResumeLayout();
      }
    }

    private static string _relation = "PESSOA_DEPEND";


    private int Tops(int maxValue, int currentValue)
    {
      return currentValue <= maxValue ? currentValue : maxValue;
    }
    private void btnRegra_Click(object sender, EventArgs e)
    {

      var inicio = DateTime.Now;

      var logCalculo = new StringBuilder();

      var logForaCalculo = new StringBuilder();

      long ticks = 0;
      var cont = 0;

      using (var access = Manager.GetAccess())
      {
        foreach (DataRow row in dsMain.Tables["PESSOA"].Rows)
        {
          var inicioGeralFunc = DateTime.Now;

          if ((row.Field<string>("ATIVO") == "S") &&
            ((row.Field<decimal>("SALARIO") >= 1000) && (row.Field<decimal>("SALARIO") < 5000)))
          {
            cont++;
            logCalculo.AppendLine($">   Processando dados do funcionário {row["ID"]}:{row["NOME"]}");
            decimal percentual = 1.1M;
            logCalculo.AppendLine($"    Percentual Base: {percentual - 1}");

            var dependentesMulheres = row.GetChildRows(_relation)
              .Where(x => x.Field<string>("SEXO") == "F" && x.Field<DateTime>("NASCIMENTO").Month > 7).Count();

            logCalculo.AppendLine($"    Dependentes do Sexo Feminino nascidas após mês de Julho: {dependentesMulheres}");

            if (dependentesMulheres > 0)
            {
              dependentesMulheres = Tops(3, dependentesMulheres);
              logCalculo.AppendLine($"    Aplicando limite máximo de dependentes: {dependentesMulheres}");
              logCalculo.AppendLine($"    Percentual Ajustado para dependentes: ({percentual}) + ({dependentesMulheres * 0.01M}) = [{percentual + dependentesMulheres * 0.01M}]");
              percentual += dependentesMulheres * 0.01M;
            }

            var anosTrabalhados = ((DateTime.Now - row.Field<DateTime>("INiCIOCONTRATO")).Days / 365);
            logCalculo.AppendLine($"    Quantidade de anos trabalhados: {anosTrabalhados}");

            anosTrabalhados = Tops(3, anosTrabalhados);
            logCalculo.AppendLine($"    Aplicando limite máximo de anos trabalhados: {anosTrabalhados}");

            percentual += (anosTrabalhados) * 0.02M;
            logCalculo.AppendLine($"    Percentual Ajustado para anos trabalhados: {percentual - 1}");

            var novoSalario = row.Field<decimal>("SALARIO") * percentual;
            var salarioAntigo = row.Field<decimal>("SALARIO");

            logCalculo.AppendLine($"    Novo Salário = ({salarioAntigo}) * ({percentual}) = {novoSalario} [+{(percentual - 1) * 100}%]  ");

            row.BeginEdit();
            row["SALARIO"] = novoSalario;
            row.EndEdit();
            logCalculo.AppendLine("    Gravando dados do funcionario");
            var inicioGrava = DateTime.Now;
            access.QueryUpdate(dsMain.Tables["PESSOA"]);
            logCalculo.AppendLine($"    Novo salário calculado com sucesso");
            logCalculo.AppendLine($"    Tempo de Gravação no banco         : {DateTime.Now - inicioGrava}");
            var tempoCalcFun = DateTime.Now - inicioGeralFunc;
            logCalculo.AppendLine($"    Tempo de Cálculo para o funcionário: {tempoCalcFun}");
            logCalculo.AppendLine("    -------------------------------------------------------------------");
            ticks += tempoCalcFun.Ticks;
          }
          else
          {
            logForaCalculo.AppendLine($"    Funcionário {row["NOME"]}, Salário {row["SALARIO"]}, Situacão {row["ATIVO"]}");
          }
        }
      }
      logCalculo.AppendLine();
      logCalculo.AppendLine($"    Tempo total do Cálculo            : {DateTime.Now - inicio}");
      logCalculo.AppendLine($"    Funcionários que receberam aumento: {cont}");
      if (ticks > 0)
        logCalculo.AppendLine($"    Tempo médio por funcionário       : {TimeSpan.FromTicks((ticks / cont))}");
      logCalculo.AppendLine();
      logCalculo.AppendLine();
      logCalculo.AppendLine("    Funcionários fora da regra:");
      logCalculo.Append(logForaCalculo.ToString());
      txbLog.Text = logCalculo.ToString();
      tabControl1.SelectedIndex = 1;
    }
  }
}