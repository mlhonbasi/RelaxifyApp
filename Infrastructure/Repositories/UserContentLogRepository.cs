using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class UserContentLogRepository(RelaxifyDbContext context) : GenericRepository<UserContentLog>(context), IUserContentLogRepository
    {
    }
}
