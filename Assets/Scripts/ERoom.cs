using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ERoom : MonoBehaviour
{
    public string roomName;
    public Dictionary<string, Dictionary<string, EAppliance>> groups = new Dictionary<string, Dictionary<string, EAppliance>>();
    [System.NonSerialized] public List<float> consumptions = new List<float>();
    float lastUpdateTime;
    float deltaConsumption;
    void Start()
    {
        EManager.instance.AddRoom(roomName, this);
    }

    void Update()
    {
        if (Time.time - lastUpdateTime > EManager.instance.updateDelta)
        {
            lastUpdateTime = Time.time;
            consumptions.Add(deltaConsumption);
            deltaConsumption = 0;
        }
    }

    public void AddAppliance(string type,string name,EAppliance appliance)
    {
        if (!groups.ContainsKey(type))groups.Add(type, new Dictionary<string, EAppliance>());
        groups[type][name] = appliance;
    }

    public void Consume(float consumption)
    {
        deltaConsumption += consumption;
        EManager.instance.Consume(consumption);
    }
}
