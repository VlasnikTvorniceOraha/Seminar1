using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Drawing;
using System.Net;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    //popup treba ici prema gore svaki frame

    public Transform endPos;

    public TMP_Text tekst;

    public UnityEngine.UI.Image slika;
    // Start is called before the first frame update
    void Start()
    {
        endPos = GameObject.FindGameObjectWithTag("EndPosUI").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, endPos.position, 0.5f);

        if (this.transform.position.y > endPos.position.y - 50) {
            //fade outaj sliku i tekst
            //Debug.Log("Fadeam");
            float newAlpha = tekst.color.a - 0.01f;
            tekst.color = new UnityEngine.Color(1, 1, 1, newAlpha);
            slika.color = new UnityEngine.Color(1, 1, 1, newAlpha);
            //Debug.Log(tekst.color);
            if (newAlpha < 0f) {
                Destroy(slika.gameObject);
                Destroy(this.gameObject);

            }

        }
        
    }
}
