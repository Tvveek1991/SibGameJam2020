using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InterfaceTools
{
    public static T GetInteface<T>(Component component)
    {
        if (component == null)
            return default;

        T inter = component.GetComponent<T>();

        if (inter == null)
            return default;

        return inter;
    }
}

