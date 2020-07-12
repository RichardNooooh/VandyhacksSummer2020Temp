using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateBox : MonoBehaviour
{
    public GameObject refToBoxObject;
    public GameObject refToInputField;

    public void OnClick()
    {
        GameObject newCreatedBox = Instantiate(refToBoxObject, Vector3.zero, Quaternion.identity);
        float givenMass = float.Parse(refToInputField.GetComponent<Text>().text);
        newCreatedBox.GetComponent<Rigidbody2D>().mass = givenMass;

        UnityEngine.Debug.Log("Created box of mass: " + givenMass);

    }
}