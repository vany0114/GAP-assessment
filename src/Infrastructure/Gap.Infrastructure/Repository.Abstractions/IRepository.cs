using Gap.Infrastructure.DDD;

namespace Gap.Infrastructure.Repository.Abstractions
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
