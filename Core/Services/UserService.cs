using Core.Interfaces.Repositories.Users;
using Core.Models.Users;

namespace Core.Services;

public class UserService(IUserRepository userRepository)
{
    public async Task<User> GetUserAsync(int id, CancellationToken cancellationToken = default)
        => await userRepository.GetUserByIdAsync(id, cancellationToken);

    public async Task<IEnumerable<User>> GetUsersAsync(CancellationToken cancellationToken = default)
        => await userRepository.GetAllUsersAsync(cancellationToken);

    public async Task<User> AddUserAsync(User user, CancellationToken cancellationToken = default)
        => await userRepository.AddUserAsync(user, cancellationToken);

    public async Task<User> UpdateUserAsync(User user, CancellationToken cancellationToken = default)
        => await userRepository.UpdateUserAsync(user, cancellationToken);

    public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken = default)
        => await userRepository.DeleteUserAsync(id, cancellationToken);
}
