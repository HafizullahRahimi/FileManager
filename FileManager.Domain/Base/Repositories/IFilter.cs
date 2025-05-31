using System.Linq.Expressions;

namespace FileManager.Domain.Base.Repositories;
public interface IFilter<TEntity>
{
    Expression<Func<TEntity, bool>> ToExpression();
}