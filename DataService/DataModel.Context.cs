namespace DataService
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VismaEntities : DbContext
    {
        public VismaEntities()
            : base("name=VismaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Agr> Agrs { get; set; }
        public virtual DbSet<Hol> Hols { get; set; }
        public virtual DbSet<Ord> Ords { get; set; }
        public virtual DbSet<Prod> Prods { get; set; }
        public virtual DbSet<R1> R1 { get; set; }
        public virtual DbSet<R5> R5 { get; set; }
        public virtual DbSet<R8> R8 { get; set; }
        public virtual DbSet<R9> R9 { get; set; }
        public virtual DbSet<Txt> Txts { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
    }
}
