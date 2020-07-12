using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePoint : MonoBehaviour
{
    public GameObject refToPointObject;
    public GameObject refToSimRoot;

    public Vector3 creationPosition = new Vector3(0, 4.5f, 0);

    public void OnClick() 
    {
        GameObject newCreatedPointObject = Instantiate(refToPointObject, creationPosition, Quaternion.identity);
        newCreatedPointObject.transform.parent = refToSimRoot.transform;
    }
}
