using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGameLogger : MonoBehaviour
{
    // Skripta koja spawna popupove na donjem desnom dijelu ekrana

    public Transform background;

    public Transform startPos;

    public GameObject popupPrefab;

    AudioSource source;
    void Start()
    {
        source = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPopup(string tekst) {
        //spawnoje

        GameObject newPopup = Instantiate(popupPrefab, background, false);

        newPopup.GetComponent<TMP_Text>().text = tekst;

        newPopup.transform.position = startPos.position;

        source.Play();




    }
}
