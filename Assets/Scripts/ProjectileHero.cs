using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHero : MonoBehaviour
{

    private BoundsCheck bndCheck;
    private Renderer rend;
    [Header("Set Dynamically")]
    public Rigidbody rigid;
    [SerializeField] // a
    private WeaponType _type; // b

    public WeaponType type
    { // c
        get
        {
            return (_type);
        }
        set
        {
            SetType(value); // c
        }
    }
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
        rend = GetComponent<Renderer>(); // d
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Sets the _type private field and colors this projectile to match the
    /// WeaponDefinition.
    /// </summary>
    /// <param name="eType">The WeaponType to use.</param>
    public void SetType(WeaponType eType)
    { // e
      // Set the _type
        _type = eType;
        WeaponDefinition def = Main.GetWeaponDefinition(_type);
        rend.material.color = def.projectileColor;
    }
}
