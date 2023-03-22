using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomObserver : Singleton<BoomObserver>
{
    [SerializeField] private List<IObserver> list_Observers = new List<IObserver>();
    [SerializeField] private ScoreObj scoreObj;
    [SerializeField] private IObserverBoos observerBoos;



    public void ResisterObserver(IObserver observer)
    {
        list_Observers.Add(observer);
    }
    public void ResisterObserver(IObserverBoos observer)
    {
        observerBoos = (observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        list_Observers.Remove(observer);
    }
    public void RemoveObserver(IObserverBoos observer)
    {
        observerBoos = null;
    }
    public void NotifyObservers()
    {
        foreach (var observer in list_Observers)
        {
            observer.DestroyObj();

            GameObject obj = Instantiate(scoreObj).gameObject;
            obj.transform.position = observer.ThisTransform()
            + new Vector3(Random.Range(-1.5f, 1.5f), Random.Range(-1.5f, 1.5f), 0);
        }

        if (observerBoos != null) observerBoos.OnBoomDamage();

        list_Observers.Clear();
    }
}
