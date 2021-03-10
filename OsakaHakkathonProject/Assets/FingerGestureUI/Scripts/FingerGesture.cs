using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[DisallowMultipleComponent,RequireComponent(typeof(FingerGestureDetect))]
public class FingerGesture : MonoBehaviour
{
    [SerializeField]
    FingerGestureDetect fingerDetect;
    
    [SerializeField,Header("WaitTime")]
    public float SelectWaitTime;

    [Header("FingerStatus")]
    public bool thumbActive, indexActive, middleActive, ringAndPinkyActive=false;

    public void fingerStatus(string name)
    {
        switch (name)
        {
            case "thumb":
                thumbActive = true;
                break;
            case "index":
                indexActive = true;
                break;
            case "middle":
                middleActive = true;
                break;
            case "ringandpinky":
                ringAndPinkyActive = true;
                break;

            
            case "thumblost":
                thumbActive = false;
                break;
            case "indexlost":
                indexActive = false;
                break;
            case "middlelost":
                middleActive = false;
                break;
            case "ringandpinkylost":
                ringAndPinkyActive = false;
                break;

        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (indexActive)
        {
           // StartCoroutine("EventLagTime(1)");
            
        }
        else
        {
            
        }

        if (middleActive)
        {
            
        }
        else
        {
            
        }

        if (ringAndPinkyActive)
        {
            
        }
        else
        {
            
        }
    }

    public void indexEvent()
    {
          
    }
    
    public void SelectGesture()
    {
        
    }

    IEnumerator EventLagTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
    private void Reset()
    {
        fingerDetect = this.GetComponent<FingerGestureDetect>();
    }
}
