using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Utils : MonoBehaviour
{
// Returns a list of all Materials on this GameObject and its children
    static public Material[] GetAllMaterials(GameObject go )
    { 
        Renderer[] rends = go.GetComponentsInChildren<Renderer>(); 
        List<Material> mats = new List<Material>();
        foreach (Renderer rend in rends)
        { // c
            mats.Add(rend.material);
        }
        return (mats.ToArray()); 
    }
}
