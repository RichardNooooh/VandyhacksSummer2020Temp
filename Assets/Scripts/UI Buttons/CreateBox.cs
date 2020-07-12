using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject refToBoxObject; 

    public void OnClick() 
    {
        
        Instantiate(refToBoxObject, Vector3.zero, Quaternion.identity);

    }
}