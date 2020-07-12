using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableButtonInputField : MonoBehaviour
{
    public string input;

    public GameObject refToInputField;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnClick() 
    {
        //Stores string
        GameObject simulationInputObject = refToInputField.transform.GetChild(1).gameObject;

        input = simulationInputObject.GetComponent<Text>().text;
        if (string.IsNullOrEmpty(input))
        {
            input = "sim1";
        }

        //Disables inputbar and button
        this.gameObject.SetActive(false);
        refToInputField.SetActive(false);
    }
     
}
