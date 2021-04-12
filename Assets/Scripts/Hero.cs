using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    static public Hero S;
    [Header("Set in Inspector")]
    // These fields control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public GameObject explosion;
    public float projectileSpeed = 40;
    public Weapon[] weapons;
    public Text score;


    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject lastTriggerGo = null;
    // Declare a new delegate type WeaponFireDelegate
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    public int goal;


    void Start()
    {
        S = this;

        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);
        PauseMenu.complete = false;
        Enemy_0.goalProgress = 0;

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            goal = 25;
        } else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            goal = 30;
        } else if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            goal = 35;
        } else if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            goal = 10000;
        }

    }
    void Update()
    {
        // Pull in information from the Input class
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);
        // Allow the ship to fire
        if (Input.GetAxis("Jump") == 1 && fireDelegate != null)
        {
            fireDelegate();
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            score.text = "Score : " + Enemy_0.totalScore;
        } else
        {
            score.text = "Score : " + Enemy_0.totalScore + "\\" + (goal*100);
            //goalProgress.text = "Goal : " + Enemy_0.goalProgress + "\\" + goal;
             if (Enemy_0.goalProgress >= goal) 
            {
                 PauseMenu.complete = true;
             }
        }

        
        
    }

    void OnTriggerEnter(Collider other)
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        if (go == lastTriggerGo)
        { 
            return;
        }
        lastTriggerGo = go; 
        if (go.tag == "Enemy")
        {
            shieldLevel--;
            Destroy(go);
            var cloneExplosion=Instantiate(explosion, go.transform.position, Quaternion.identity);
            Destroy(cloneExplosion, 2f);
        }
        else if (go.tag == "PowerUp")
        {
            AbsorbPowerUp(go);
        }
        else
        {
            print("Triggered by non-Enemy: " + go.name);
        }
    }


    public void AbsorbPowerUp(GameObject go)
    {
        PowerUp pu = go.GetComponent<PowerUp>();
        switch (pu.type)
        {

            case WeaponType.shield: 
                shieldLevel++;
                break;
            default: 
                if (pu.type == weapons[0].type)
                { // If it is the same type 
                    Weapon w = GetEmptyWeaponSlot();
                    if (w != null)
                    {
                        // Set it to pu.type
                        w.SetType(pu.type);
                    }
                }
                else
                { // If this is a different weapon type 
                    ClearWeapons();
                    weapons[0].SetType(pu.type);
                }
                break;
        }
                pu.AbsorbedBy(this.gameObject);
   }


    public float shieldLevel
    {
        get
        {
            return (_shieldLevel); 
        }
        set
        {
            _shieldLevel = Mathf.Min(value, 4); 

            // If the shield is going to be set to less than zero
            if (value < 0)
            {
                Destroy(this.gameObject);
                var cloneExplosion=Instantiate(explosion, gameObject.transform.position, Quaternion.identity);
                Destroy(cloneExplosion, 2f);
                // Tell Main.S to restart the game after a delay
                Main.S.DelayedRestart(gameRestartDelay);
            }
        }
    }

    Weapon GetEmptyWeaponSlot()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].type == WeaponType.none)
            {
                return (weapons[i]);
            }
        }
        return (null);
    }
    void ClearWeapons()
    {
        foreach (Weapon w in weapons)
        {
            w.SetType(WeaponType.none);
        }
    }
}
