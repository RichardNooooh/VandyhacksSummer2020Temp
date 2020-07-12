using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateExternalForce : MonoBehaviour
{
    public GameObject refToExternalForceObject;
    public GameObject refToInputField;

    public Vector3 creationPosition = new Vector3(4, -5, 0);

    public void OnClick()
    {
        GameObject newCreatedExternalObject = Instantiate(refToExternalForceObject, creationPosition, Quaternion.identity);
        float givenForceMagnitude = float.Parse(refToInputField.GetComponent<Text>().text);
        UnityEngine.Debug.Log("Created box of mass: " + givenForceMagnitude);
        newCreatedExternalObject.GetComponent<ExternalForce>().forceMagnitude = givenForceMagnitude;

    }
}
