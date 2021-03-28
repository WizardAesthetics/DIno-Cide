using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour
{
    //Vairable that you need to set
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true;

    //Variables that you show in inspecteor but dont need to set
    [Header("Set Dynamically")]
    public bool isOnScreen = true;
    public float camWidth;
    public float camHeight;

    //Variables you dont need to see at all
    [HideInInspector]
    public bool offRight, offLeft, offUp, offDown;

    //Awake this is called before start
    void Awake()
    {
        camHeight = Camera.main.orthographicSize;  //Finds the caerma Hight
        camWidth = camHeight * Camera.main.aspect; //findes the cmaera with
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position; //finds current position of this object
        isOnScreen = true;
        offRight = offLeft = offUp = offDown = false; 

        //check to see if the object is with in the map bounderies
        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true; 
        }
        if (pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true; 
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true; 
        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true; 
        }

        isOnScreen = !(offRight || offLeft || offUp || offDown);
        if (keepOnScreen && !isOnScreen)
        {
            transform.position = pos;
            isOnScreen = true;
            offRight = offLeft = offUp = offDown = false; 
        }
    }

    // Draw the bounds in the Scene pane using OnDrawGizmos()
    void OnDrawGizmos()
    { 
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
