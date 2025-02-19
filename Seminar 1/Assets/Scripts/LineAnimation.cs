using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public enum AnimationType
    {
        Growing,
        None
    }

    public AnimationType animationType;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTargets(Transform object1, Transform object2)
    {
        lineRenderer.SetPosition(0, object1.position);

        lineRenderer.SetPosition(1, object2.position);
    }
}
