﻿using Mapster;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Local;
using Yi.Framework.Bbs.Domain.Entities;
using Yi.Framework.Bbs.Domain.Entities.Integral;
using Yi.Framework.Bbs.Domain.Shared.Caches;
using Yi.Framework.Bbs.Domain.Shared.Consts;
using Yi.Framework.Bbs.Domain.Shared.Etos;

namespace Yi.Framework.Bbs.Domain.Managers
{
    public class LevelManager : DomainService
    {
        private ILocalEventBus _localEventBus;
        private IDistributedCache<List<LevelCacheItem>> _levelCache;
        private IRepository<LevelAggregateRoot> _repository;
        private IRepository<BbsUserExtraInfoEntity> _bbsUserRepository;
        public LevelManager( ILocalEventBus localEventBus,
            IDistributedCache<List<LevelCacheItem>> levelCache, IRepository<LevelAggregateRoot> repository, IRepository<BbsUserExtraInfoEntity> bbsUserRepository)
        {
            _localEventBus = localEventBus;
            _repository = repository;
            _bbsUserRepository = bbsUserRepository;
            _levelCache = levelCache;
        }


        /// <summary>
        /// 获取等级映射，所有获取等级操作通过这里操作
        /// </summary>
        /// <returns></returns>
        public async Task<Dictionary<int, LevelCacheItem>> GetCacheMapAsync()
        {
            var items =await _levelCache.GetOrAddAsync(LevelConst.LevelCacheKey, async () =>
            {
                var cacheItem = (await _repository.GetListAsync())
                    .OrderByDescending(x => x.CurrentLevel).ToList()
                    .Adapt<List<LevelCacheItem>>();
                return cacheItem;
            });
            return items.ToDictionary(x=>x.CurrentLevel);
        }
        
        /// <summary>
        /// 使用钱钱投喂等级
        /// </summary>
        /// <returns></returns>
        public async Task ChangeLevelByMoneyAsync(Guid userId, int moneyNumber)
        {
            //通过用户id获取用户信息的经验和等级
            var userInfo = await _bbsUserRepository.GetAsync(x=>x.UserId==userId);

            //钱钱和经验的比例为1：1
            //根据钱钱修改经验
            var currentNewExperience = userInfo.Experience + moneyNumber * 1;

            //修改钱钱，如果钱钱不足，直接会丢出去
            await _localEventBus.PublishAsync(new MoneyChangeEventArgs { UserId = userId, Number = -moneyNumber },
                false);

            //更改最终的经验再变化等级
            var levelList = (await GetCacheMapAsync()).Values;
            var currentNewLevel = 1;
            foreach (var level in levelList)
            {
                if (currentNewExperience >= level.MinExperience)
                {
                    currentNewLevel = level.CurrentLevel;
                    break;
                }
            }

            userInfo.Level = currentNewLevel;
            userInfo.Experience = currentNewExperience;
            await _bbsUserRepository.UpdateAsync(userInfo);
        }
    }
}