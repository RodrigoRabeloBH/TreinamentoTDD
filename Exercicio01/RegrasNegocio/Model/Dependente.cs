using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegrasNegocio
{
  public class Dependente
  {
    public long Id { get; set; }
    public long PessoaId { get; set; }
    public string Nome { get; set; }
    public DateTime Nascimento { get; set; }
    public string Sexo { get; set; }

    public Dependente(DataRow row)
    {
      Id = row.Field<long>("ID");
      PessoaId = row.Field<long>("Pessoa");
      Nome = row.Field<string>("Nome");
      Nascimento = row.Field<DateTime>("Nascimento");
      Sexo = row.Field<string>("Sexo");
    }
  }
}
