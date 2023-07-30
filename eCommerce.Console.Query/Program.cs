using eCommerce.API.DataBase;
using eCommerce.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Reflection;

Console.WriteLine("LISTA DE USUÁRIOS\n\n");

var db = new eCommerceContext();
var usuarios = db.Usuarios!.ToList();

//db.Usuarios!.{ Methods > LINQ > EF > SQL > SGBD}.ToList().{ Methods > LINQ > C# > Processador+Memory };

foreach (var usuario in usuarios)
{
    Console.WriteLine("-" + usuario.Nome);
}

Console.WriteLine("\n\nBuscar 1 usuario!\n\n");

//var usuario01 = db.Usuarios!.Find(3); 

//var usuario01 = db.Usuarios!.Where(a => a.Email == "dawdaw.ad@gmail.com").First();
//var usuario01 = db.Usuarios!.Where(a => a.Email == "fernando@gmail.com").FirstOrDefault();
//var usuario01 = db.Usuarios!.OrderBy(a => a.Id).Last();
//var usuario01 = db.Usuarios!.OrderBy(a => a.Id).Where(a => a.Email == "dawdaw.ad@gmail.com").LastOrDefault();
//var usuario01 = db.Usuarios!.Single(a => a.Nome.Contains("a"));
//var usuario01 = db.Usuarios!.Single(a => a.Nome == "Fernando");
//var usuario01 = db.Usuarios!.SingleOrDefault(a => a.Nome == "Fernando");

//db.Usuarios!.FirstOrDefault(a => a.Email == "fernando@gmail.com");

var count = db.Usuarios!.Where(a => a.Nome.Contains("a")).Count();

Console.WriteLine($"\nNúmero de usuários que contem a letra A em seu nome: {count}");

var max = db.Usuarios!.Max(a => a.Nome);
Console.WriteLine($"Maior ID: {max}");

var min = db.Usuarios!.Min(a => a.Nome);
Console.WriteLine($"Menor ID: {min}");

//if (usuario01 == null)
//    Console.WriteLine("Usuario não encontrado!");
//else
//    Console.WriteLine($"Código: {usuario01!.Id}\nNome: {usuario01.Nome}");

//var usuarioList = db.Usuarios!.Where(a => a.Nome == "Fernando Lemos Trevisano" || a.Nome == "Lucas").ToList();


//var usuarioList = db.Usuarios!.Where(a => a.Nome.StartsWith("L")).ToList();
//var usuarioList2 = db.Usuarios!.Where(a => a.Nome.EndsWith("s")).ToList();
//var usuarioList3 = db.Usuarios!.Where(a => a.Nome.Contains("Fernando")).ToList();
//var usuarioList = db.Usuarios!.ToList();
//var usuarioListOrder = db.Usuarios!.OrderBy(a => a.Sexo).ThenByDescending(a=>a.Nome).ToList();
var usuarioListInclude = db.Usuarios!.Include(a => a.Contato).Include(a => a.EnderecosEntrega!.Where(a=>a.Estado == "SP")).Include(a=>a.Departamentos).ToList();

foreach (var usuario in usuarioListInclude)
{
    Console.WriteLine($"\nNome: {usuario.Nome} - Contato: {usuario.Contato!.Telefone} - Endereços: {usuario.EnderecosEntrega!.Count} - Departamentos: {usuario.Departamentos!.Count}");
    foreach (var endereco in usuario.EnderecosEntrega)
    {
        Console.WriteLine($"-- {endereco.NomeEndereco}: {endereco.CEP} - {endereco.Estado} - {endereco.Bairro} - {endereco.Endereco}");
    }
}


Console.WriteLine("\n\nLISTA DE USUÁRIOS (THEN INCLUDE)");

var contatos = db.Contatos!.Include(a=>a.Usuario).ThenInclude(u=>u!.EnderecosEntrega).Include(a=>a.Usuario).ThenInclude(u=>u!.Departamentos).ToList();

foreach (var contato in contatos)
{
    Console.WriteLine($"Contatos: {contato.Telefone} -> {contato.Usuario!.Nome} - QT END: {contato.Usuario.EnderecosEntrega!.Count} - QT DEP: {contato.Usuario.Departamentos!.Count}");
}


db.ChangeTracker.Clear();


Console.WriteLine("\n\nLISTA DE USUÁRIOS (AUTOINCLUDE)");
var usuariosAutoInclude = db.Usuarios!.IgnoreAutoIncludes().ToList();

