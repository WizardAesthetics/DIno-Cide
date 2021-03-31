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
    public float showDamageDuration = 0.1f; // # seconds to show damage 
    public float powerUpDropChance = 1f;

    [Header("Set Dynamically: Enemy")]
    public Color[] originalColors;
    public Material[] materials;// All the Materials of this & its children
    public bool showingDamage = false;
    public float damageDoneTime; // Time to stop showing damage
    public bool notifiedOfDestruction = false; // Will be used later

    protected BoundsCheck bndCheck;

    public GameObject explosion;


    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        // Get materials and colors for this GameObject and its children
        materials = Utils.GetAllMaterials(gameObject); 
        originalColors = new Color[materials.Length];
        for (int i = 0; i < materials.Length; i++)
        {
            originalColors[i] = materials[i].color;
        }
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
        //costansly looking if damage was waken
        if (showingDamage && Time.time > damageDoneTime)
        { 
            UnShowDamage();
        }
        //chekcing if its off the screen
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
    { 
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero": 
                ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
                // If this Enemy is off screen destory it
                if (!bndCheck.isOnScreen)
                { 
                    Destroy(otherGO);
                    break;
                }
                ShowDamage();
                // digures out how much damaged was caused
                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                { 
                  // Destroy this Enemy
                    if (!notifiedOfDestruction)
                    {
                        Main.S.shipDestroyed(this);
                        Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                    }
                    notifiedOfDestruction = true;
                    // Destroy this Enemy
                    Destroy(this.gameObject);

                }
                Destroy(otherGO);
                break;
                default:
                break;
        }
    }

    //Method that souw damage when enemy_0 is hit
    void ShowDamage()
    { 
        foreach (Material m in materials)
        {
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    //turns the red off and give it the illusionof slashing
    void UnShowDamage()
    { 
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].color = originalColors[i];
        }
        showingDamage = false;
    }
}