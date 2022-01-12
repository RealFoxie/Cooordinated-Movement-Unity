
using UnityEngine;

public static class Math
{
    public static int mod(int i, int m)
    {
        return (i % m + m) % m;
    }

    public static Vector2 AngleToVec(float angleInRad)
    {
        return new Vector2(Mathf.Sin(angleInRad), Mathf.Cos(angleInRad));
    }
}

