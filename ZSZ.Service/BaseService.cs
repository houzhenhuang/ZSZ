using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    class BaseService<T> : IServiceSupport where T : BaseEntity
    {
        private ZSZDbContext context;
        public BaseService(ZSZDbContext ctx)
        {
            this.context = ctx;
        }
        /// <summary>
        /// 获取所有没有软删除的数据
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return context.Set<T>().Where(e => e.IsDeleted == false);
        }
        /// <summary>
        /// 获取所有没有软删除的数据总数
        /// </summary>
        /// <returns></returns>
        public long GetTotalCount()
        {
            return GetAll().LongCount();
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IQueryable<T> GetPageData(int pageIndex, int pageSize)
        {
            return this.GetAll().OrderBy(p => p.CreateDateTime).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public T GetById(long id)
        {
            return this.GetAll().Where(p => p.Id == id).SingleOrDefault();
        }
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="id"></param>
        public void MarkDeleted(long id)
        {
            var entity = this.GetById(id);
            entity.IsDeleted = true;
            context.SaveChanges();
        }


    }
}
