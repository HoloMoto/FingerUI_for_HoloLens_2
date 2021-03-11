using System;
using UnityEngine;

using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;
using UnityEngine.Events;


public class FingerGestureDetect : MonoBehaviour
{
    [SerializeField,Header("Target"),Tooltip("Tracked Hands")]
    Handedness handType;
    
    [SerializeField,Range(0,90),Header("Threshold")]
    private float indexThreshold = 5;

    [SerializeField,Range(0,90)]
    private float middleThreshold,ringAndPinkyFingerThreshpld, thumbThreshold;

    [SerializeField, Range(0, 90)] 
    private float facingThreshold;

    [SerializeField,Header("Index"),Header("Events")] 
    UnityEvent OnIndexFingerDetect;

    [SerializeField] 
    UnityEvent OnIndexFingerLost;

    [SerializeField,Header("Middle")] 
    UnityEvent OnMiddleFingerDetect;

    [SerializeField] 
    UnityEvent OnMiddleFingerLost;

    [SerializeField, Header("Ring And Pinky")] 
    UnityEvent OnRingAndPinkyDetect;
    [SerializeField]
    UnityEvent OnRingAndPinkyLost;
    [SerializeField,Header("Thumb"),] 
    UnityEvent OnThumbFingerDetect;

    [SerializeField] 
    UnityEvent OnThumbFingerLost;

    [SerializeField, Header("FirstEvent")] 
    UnityEvent OnIndexFingerFirst;  
    [SerializeField] 
    UnityEvent OnMiddleFingerFirst,OnRingAndPinkyFingerFirst;

    [SerializeField, Header("FirstLostEvent")]
     UnityEvent OnIndexLostFirst;

     [SerializeField] UnityEvent OnMiddleLostFirst,OnRingAndPinkyLostFirst;
    
     bool?  handdetected;

     bool indexFirstfinger,middleFirstfinger,ringandPinkyFirstfinger,indexFirstlostfinger,middleFirstLostfinger,ringandPinkyFirstLostfinger;

