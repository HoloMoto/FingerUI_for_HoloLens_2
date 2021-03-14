
using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent,RequireComponent(typeof(FingerGestureDetect))]
public class FingerGesture : MonoBehaviour
{
    [SerializeField]
    FingerGestureDetect fingerDetect;
    
    [SerializeField,Header("WaitTime")]
    public float SelectWaitTime;

    [Header("FingerStatus")]
    public bool thumbActive, indexActive, middleActive, ringAndPinkyActive,handlost=false;

    [SerializeField,Header("Event")] 
    UnityEvent firstInventory;
    [SerializeField] 
    UnityEvent LostInventry;

    [SerializeField] private UnityEvent ThumbOn,IndexOn, MiddleOn, RingOn,ThumbOff, IndexOff, MiddleOff, RingOff,HandLost;

    
    public bool eventstatus1, eventstatus2, eventstatus3 = false;
    
    [SerializeField]
    private bool setFirst,setSecond,setThird = true;
    public void fingerStatus(string name)
    {
        switch (name)
        {
            case "thumb":
                handlost = false;
                thumbActive = true;
                break;
            case "index":
                handlost = false;
                indexActive = true;
                break;
            case "middle":
                handlost = false;
                middleActive = true;
                break;
            case "ringandpinky":
                handlost = false;
                ringAndPinkyActive = true;
                break;

            
            case "thumblost":
                handlost = false;
                thumbActive = false;
                break;
            case "indexlost":
                handlost = false;
                indexActive = false;
                break;
            case "middlelost":
                handlost = false;
                middleActive = false;
                break;
            case "ringandpinkylost":
                handlost = false;
                ringAndPinkyActive = false;
                break;

            case "Lost":
                handlost = true;
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (!indexActive&&!middleActive&&!ringAndPinkyActive&&!thumbActive)
        {
            Debug.Log("AllFingerClosed");
            //   LostInventry.Invoke();

                IndexOff.Invoke();
                
                MiddleOff.Invoke();
                
                MiddleOff.Invoke();

                fingerEvent("eventend");
            eventstatus1 = false;
            eventstatus2 = false;
            eventstatus3 = false;
        }
        if (!indexActive&&!middleActive&&!ringAndPinkyActive&&thumbActive)
        {
            IndexOff.Invoke();
                
            MiddleOff.Invoke();
                
            MiddleOff.Invoke();
            LostInventry.Invoke();
            eventstatus1 = false;
            eventstatus2 = false;
            eventstatus3 = false;
        }
        if (indexActive&&!middleActive&&!ringAndPinkyActive&&thumbActive)
        {
            if (setFirst)
            {
                Debug.Log("1");
                setFirst = !setFirst;
                eventstatus1 = true;
                fingerEvent("index");
               
            }
        }
        else
        {
            if (!setFirst )
            {
                setFirst = true;
                if (eventstatus1)
                {
                    eventstatus1 = false;   
                    fingerEvent("event1");
                }
            }
            
        }

        
        if (indexActive && middleActive && !ringAndPinkyActive&& thumbActive)
        {
            if (setSecond)
            {
                Debug.Log("2");
                setSecond = !setSecond;
                eventstatus2 = true;
                fingerEvent("middle");
            }
            
        }
        else
        {
            if (!setSecond)
            {
                setSecond = true;
                if (eventstatus2)
                {
                    eventstatus2 = false;     
                    fingerEvent("event2");
                }
            }
        }
        
        
        if (indexActive&&middleActive&&ringAndPinkyActive && thumbActive)
        {
            if (setThird)
            {
                Debug.Log("3");
                setThird = !setThird;
                eventstatus3 = true;
                fingerEvent("ring");
            }
        }
        else
        {
            if (!setThird)
            {
                setThird = true;
                if (eventstatus3)
                {
                    eventstatus3 = false;   
                    Debug.Log("LingOf");
                    fingerEvent("event3");
                }
            }
        }

        if (handlost)
        {
            HandLost.Invoke();
            eventstatus1 = false;
            eventstatus2 = false;
            eventstatus3 = false;
            setFirst = true;
            setSecond = true;
            setThird = true;
        }

        if (thumbActive)
        {
            ThumbOn.Invoke();
        }
        else
        {
            ThumbOff.Invoke();
        }
    }

    void fingerEvent(string st)
    {
        switch (st)
        {
            case "index":
                IndexOn.Invoke();
                if (eventstatus2)
                {
                    MiddleOff.Invoke();
                }

                if (eventstatus3)
                {
                    RingOff.Invoke();
                }
                
                break;
            case"middle":
                MiddleOn.Invoke();
                if (eventstatus1)
                {
                    IndexOff.Invoke();
                }
                if (eventstatus3)
                {
                    RingOff.Invoke();
                }

                break;
            case "ring":
                if (eventstatus1)
                {
                    IndexOff.Invoke();
                }

                if (eventstatus2)
                {
                    MiddleOff.Invoke();
                }
                RingOn.Invoke();
                break;
            case "event1":
                IndexOff.Invoke();
                break;
            case "event2":
                MiddleOff.Invoke();
                break;
            case "event3":
                RingOff.Invoke();
                break;
        }
    }
    
    private void Reset()
    {
        fingerDetect = this.GetComponent<FingerGestureDetect>();
    }
}
