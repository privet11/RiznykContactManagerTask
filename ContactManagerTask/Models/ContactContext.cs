using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ContactManagerTask.Models
{
    public class ContactContext : DbContext
    {
        public ContactContext() : base("DefaultConnection") { }

        public DbSet<Contact> Contacts { get; set; }

    }



    public class BookDbInitializer : CreateDatabaseIfNotExists<ContactContext>
    {
        protected override void Seed(ContactContext db)
        {
            base.Seed(db);
        }

    }
}