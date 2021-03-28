using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_0 : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; // The speed in m/s
    public float fireRate = 0.3f; // Seconds/shot (Unused)
    public float health = 10;
    public int score = 100; // Points earned for destroying this
    protected BoundsCheck bndCheck;


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos
    { 
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    void Update()
    {
        Move();
        if (bndCheck != null && bndCheck.offDown)
        { 
            Destroy(gameObject); 
        }
    }
    public virtual void Move()
    { 
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll)
    { // a
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero": // b
                ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
                // If this Enemy is off screen, don't damage it.
                if (!bndCheck.isOnScreen)
                { // c
                    Destroy(otherGO);
                    break;
                }
                // Hurt this Enemy
                // Get the damage amount from the Main WEAP_DICT.
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                { // d
                  // Destroy this Enemy
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;

                default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name); // f
                break;
        }
    }
}