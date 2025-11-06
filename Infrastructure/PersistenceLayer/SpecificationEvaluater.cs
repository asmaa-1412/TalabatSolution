using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLayer
{
    public static class SpecificationEvaluater
    {
        public static IQueryable<TEntity> CreateQuery<TEntity,TKey>(IQueryable<TEntity> inputQuery,ISpecifications<TEntity, TKey> spacifications)
            where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if(spacifications.Criteria != null)
            {
                query = query.Where(spacifications.Criteria);
            }
            

            if (spacifications.Orderby != null)
            {
                query = query.OrderBy(spacifications.Orderby);
            }
            if (spacifications.OrderbyDesc != null)
            {
                query = query.OrderByDescending(spacifications.OrderbyDesc);
            }

            if(spacifications.IncludeExpression != null && spacifications.IncludeExpression.Count > 0)
            {
                foreach (var include in spacifications.IncludeExpression)
                {
                    query.Include(include);
                }
            }
            return query;

        }
    }
}
