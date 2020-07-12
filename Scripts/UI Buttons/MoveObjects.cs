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

    public enum ObjectConnectionType 
    {
        ERROR = 0,
        EXTERNAL_FORCE = 1,
        MASS_CUBE = 2, 
        SPRING = 3
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
            UnityEngine.Debug.Log("isSelecting: " + isSelecting);
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
                {
                    selectedGameObject.GetComponent<AbstractSimForce>().o1 = this.gameObject;
                    UnityEngine.Debug.Log("Connect this object: " + selectedGameObject + " o1 reference to " + this.gameObject);
                }
                else if (currentConnectionSelection == ToFromSelection.FROM)
                {
                    selectedGameObject.GetComponent<AbstractSimForce>().o2 = this.gameObject;
                    UnityEngine.Debug.Log("Connect this object: " + selectedGameObject + " o2 reference to " + this.gameObject);
                }

            }
            //reset menu and variables TODO
            isSelecting = false;
            selectedGameObject = null;
            currentConnectionSelection = ToFromSelection.NOT_SELECTED;

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

            var thisGameObject = this.gameObject;
            selectedGameObject = this.gameObject;

            if (!thisGameObject.CompareTag("Mass Cube"))
            {
                selectionMenuObject.SetActive(true);
                selectionMenuObject.transform.position = mousePos;
            }
            //if (thisGameObject.CompareTag("External Forces"))
            //    ConfigureExternalForcesMenu();
            //else if (thisGameObject.CompareTag("Mass Cube"))
            //    ConfigureMassCubeMenu();
            //else if (thisGameObject.CompareTag("Spring"))
            //    ConfigureSpringMenu();

        }
    }

    public void ToButtonSelection()
    {
        UnityEngine.Debug.Log("To button is pressed");
        isSelecting = true;
        //selectedGameObject = this.gameObject;
        currentConnectionSelection = ToFromSelection.TO;
    }

    public void FromButtonSelection() 
    {
        UnityEngine.Debug.Log("From button is pressed");
        isSelecting = true;
        //selectedGameObject = this.gameObject;
        currentConnectionSelection = ToFromSelection.FROM;
    }
}