﻿using System;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.EventBus.Local;
using Yi.Framework.Bbs.Domain.Shared.Etos;

namespace Yi.Framework.Bbs.Application.Services.Integral
{
    public class LuckyService : ApplicationService
    {
        private ILocalEventBus _localEventBus;
        public LuckyService(ILocalEventBus localEventBus) { _localEventBus = localEventBus; }

        /// <summary>
        /// 大转盘
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<int> PostWheel()
        {
            int[] values=new int[10] { 0,10,30,50,60,80,90,100,200,666};
            var index = GetWheelIndex();
            var value = values[index]-50;

            //修改钱钱，如果钱钱不足，直接会丢出去,那本次抽奖将无效
            await _localEventBus.PublishAsync(new MoneyChangeEventArgs { UserId = CurrentUser.Id!.Value, Number = value }, false);

            return index;
        }

        private int GetWheelIndex()
        {
            int[] probabilities = { 20, 20, 30, 30, 5, 3, 2, 2, 2, 1 };

            int total = 0;
            foreach (var prob in probabilities)
            {
                total += prob;
            }

            int randomNum = new Random().Next(1, total + 1);

            int cumulativeProb = 0;
            for (int i = 0; i < probabilities.Length; i++)
            {
                cumulativeProb += probabilities[i];
                if (randomNum <= cumulativeProb)
                {
                    return i;
                }
            }
            var value = probabilities.Length - 1;
            return value;
        }
    }
}
