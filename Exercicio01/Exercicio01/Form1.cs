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
      ServerName = "LOCALHOST\\SQLEXPRESS",
      UserName = "sa",
      Password = "sa",
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

      CalculosFuncionario calculadora = new CalculosFuncionario();

      using (var access = Manager.GetAccess())
      {
        foreach (DataRow row in dsMain.Tables["PESSOA"].Rows)
        {
          var inicioGeralFunc = DateTime.Now;

          Pessoa funcionario = new Pessoa(row, _relation);
          double salarioAntigo = funcionario.Salario;
          funcionario.Salario = calculadora.CalculaSalarioComAumento(funcionario);

          logCalculo.Append(calculadora.GetLog());
          if (calculadora.ultimoFuncionarioTeveSalarioAtualizado)
          {
            cont++;
            row.BeginEdit();
            row["SALARIO"] = funcionario.Salario;
            row.EndEdit();
            logCalculo.AppendLine("    Gravando dados do funcionario");
            var inicioGrava = DateTime.Now;
            access.QueryUpdate(dsMain.Tables["PESSOA"]);
            logCalculo.AppendLine($"    Novo salário calculado com sucesso");
            logCalculo.AppendLine($"    Tempo de Gravação no banco         : {DateTime.Now - inicioGrava}");
          }
          var tempoCalcFun = DateTime.Now - inicioGeralFunc;
          logCalculo.AppendLine($"    Tempo de Cálculo para o funcionário: {tempoCalcFun}");
          logCalculo.AppendLine("    -------------------------------------------------------------------");
          ticks += tempoCalcFun.Ticks;
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