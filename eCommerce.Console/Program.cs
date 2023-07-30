using eCommerce.Console.Database;
using eCommerce.Console.Models;

var db = new eCommerceContext();

foreach (var usuario in db.Usuarios)
{
    Console.WriteLine(usuario.Nome);
}
