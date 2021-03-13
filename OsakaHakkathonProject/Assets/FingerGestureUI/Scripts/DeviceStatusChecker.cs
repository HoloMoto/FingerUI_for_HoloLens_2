using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using TMPro;



[RequireComponent(typeof(GridObjectCollection))]
public class DeviceStatusChecker : MonoBehaviour
{
    private GridObjectCollection goc;
    [SerializeField,Header("Battery")] [CanBeNull] TextMeshPro batteryText;
    public int battery;
        
    [SerializeField]
    private GameObject[] batteryLevelObjects;
    [SerializeField]
    TextMeshPro _dayText;
    [SerializeField]
    TextMeshPro _timeText;

    
    void Start()
    {
      BatteryStatusCheck();   
      
    }

    private void Update()
    {
        _dayText.text = (DateTime.Now.Month.ToString() +"/"+DateTime.Now.Day.ToString());
        _timeText.text = (DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString()+":"+DateTime.Now.Second.ToString());
    }

    public void BatteryStatusCheck()
    {
        float batt = SystemInfo.batteryLevel * 100f;
        //debugText
      //  batteryText.text = batt.ToString();
      //Debug.Log(batt);
        if (batt>90)
        {
            battery = 5;
            batteryLevelObjects[0].SetActive(true); batteryLevelObjects[1].SetActive(true); batteryLevelObjects[2].SetActive(true); batteryLevelObjects[3].SetActive(true); batteryLevelObjects[4].SetActive(true);
        }
        if (90>batt&&batt>80)
        {
            battery = 4;
            batteryLevelObjects[0].SetActive(true); batteryLevelObjects[1].SetActive(true); batteryLevelObjects[2].SetActive(true); batteryLevelObjects[3].SetActive(true); batteryLevelObjects[4].SetActive(false);
        }
        if (80>batt&&batt>50)
        {
            battery = 3;
            batteryLevelObjects[0].SetActive(true); batteryLevelObjects[1].SetActive(true); batteryLevelObjects[2].SetActive(true); batteryLevelObjects[3].SetActive(false); batteryLevelObjects[4].SetActive(false);
        }
        if (50>batt&&batt>20)
        {
            battery = 2;
            batteryLevelObjects[0].SetActive(true);batteryLevelObjects[1].SetActive(true);batteryLevelObjects[2].SetActive(false); batteryLevelObjects[3].SetActive(false); batteryLevelObjects[4].SetActive(false);
        }
        if (20>batt&&batt > 0)
        {
            battery = 1;
            batteryLevelObjects[0].SetActive(true);batteryLevelObjects[1].SetActive(false); batteryLevelObjects[2].SetActive(false); batteryLevelObjects[3].SetActive(false); batteryLevelObjects[4].SetActive(false);
          //  Material mat = batteryLevelObjects[0].GetComponent<Material>();
           // mat.color = Color.red;
        }
    }
}
