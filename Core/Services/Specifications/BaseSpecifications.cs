using DomainLayer.Contracts;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications(Expression<Func<TEntity, bool>>? criteria)
        {
            Criteria = criteria;

        }
        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpression { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpression.Add(includeExpression);
        }
        #region Orderby
        public Expression<Func<TEntity, object>> Orderby { get; private set; }
        public Expression<Func<TEntity, object>> OrderbyDesc { get; private set; }
        protected void AddOrderby(Expression<Func<TEntity, object>> orderbyExpression)
        {
            Orderby = orderbyExpression;
        }
        protected void AddOrderbyDesc(Expression<Func<TEntity, object>> orderbyDescExpression)
        {
            OrderbyDesc = orderbyDescExpression;
        }
        #endregion
        #region Pagination
        public int Skip { get; private set; }
        public int Take { get; private set; }
        public bool IsPaginated { get; set; }
        protected void ApplyPagination(int pageSize,int pageIndex)
        {
            IsPaginated = true;
            Take = pageSize;
            Skip = (pageIndex-1)*pageSize;
        }
        #endregion
    }
}
