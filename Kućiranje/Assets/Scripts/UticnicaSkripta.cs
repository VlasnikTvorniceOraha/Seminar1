using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UticnicaSkripta : MonoBehaviour
{
    //skripta za uticnice

    public GameObject spojeniObjekt;

    public LineRenderer lineRenderer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        SpojiUticnicuIObjekt();
        
        
    }

    void SpojiUticnicuIObjekt() {
        //spoji ih linijom sad, kasnije nesto pametnije

        if (spojeniObjekt) {
            lineRenderer.positionCount = 2;
            Vector3[] positions = new Vector3[2];
            positions[0] = this.transform.position;
            positions[1] = spojeniObjekt.transform.position;

            lineRenderer.SetPositions(positions);
        }

    }

}
