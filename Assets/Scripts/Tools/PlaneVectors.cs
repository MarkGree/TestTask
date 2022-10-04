using UnityEngine;

public static class PlaneVectors
{
    public static Vector2 ToPlane(Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public static Vector2 Direction(Vector3 from, Vector3 to)
    {
        return new Vector2(to.x - from.x, to.z - from.z);
    }

    public static float Distance(Vector3 l, Vector3 r)
    {
        return new Vector2(l.x - r.x, l.z - r.z).magnitude;
    }
}
