using UnityEngine;
using System.Collections;

public abstract class Singleton<T> : GameEventHandler where T : GameEventHandler
{

    private static T _instance = null;
    private static bool applicationIsQuitting = false;


    public static T Instance
    {

        get
        {
            if (applicationIsQuitting)
            {
                Debug.Log(typeof(T) + " Singleton is already destroyed. Returning null. Please check HasInstance first before accessing instance in destructor.");
                return null;
            }

            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {
                    _instance = new GameObject().AddComponent<T>();
                    _instance.gameObject.name = _instance.GetType().Name;
                    GameObject.DontDestroyOnLoad(_instance.gameObject);
                }
            }

            return _instance;

        }

    }

    public static bool HasInstance
    {
        get
        {
            return !IsDestroyed;
        }
    }

    public static bool IsDestroyed
    {
        get
        {
            if (_instance == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    /// <summary>
    /// Unity가 종료되면 오브젝트가 임의의 순서로 파괴됩니다. 원칙적으로 싱글톤은 응용 프로그램이 종료 될 때만 파괴됩니다.
    /// 인스턴스가 파괴 된 후 스크립트가 인스턴스를 호출하면 응용 프로그램 재생을 중지 한 후에도 편집기 장면에 머물러있는 버그있는 빈 객체가 생성됩니다.
    /// 그래서 이러한 버그가 없도록 아래의 함수를 이용해 추가적인 처리를 합니다.
    /// </summary>
    public override void OnDestroy()
    {
        UnregisterGameEvent();
        base.OnDestroy();
        
        _instance = null;
        applicationIsQuitting = true;
    }

    protected virtual void OnApplicationQuit()
    {
        _instance = null;
        applicationIsQuitting = true;
    }

}