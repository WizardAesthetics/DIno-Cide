using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBack : MonoBehaviour
{
    public float Bspeed;
    public Renderer Brend;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD


=======
        
>>>>>>> origin/BlakesBranch
    }

    // Update is called once per frame
    void Update()
    {
        Brend.material.mainTextureOffset += new Vector2(0f, Bspeed * Time.deltaTime);
    }
}
