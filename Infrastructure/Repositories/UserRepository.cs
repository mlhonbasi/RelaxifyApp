using Domain.Interfaces;
using Infrastructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{


    public class UserRepository(RelaxifyDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        
        public async Task<User> GetUserEmailAsync(string email)
        {
            return await context.Users.FirstOrDefaultAsync(x=> x.Email == email);
        }
    }
}
