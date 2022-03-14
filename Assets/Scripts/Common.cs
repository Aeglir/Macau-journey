using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Common
{
    /// <summary>
    /// 泛型扩展方法：提取子数组
    /// </summary>
    /// <typeparam name="TSource">数组类型</typeparam>
    /// <param name="source">源数据</param>
    /// <param name="startIndex">起始位置</param>
    /// <param name="count">数据长度</param>
    /// <returns>子数组</returns>
    public static T[] CopyRange<T>(this T[] source, int startIndex, int count)
    {
        if (startIndex == 0)
            return source.Take(count).ToArray();
        else if (startIndex + count == source.Length)
            return source.Skip(startIndex).ToArray();
        else
            return source.Skip(startIndex).Take(count).ToArray();
    }
}
