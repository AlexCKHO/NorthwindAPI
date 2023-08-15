using NorthwindApi.Data.Repositories;
using NorthwindApi.Data;
using Microsoft.EntityFrameworkCore;
using NorthwindApi.Models;

namespace NorthwindApi.Tests
{
    public class RepositoryTests
    {

        private NorthwindContext _context;
        private SuppliersRepository _sut;


        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var options = new DbContextOptionsBuilder<NorthwindContext>()
                .UseInMemoryDatabase("NorthwindDB").Options;
            _context = new NorthwindContext(options);

            _sut = new SuppliersRepository(_context); //Dependency injection
        }


        [SetUp]
        public void SetUp()
        {
            if (_context.Suppliers != null)
            {
                _context.Suppliers.RemoveRange(_context.Suppliers);
            }



            _context.Suppliers!.AddRange( //Suppliers! not forgiving 
            new List<Supplier>
            {
                 new Supplier
                 {
                 SupplierId = 1,
                 CompanyName = "Sparta Global",
                 City = "Birmingham",
                 Country = "UK",
                 ContactName = "Nish Mandal",
                 ContactTitle = "Manager"
                 },
                 new Supplier {
                 SupplierId = 2,
                 CompanyName = "Nintendo",
                 City = "Tokyo",
                 Country = "Japan",
                 ContactName = "Shigeru Miyamoto",
                 ContactTitle = "CEO"
                 }
            });
            _context.SaveChanges();
        }

        [Category("Happy Path")]
        [Category("FindAsync")]
        [Test]
        public void FindAsync_GivenValidID_ReturnsCorrectSupplier()
        {
            var result = _sut.FindAsync(1).Result;
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<Supplier>());
            Assert.That(result.CompanyName, Is.EqualTo("Sparta Global"));
        }

        [Test]
        public void GetAllAsync_ReturnsCorrectNumberOfSupplier()
        {
            var result = _sut.GetAllAsync().Result;

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.TypeOf<List<Supplier>>());
            Assert.That(2, Is.EqualTo(result.Count()));

        }
        [Test]
        public void Remove_FindAsyncReturnsNull()
        {
            var DeleteSupplier = _sut.FindAsync(1).Result;
            _sut.Remove(DeleteSupplier);
            _sut.SaveAsync();
            var result = _sut.FindAsync(1).Result;

            Assert.AreEqual(result, null);

        }

       [Test]
        public void Add_ReturnCorrectNumberOfSupplier()
        {
            _sut.Add(new Supplier
            {
                SupplierId = 3,
                CompanyName = "Sparta Global",
                City = "Birmingham",
                Country = "UK",
                ContactName = "Nish Mandal",
                ContactTitle = "Manager"
            });

            _sut.SaveAsync().Wait();

            var result = _sut.GetAllAsync().Result;

            Assert.AreEqual(result.Count(), 3);

        }

        [Test]
        public void FindAsync_GivenInvaildID_ReturnCorrectAttributes()
        {
            var result = _sut.FindAsync(99).Result;


            Assert.AreEqual(result, null);

        }

        [Test]
        public void Update_ReturnCorrectAttributes()
        {

            var existingSupplier = _sut.FindAsync(1).Result;
            existingSupplier.CompanyName = "Testing";
            existingSupplier.City = "Testing";
            existingSupplier.Country = "Testing";
            existingSupplier.ContactName = "Testing";

            _sut.Update(existingSupplier);
            _sut.SaveAsync().Wait();

            var result = _sut.FindAsync(1).Result;

            Assert.AreEqual(result.CompanyName, "Testing");
        }



    }
}
