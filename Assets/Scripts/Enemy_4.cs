using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : MonoBehaviour
{
<<<<<<< Updated upstream
    //Javent wrote this code yet BLAKE JOHNSON
=======
    // These three fields need to be defined in the Inspector pane
    public string name; // The name of this part
    public float health; // The amount of health this part has
    public string[] protectedBy; // The other parts that protect this
                                 // These two fields are set automatically in Start().
                                 // Caching like this makes it faster and easier to find these later
    [HideInInspector] // Makes field on the next line not appear in the Inspector
    public GameObject go; // The GameObject of this part
    [HideInInspector]
    public Material mat; // The Material to show damage
}
public class Enemy_4 : Enemy_0
{
    [Header("Set in Inspector: Enemy_4")] // a
    public Part[] parts; // The array of ship Parts
    private Vector3 p0, p1; // The two points to interpolate
    private float timeStart; // Birth time for this Enemy_4
    private float duration = 4; // Duration of movement
    void Start()
    {
        // There is already an initial position chosen by Main.SpawnEnemy()
        // so add it to points as the initial p0 & p1
        p0 = p1 = pos;
        InitMovement();
        // Cache GameObject & Material of each Part in parts
        Transform t;
        foreach (Part prt in parts)
        {
            t = transform.Find(prt.name);
            if (t != null)
            {
                prt.go = t.gameObject;
                prt.mat = prt.go.GetComponent<Renderer>().material;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            health = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            health = 5;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            health = 6;
        }
        else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            health = 8;
        }
    }

>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
