using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseObject : MonoBehaviour
{
    public List<GameObject> tiedObjects = new List<GameObject>();

    public bool hasSwitch;

    public bool hasPower;

    public GameObject lights;

    public enum objectType 
    {
        SwitchOnly,
        SwitchAndPower,
        PowerOnly,
        None

        
    }

    public objectType TipObjekta;

    // Start is called before the first frame update
    void Start()
    {
        if (TipObjekta == objectType.SwitchOnly || TipObjekta == objectType.SwitchAndPower)
        {
            lights = transform.Find("Lights").gameObject;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
