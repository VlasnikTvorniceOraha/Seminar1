using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampaSkripta : MonoBehaviour
{
    //skripta za lampe

    public GameObject svijetla; //svijetla za iskljuciti/ukljuciti

    public GameObject prekidac; //prekidac za palit/gasit svijetla

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpaliUgasiSvijetla() {

        if (svijetla.activeSelf == false) {
            //upali svijetla
            svijetla.SetActive(true);
        } else {
            //ugasi svijetla
            svijetla.SetActive(false);
        }

    }
}
