using Domain.Interfaces;
using Infrastructure.Context;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{


    public class UserRepository(RelaxifyDbContext _context) : GenericRepository<User>(_context), IUserRepository
    {
        
        public async Task<User> GetUserEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(x=> x.Email == email);
        }
    }
}
