using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    //the objects the light switch is tied to
    public List<GameObject> tiedObjects = new List<GameObject>();

    //lights of all objects
    public List<HouseObject> lights = new List<HouseObject>();

    //shorthand for visual change
    public Transform switchToRotate;

    AudioSource audioSource;

    //is the light switch on or not
    private bool toggled;

    // Start is called before the first frame update
    void Start()
    {
        toggled = false;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void SetTiedObject(GameObject game)
    {
        //add object to switch list
        tiedObjects.Add(game);

        HouseObject houseObject = game.GetComponent<HouseObject>();

        houseObject.tiedObjects.Add(gameObject);

        //Add all lights of object to lights list

        lights.Add(houseObject);

        //toggle option in script

        houseObject.hasSwitch = true;
    }

    public void RemoveTiedObject(GameObject game)
    {
        //turn off all lights
        if (toggled)
        {
            ToggleSwitch();
        }

        HouseObject houseObject = game.GetComponent<HouseObject>();

        houseObject.tiedObjects.Remove(gameObject);

        //remove lights of object
        lights.Remove(houseObject);

        //Remove object from switch list
        tiedObjects.Remove(game);

        houseObject.hasSwitch = false;
        
    }

    public void ToggleSwitch()
    {

        if (!toggled)
        {
            toggled = true;
            //play higher pitched sound for on
            audioSource.pitch = 1.25f;
            audioSource.Play();

            //turn on lights for object
            if (tiedObjects.Count > 0)
            {
                foreach(HouseObject lightObject in lights) 
                {
                    //check if object has power
                    if (lightObject.hasPower == true)
                    {
                        lightObject.lights.SetActive(true);
                    }
                    
                }
            }

            //rotate switch
            switchToRotate.rotation = Quaternion.Euler(new Vector3(10, 0, 0));
        }
        else if (toggled)
        {
            toggled = false;
            //play lower pitched sound for off
            audioSource.pitch = 1f;
            audioSource.Play();

            //turn on lights for object
            if (tiedObjects.Count > 0)
            {
                foreach(HouseObject lightObject in lights) 
                {
                    lightObject.lights.SetActive(false);
                }
            }

            //rotate switch
            switchToRotate.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

    }

}
