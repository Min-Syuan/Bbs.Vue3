﻿using Volo.Abp.Application.Dtos;
using Yi.Framework.Bbs.Domain.Shared.Enums;

namespace Yi.Framework.Bbs.Application.Contracts.Dtos.Assignment;

public class AssignmentGetListOutputDto:EntityDto<Guid>
{
    
    /// <summary>
    /// 当前步骤数
    /// </summary>
    public int CurrentStepNumber { get; set; }

    /// <summary>
    /// 总共步骤数
    /// </summary>
    public int TotalStepNumber { get; set; }

    /// <summary>
    /// 任务状态
    /// </summary>
    public AssignmentStateEnum AssignmentState { get; set; }

    /// <summary>
    /// 任务奖励的钱钱数量
    /// </summary>
    public decimal RewardsMoneyNumber { get; set; }
    /// <summary>
    /// 任务过期时间
    /// </summary>
    public DateTime? ExpireTime { get; set; }

    public DateTime? CompleteTime { get; set; }
    
    
    public DateTime CreationTime { get; }
    public int OrderNum { get; set; }
}