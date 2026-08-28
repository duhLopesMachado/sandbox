
namespace haylie.git {

    public interface IGitAuth
    {
        Task<string> GetAccessTokenAsync(
            CancellationToken cancellationToken = default(CancellationToken));
    }

}