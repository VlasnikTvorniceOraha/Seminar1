using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSocket : MonoBehaviour
{

    public List<GameObject> tiedObjects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SetTiedObject(GameObject game)
    {
        //add object to switch list
        tiedObjects.Add(game);

        HouseObject houseObject = game.GetComponent<HouseObject>();

        houseObject.tiedObjects.Add(gameObject);

        //toggle option in script

        houseObject.hasPower = true;
    }

    public void RemoveTiedObject(GameObject game)
    {
        
        //Remove object from switch list
        tiedObjects.Remove(game);

        HouseObject houseObject = game.GetComponent<HouseObject>();

        houseObject.tiedObjects.Remove(gameObject);

        houseObject.hasPower = false;
        
    }
}
