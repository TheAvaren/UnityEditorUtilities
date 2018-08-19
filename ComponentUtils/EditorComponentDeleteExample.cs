using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//Created by Christopher Lovell,
public class EditorComponentQueueDeleteExample : MonoBehaviour, IInterfaceName

    // Use this for initialization
    void Start () 
    {
    	//Will not be called if we have ResetCheck in Awake();
    	Debug.Log("Start!");
    }
    
    //Will only work while in playmode
    void Awake()
    {
     	EditorComponentQueueDelete.ResetCheck<IInterfaceName>(gameObject, this);
    }

    //Will only work outside of playmode
    void Reset()
    {
        EditorComponentQueueDelete.ResetCheck<IInterfaceName>(gameObject, this);//+1 overload where int equals max number before you can't add anymore.
    }
}
