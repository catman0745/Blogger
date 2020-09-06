namespace Catman.Blogger.Core.Repositories
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task SaveChangesAsync();
    }
}
