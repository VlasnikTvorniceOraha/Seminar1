using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrekidacSkripta : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioSource audioSource;

    public GameObject spojeniObjekt; //Stvar za koju je prekidac namijenjen
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver() {

        Debug.Log("Hover");

        if (Input.GetMouseButtonDown(0)) {
            //igrac interaktira sa svijetlom
            audioSource.Play();

            if (spojeniObjekt != null) {
                hendlajObjekt();
            }

        }

    }

    void hendlajObjekt() {

        //odredi koji tip objekta je na prekidacu i odredi sto napraviti s njim

        switch (spojeniObjekt.tag) {

            case "Lampa":
            //upali ili ugasi svijetla lampi
            spojeniObjekt.GetComponent<LampaSkripta>().UpaliUgasiSvijetla();
            break;

            default:
            break;

        }

    }
}
