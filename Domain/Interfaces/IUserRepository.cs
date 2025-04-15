using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User> GetUserEmailAsync(string email);
    }

}
