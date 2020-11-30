using System.Linq;

namespace _Support.Extensions
{
    public static class LinqExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
            this IQueryable<T> source,
            string ordering,
            params object[] values)
        {
            return source.OrderBy(arg => ordering);
        }
    }
}