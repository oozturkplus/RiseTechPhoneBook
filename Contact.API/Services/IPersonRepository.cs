using Contact.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contact.API.Services
{
    public interface IPersonRepository
    {
        Task AddPersonAsync(Person person);
        void RemovePersonAsync(Person person);

        Task<Person?> GetPersonAsync(Guid personId, bool includeContactInfos);

        Task AddContactInfoAsync(Guid personId, ContactInfo contactInfo);
        void RemoveContactInfoAsync(ContactInfo contactInfo);

        Task<IEnumerable<Person>> GetPersonsAsync(bool includeContactInfos);

        Task<bool> SaveChangesAsync();

        Task<bool> PersonExistsAsync(Guid personId);

        Task<ContactInfo?> GetContactInfoForPersonAsync(
            Guid contactInfoId);
    }
}
