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
    public static System.Tuple<int, T> GetRandom<T>(T[] data)
    {
        int index = Random.Range(0,data.Length);
        return new System.Tuple<int, T>(index,data[index]);
    }
    public static int[] GetRoandomsInEqa(int start,int end,int len)
    {
        System.Collections.Generic.HashSet<int> t=new System.Collections.Generic.HashSet<int>();
        int[] res = new int[len];
        for(int i=0;i<len;)
        {
            int n=Random.Range(start,end);
            if(t.Contains(n))
                continue;
            t.Add(n);
            res[i]=n;
            i++;
        }
        return res;
    }
    public static T[] GetElements<T>(int[] indexs,T[] elements)
    {
        T[] res = new T[indexs.Length];
        for(int i=0;i<indexs.Length;i++)
        {
            res[i]=elements[indexs[i]];
        }
        return res;
    }
}
