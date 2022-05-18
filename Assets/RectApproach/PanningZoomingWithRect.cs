using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanningZoomingWithRect : MonoBehaviour
{
    public Material panningMaterial;
    void Start()
    {
        panningMaterial.SetTextureOffset("_MainTex", Vector2.zero);
        panningMaterial.SetTextureScale("_MainTex", Vector2.one);
    }

    // Update is called once per frame
    float scaleAmount = 1.0f;
    Vector2 lastMousePosition = Vector2.zero;
    Rect canvasOffset = new Rect(0,0,1,1); 

    //The issue with this is that the panning isn't consistent when zoomed in.
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float scaleDelta = Input.mouseScrollDelta.y * Time.deltaTime * 10.0f;
        Vector2 positionDelta = Vector2.zero;

        if(Input.GetMouseButton(0)){
            positionDelta = lastMousePosition - mousePos;
        }
        Vector2 pivot = mousePos;

        canvasOffset.size += scaleDelta * Vector2.one;//This scales from the top left corner -- the corner now needs to be offset
        canvasOffset.position -= mousePos * scaleDelta;// Scale from mouse position
        canvasOffset.position += positionDelta;

        panningMaterial.SetTextureOffset("_MainTex", canvasOffset.position);
        panningMaterial.SetTextureScale("_MainTex", canvasOffset.size);

        if(Input.GetMouseButtonDown(1)){
            canvasOffset.size = Vector2.one;
            canvasOffset.position = Vector2.zero;
        }
        lastMousePosition = mousePos;
    }
}
