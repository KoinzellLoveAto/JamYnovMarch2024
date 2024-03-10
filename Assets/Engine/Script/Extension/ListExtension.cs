using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RakaExtension.ListExtension
{
    public static class ListExtension
    {
        internal static void OrderRandom<T>(this List<T> a_list)
        {
            int nbElem = a_list.Count;
            List<T> tmp = new List<T>(a_list);
            List<T> copy = new List<T>(a_list);
            a_list.Clear();

            for (int index = 0; index < nbElem; index++)
            {
                int rnd = Random.Range(0, tmp.Count);
                a_list.Add(tmp[rnd]);
                tmp.Remove(tmp[rnd]);
            }
        }

        internal static T PickRandom<T>(this List<T> a_list)
        {
            int rndIndex = Random.Range(0, a_list.Count-1);
            return a_list[rndIndex];
        }

    }
}
