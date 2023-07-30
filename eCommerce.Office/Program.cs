using eCommerce.Office;
using eCommerce.Office.Models;
using Microsoft.EntityFrameworkCore;

var db = new eCommerceOfficeContext();

#region Many-To-Many 2x One-To-Many = EF CORE < 5 pra baixo (3.1.....1.0).
//Setor > ColaboradoresSetores > Colaborador

var resultadoDaConsulta = db.Setores!.Include(a => a.ColaboradoresSetores)!.ThenInclude(a => a.Colaborador);

foreach (var setor in resultadoDaConsulta)
{
    Console.WriteLine("\n"+ setor.Nome);
    foreach (var colabSetor in setor.ColaboradoresSetores!)
    {
        Console.WriteLine($"- COLABORADOR: {colabSetor.Colaborador.Nome}");
    }
}
#endregion

#region Many-To-Many for EF CORE 5.0+
var resultadoTurma = db.Colaboradores!.Include(a => a.Turmas);

Console.WriteLine("------------------------------------");
foreach (var colab in resultadoTurma)
{
    Console.WriteLine(colab.Nome);
    foreach (var turma in colab.Turmas)
    { 
        Console.WriteLine("- " + turma.Nome);
    }
}
//var refTurma = "REF. TURMA A1";
//var refColb = new Colaborador();
//refColb.Turmas.Add(RefTurm); (1, 2)
//db.SaveChanges();
#endregion

#region Many-To-Many + Payload for EF CORE 5.0+

Console.WriteLine("-----------------------------------------");
//var colabVeiculo = db.Colaboradores!.Include(a => a.Veiculos);
var colabVeiculo = db.Colaboradores!.Include(a => a.ColaboradoresVeiculos )!.ThenInclude(a => a.Veiculo); //Para puxar a data com uma navegação maior  

foreach (var colab in colabVeiculo)
{
    Console.WriteLine("\n" + colab.Nome);
    //foreach (var vinculo in colab.Veiculos)
    foreach (var vinculo in colab.ColaboradoresVeiculos!) //Para puxar a data com uma navegação maior
    {
        //Console.WriteLine($"\nVeiculo: {veiculo.Nome}\nPlaca: {veiculo.Placa}");
        Console.WriteLine($"\nVeiculo: {vinculo.Veiculo!.Nome}\nPlaca: {vinculo.Veiculo.Placa}\nData de Início: {vinculo.DataInicioDeVinculo}"); //Para puxar a data com uma navegação maior
    }
}

var vinculo01 = db.Set<ColaboradorVeiculo>().SingleOrDefault(a => a.ColaboradorId == 1 && a.VeiculoId == 1);

Console.WriteLine("\nData de vínculo usando o SET no banco: " + vinculo01!.DataInicioDeVinculo);
#endregion 

