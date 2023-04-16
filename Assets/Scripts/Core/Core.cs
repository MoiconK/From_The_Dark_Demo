using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    public readonly List<CoreComponent> CoreComponents = new List<CoreComponent>();

    public void LogicUpdate()
    {
        foreach (CoreComponent component in CoreComponents)
        {
            component.LogicUpdate();
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        var comp = CoreComponents
            .OfType<T>()
            .FirstOrDefault();

        if(comp) return comp;

        //Debug.Log($"Type of comp found: {comp.GetType()}");       
            comp = GetComponentInChildren<T>();

        if (comp) return comp;
            
        Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");      

        return null;
    }

    public void AddComponent(CoreComponent component)
    {
        if (!CoreComponents.Contains(component))
        {
            CoreComponents.Add(component);
        }
    }
}
