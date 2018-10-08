using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GlobalInfoBar : MonoBehaviour {

    [SerializeField] Text m_gemAmount;
    [SerializeField] Text m_goldAmount;
    [SerializeField] Text m_batteryMeter;

    Image m_batteryFill;

    // Use this for initialization
    void Start () {
        m_batteryFill = m_batteryMeter.GetComponentInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if (SystemInfo.batteryLevel == -1)
            m_batteryMeter.text = "N/A";
        else
        {
            m_batteryMeter.text = ((int)(SystemInfo.batteryLevel * 100)).ToString() + (SystemInfo.batteryLevel == 1f ? "":"%");
            m_batteryFill.fillAmount = SystemInfo.batteryLevel;
        }
	}
}
