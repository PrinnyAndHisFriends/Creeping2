using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 普通的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : new()
{
    private static T instance = default(T);

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }
}

/// <summary>
/// mono的单例
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (FindObjectsOfType<T>().Length > 1)
                {
                    Debug.LogError("More than 1!");
                    return instance;
                }

                if (instance == null)
                {
                    string instanceName = typeof(T).Name;
                    Debug.LogError("Instance Name: " + instanceName);
                    GameObject instanceGO = GameObject.Find(instanceName);

                    if (instanceGO == null)
                        instanceGO = new GameObject(instanceName);
                    instance = instanceGO.AddComponent<T>();
                    //DontDestroyOnLoad(instanceGO);
                    Debug.LogError("Add New Singleton " + instance.name + " in Game!");
                }
            }
            return instance;
        }
    }

    public static bool HasInstance()
    {
        return instance != null;
    }

    public void Log(string msg)
    {
        Debug.Log(ToString() + msg);
    }
}

