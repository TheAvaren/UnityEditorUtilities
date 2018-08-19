using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//Created by Christopher Lovell,
//Only allows a certain amount of components of the same type on a GameObject
//Limitations:You must be able to edit the component and have this:
/*
private void Reset()
{
    EditorComponentQueueDelete.Reset<T>(gameObject, this);
}
*/

public static class EditorComponentQueueDelete {

    static Queue<Component> markForDelete = new Queue<Component>();

    public static int queuedAmount { get { return markForDelete.Count; } }

    static UnityEditorComponentManager()
    {
#if UNITY_EDITOR
        //Raised when an object or group of objects in the hierarchy changes.
        EditorApplication.hierarchyChanged += () => { OnHierarchyChanged(); };
#endif
    }

    private static void MarkForDeletetion(Component c)
    {
#if UNITY_EDITOR
        markForDelete.Enqueue(c);
#else
        Debug.Log("Cannot Queue:" + c.name + " Because we are not in the editor!");
#endif
    }

#if UNITY_EDITOR
    //Only exists in editor runtime
    private static void OnHierarchyChanged()
    {
        int DequeueAmount = queuedAmount;
        for (int i = 0; i < DequeueAmount; i++)
        {
            UnityEngine.Object.DestroyImmediate(markForDelete.Dequeue());
        }
    }
#endif

    /// <summary>
    /// Enqueue 'c' for deletetion if 'gameObject' has more than one 'T' attached -- EDITOR ONLY
    /// </summary>
    public static void ResetCheck<T>(GameObject gameObject, Component c, int maxAmount = 2)
    {
        Type t = typeof(T);
        if (gameObject.GetComponents<T>().Length >= maxAmount)
        {
            Debug.LogError("Can't add Component to GameObject because there is already a script that implements " + t.FullName);
            EditorComponentQueueDelete.MarkForDeletetion(c);
        }
    }
}
