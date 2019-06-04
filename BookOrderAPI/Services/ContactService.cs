using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookOrderAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BookOrderAPI.Services
{
    public class ContactService
    {
        private readonly IMongoCollection<Contact> _contacts;

        public ContactService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BookOrderDb"));
            var database = client.GetDatabase("BooksDB");
            _contacts = database.GetCollection<Contact>("Contacts");
        }

        public List<Contact> Get()
        {
            return _contacts.Find(contact => true).ToList();
        }

        public Contact Get(string id)
        {
            return _contacts.Find<Contact>(contact => contact.Id == id).FirstOrDefault();
        }

        public Contact Create(Contact contact)
        {
            _contacts.InsertOne(contact);
            return contact;
        }

        public void Update(string id, Contact contactIn)
        {
            _contacts.ReplaceOne(contact => contact.Id == id, contactIn);
        }

        public void Remove(Contact contactIn)
        {
            _contacts.DeleteOne(contact => contact.Id == contactIn.Id);
        }

        public void Remove(string id)
        {
            _contacts.DeleteOne(contact => contact.Id == id);
        }
    }
}
