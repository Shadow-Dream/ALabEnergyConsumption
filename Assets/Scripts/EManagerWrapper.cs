using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EManager
{
    public Dictionary<string, ERoom> rooms = new Dictionary<string, ERoom>();
    public List<float> consumptions = new List<float>();
    public static EManager _instance;
    public static EManager instance
    {
        get
        {
            if(_instance==null)_instance = new EManager();
            return _instance;
        }
    }
    
    float lastUpdateTime;
    float deltaConsumption;
    public float updateDelta = 2;

    public void AddRoom(string name,ERoom room)
    {
        rooms[name] = room;
    }

    public void Update()
    {
        if (Time.time - lastUpdateTime > updateDelta)
        {
            lastUpdateTime = Time.time;
            consumptions.Add(deltaConsumption);
            deltaConsumption = 0;
        }
    }

    public void Consume(float consumption)
    {
        deltaConsumption += consumption;
    }
}

public class EManagerWrapper:MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        EManager.instance.Update();
    }
}