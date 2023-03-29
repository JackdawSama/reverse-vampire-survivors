using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TheSubject : MonoBehaviour
{
    private List<TheObserver> observers = new List<TheObserver>();

    public void AddObserver(TheObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(TheObserver observer)
    {
        observers.Remove(observer);
    }

    protected void NotifyObservers(TheAction action)
    {
        observers.ForEach(observer => observer.Notify(action));
    }
}
