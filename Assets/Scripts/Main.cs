using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S; // A singleton for Main
    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; // Array of Enemy prefabs
    public float enemySpawnPerSecond = 0.5f; // # Enemies/second
    public float enemyDefaultPadding = 1.5f; // Padding for position
    public WeaponDefinition[] weaponDefinitions;
    private BoundsCheck bndCheck;
    static Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;
    void Awake()
    {
        S = this;
        // Set bndCheck to reference the BoundsCheck component on this GameObject
        bndCheck = GetComponent<BoundsCheck>();
        // Invoke SpawnEnemy() once (in 2 seconds, based on default values)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
        // A generic Dictionary with WeaponType as the key
        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>(); // a
        foreach (WeaponDefinition def in weaponDefinitions)
        { // b
            WEAP_DICT[def.type] = def;
        }
    }
    public void SpawnEnemy()
    {
        // Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length); // b
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]); // c
                                                                     // Position the Enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding; // d
        if (go.GetComponent<BoundsCheck>() != null)
        { // e
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        // Set the initial position for the spawned Enemy // f
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPadding;
        float xMax = bndCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPadding;
        go.transform.position = pos;
        // Invoke SpawnEnemy() again
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond); // g
    }

    public void DelayedRestart(float delay)
    {
        // Invoke the Restart() method in delay seconds
        Invoke("Restart", delay);
    }
    public void Restart()
    {
        // Reload _Scene_0 to restart the game
        SceneManager.LoadScene("Level_1");
    }

    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    { // a
      // Check to make sure that the key exists in the Dictionary
      // Attempting to retrieve a key that didn't exist, would throw an error,
      // so the following if statement is important.
        if (WEAP_DICT.ContainsKey(wt))
        { // b

            return (WEAP_DICT[wt]);
        }
        // This returns a new WeaponDefinition with a type of WeaponType.none,
        // which means it has failed to find the right WeaponDefinition
        return (new WeaponDefinition()
        );
    }
}