foreach (var usuario in usuariosAutoInclude)
{
    Console.WriteLine($"Nome: {usuario.Nome} - TEL: {usuario.Contato?.Telefone}");
}


/*
 Explicit Load - Carregamento explícito
 */

db.ChangeTracker.Clear();

Console.WriteLine("\n\nCarregamento explícito(ExplicitLoad)");

var usuarioExplicitLoad = db.Usuarios!.IgnoreAutoIncludes().FirstOrDefault(a => a.Id == 1);

Console.WriteLine($"\n\nNome: {usuarioExplicitLoad!.Nome} - TEL: {usuarioExplicitLoad.Contato?.Telefone} - END: {usuarioExplicitLoad.EnderecosEntrega?.Count}");

db.Entry(usuarioExplicitLoad).Reference(a => a.Contato).Load();
db.Entry(usuarioExplicitLoad).Collection(a => a.EnderecosEntrega!).Load();

Console.WriteLine($"\n\nNome: {usuarioExplicitLoad!.Nome} - TEL: {usuarioExplicitLoad.Contato!.Telefone} - END: {usuarioExplicitLoad.EnderecosEntrega?.Count}");

var enderecos = db.Entry(usuarioExplicitLoad).Collection(a => a.EnderecosEntrega!).Query().Where(a => a.Estado == "SP").ToList();

foreach (var endereco in enderecos)
{
    Console.WriteLine($"\n-- Endereço: {endereco.NomeEndereco}: {endereco.Estado}: {endereco.Bairro}: {endereco.Endereco}: {endereco.Usuario!.Nome}");
}


/*
 LAZY LOADING – CARREGAMENTO PREGUIÇOSO
 */


Console.WriteLine("\nCARREGAMENTO PREGUIÇOSO");
db.ChangeTracker.Clear();
var usuarioLazyLoad = db.Usuarios!.Find(1);

Console.WriteLine($"\nNome: {usuarioLazyLoad!.Nome} - QTD END: {usuarioLazyLoad.EnderecosEntrega?.Count}");

/*
 SPLITQUERY - QUERY DIVIDIA
 */

Console.WriteLine("\n QUERY DIVIDIA");
var usuarioSplitQuery = db.Usuarios!.AsSingleQuery().Include(a => a.EnderecosEntrega).FirstOrDefault(a => a.Id == 1);
Console.WriteLine($"\nNome: {usuarioSplitQuery!.Nome} - QTD END: {usuarioSplitQuery.EnderecosEntrega?.Count}");


/*
 SKIP TAKE
 */

Console.WriteLine("\n\nSKIP TAKE");
var usuarioSkipTake = db.Usuarios!.Skip(1).Take(2).ToList();

foreach (var usuario in usuarioSkipTake)
{
    Console.WriteLine($"\n-- {usuario.Nome}");
}

/*

SELECT 

 */

Console.WriteLine("\nSELECT");
var usuarioSelect = db.Usuarios!.Where(a => a.Id > 2).Select(a => new Usuario { Id = a.Id, Nome = a.Nome, Mae = a.Mae }).ToList();

foreach (var usuario in usuarioSelect)
{
    Console.WriteLine($"\n- COD: {usuario.Id} - Nome: {usuario.Nome} - Mãe: {usuario.Mae}");
}


/*
 SQL, QUERY BEM PERFORMÁTICA.
 CONSULTA COM SQL PURO
 */

db.ChangeTracker.Clear();

Console.WriteLine("\nEXECUÇÃO SQL");



var nome = new SqlParameter("@nome", "F ernando%");
var usuariosSqlRaw = db.Usuarios!.FromSqlRaw($"SELECT * FROM [Usuarios] WHERE Nome LIKE @nome", nome).IgnoreAutoIncludes().ToList();

foreach (var usuario in usuariosSqlRaw)
{
    Console.WriteLine($"\n- COD: {usuario.Id} - Nome: {usuario.Nome} - Mãe: {usuario.Mae}");
}

/*
 
 Podemos executar comando sem retorno:
    - INSERT, UPDATE, DELETE
    - STORED PROCEDURES.
 
 */


Console.WriteLine("\nEXECUÇÃO DE ATUALIZAÇÃO COM SQL PURO");
var mae = "Agnes";
db.Database.ExecuteSqlInterpolated($"UPDATE [usuarios] SET [Mae] = {mae} WHERE Id = 1");