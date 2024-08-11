﻿using Yi.Framework.Bbs.Domain.Entities.Assignment;

namespace Yi.Framework.Bbs.Domain.Managers.AssignmentProviders;

public class AssignmentContext
{
    /// <summary>
    /// 全部的任务定义
    /// </summary>
    public List<AssignmentDefineAggregateRoot> AllAssignmentDefine { get; }

    /// <summary>
    /// 当前用户的全部任务数据
    /// </summary>
    public List<AssignmentAggregateRoot> CurrentUserAssignments { get; }

    /// <summary>
    /// 当前用户id
    /// </summary>
    public Guid CurrentUserId { get; set; }
}