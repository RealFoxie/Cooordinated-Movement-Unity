
using System.Collections.Generic;
using UnityEngine;

public static class Math
{
    public static int mod(int i, int m)
    {
        return (i % m + m) % m;
    }

    public static Vector2 AngleToVec(float angleInRad)
    {
        return new Vector2(Mathf.Sin(angleInRad), Mathf.Cos(angleInRad)).normalized;
    }
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

