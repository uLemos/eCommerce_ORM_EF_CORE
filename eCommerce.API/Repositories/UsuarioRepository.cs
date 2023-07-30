using eCommerce.API.DataBase;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly eCommerceContext _db;

        public UsuarioRepository(eCommerceContext db)
        {
            _db = db;
        }

        //public static List<Usuario> _db = new List<Usuario>(); //Virá da api
        public List<Usuario> Get()
        {
            //return _db.Usuarios!.OrderBy(a => a.Id).ToList();  
            return _db.Usuarios!.Include(a => a.Contato).OrderBy(a => a.Id).ToList();  
        }

        public Usuario Get(int id)
        {
            //EFCORE = 1; Dapper = +20; ADO.NET = +40; Questão de linhas de código...

            //return _db.Usuarios!.Find(id)!;
            return _db.Usuarios!.Include(a => a.Contato).Include(a => a.EnderecosEntrega).Include(a => a.Departamentos).FirstOrDefault(a => a.Id == id)!;
        }

        public void Add(Usuario usuario)
        {
            //Padrão de projetos
            //Unit Of Works

            CriarVinculoDoUsuarioComDepartamento(usuario);

            //Vão para a memória - EF Core
            _db.Usuarios!.Add(usuario); //Adicionando na Lista
            //Saem da memória e vão para o banco, como se fosse um commit.
            _db.SaveChanges();


        }

        public void Update(Usuario usuario)
        {
            //DELETA TODOS OS VÍNCULOS E RECRIA TODOS OS VÍNCULOS, EVITANDO A PREOCUPAÇÃO COM A PARTE DO UPDATE.

            //TODO - Excluir os Vínculos do Usuario com o Departamento
            ExcluirVinculoDoUsuarioComDepartamento(usuario);
            //_db.SaveChanges(); -> Se eu quiser excluir antes de aplicar a atualização.

            //TODO - Criar Vínculos e departamentos se necessário
            CriarVinculoDoUsuarioComDepartamento(usuario);

            _db.Usuarios!.Update(usuario); //Pega ndo o Id que está no Get, q o Get pegou, ou seja, basta remover e adicionar um novo.
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db.Usuarios!.Remove(Get(id));
            //_db.Remove(Get(id)); //Recebendo o id do usuário.
            _db.SaveChanges();
        }
        private void ExcluirVinculoDoUsuarioComDepartamento(Usuario usuario)
        {
            var usuarioDoBanco = _db.Usuarios!.Include(a => a.Departamentos).FirstOrDefault(a => a.Id == usuario.Id);

            foreach (var departamento in usuarioDoBanco!.Departamentos!)
            {
                usuarioDoBanco.Departamentos.Remove(departamento);
            }
            _db.SaveChanges();
            _db.ChangeTracker.Clear(); //Limpa a lista de objetos q está sendo acompanhado.
        }

        private void CriarVinculoDoUsuarioComDepartamento(Usuario usuario)
        {
            if (usuario.Departamentos != null)
            {
                var departamentos = usuario.Departamentos;

                usuario.Departamentos = new List<Departamento>();

                foreach (var departamento in departamentos)
                {
                    if (departamento.Id > 0)
                    {
                        //Adicionando uma ref. no registro do banco de dados
                        usuario.Departamentos.Add(_db.Departamentos!.Find(departamento.Id)!);
                    }
                    else
                    {
                        //Adicionando uma ref. a um objeto novo, que não existe no SGBD. (Novo registro de departamento)            
                        usuario.Departamentos.Add(departamento);
                    }
                }
            }
        }
    }
}
