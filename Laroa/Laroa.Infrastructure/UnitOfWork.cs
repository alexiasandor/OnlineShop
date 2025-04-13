using Laroa.Domain.Interfaces.Repositories;

namespace Laroa.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext applicationDbContext, IProductRepository productRepository, IOrderRepository orderRepository, IProductImageRepository productImageRepository, IReviewRepository reviewRepository, IReduceriRepository reduceriRepository, IAdminRepository adminRepository, IUserRepository userRepository)
        {
            _context = applicationDbContext;
            ProductRepository = productRepository;
            OrderRepository = orderRepository;
            ProductImageRepository = productImageRepository;
            ReviewRepository = reviewRepository;
            ReduceriRepository = reduceriRepository;
            AdminRepository = adminRepository;
            UserRepository = userRepository;
        }

        public IProductRepository ProductRepository { get; private set; }
        public IOrderRepository OrderRepository { get; private set; }
        public IProductImageRepository ProductImageRepository { get; private set; }
        public IReviewRepository ReviewRepository { get; private set; }
        public IReduceriRepository ReduceriRepository { get; private set; }
        public IAdminRepository AdminRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
