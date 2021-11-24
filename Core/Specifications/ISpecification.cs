using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
        // BASIC PROPERTIES
        Expression<Func<T, bool>> Criteria { get; } // where
        List<Expression<Func<T, object>>> Includes { get; }

        // ORDER BY PROPERTIES
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }

        // PAGINATION PROPERTIES
        int Skip { get; } // skip certain amount of records
        int Take { get; } // take certain amount of records
        bool IsPagingEnabled { get; } // to check if pagiantion is enabled
    }
}
