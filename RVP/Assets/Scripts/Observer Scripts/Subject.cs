using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObservers(Actions action)
    {
        // foreach(IObserver observer in observers)
        // {
        //     observer.OnNotify(action);
        // }
        observers.ForEach(observer => observer.OnNotify(action));
    }
} 