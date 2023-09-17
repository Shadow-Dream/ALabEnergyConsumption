using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EAppliance : MonoBehaviour
{
    public string applianceName;
    public string applianceType;
    public ERoom room;
    [System.NonSerialized] public List<float> consumptions = new List<float>();
    float lastUpdateTime;
    float deltaConsumption;
    void Start()
    {
        room.AddAppliance(applianceType, applianceName, this);
    }

    void Update()
    {
        if(Time.time - lastUpdateTime > EManager.instance.updateDelta)
        {
            lastUpdateTime = Time.time;
            consumptions.Add(deltaConsumption);
            deltaConsumption = 0;
        }
    }

    public void Consume(float consumption)
    {
        deltaConsumption += consumption;
        room.Consume(consumption);
    }
}
