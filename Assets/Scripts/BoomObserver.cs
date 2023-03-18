using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomObserver : Singleton<BoomObserver>
{
    [SerializeField] private List<IObserver> list_Observers = new List<IObserver>();
    [SerializeField] private ScoreObj scoreObj;
    public void ResisterObserver(IObserver observer)
    {
        list_Observers.Add(observer);
    }
    public void RemoveObserver(IObserver observer)
    {
        list_Observers.Remove(observer);
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

        list_Observers.Clear();
    }
}
