using Contact.API.Entities;
using Contact.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.API.Services
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ContactContext _context;

        public PersonRepository(ContactContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddContactInfoAsync(Guid personId,ContactInfo contactInfo)
        {
            var person = await GetPersonAsync(personId,false);
            if (person != null)
            {
                person.ContactInfos.Add(contactInfo);
            }
        }

        public async Task AddPersonAsync(Person person)
        {
            await _context.Person.AddAsync(person);
        }

        public async Task<Person?> GetPersonAsync(Guid personId, bool includeContactInfos)
        {
            if (includeContactInfos)
            {
                return await _context.Person.Include(c => c.ContactInfos)
                      .Where(c => c.Id == personId).FirstOrDefaultAsync();
            }

            return await _context.Person
                      .Where(c => c.Id == personId).FirstOrDefaultAsync();
        }
         

        public async Task<IEnumerable<Person>> GetPersonsAsync(bool includeContactInfos)
        {
            if (includeContactInfos)
            {
                return await _context.Person.Include(c => c.ContactInfos)
                    .OrderBy(c => c.FirstName).ToListAsync();
            }

            return await _context.Person.OrderBy(c => c.FirstName)
                .ToListAsync();
        }



        public void RemoveContactInfoAsync(ContactInfo contactInfo)
        {
            _context.ContactInfo.Remove(contactInfo);
        }

        

        public void RemovePersonAsync(Person person)
        {
            _context.Person.Remove(person);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task<bool> PersonExistsAsync(Guid personId)
        {
            return await _context.Person.AnyAsync(c => c.Id == personId);
        }

        public async Task<ContactInfo?> GetContactInfoForPersonAsync(
            Guid contactInfoId)
        {
            return await _context.ContactInfo
               .Where(p => p.Id == contactInfoId)
               .FirstOrDefaultAsync();
        }


    }
}
