using Dapper;
using eCommerce.API.DataBase;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

var db = new eCommerceContext();

#region Global Filter

Console.WriteLine("GLOBAL FILTER(FILTROS GLOBAIS)");
/*
 * //Ignorar o global filter em uma consulta
    var usuarios = db.Usuarios!.IgnoreQueryFilters().ToList(); 
 */
var usuarios = db.Usuarios!.ToList();

foreach (var usuario in usuarios)
{
    Console.WriteLine($"- Nome: {usuario.Nome}");
}

#endregion



/*
TemporalAll
Console.WriteLine("\nDADOS TEMPORAIS(Históricos)");
var usuarioTemp = db.Usuarios!.TemporalAll().Where(a => a.Id == 1).OrderBy(a => EF.Property<DateTime>(a, "PeriodoInicial")).ToList(); 
 */

/*
TemporalAsOf
var asOf = new DateTime(2023, 07, 15, 19, 19, 00);
var usuarioTemp = db.Usuarios!.TemporalAsOf(asOf).Where(a => a.Id == 1).OrderBy(a => EF.Property<DateTime>(a, "PeriodoInicial")).ToList();
 */

/*
TemporalBetween - TemporalFromTo
var From = new DateTime(2023, 07, 15, 19, 19, 00);
var To = new DateTime(2023, 07, 15, 19, 25, 11);
var usuarioTemp = db.Usuarios!.TemporalFromTo(From, To).Where(a => a.Id == 1).OrderBy(a => EF.Property<DateTime>(a, "PeriodoInicial")).ToList();
 */

Console.WriteLine("\nDADOS TEMPORAIS(Históricos)");

var From = new DateTime(2023, 07, 15, 19, 15, 00);
var To = new DateTime(2023, 07, 15, 19, 22, 00);
var usuarioTemp = db.Usuarios!.TemporalContainedIn(From, To).Where(a => a.Id == 1).OrderBy(a => EF.Property<DateTime>(a, "PeriodoInicial")).ToList();

foreach (var usuario in usuarioTemp)
{
    Console.WriteLine($"-- Nome: {usuario.Nome} -- Mae: {usuario.Mae}");
}




Console.WriteLine("\nINTEGRAÇÃO COM DAPPER");
var connection = db.Database.GetDbConnection();
var usuariosDapper = connection.Query<Usuario>("SELECT * FROM [Usuarios]");

foreach (var usuario in usuariosDapper)
{
    Console.WriteLine($"-- Nome: {usuario.Nome} -- Mae: {usuario.Mae}");
}
