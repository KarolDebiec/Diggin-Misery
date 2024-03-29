﻿using UnityEngine;

/// <summary>
/// Singleton. See <a href="https://github.com/UnityCommunity/UnitySingleton">Unity Singleton</a>
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    #region Fields
    /// <summary>
    /// The instance.
    /// </summary>
    private static T instance;

    #endregion

    #region Properties
    /// <summary>
    /// Gets the instance.
    /// </summary>
    /// <value>The instance.</value>
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                    //DontDestroyOnLoad(instance);
                }
            }
            return instance;
        }
    }
    #endregion

    #region Methods
    /// <summary>
    /// Use this for initialization.
    /// </summary>
    public virtual void Awake()
    {
        if (instance == null || gameObject.name=="ChunkManager")
        {
            instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Singleton destroyed " + gameObject.name);
            Destroy(gameObject);
        }
    }

    public void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    #endregion
}