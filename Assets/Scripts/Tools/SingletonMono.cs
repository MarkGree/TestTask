using UnityEngine;

public abstract class SingletonMono<T> : MonoBehaviour
{
    protected static T instance;
    public static T Instance => instance;

    protected virtual void Awake()
    {
        instance = GetComponent<T>();
    }
}
