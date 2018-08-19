using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class EditorComponentDelete {

    static Queue<Component> markForDelete = new Queue<Component>();

    public static int queuedAmount { get { return markForDelete.Count; } }

#if UNITY_EDITOR
    static EditorComponentDelete()
    {
        //Raised when an object or group of objects in the hierarchy changes.
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }
#endif

    private static void MarkForDeletetion(Component c)
    {
        markForDelete.Enqueue(c);
    }


    //Only exists in editor runtime
    private static void OnHierarchyChanged()
    {
        if (markForDelete.Count <= 0)
            return;

        int DequeueAmount = queuedAmount;
        for (int i = 0; i < DequeueAmount; i++)
        {
            UnityEngine.Object.DestroyImmediate(markForDelete.Dequeue());
        }
    }

    /// <summary>
    /// Enqueue 'c' for deletetion if 'gameObject' has more than one 'T' attached -- EDITOR ONLY
    /// </summary>
    public static void ResetCheck<T>(GameObject gameObject, Component c, int maxIntances = 2)
    {
        Type t = typeof(T);
        if (gameObject.GetComponents<T>().Length >= maxIntances)
        {
            Debug.LogError("Cannot add \"Componentable:\" " + t.FullName + " to GameObject because it already exist on " + gameObject.name);
#if UNITY_EDITOR
            if (EditorApplication.isPlaying == true)
            {
#endif
            UnityEngine.Object.DestroyImmediate(c);
#if UNITY_EDITOR
            }
            else
            {
                EditorComponentDelete.MarkForDeletetion(c);
            }
#endif
        }
    }
}
