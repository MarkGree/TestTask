using UnityEngine;

public static class VectorE
{
    public static float GetRandom(this Vector2 range) => Random.Range(range.x, range.y);
}
