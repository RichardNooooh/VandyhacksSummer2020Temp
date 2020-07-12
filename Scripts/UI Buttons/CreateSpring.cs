using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateSpring : MonoBehaviour
{
    public GameObject refToSpringObject;
    public GameObject refToInputFieldHooke;
    public GameObject refToInputFieldLength;
    public GameObject refToSimRoot;

    public Vector3 creationPosition = new Vector3(4, -5, 0);

    public void OnClick()
    {
        GameObject newCreatedSpringObject = Instantiate(refToSpringObject, creationPosition, Quaternion.identity);
        float givenHooke = float.Parse(refToInputFieldHooke.GetComponent<Text>().text);
        float givenLength = float.Parse(refToInputFieldLength.GetComponent<Text>().text);


        newCreatedSpringObject.GetComponent<SpringForce>().springConstant = givenHooke;
        newCreatedSpringObject.GetComponent<SpringForce>().equilibriumLength = givenLength;
        newCreatedSpringObject.transform.parent = refToSimRoot.transform;


        UnityEngine.Debug.Log("Created Spring with the Equilibrium Length of : " + givenLength + "\n and Hooke Constant of : " + givenHooke);
    }
}