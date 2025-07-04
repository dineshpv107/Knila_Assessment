using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Knila.BAL.Implementations;
using Knila.BAL.Interface;
using Knila.DAL.Models;
using Moq;
using Microsoft.Extensions.Logging;

namespace Knila.Tests.Repositories
{
    public class ContactRepositoryTests
    {
        private KnilaDbContext _context;
        private ContactRepository _repository;

        [SetUp]
        public void Setup()
        {
            // Use In-Memory database in testing environment
            var options = new DbContextOptionsBuilder<KnilaDbContext>()
                .UseInMemoryDatabase("TestDatabase") // In-memory database for testing
                .Options;

            _context = new KnilaDbContext(options);
            var logger = new Mock<ILogger<ContactRepository>>();
            _repository = new ContactRepository(logger.Object, _context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose(); // Dispose of the context after tests
        }

        [Test]
        public async Task AddOrUpdateContact_AddsNewContact()
        {
            var contact = new Contact { FirstName = "John", LastName = "Doe" };

            await _repository.AddOrUpdateContact(contact);

            Assert.That(_context.Contacts.Count(), Is.EqualTo(1)); // Assert the contact was added
        }

        [Test]
        public async Task GetAllContacts_ReturnsContacts()
        {
            _context.Contacts.Add(new Contact { FirstName = "Jane", LastName = "Doe" });
            await _context.SaveChangesAsync();

            var contacts = await _repository.GetAllContact();

            Assert.That(contacts.Count, Is.EqualTo(1)); // Assert contacts are returned
        }

        [Test]
        public async Task DeleteContact_RemovesContact()
        {
            var contact = new Contact { FirstName = "Tom", LastName = "Smith" };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            await _repository.DeleteContact(new[] { contact });

            Assert.That(_context.Contacts.Count(), Is.EqualTo(0)); // Assert contact was deleted
        }
    }
}
