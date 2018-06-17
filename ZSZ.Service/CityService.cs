using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;
using System.Data.Entity;

namespace ZSZ.Service
{
    public class CityService : ICityService
    {
        public long AddNew(string cityName)
        {
            using (ZSZDbContext context = new ZSZDbContext())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(context);
                bool isExists = service.GetAll().Any(p => p.Name == cityName);
                if (isExists)
                {
                    throw new AggregateException("城市已经存在!");
                }
                CityEntity city = new CityEntity();
                city.Name = cityName;
                context.Cities.Add(city);
                context.SaveChanges();
                return city.Id;
            }
        }

        public CityDTO[] GetAll()
        {
            using (ZSZDbContext context=new ZSZDbContext ())
            {
                BaseService<CityEntity> service = new BaseService<CityEntity>(context);
                return service.GetAll().AsNoTracking().Select(p=>new CityDTO {
                    Name=p.Name,
                    Id=p.Id
                }).ToArray();
            }
        }

        public CityDTO GetById(long id)
        {
            throw new NotImplementedException();
        }
    }
}
