using ContactManagerTask.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using LumenWorks.Framework.IO.Csv;
using System.Data.Entity.Migrations;

namespace ContactManagerTask.Controllers
{
    public class HomeController : Controller
    {
        ContactContext db = new ContactContext();

        public ActionResult Index()
        {
            var result = (from c in db.Contacts
                          select new
                          {
                              c.Id,
                              c.Name,
                              c.DateOfBirth,
                              c.Married,
                              c.Phone,
                              c.Salary
                          }).ToList().Select(x => new Contact
                          {
                              Id = x.Id,
                              Name = x.Name,
                              DateOfBirth = x.DateOfBirth,
                              Married = x.Married,
                              Phone = x.Phone,
                              Salary = x.Salary
                          });
            return View(result);
        }

        public HomeController()
        {
        }

        [HttpPost]
        public void Index(HttpPostedFileBase csvFile)
        {
            string path = "";

            if (csvFile.ContentLength > 0)
            {
                string fileName = Path.GetFileName(csvFile.FileName);
                path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);

                try
                {
                    csvFile.SaveAs(path);
                }
                catch (Exception ex)
                {
                    ViewData["Feedback"] = ex.Message;
                }
            }
            else
            {
                ViewData["Feedback"] = "Please select a file";
            }
            var csvTable = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead(path)), true))
            {
                csvTable.Load(csvReader);
            }

            List<Contact> contacts = new List<Contact>();

            for (int i = 0; i < csvTable.Rows.Count; i++)
            {
                string[] data = csvTable.Rows[i][0].ToString().Split(new char[] { ';' });
                contacts.Add(new Contact { Name = data[0].ToString(), DateOfBirth = Convert.ToDateTime(data[1]), Married = Convert.ToBoolean(data[2]), Phone = data[3].ToString(), Salary = Convert.ToDecimal(data[4]) });
            }

            foreach (var c in contacts)
            {
                db.Entry(c).State = EntityState.Added;
            }
            db.SaveChanges();
            Response.Write("CSV loaded!");
        }

        public ActionResult EditContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact != null)
            {
                return View(contact);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public void EditContact(Contact c)
        {
            Contact newContact = db.Contacts.Find(c.Id);
            newContact.Name = c.Name;
            newContact.DateOfBirth = c.DateOfBirth;
            newContact.Married = c.Married;
            newContact.Phone = c.Phone;
            newContact.Salary = c.Salary;

            db.Set<Contact>().AddOrUpdate(newContact);
            db.SaveChanges();
            Response.Write("Contact edited!");
        }

        public void DeleteContact(int id)
        {
            Contact contact = db.Contacts.Find(id);
            if (contact != null)
            {
                db.Contacts.Remove(contact);
            }
            db.SaveChanges();
            Response.Write("Contact deleted!");
        }
    }
}