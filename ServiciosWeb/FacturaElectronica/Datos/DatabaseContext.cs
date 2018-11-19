using System;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using LeandroSoftware.FacturaElectronicaHacienda.Dominio.Entidades;

namespace LeandroSoftware.FacturaElectronicaHacienda.Datos
{
    public interface IDbContext : IDisposable
    {
        DbSet<Empresa> EmpresaRepository { get; set; }
        DbSet<Provincia> ProvinciaRepository { get; set; }
        DbSet<Canton> CantonRepository { get; set; }
        DbSet<Distrito> DistritoRepository { get; set; }
        DbSet<Barrio> BarrioRepository { get; set; }
        DbSet<CantFEMensualEmpresa> CantFEMensualEmpresaRepository { get; set; }
        DbSet<TipoDeCambioDolar> TipoDeCambioDolarRepository { get; set; }
        DbSet<DocumentoElectronico> DocumentoElectronicoRepository { get; set; }
        DbSet<Padron> PadronRepository { get; set; }

        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;
        void NotificarModificacion<TEntity>(TEntity entidad) where TEntity : class;
        void NotificarEliminacion<TEntity>(TEntity entidad) where TEntity : class;
        void ExecuteProcedure(string procedureName, object[] objParameters);
        void Commit();
        void RollBack();
    }

    public partial class DatabaseContext : DbContext, IDbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DatabaseContext(string conectionString)
        {
            Database.Connection.ConnectionString = conectionString;
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Empresa> EmpresaRepository { get; set; }
        public DbSet<Provincia> ProvinciaRepository { get; set; }
        public DbSet<Canton> CantonRepository { get; set; }
        public DbSet<Distrito> DistritoRepository { get; set; }
        public DbSet<Barrio> BarrioRepository { get; set; }
        public DbSet<CantFEMensualEmpresa> CantFEMensualEmpresaRepository { get; set; }
        public DbSet<TipoDeCambioDolar> TipoDeCambioDolarRepository { get; set; }
        public DbSet<DocumentoElectronico> DocumentoElectronicoRepository { get; set; }
        public DbSet<Padron> PadronRepository { get; set; }

        public void ApplyCurrentValues<TEntity>(TEntity original, TEntity entidad) where TEntity : class
        {
            base.Entry<TEntity>(original).CurrentValues.SetValues(entidad);
            base.Entry<TEntity>(original).State = EntityState.Modified;
        }
        public void NotificarModificacion<TEntity>(TEntity entidad) where TEntity : class
        {
            base.Entry<TEntity>(entidad).State = EntityState.Modified;
        }

        public void NotificarEliminacion<TEntity>(TEntity entidad) where TEntity : class
        {
            base.Entry<TEntity>(entidad).State = EntityState.Deleted;
        }

        public DbContextTransaction GetDatabaseTransaction()
        {
            return base.Database.BeginTransaction();
        }

        public void ExecuteProcedure(string procedureName, object[] objParameters)
        {
            String strParameters = "";
            foreach (int parameter in objParameters)
            {
                if (strParameters != "")
                    strParameters += ", ";
                strParameters += parameter;
            }
            base.Database.ExecuteSqlCommand("call " + procedureName + "(" + strParameters + ")");
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public void RollBack()
        {
            var changedEntries = base.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
            {
                entry.State = EntityState.Detached;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
            {
                entry.State = EntityState.Unchanged;
            }
        }
    }
}
