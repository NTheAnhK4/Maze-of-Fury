using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : ParentBehavior where T : ParentBehavior
{
    private static T instance;
    public static T Instance => instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Only one " + typeof(T) + " allows to exist");
        }

        
        instance = (T)(ParentBehavior)this;
    }
}
