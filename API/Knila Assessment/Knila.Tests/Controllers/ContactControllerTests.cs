using NUnit.Framework;
using Moq;
using Knila.BAL.Interface;
using Knila.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Knila_Assessment.Controllers;

namespace Knila.Tests.Controllers
{
    public class ContactControllerTests
    {
        private Mock<IContactRepository> _mockRepo;
        private ContactController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRepo = new Mock<IContactRepository>();
            _controller = new ContactController(_mockRepo.Object, null);
        }

        [Test]
        public async Task GetAllContacts_ReturnsListOfContacts()
        {
            // Arrange
            var expected = new List<Contact> {
                new Contact { Id = 1, FirstName = "John", LastName = "Doe" },
                new Contact { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            _mockRepo.Setup(repo => repo.GetAllContact()).ReturnsAsync(expected);

            // Act
            var result = await _controller.GetAllContacts();

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
        }

        [Test]
        public async Task AddOrUpdateContact_CallsRepositoryMethod()
        {
            // Arrange
            var contact = new Contact { Id = 0, FirstName = "Test", LastName = "User" };

            // Act
            await _controller.AddOrUpdateContact(contact);

            // Assert
            _mockRepo.Verify(r => r.AddOrUpdateContact(contact), Times.Once);
        }

        [Test]
        public async Task RemoveContacts_CallsDeleteContact()
        {
            // Arrange
            var contacts = new[] { new Contact { Id = 1 }, new Contact { Id = 2 } };

            // Act
            await _controller.RemoveContacts(contacts);

            // Assert
            _mockRepo.Verify(r => r.DeleteContact(contacts), Times.Once);
        }
    }
}
