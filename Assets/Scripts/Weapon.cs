using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    none, // The default / no weapon
    blaster, // A simple blaster
    spread, // Two shots simultaneously
    shield // Raise shieldLevel
}

[System.Serializable] 
public class WeaponDefinition
{
    public WeaponType type = WeaponType.none;
    public string letter; // Letter to show on the power-up
    public Color color = Color.white; // Color of Collar & power-up
    public GameObject projectilePrefab; // Prefab for projectiles
    public Color projectileColor = Color.white;
    public float damageOnHit = 0; // Amount of damage caused
    public float continuousDamage = 0; // Damage per second (Laser)
    public float delayBetweenShots = 0;
    public float velocity = 20;
}
public class Weapon : MonoBehaviour
{
    static public Transform PROJECTILE_ANCHOR;
    [Header("Set Dynamically")]
    [SerializeField]
    private WeaponType _type = WeaponType.none;
    public WeaponDefinition def;
    public GameObject collar;
    public float lastShotTime; // Time last shot was fired
    private Renderer collarRend;
    void Start()
    {
        collar = transform.Find("Collar").gameObject;
        collarRend = collar.GetComponent<Renderer>();
        // Call SetType() for the default _type of WeaponType.none
        SetType(_type);

         // Dynamically create an anchor for all Projectiles
        if (PROJECTILE_ANCHOR == null)
        { 
            GameObject go = new GameObject("_ProjectileAnchor");
            PROJECTILE_ANCHOR = go.transform;
        }
        // Find the fireDelegate of the root GameObject
        GameObject rootGO = transform.root.gameObject; 
        if (rootGO.GetComponent<Hero>() != null)
        { 
            rootGO.GetComponent<Hero>().fireDelegate += Fire;
        }
    }
    public WeaponType type
    {
        get { return (_type); }
        set { SetType(value); }
    }
    public void SetType(WeaponType wt)
    {
        _type = wt;
        if (type == WeaponType.none)
        { 
            this.gameObject.SetActive(false);
            return;
        }
        else
        {
            this.gameObject.SetActive(true);
        }
        def = Main.GetWeaponDefinition(_type); 
        collarRend.material.color = def.color;
        lastShotTime = 0; // You can fire immediately after _type is set. 


    }
    public void Fire()
    {
        // If this.gameObject is inactive, return
        if (!gameObject.activeInHierarchy) return; 
        // If it hasn't been enough time between shots, return
        if (Time.time - lastShotTime < def.delayBetweenShots)
        { 
            return;
        }
        ProjectileHero p;
        Vector3 vel = Vector3.up * def.velocity; 
        if (transform.up.y < 0)
        {
            vel.y = -vel.y;
        }
        switch (type)
        { 
            case WeaponType.blaster:
                p = MakeProjectile();
                p.rigid.velocity = vel;
                this.GetComponent<AudioSource>().Play();
                break;
            case WeaponType.spread: 
                p = MakeProjectile(); // Make middle Projectile
                p.rigid.velocity = vel;
                p = MakeProjectile(); // Make right Projectile
                p.transform.rotation = Quaternion.AngleAxis(10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                p = MakeProjectile(); // Make left Projectile
                p.transform.rotation = Quaternion.AngleAxis(-10, Vector3.back);
                p.rigid.velocity = p.transform.rotation * vel;
                this.GetComponent<AudioSource>().Play();
                break;
        }
    }
    public ProjectileHero MakeProjectile()
    { 
        GameObject go = Instantiate<GameObject>(def.projectilePrefab);
        if (transform.parent.gameObject.tag == "Hero")
        { 
            go.tag = "ProjectileHero";
            go.layer = LayerMask.NameToLayer("ProjectileHero");
        }
        else
        {
            go.tag = "ProjectileEnemy";
            go.layer = LayerMask.NameToLayer("ProjectileEnemy");
        }
        go.transform.position = collar.transform.position;
        go.transform.SetParent(PROJECTILE_ANCHOR, true); 
        ProjectileHero p = go.GetComponent<ProjectileHero>();
        p.type = type;
        lastShotTime = Time.time; 
        return (p);
    }
}
