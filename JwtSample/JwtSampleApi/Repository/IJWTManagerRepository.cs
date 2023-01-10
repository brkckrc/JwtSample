using JwtSampleApi.Models;

namespace JwtSampleApi.Repository
{
    public interface IJWTManagerRepository
    {
        Tokens Authenticate(Users user);
    }
}