    // Update is called once per frame
    void Update()
    {
        handdetected = HandJointUtils.FindHand(handType)?.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose PalmPose);
        if ( handdetected != null && handdetected==true )
        {
            
            if (indexfingerDetected()&& HanddirDetected())
            {
                OnIndexFingerDetect.Invoke();
                if (indexFirstfinger)
                {
                    indexFirstfinger = false;
                    //indexFirstlostfinger = true;
                    OnIndexFingerFirst.Invoke();
                }
            }
            else
            {
                OnIndexFingerLost.Invoke();
                if (!indexFirstfinger)
                {
                    indexFirstfinger = true;
                    //indexFirstlostfinger = false;
                    OnIndexLostFirst.Invoke();
                }
            }
            

            if (middlefingerDetected()&& HanddirDetected())
            {
                OnMiddleFingerDetect.Invoke();
                if (middleFirstfinger)
                {
                    middleFirstfinger = false;
                    //middleFirstLostfinger = true;
                    OnMiddleFingerFirst.Invoke();
                }
            }
            else
            {
                OnMiddleFingerLost.Invoke();
                if (!middleFirstfinger)
                {
                    middleFirstfinger = true;
                   // middleFirstLostfinger = false;
                    OnMiddleLostFirst.Invoke();
                }
            }

            if (RingAndPinkyFingerDetected()&& HanddirDetected())
            {
                OnRingAndPinkyDetect.Invoke();
                if (ringandPinkyFirstfinger)
                {
                    ringandPinkyFirstfinger = false;
                    //ringandPinkyFirstLostfinger = true;
                    OnRingAndPinkyFingerFirst.Invoke();
                }
            }
            else
            {
                OnRingAndPinkyLost.Invoke();
                if (!ringandPinkyFirstfinger)
                {
                    ringandPinkyFirstfinger = true;
                    //ringandPinkyFirstLostfinger = false;
                    OnRingAndPinkyLostFirst.Invoke();
                }
            }
        }
        else
        {
            OnIndexFingerLost.Invoke();
            //indexFirstfinger = true;
            OnMiddleFingerLost.Invoke();
            //middleFirstfinger = true;
            OnRingAndPinkyLost.Invoke();
            //ringandPinkyFirstfinger = true;
        }
    }

    private bool HanddirDetected()
    {
        var jointedHand = HandJointUtils.FindHand(handType);
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm,out MixedRealityPose palmPose))
        {
            if ( facingThreshold<Vector3.Angle(palmPose.Up, CameraCache.Main.transform.forward) )
            {
                Debug.Log(palmPose.Up);
                return true;
            }
        }

        return false;
    }


    
    private bool indexfingerDetected()
    {
        var jointedHand = HandJointUtils.FindHand(handType);
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm,out MixedRealityPose PalmPose))
        {
            //各関節のpose
            MixedRealityPose indexTipPose,indexDistalPose,IndexKnucklePose,indexMiddlePose; 
            if(jointedHand.TryGetJoint(TrackedHandJoint.IndexTip,out indexTipPose)&& jointedHand.TryGetJoint(TrackedHandJoint.IndexDistalJoint,out indexDistalPose)&&jointedHand.TryGetJoint(TrackedHandJoint.IndexMiddleJoint,out indexMiddlePose)&& jointedHand.TryGetJoint(TrackedHandJoint.IndexKnuckle,out IndexKnucklePose))
            {
                Vector3 finger1 = IndexKnucklePose.Position - PalmPose.Position;
                Vector3  finger2 = indexMiddlePose.Position - IndexKnucklePose.Position;
                Vector3 finger3 = indexDistalPose.Position - indexMiddlePose.Position;
                Vector3 finger4 = indexTipPose.Position - indexDistalPose.Position;
                
            float c = Vector3.Angle(PalmPose.Position, finger1);
            float d = Vector3.Angle(finger1, finger2);
            float e = Vector3.Angle(finger2, finger3);
            float f = Vector3.Angle(finger3, finger4);

            float aba = (Mathf.Abs(d) + Mathf.Abs(e) + Mathf.Abs(f)) / 3;

                if (aba < indexThreshold)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private bool middlefingerDetected()
    {
        var jointedHand = HandJointUtils.FindHand(handType);
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose PalmPose))
        {
            MixedRealityPose middleTipsPose,middleDistalPose, middleKnucklePose,middleMiddlePose;
            if (jointedHand.TryGetJoint(TrackedHandJoint.MiddleTip, out middleTipsPose) &&
                jointedHand.TryGetJoint(TrackedHandJoint.MiddleDistalJoint, out middleDistalPose) &&
                jointedHand.TryGetJoint(TrackedHandJoint.MiddleKnuckle, out middleKnucklePose)&&jointedHand.TryGetJoint(TrackedHandJoint.MiddleMiddleJoint,out middleMiddlePose))
            {
                Vector3 finger1 = middleKnucklePose.Position - PalmPose.Position;
                Vector3 finger2 = middleMiddlePose.Position - middleKnucklePose.Position;
                Vector3 finger3 = middleDistalPose.Position - middleMiddlePose.Position;
                Vector3 finger4 = middleTipsPose.Position - middleDistalPose.Position;
                
                float c = Vector3.Angle(PalmPose.Position, finger1);
                float d = Vector3.Angle(finger1, finger2);
                float e = Vector3.Angle(finger2, finger3);
                float f = Vector3.Angle(finger3, finger4);

                float aba = (Mathf.Abs(d) + Mathf.Abs(e) + Mathf.Abs(f)) / 3;

                if (aba < middleThreshold)
                {
                    return true;
                }
                
            }
        }

        return false;
    }

    private bool RingAndPinkyFingerDetected()
    {
        //※メモ　HoloLens2の場合実装的には薬指、小指それぞれの判定は可能だが、実機の場合特に小指単体のトラッキング精度に問題があるため薬指、小指はひとまとめで一つの指として検知する。
        var jointedHand =HandJointUtils.FindHand(handType);
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm,out MixedRealityPose PalmPose))
        {
            MixedRealityPose PinkyTipPose,
                PinkyDistalPose,
                PinkyMiddlePose,
                PinkyKnucklePose,
                RingTipPose,
                RingDistalPose,
                RingMiddlePose,
                RingKnucklePose;
            if (jointedHand.TryGetJoint(TrackedHandJoint.RingTip,out RingTipPose)&& jointedHand.TryGetJoint(TrackedHandJoint.RingMiddleJoint ,out RingMiddlePose)&&jointedHand.TryGetJoint(TrackedHandJoint.RingDistalJoint ,out RingDistalPose)
                && jointedHand.TryGetJoint(TrackedHandJoint.RingKnuckle ,out RingKnucklePose)) 
            {
                Vector3 finger1 = RingKnucklePose.Position - PalmPose.Position;
                Vector3 finger2 = RingMiddlePose.Position - RingKnucklePose.Position;
                Vector3 finger3 = RingDistalPose.Position - RingMiddlePose.Position;
                Vector3 finger4 = RingTipPose.Position - RingDistalPose.Position;
                
                float c = Vector3.Angle(PalmPose.Position, finger1);
                float d = Vector3.Angle(finger1, finger2);
                float e = Vector3.Angle(finger2, finger3);
                float f = Vector3.Angle(finger3, finger4);

                float aba = (Mathf.Abs(d) + Mathf.Abs(e) + Mathf.Abs(f)) / 3;

                if (aba < ringAndPinkyFingerThreshpld)
                {
                    return true;
                }
            }

        }

        return false;
    }

    private bool ThumbDetected()
    {
        //※メモ　親指は他の指と異なり特殊でまっすぐ伸びているか？の判定＋親指の向きで判定する。
        var jointedHand = HandJointUtils.FindHand(handType);
        //Get TargetHamd
        if (jointedHand.TryGetJoint(TrackedHandJoint.Palm, out MixedRealityPose PalmPose))
        {
            MixedRealityPose ThumbTipPose, ThumbDistalPose, ThumbProximalPose;
            //Get finger Joint Deta
            if (jointedHand.TryGetJoint(TrackedHandJoint.ThumbTip, out ThumbTipPose) &&
                jointedHand.TryGetJoint(TrackedHandJoint.ThumbDistalJoint, out ThumbDistalPose) &&
                jointedHand.TryGetJoint(TrackedHandJoint.ThumbProximalJoint ,out ThumbProximalPose))
            {
                Vector3 finger1 = ThumbProximalPose.Position - PalmPose.Position;
                Vector3 finger2 = ThumbDistalPose.Position - ThumbProximalPose.Position;
                Vector3 finger3 = ThumbTipPose.Position - ThumbProximalPose.Position;

                float c = Vector3.Angle(PalmPose.Position, finger1);
                float d = Vector3.Angle(finger1, finger2);
                float e = Vector3.Angle(finger2, finger3);

                float aba = (Mathf.Abs(d) + Mathf.Abs(e)/ 2);

                if (aba < thumbThreshold)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void Reset()
    {

        if (this.GetComponent<SolverHandler>() != null)
        {
            handType = this.GetComponent<SolverHandler>().TrackedHandness;
        }
        
    }
}
