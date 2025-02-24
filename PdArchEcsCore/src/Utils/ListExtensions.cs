namespace PdArchEcsCore.Utils;

using System;
using System.Collections.Generic;

public static class ListExtensions
{
    public static bool IsInRange<T>(this List<T> list, int index)
    {
        return index >= 0 && index < list.Count;
    }

    public static void RemoveAtLast<T>(this IList<T> list)
        => list.RemoveAt(list.Count - 1);

    public static T RemoveWithSwapAtIndex<T>(this IList<T> list, int index)
    {
        var lastIndex = list.Count - 1;
        var value = list[index];
        list[index] = list[lastIndex];
        list.RemoveAt(lastIndex);
        return value;
    }

    public static int RemoveAllWithSwap<T>(this IList<T> list, Func<T, bool> condition)
    {
        var count = 0;
        var index = 0;
        while (index < list.Count)
        {
            var item = list[index];
            if (!condition(item))
            {
                index++;
                continue;
            }

            var lastIndex = list.Count - 1;
            list[index] = list[lastIndex];
            list.RemoveAt(lastIndex);
            count++;
        }

        return count;
    }

    public static bool RemoveWithSwapFirst<T>(this IList<T> list, Func<T, bool> condition)
    {
        var index = 0;
        while (index < list.Count)
        {
            var item = list[index];
            if (!condition(item))
            {
                index++;
                continue;
            }

            var lastIndex = list.Count - 1;
            list[index] = list[lastIndex];
            list.RemoveAt(lastIndex);
            return true;
        }

        return false;
    }
}
