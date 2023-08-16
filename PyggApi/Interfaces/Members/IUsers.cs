namespace PyggApi.Interfaces.Members
{
    public interface IUsers
    {
        Task<List<User>> AddUser(User user);
        Task<User> LoginUser(User user);
    }
}
