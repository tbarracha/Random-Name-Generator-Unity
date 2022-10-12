using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilsArray
{
    #region Array Extensions
    public static bool Exists<T>(this T[] array)
    {
        if (array != null && array.Length > 0)
            return true;
        else
            return false;
    }

    public static bool Exists<T>(this T[] array, int minLength)
    {
        if (array != null && array.Length >= minLength)
            return true;
        else
            return false;
    }


    public static bool ContainsObject(this Object[] array, Object obj)
    {
        if (Exists(array))
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == obj)
                    return true;
            }

            return false;
        }

        else
        {
            Debug.Log("Array doesn't exist");
            return false;
        }
    }

    /// <summary>
    /// Find and empty spot on array and fill it with selected element
    /// </summary>
    public static bool Add<T>(this T[] array, T element)
    {
        if (Exists(array))
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                {
                    array[i] = element;
                    return true;
                }
            }

            return false;
        }

        else
            return false;
    }


    public static int GetEmptyIndex<T>(this T[] array)
    {
        if (array.Exists())
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == null)
                    return i;
            }

            return -1;
        }

        else
            return -1;
    }

    public static T GetRandom<T>(this T[] array)
        => array[Random.Range(0, array.Length)];

    public static List<T> GetRandomNonRepeat<T>(this T[] array, int amount, float divisionIfExceed = 2)
    {
        if (array.Length <= amount)
            amount = Mathf.RoundToInt(array.Length / divisionIfExceed);

        List<T> randList = new List<T>();

        for (int i = 0; i < amount; i++)
        {
            T rand = GetRandom(array);

            while (randList.Contains(rand))
                rand = GetRandom(array);

            randList.Add(rand);
        }

        return randList;
    }
    #endregion // arrays


    #region List Extensions
    public static bool Exists<T>(this List<T> list)
        => list != null && list.Count > 0 ? true : false;

    public static bool AddSafe<T>(this List<T> list, T element)
    {
        if (list != null && list.Contains(element) == false)
        {
            list.Add(element);
            return true;
        }

        else
            return false;
    }

    public static bool RemoveSafe<T>(this List<T> list, T element)
    {
        if (list != null && list.Contains(element))
        {
            list.Remove(element);
            return true;
        }

        else
            return false;
    }


    /// <summary>
    /// Add array elements to list
    /// </summary>
    public static List<T> AddArrayToList<T>(this List<T> listToAdd, T[] array)
    {
        for (int i = 0; i < array.Length; i++)
            listToAdd.AddSafe(array[i]);

        return listToAdd;
    }


    /// <summary>
    /// Add list elements to another list without repeating
    /// </summary>
    public static List<T> AddListToList<T>(this List<T> listToAdd, List<T> thisToBeAdded)
    {
        for (int i = 0; i < thisToBeAdded.Count; i++)
            listToAdd.AddSafe(thisToBeAdded[i]);

        return listToAdd;
    }


    /// <summary>
    /// Returns a single random element from a list
    /// </summary>
    public static T GetRandom<T>(this List<T> list)
        => list[Random.Range(0, list.Count)];

    /// <summary>
    /// Returns several random elements from a list without repeating values
    /// </summary>
    public static List<T> GetRandomNonRepeat<T>(this List<T> list, int amount, float divisionIfExceed = 2)
    {
        if (list.Count <= amount)
            amount = Mathf.RoundToInt(list.Count / divisionIfExceed);

        List<T> randList = new List<T>();

        for (int i = 0; i < amount; i++)
        {
            T rand = GetRandom(list);

            while (randList.Contains(rand))
                rand = GetRandom(list);

            randList.Add(rand);
        }

        return randList;
    }
    #endregion // lists

    public static List<T> RemoveEmpty<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
                list.Remove(list[i]);
        }

        return list;
    }

    public static T[] RemoveDuplicates<T>(this T[] array)
    {
        List<T> list = new List<T>();
        for (int i = 0; i < array.Length; i++)
            list.Add(array[i]);

        return RemoveDuplicates<T>(list).ToArray();
    }

    public static List<T> RemoveDuplicates<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T item = list[i];

            for (int j = 0; j < list.Count; j++)
            {
                if (j == i)
                    continue;

                if (item.Equals(list[j]))
                    list.Remove(list[j]);
            }
        }

        return list;
    }

    public static List<T> ReverseList<T>(this List<T> list, bool debug = false)
    {
        if (list.Count == 0)
        {
            Debug.Log("List too short!");
            return list;
        }

        var reversed = new List<T>();

        for (int i = 0; i < list.Count; i++)
        {
            int index = Mathf.Clamp(list.Count - 1 - i, 0, list.Count - 1);

            if (debug)
            {
                Debug.Log("List count: " + list.Count);
                Debug.Log("Reversed Index: " + index);
            }

            reversed.Add(list[index]);
        }

        return reversed;
    }

    public static List<T> ReverseArray<T>(this T[] array)
    {
        var reversed = new List<T>();

        for (int i = 0; i < array.Length; i++)
        {
            int index = Mathf.Clamp(array.Length - 1 - i, 0, array.Length);
            reversed.Add(array[index]);
        }

        return reversed;
    }

    public static List<T> ArrayToList<T>(this T[] array)
    {
        var list = new List<T>();

        for (int i = 0; i < array.Length; i++)
            list.Add(array[i]);

        return list;
    }
}
