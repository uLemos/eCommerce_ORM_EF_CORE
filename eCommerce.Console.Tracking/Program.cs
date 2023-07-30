using eCommerce.API.DataBase;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

var db = new eCommerceContext();

//Unit Of Works
//Add, Update, Remove

var usuario01 = new Usuario { Nome = "Lemos" };
db.Usuarios!.Add(usuario01);

var usuario02 = db.Usuarios.Find(2);
usuario02!.Nome = "Fernando";
db.Usuarios.Update(usuario02);

var usuario03 = db.Usuarios.Find(3);
db.Usuarios.Remove(usuario03!);

var usuario04 = new Usuario() { Id = 5, Nome = "Fernando Trevisano Amor da vida da Ana Carolina", Mae = "Shirley" };
db.Usuarios.Attach(usuario04);
db.Entry(usuario04).State = EntityState.Deleted;
db.Entry(usuario04).Property(a => a.Mae).IsModified = false; 
db.Entry(usuario04).Property(a => a.Nome).IsModified = true; 

Console.WriteLine(db.ChangeTracker.DebugView.LongView);









//db.ChangeTracker.Clear();

/*
void TrackingNoTracking()
{

    ////COM TRACKING
    //var usuario01 = db.Usuarios!.FirstOrDefault(a => a.Id == 1);
    //usuario01!.Nome = "Fernando Lemos Trevisano";

    //db.SaveChanges();



    //SEM TRACKING
    var db = new eCommerceContext();
    var usuario01 = db.Usuarios!.AsNoTracking().FirstOrDefault(a => a.Id == 1);
    usuario01!.Nome = "Fernando Lemos";

    db.Update(usuario01);
    db.SaveChanges();
}

*/