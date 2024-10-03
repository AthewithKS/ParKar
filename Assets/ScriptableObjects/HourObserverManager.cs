using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="HoureObserverMangager", menuName ="ScriptableObject/HourObservermanager")]
public class HourObserverManager : ScriptableObject
{
    List<IHourObserver> hourObservers = new List<IHourObserver>();

    public void Register(IHourObserver observer)
    {
        if(!hourObservers.Contains(observer))
        {
            hourObservers.Add(observer);
        }
    }
    public void UnRegister(IHourObserver observer)
    {
        if(!hourObservers.Contains(observer))
        {
            hourObservers.Remove(observer);
        }
    }
    public void Raise( float currentHour)
    {
        foreach(var Observer in hourObservers)
        {
            Observer.onHourChanged(currentHour);
        }
    }
}
