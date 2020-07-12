using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
    private static GameObject selectedGameObject = null; // for connecting

    public static GameObject selectionMenuObject = null;
    public static ToFromSelection currentConnectionSelection;
    

    public enum ToFromSelection 
    {
        NOT_SELECTED = 0,
        TO = 1,
        FROM = 2
    }


    void Start()
    {
        if (selectionMenuObject == null)
        {
            selectionMenuObject = GameObject.FindWithTag("UI-SelectionMenu");
            UnityEngine.Debug.Log("Obtained selectionMenuObject: " + selectionMenuObject);
            selectionMenuObject.SetActive(false);
        }

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

    //Used for moving objects and selecting connections
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
            else
            {
                if (currentConnectionSelection == ToFromSelection.TO)
                    selectedGameObject.GetComponent<AbstractSimForce>().o1 = this.gameObject;
                else if (currentConnectionSelection == ToFromSelection.FROM)
                    selectedGameObject.GetComponent<AbstractSimForce>().o2 = this.gameObject;

            }
            //reset menu and variables TODO
            isSelecting = false;
            selectedGameObject = null;
            currentConnectionSelection = ToFromSelection.NOT_SELECTED;

            foreach (Transform selectionMenuItem in selectionMenuObject.transform)
            {
                GameObject childGameObject = selectionMenuItem.gameObject;
                childGameObject.SetActive(true);
                if (childGameObject.CompareTag("UI-SelectionMenuInputField"))
                {
                    Transform placeHolderTransform = selectionMenuItem.GetChild(0);
                    if (!placeHolderTransform.gameObject.CompareTag("UI-SelectionMenuInputPlaceholder"))
                        placeHolderTransform = selectionMenuItem.GetChild(1);
                    
                    placeHolderTransform.GetComponent<Text>().text = "reset";
                }
            }
            selectionMenuObject.SetActive(false);
        }
    }

    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    //Used right clicking configuration selection menu
    private void OnMouseOver()
    {
        //Right mouse click selection
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 gameWorldMousePos = Camera.main.ScreenToWorldPoint(mousePos);

            //this.gameObject
            UnityEngine.Debug.Log("Right mouse click is pressed and this object: \n" + this.gameObject + " is selected.");
            
            selectionMenuObject.SetActive(true);
            selectionMenuObject.transform.position = mousePos;

            UnityEngine.Debug.Log("Is selection menu active?: " + selectionMenuObject.activeSelf);

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
        Transform selectionMenuTransform = selectionMenuObject.transform;

        //Set InputField1 inactive
        selectionMenuTransform.GetChild(2).gameObject.SetActive(false); //this is InputField1

        //Set placeholder text of input
        Transform inputFieldObject0 = selectionMenuTransform.GetChild(1);
        GameObject placeholderObject0 = inputFieldObject0.GetChild(0).gameObject;

        //At creation, the inputfield generates a child GameObject at index 0 called InputField0 Input Caret
        if (!placeholderObject0.CompareTag("UI-SelectionMenuInputPlaceholder"))
            placeholderObject0 = inputFieldObject0.GetChild(1).gameObject;
        placeholderObject0.GetComponent<Text>().text = "Force (N)";

    }

    //only 1 value
    private void ConfigureMassCubeMenu()
    {
        Transform selectionMenuTransform = selectionMenuObject.transform;

        //Set InputField1 and ConnectorButtons inactive, keep track of it for reactivation
        selectionMenuTransform.GetChild(2).gameObject.SetActive(false); //InputField1
        selectionMenuTransform.GetChild(3).gameObject.SetActive(false); //ConnectorButton0
        selectionMenuTransform.GetChild(4).gameObject.SetActive(false); //ConnectorButton1

        //Set placeholder text of input
        Transform inputFieldObject0 = selectionMenuTransform.GetChild(1);
        GameObject placeholderObject0 = inputFieldObject0.GetChild(0).gameObject;

        //At creation, the inputfield generates a child GameObject at index 0 called InputField0 Input Caret
        if (!placeholderObject0.CompareTag("UI-SelectionMenuInputPlaceholder"))
            placeholderObject0 = inputFieldObject0.GetChild(1).gameObject;
        placeholderObject0.GetComponent<Text>().text = "Mass (kg)";
    }

    //all 4 options
    private void ConfigureSpringMenu()
    {
        Transform selectionMenuTransform = selectionMenuObject.transform;

        //Set placeholder text of inputs
        Transform inputFieldObject0 = selectionMenuTransform.GetChild(1);
        GameObject placeholderObject0 = inputFieldObject0.GetChild(0).gameObject;
        
        //At creation, the inputfield generates a child GameObject at index 0 called InputField0 Input Caret
        if (!placeholderObject0.CompareTag("UI-SelectionMenuInputPlaceholder"))
            placeholderObject0 = inputFieldObject0.GetChild(1).gameObject;
        placeholderObject0.GetComponent<Text>().text = "Length (m)";


        Transform inputFieldObject1 = selectionMenuTransform.GetChild(2);
        GameObject placeholderObject1 = inputFieldObject1.GetChild(0).gameObject;
        
        //At creation, the inputfield generates a child GameObject at index 0 called InputField1 Input Caret
        if (!placeholderObject1.CompareTag("UI-SelectionMenuInputPlaceholder"))
            placeholderObject1 = inputFieldObject1.GetChild(1).gameObject;
        placeholderObject1.GetComponent<Text>().text = "Constant (N/m)";

        UnityEngine.Debug.Log("Selected external force object, placeholder objects: " + placeholderObject0 + " and " + placeholderObject1);
    }

    public void ToButtonSelection()
    {
        isSelecting = true;
        selectedGameObject = this.gameObject;
        currentConnectionSelection = ToFromSelection.TO;
    }

    public void FromButtonSelection() 
    {
        isSelecting = true;
        selectedGameObject = this.gameObject;
        currentConnectionSelection = ToFromSelection.FROM;
    }
}