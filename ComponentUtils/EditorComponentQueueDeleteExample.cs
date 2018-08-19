using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EditorComponentQueueDeleteExample : MonoBehaviour, IInterfaceType {

    // Use this for initialization
    void Start () {
    
	  }
	
    // Update is called once per frame
    void Update () {

    }

    void Reset()
    {
        EditorComponentQueueDelete.ResetCheck<IInterfaceType>(gameObject, this);//+1 overload where int equals max number before you can't add anymore.
    }
}
