//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;
//using System.Linq;
//using System.Web;

//namespace ContactManagerTask.Models
//{
//    interface IRepository<T> : IDisposable
//        where T : class
//    {
//        void CreateContact(Contact item);
//        void UpdateContact(Contact item);
//        void DeleteContact(int id);
//        Contact GetContact(int id);
//        IEnumerable<Contact> GetContactsList();

//        void Save();
//    }

//    public class ContactRepository : IRepository<Contact>
//    {
//        private ContactContext db;
//        public ContactRepository()
//        {
//            db = new ContactContext();
//        }
//        public void CreateContact(Contact item)
//        {
//            db.Entry(item).State = EntityState.Added;
//            db.SaveChanges();
//        }

//        public Contact GetContact(int id)
//        {
//            return db.Contacts.Find(id);
//        }

//        public void UpdateContact(Contact item)
//        {
//            db.Set<Contact>().AddOrUpdate(item);
//            db.SaveChanges();
//        }

//        public void DeleteContact(int id)
//        {
//            Contact contact = db.Contacts.Find(id);
//            if (contact != null)
//            {
//                db.Contacts.Remove(contact);
//            }
//            db.SaveChanges();
//        }

//        public IEnumerable<Contact> GetContactsList()
//        {
//            return db.Contacts;
//        }

//        public void Dispose()
//        {
//            throw new NotImplementedException();
//        }

//        public void Save()
//        {
//            db.SaveChanges();
//        }
//    }
//}