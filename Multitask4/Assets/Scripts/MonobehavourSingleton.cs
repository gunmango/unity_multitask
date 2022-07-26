using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//꺽쇠만 열어도 템플릿 
//컴
public class MonobehavourSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;

    public static T instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if(_instance == null)
                {
                    GameObject newObject = new GameObject();
                    _instance = newObject.AddComponent<T>();
                }

                //DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }
}
