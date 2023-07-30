namespace eCommerce.Models
{
    public class Contato
    {
        public int Id { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        /*
         Coluna MER(Modelo de Entidade Relacional)
         Convesão: {Modelo} + {PK} = UsuarioId

        UsuarioId(MER - Coluna) - FK(Propriedade do POO)
         */
        //[ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        /*
         POO (Navegar entre os objetos/tabelas)
        FK(Propriedade do POO) -> UsuarioId(MER - Coluna)
            [ForeignKey("UsuarioId")]
         */
        public Usuario? Usuario { get; set; }
    }
}
