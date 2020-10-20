namespace UnitTestsProject
{
  using DatabaseLib;
  using Rhino.Mocks;
  using System;
  using System.Data;


  public class ClasseBaseTestesUnitarios
  {
    #region metodos auxiliares


    protected TablePessoa CriaTabelaPessoa(decimal salario, char situacao, DateTime admissao)
    {
      var tablePessoa = new TablePessoa("PESSOA")
      {
        Rows =
          {
            new object[]
            {
              1,
              "NomeFuncionário",
              "email@email.com",
              new DateTime(1990,1,1),
              admissao,
              situacao,
              "M",
              salario
            }
          }
      };

      return tablePessoa;

    }

    protected TableDependente CriatabelaDependentes(int idDependente, params DadosDependente[] dependentes)
    {
      var result = new TableDependente("DEPENDENTE");
      var cont = 0;
      foreach (DadosDependente dependente in dependentes)
      {
        result.Rows.Add(++cont, idDependente, $"Dependente {cont}", dependente.Nascimento, dependente.Sexo);
      }
      return result;
    }

    protected IDbAccess StubDbAccess(DataTable pessoa, DataTable dependente)
    {
      var dba = MockRepository.GenerateStub<IDbAccess>();
      dba.Stub(x => x.QueryFill(Arg<DataSet>.Is.Anything, Arg<string>.Is.Equal("PESSOA"),
       Arg<string>.Is.Anything,
       Arg<object>.Is.Anything,
       Arg<IDbTransaction>.Is.Anything,
       Arg<bool>.Is.Anything,
       Arg<int?>.Is.Anything,
       Arg<CommandType?>.Is.Anything
       )).Do(new DbAccess.QueryFillDelegate((dSet, tableName, c, d, e, f, g, h) => { dSet.Tables.Add(pessoa); }));

      dba.Stub(x => x.QueryFill(Arg<DataSet>.Is.Anything, Arg<string>.Is.Equal("DEPENDENTE"),
       Arg<string>.Is.Anything,
       Arg<object>.Is.Anything,
       Arg<IDbTransaction>.Is.Anything,
       Arg<bool>.Is.Anything,
       Arg<int?>.Is.Anything,
       Arg<CommandType?>.Is.Anything
       )).Do(new DbAccess.QueryFillDelegate((dSet, tableName, c, d, e, f, g, h) => { dSet.Tables.Add(dependente); }));
      return dba;
    }

    protected DataSet CriaRetornoFuncionarios(DataTable pessoa, DataTable dependente)
    {
      var dsMain = new DataSet();
      dsMain.Tables.Add(pessoa);
      dsMain.Tables.Add(dependente);
      dsMain.Relations.Add(new DataRelation(
        "PESSOA_DEPEND", dsMain.Tables["PESSOA"].Columns["ID"],
        dsMain.Tables["DEPENDENTE"].Columns["PESSOA"],
        true));
      return dsMain;
    }

    protected class TablePessoa : DataTable
    {
      public TablePessoa() : base("PESSOA")
      {
        CreateColumns();
      }
      public TablePessoa(string tableName) : base("PESSOA")
      {
        CreateColumns();
      }


      protected void CreateColumns()
      {
        Columns.Add(new DataColumn("ID", typeof(long)) { AllowDBNull = false });
        Columns.Add(new DataColumn("NOME", typeof(string)) { AllowDBNull = false });
        Columns.Add(new DataColumn("EMAIL", typeof(string)) { AllowDBNull = false });
        Columns.Add(new DataColumn("NASCIMENTO", typeof(DateTime)) { AllowDBNull = false });
        Columns.Add(new DataColumn("INICIOCONTRATO", typeof(DateTime)) { AllowDBNull = false });
        Columns.Add(new DataColumn("ATIVO", typeof(string)) { AllowDBNull = false });
        Columns.Add(new DataColumn("SEXO", typeof(string)) { AllowDBNull = false });
        Columns.Add(new DataColumn("SALARIO", typeof(decimal)) { AllowDBNull = false });
        PrimaryKey = new DataColumn[] { Columns["ID"] };
      }
    }

    protected class TableDependente : DataTable
    {
      public TableDependente() : base("DEPENDENTE")
      {
        CreateColumns();
      }
      public TableDependente(string tableName) : base("DEPENDENTE")
      {
        CreateColumns();
      }


      protected void CreateColumns()
      {
        Columns.Add(new DataColumn("ID", typeof(long)) { AllowDBNull = false });
        Columns.Add(new DataColumn("PESSOA", typeof(long)) { AllowDBNull = false });
        Columns.Add(new DataColumn("NOME", typeof(string)) { AllowDBNull = false });
        Columns.Add(new DataColumn("NASCIMENTO", typeof(DateTime)) { AllowDBNull = false });
        Columns.Add(new DataColumn("SEXO", typeof(string)) { AllowDBNull = false });
        PrimaryKey = new DataColumn[] { Columns["ID"] };
      }



    }

    #endregion

  }
  public struct DadosDependente
  {
    public DadosDependente(DateTime nascimento, char sexo)
    {
      Nascimento = nascimento;
      Sexo = sexo;
    }
    public DateTime Nascimento;
    public char Sexo;
  }
}
