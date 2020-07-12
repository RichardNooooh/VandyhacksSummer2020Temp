using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DiscardSimButtonManager : MonoBehaviour
{
    public GameObject root;

    public void OnClick() 
    {
        UnityEngine.Debug.Log("Removing all items in simulation and pausing sim.");
        foreach (Transform child in root.transform) 
            Destroy(child.gameObject);

        Time.timeScale = 0;
    }
}
