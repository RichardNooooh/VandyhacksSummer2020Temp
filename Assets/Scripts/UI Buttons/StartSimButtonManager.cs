using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class StartSimButtonManager : MonoBehaviour
{
    void Awake() 
    {
        Time.timeScale = 0;
    }

    public void OnClick() 
    {
        UnityEngine.Debug.Log("Called start sim()");
        Time.timeScale = 1;
    }
}
