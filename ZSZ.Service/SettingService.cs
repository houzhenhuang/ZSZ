using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZSZ.DTO;
using ZSZ.IService;
using ZSZ.Service.Entities;

namespace ZSZ.Service
{
    public class SettingService : ISettingService
    {
        public SettingDTO[] GetAll()
        {
            using (ZSZDbContext context = new ZSZDbContext())
            {
                BaseService<SettingEntity> service = new BaseService<SettingEntity>(context);
                return service.GetAll().AsNoTracking().Select(p => new SettingDTO
                {
                    Id = p.Id,
                    CreateDateTime = p.CreateDateTime,
                    Name = p.Name,
                    Value = p.Value
                }).ToArray();
            }
        }

        public bool? GetBoolValue(string name)
        {
            string value = this.GetValue(name);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }

        public int? GetIntValue(string name)
        {
            string value = this.GetValue(name);
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return Convert.ToInt32(value);
            }
        }

        public string GetValue(string name)
        {
            using (ZSZDbContext context = new ZSZDbContext())
            {
                BaseService<SettingEntity> service = new BaseService<SettingEntity>(context);
                SettingEntity entity = service.GetAll().AsNoTracking().SingleOrDefault(p => p.Name == name);
                return entity == null ? null : entity.Value;
            }
        }

        public void SetBoolValue(string name, bool value)
        {
            this.SetValue(name, value.ToString());
        }

        public void SetIntValue(string name, int value)
        {
            this.SetValue(name, value.ToString());
        }

        public void SetValue(string name, string value)
        {
            using (ZSZDbContext context = new ZSZDbContext())
            {
                BaseService<SettingEntity> service = new BaseService<SettingEntity>(context);

                SettingEntity entity = service.GetAll().SingleOrDefault(p => p.Name == name);
                if (entity == null)
                {
                    context.Settings.Add(new SettingEntity { Name = name, Value = value });
                }
                else
                {
                    entity.Value = value;
                }
                context.SaveChanges();
            }
        }
    }
}
