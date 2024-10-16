namespace Phenix.iTOS.CollaborativeTruckSchedulingService.Models;

/// <summary>
/// 运输模式
/// </summary>
public enum CarryingTaskMode
{
    /// <summary>
    /// 随意装载
    /// 本小箱运输任务之外允许拼搭另一个小箱运输任务
    /// 两小箱运输任务之间允许有不同的装载位置和卸载位置
    /// 集卡在岸桥处根据QuayCraneProcess及装卸位置是否一致考虑采取OneByOne还是Together装卸进行对位
    /// 集卡在场桥处采取OneByOne装卸进行对位
    /// OneByOne装卸采取先前箱后后箱进行对位的次序
    /// </summary>
    WhateverLoad = 0,

    /// <summary>
    /// 双箱满载
    /// 一个任务下前后箱有相同的装载位置和卸载位置
    /// 集卡在岸桥处根据QuayCraneProcess考虑采取OneByOne还是Together装卸进行对位
    /// 集卡在场桥处采取OneByOne装卸进行对位
    /// OneByOne装卸采取先前箱后后箱进行对位的次序
    /// </summary>
    TwinLiftFullLoad = 1,

    /// <summary>
    /// 单箱满载
    /// 一个作业循环下仅执行一个任务且仅运输一个小箱或大箱
    /// </summary>
    SingleLiftFullLoad = 2,
}