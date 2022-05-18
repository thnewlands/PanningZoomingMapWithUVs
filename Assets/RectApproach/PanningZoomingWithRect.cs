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
    Vector2 lastMousePosition = Vector2.zero;
    Rect canvasOffset = new Rect(0,0,1,1); 

    float minScale = 0.01f;
    float maxScale = 5.0f;

    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float normalizedScale = Mathf.InverseLerp(minScale,maxScale, canvasOffset.size.x); //TODO: Handle multiple axis
        Vector2 scaleFactor = Mathf.Pow(Mathf.Clamp(normalizedScale, .01f, .25f), 1.25f) * Vector2.one; //Zoom slower when we get closer

        Vector2 scaleDelta = Input.mouseScrollDelta.y *  scaleFactor;
        Vector2 positionDelta = Vector2.zero;

        Vector2 clampedSize = canvasOffset.size + scaleDelta;
        clampedSize = new Vector2(
            Mathf.Clamp(clampedSize.x, minScale, maxScale),
            Mathf.Clamp(clampedSize.y, minScale, maxScale)
        );
        scaleDelta = clampedSize - canvasOffset.size;

        if(Input.GetMouseButton(0)){
            positionDelta = lastMousePosition - mousePos;
        }

        /*
        //Idea for clamping delta -- doesn't work
        Vector2 clampedPosition = canvasOffset.position;
        clampedPosition -= mousePos * scaleDelta;// Scale from mouse position
        clampedPosition += positionDelta * clampedSize;
        clampedPosition = new Vector2(
            Mathf.Clamp(clampedPosition.x, -1.0f, 0.0f),
            Mathf.Clamp(clampedPosition.y, -1.0f, 0.0f)
        );
        positionDelta = clampedPosition - canvasOffset.position;
        */

        //This is really the heart of the technique:
        canvasOffset.size += scaleDelta;//This scales from the top left corner -- the corner now needs to be offset
        canvasOffset.position -= mousePos * scaleDelta;// Scale from mouse position
        canvasOffset.position += positionDelta * canvasOffset.size;

        panningMaterial.SetTextureOffset("_MainTex", canvasOffset.position);
        panningMaterial.SetTextureScale("_MainTex", canvasOffset.size);

        if(Input.GetMouseButtonDown(1)){
            canvasOffset.size = Vector2.one;
            canvasOffset.position = Vector2.zero;
        }
        lastMousePosition = mousePos;
    }
}
