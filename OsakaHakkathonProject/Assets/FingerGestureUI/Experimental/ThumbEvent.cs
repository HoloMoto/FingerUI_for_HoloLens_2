using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
public class ThumbEvent : MonoBehaviour
{
//    [SerializeField] private TextMeshPro text;
    [SerializeField] FingerGesture fg;

    [SerializeField] private UnityEvent event1,event2,event3;

    [SerializeField] private UnityEvent end1, end2, end3;
    private bool one=true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          
    }
    
    public void action()
    {

        if (fg.eventstatus1)
        {
            event1.Invoke();
            end2.Invoke();
            end3.Invoke();
           // text.text = fg.eventstatus1.ToString();
        }

        if (fg.eventstatus2)
        {
            event2.Invoke();
            end1.Invoke();
            end3.Invoke();
        }

        if (fg.eventstatus3)
        {
            event3.Invoke();
            end1.Invoke();
            end2.Invoke();
        }
        else
        {
            end1.Invoke();
            end2.Invoke();
            end3.Invoke();
        }
        
        this.enabled = false;
    }

    
    
}
