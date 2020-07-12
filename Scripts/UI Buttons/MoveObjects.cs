using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class MoveObjects : MonoBehaviour
{
    //2D version (adapted from https://www.youtube.com/watch?v=eUWmiV4jRgU)
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;

    private static bool isSelecting = false;
    private static GameObject selectedGameObject = null;

    public static GameObject selectionMenuObject;

    public static bool[] indicesOfInactiveMenuObjects;

    void Start() 
    {
        selectionMenuObject = GameObject.FindWithTag("UI-SelectionMenu");
        indicesOfInactiveMenuObjects = new bool[4];
    }

    void Update()
    {
        if (isBeingHeld == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, 0);
        }
    }

    private void OnMouseDown()
    {
        //Left mouse click selection
        if (Input.GetMouseButtonDown(0))
        {
            if (!isSelecting)
            {
                UnityEngine.Debug.Log("Left mouse click is pressed");
                Vector3 mousePos;
                mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(mousePos);

                startPosX = mousePos.x - this.transform.localPosition.x;
                startPosY = mousePos.y - this.transform.localPosition.y;

                isBeingHeld = true;
            }
            else //choosing game object TODO
            { }
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    private void OnMouseOver() 
    {
        //Right mouse click selection
        if (Input.GetMouseButtonDown(1))
        {

            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //this.gameObject
            UnityEngine.Debug.Log("Right mouse click is pressed and this object: \n" + this.gameObject + " is selected.");

            var thisGameObject = this.gameObject;
            if (thisGameObject.CompareTag("External Forces"))
                ConfigureExternalForcesMenu();
            else if (thisGameObject.CompareTag("Mass Cube"))
                ConfigureMassCubeMenu();
            else if (thisGameObject.CompareTag("Spring"))
                ConfigureSpringMenu();

        }
    }

    //only 1 value, and the 2 connectors
    private void ConfigureExternalForcesMenu() 
    {
        selectionMenuObject.SetActive(true);
        Transform selectionMenuTransform = selectionMenuObject.transform;
        
        //Set InputField1 inactive, keep track of it for reactivation
        selectionMenuTransform.GetChild(2).gameObject.SetActive(false); //this is InputField1
        indicesOfInactiveMenuObjects[2] = true;

        //Set placeholder text of input
        GameObject inputField0bject = selectionMenuTransform.GetChild(1);
        GameObject placeholderObject = inputFieldObject.GetChild(0);
        placeholderObject.GetComponent<Text>().text = "Force (N)";

    }

    private void ConfigureMassCubeMenu() 
    {
        selectionMenuObject.SetActive(true);
        Transform selectionMenuTransform = selectionMenuObject.transform;

        //Set InputField1 and ConnectorButtons inactive, keep track of it for reactivation
        selectionMenuTransform.GetChild(2).gameObject.SetActive(false); //InputField1
        selectionMenuTransform.GetChild(3).gameObject.SetActive(false); //ConnectorButton0
        selectionMenuTransform.GetChild(4).gameObject.SetActive(false); //ConnectorButton1
        indicesOfInactiveMenuObjects[2] = true;
        indicesOfInactiveMenuObjects[3] = true;
        indicesOfInactiveMenuObjects[4] = true;

        //Set placeholder text of input
        GameObject inputField0bject = selectionMenuTransform.GetChild(1);
        GameObject placeholderObject = inputFieldObject.GetChild(0);
        placeholderObject.GetComponent<Text>().text = "Mass (kg)";
    }


    private void ConfigureSpringMenu() 
    {
        selectionMenuObject.SetActive(true);
    }
}