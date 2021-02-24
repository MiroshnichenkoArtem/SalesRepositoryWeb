using System;
using SalesRepositoryWeb.Models;

namespace SalesRepositoryWeb.DAL
{
    public class UnitOfWork : IDisposable
    {
        private SalesContext db = new SalesContext();

        private GenericRepository<Manager> _managerRepository;
        private GenericRepository<Sale> _salesRepository;
        private GenericRepository<Product> _productRepository;
        private GenericRepository<Customer> _customerRepository;

        public GenericRepository<Manager> Managers =>
            _managerRepository ?? (_managerRepository = new GenericRepository<Manager>(db));

        public GenericRepository<Sale> Sales =>
            _salesRepository ?? (_salesRepository = new GenericRepository<Sale>(db));

        public GenericRepository<Customer> Customers =>
            _customerRepository ?? (_customerRepository = new GenericRepository<Customer>(db));

        public GenericRepository<Product> Products =>
            _productRepository ?? (_productRepository = new GenericRepository<Product>(db));

        private bool _disposed;

        public virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this._disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}