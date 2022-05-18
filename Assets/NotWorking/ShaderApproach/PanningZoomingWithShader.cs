using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningZoomingWithShader : MonoBehaviour
{
    public Material panningMaterial;
    void Start()
    {
        panningMaterial.SetVector("_Zoom", Vector2.one);
        panningMaterial.SetVector("_Pan", Vector2.zero);
        panningMaterial.SetVector("_Pivot", Vector2.zero);
    }

    // Update is called once per frame
    float scaleAmount = 1.0f;
    Vector2 lastMousePosition = Vector2.zero;
    Vector2 pan = Vector2.zero;


    //The issue with this is that the mouse offset begins to shake the camera when zoomed in.
    //It looks fine when the mouse and canvas aren't moving though.


    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float scaleDelta = Input.mouseScrollDelta.y * Time.deltaTime * 10.0f;
        scaleAmount += scaleDelta;

        if(Input.GetMouseButton(0)){
            Vector2 positionDelta = lastMousePosition - mousePos;
            pan += positionDelta;
        }

        Vector2 pivot = mousePos; 
        Vector2 scale  = Vector2.one * scaleAmount; 

        panningMaterial.SetVector("_Zoom", scale);
        panningMaterial.SetVector("_Pan", pan);
        panningMaterial.SetVector("_Pivot", pivot);

        if(Input.GetMouseButtonDown(1)){
            scaleAmount = 1.0f;
            pan = Vector2.zero;
        }
        lastMousePosition = mousePos;
    }
}
