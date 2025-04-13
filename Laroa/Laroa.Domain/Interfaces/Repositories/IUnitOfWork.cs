namespace Laroa.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        public IProductRepository ProductRepository { get; }
        public IOrderRepository OrderRepository { get; } 
        public IProductImageRepository ProductImageRepository { get; }
        public IReviewRepository ReviewRepository { get; }
        public IReduceriRepository ReduceriRepository { get; }
        public IAdminRepository AdminRepository { get; }
        public IUserRepository UserRepository { get; }

        Task Save();
    }
}