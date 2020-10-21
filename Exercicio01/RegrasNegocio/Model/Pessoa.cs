using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasNegocio
{
  public class Pessoa
  {
    public long Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public DateTime Nascimento { get; set; }
    public DateTime InicioContrato { get; set; }
    public bool Ativo { get; set; }
    public string Sexo { get; set; }
    public double Salario { get; set; }
    public List<Dependente> Dependentes { get; set; }

    public int DependentesDentroDaRegra(Func<Dependente, bool> predicate)
    {
      return Dependentes.Where(predicate).Count();
    }
   
    public int AnosTrabalhados()
    {
      return ((DateTime.Now - InicioContrato).Days / 365);
    }

    public Pessoa(DataRow row, string relation)
    {
      Id = row.Field<long>("ID");
      Nome = row.Field<string>("Nome");
      Email = row.Field<string>("Email");
      Nascimento = row.Field<DateTime>("Nascimento");
      InicioContrato = row.Field<DateTime>("InicioContrato");
      Sexo = row.Field<string>("Sexo");
      Ativo = row.Field<string>("ATIVO") == "S";
      Salario = (double)row.Field<decimal>("Salario");

      Dependentes = new List<Dependente>();

      List<DataRow> dependentes = row.GetChildRows(relation).ToList();
      if (dependentes != null)
      {
        dependentes.ForEach(dr =>
         {
           Dependentes.Add(new Dependente(dr));
         });
      }
    }
  }
}
