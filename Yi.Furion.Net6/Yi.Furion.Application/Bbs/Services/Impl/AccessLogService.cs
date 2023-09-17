﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTool.Extension;
using Mapster;
using Yi.Framework.Infrastructure.Ddd.Repositories;
using Yi.Furion.Core.Bbs.Dtos.AccessLog;
using Yi.Furion.Core.Bbs.Entities;

namespace Yi.Furion.Application.Bbs.Services.Impl
{
    [ApiDescriptionSettings("BBS")]
    public class AccessLogService : IAccessLogService, IDynamicApiController
    {
        private readonly IRepository<AccessLogEntity> _repository;
        public AccessLogService(IRepository<AccessLogEntity> repository) { _repository = repository; }

        public DateTime GetWeekFirst()
        {
            var week = DateTime.Now.DayOfWeek;
            switch (week)
            {
                case DayOfWeek.Sunday:
                    return DateTime.Now.AddDays(-7).Date;

                case DayOfWeek.Monday:
                    return DateTime.Now.AddDays(-1).Date;

                case DayOfWeek.Tuesday:
                    return DateTime.Now.AddDays(-2).Date;

                case DayOfWeek.Wednesday:
                    return DateTime.Now.AddDays(-3).Date;

                case DayOfWeek.Thursday:
                    return DateTime.Now.AddDays(-4).Date;

                case DayOfWeek.Friday:
                    return DateTime.Now.AddDays(-5).Date;

                case DayOfWeek.Saturday:
                    return DateTime.Now.AddDays(-6).Date;

                default:
                    throw new ArgumentException("日期错误");
            }
        }

        /// <summary>
        /// 触发
        /// </summary>
        /// <returns></returns>
        [HttpPost("")]
        public async Task AccessAsync()
        {
            //可判断http重复，防止同一ip多次访问
            var last = await _repository._DbQueryable.OrderByDescending(x => x.CreationTime).FirstAsync();

            if (last is null || last.CreationTime.Date != DateTime.Today)
            {
                await _repository.InsertAsync(new AccessLogEntity());
            }
            else
            {
                await _repository._Db.Updateable<AccessLogEntity>().SetColumns(it => it.Number == it.Number + 1).Where(it => it.Id == last.Id).ExecuteCommandAsync();
            }
        }

        /// <summary>
        /// 获取当前周数据
        /// </summary>
        /// <returns></returns>
        public async Task<AccessLogDto[]> GetWeekAsync()
        {
            var lastSeven = await _repository._DbQueryable.OrderByDescending(x => x.CreationTime).ToPageListAsync(1, 7);

            return WeekTimeHandler(lastSeven.ToArray());
        }

        private AccessLogDto[] WeekTimeHandler(AccessLogEntity[] data)
        {
            data = data.Where(x => x.CreationTime >= GetWeekFirst()).OrderByDescending(x => x.CreationTime).DistinctBy(x => x.CreationTime.DayOfWeek).ToArray();

            Dictionary<DayOfWeek, AccessLogDto> processedData = new Dictionary<DayOfWeek, AccessLogDto>();

            // 初始化字典，将每天的数据都设为0
            foreach (DayOfWeek dayOfWeek in Enum.GetValues(typeof(DayOfWeek)))
            {
                processedData.Add(dayOfWeek, new AccessLogDto());
            }


            // 处理原始数据
            foreach (var item in data)
            {
                DayOfWeek dayOfWeek = item.CreationTime.DayOfWeek;
                // 如果当天有数据，则更新字典中的值为对应的Number
                var sss = data.Adapt<AccessLogDto>();
                processedData[dayOfWeek] = item.Adapt<AccessLogDto>();

            }
            var result = processedData.Values.ToList();

            //此时的时间是周日-周一-周二，需要处理
            var first = result[0]; // 获取第一个元素
            result.RemoveAt(0); // 移除第一个元素
            result.Add(first); // 将第一个元素添加到末尾

            return result.ToArray();
        }
    }
}
