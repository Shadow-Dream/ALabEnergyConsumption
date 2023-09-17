using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ApplianceConsumeUI : MonoBehaviour
{
    public GameObject label;
    public GameObject status;
    public GameObject consume;
    public EAppliance eAppliance;
    public Sprite normalStatus;
    public Sprite warningStatus;

    void Start()
    {
        label.GetComponent<TMPro.TextMeshProUGUI>().text = eAppliance.applianceName;
    }


    void Update()
    {
        if (eAppliance.consumptions.Count > 0)
        {
            consume.GetComponent<TMPro.TextMeshProUGUI>().text = eAppliance.consumptions[eAppliance.consumptions.Count - 1].ToString();
        }
    }
}
