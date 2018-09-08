using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace AutofacInterceptors.Models
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("Name=DefaultConnection")
        {
        }

        public DbSet<Product> Products { get; set; }

        private string _userName;

        public string UserName
        {
            get
            {
                return _userName ?? "system";
            }
            set
            {
                _userName = value;
            }
        }

        private DateTime? _now;

        public DateTime Now
        {
            get
            {
                return _now ?? DateTime.Now;
            }
            set
            {
                _now = value;
            }
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            ObjectContext ctx = ((IObjectContextAdapter)this).ObjectContext;

            foreach (var entity in ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Modified))
            {
                BaseModel baseObject = entity.Entity as BaseModel;

                if (baseObject == null)
                {
                    continue;
                }

                baseObject.UpdatedDate = Now;
                baseObject.UpdatedUser = UserName;
            }

            foreach (var entity in ctx.ObjectStateManager.GetObjectStateEntries(EntityState.Added))
            {
                BaseModel baseObject = entity.Entity as BaseModel;

                if (baseObject == null)
                {
                    continue;
                }

                baseObject.CreatedDate = Now;
                baseObject.UpdatedDate = Now;
                baseObject.CreatedUser = UserName;
                baseObject.UpdatedUser = UserName;
            }

            return base.SaveChanges();
        }
    }
}