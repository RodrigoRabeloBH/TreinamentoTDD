namespace Exercicio01
{
  using DatabaseLib;
  using System;
  public static class GeraBancoExemplo
  {
    public static void CriaBanco(IDbAccess access)
    {
      {
        var newBase = access.ExecuteScalar<int>("SELECT 1 FROM SYS.TABLES WHERE NAME = 'PESSOA'") != 1;

        if (newBase)
        {

          access.Execute(@"IF NOT EXISTS (SELECT 1 FROM SYS.TABLES WHERE NAME = 'PESSOA')
              CREATE TABLE PESSOA (ID BIGINT NOT NULL, 
              NOME VARCHAR(254) NOT NULL,
              EMAIL VARCHAR(254),
              NASCIMENTO DATETIME NOT NULL,
              INICIOCONTRATO DATETIME NOT NULL,
              ATIVO CHAR(1) NOT NULL,
              SEXO CHAR(1) NOT NULL,
              SALARIO DECIMAL(12,2) NOT NULL DEFAULT 0,
              CONSTRAINT PK_PESSOA PRIMARY KEY (ID))");

        access.Execute(@"IF NOT EXISTS (SELECT 1 FROM SYS.TABLES WHERE NAME = 'DEPENDENTE')
              CREATE TABLE DEPENDENTE (ID BIGINT identity(1,1) NOT NULL, 
              PESSOA BIGINT NOT NULL,
              NOME VARCHAR(254) NOT NULL,
              NASCIMENTO DATETIME NOT NULL,
              SEXO CHAR(1) NOT NULL,
              CONSTRAINT PK_DEPENDENTE PRIMARY KEY (ID),
              CONSTRAINT FK_PESSOA_DEPENDENTE FOREIGN KEY (PESSOA) REFERENCES PESSOA(ID))");


          for (int i = 0; i < 500; i++)
          {
            string datePattern = "yyyy-dd-MM";
            string birthDate= DateTime.Now.AddDays(-1 * (i + 5000)).ToString(datePattern);
            string iniDate = DateTime.Now.AddDays(-1 * (i + 2000)).ToString(datePattern);

            access.Execute($@"INSERT INTO PESSOA (ID,NOME, EMAIL, NASCIMENTO, INICIOCONTRATO, SEXO, ATIVO, SALARIO) VALUES
                        ({i},'FUNCIONARIO {i:000}','{i:000}@teste.com', 
                        '{birthDate}',
                        '{iniDate}','{GetSex(i)}', '{Ativo()}', '{i * 10}')");


            var numDep = new Random().Next(1, 5);

            string data = DateTime.Now.AddDays(-1 * (i + 1000)).ToString(datePattern);

            for (int j = 0; j < numDep; j++)
            {
              access.Execute($@"INSERT INTO DEPENDENTE (PESSOA,NOME, NASCIMENTO, SEXO) VALUES
                        ({i},'DEPENDENTE {j:000}','{data}','{GetSex(i)}')");
            }
          }
        }
      }
    }

    static Random rnd = new Random();
    private static string Ativo()
    {
      return rnd.Next(1, 5) == 3 ? "N" : "S";
    }
    private static string GetSex(int cont)
    {
      return cont % 2 == 0 ? "M" : "F";
    }
  }
}
