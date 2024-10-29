using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(Collider))]
public class MoveWithMouse : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
{

   
    
   
    public float movementSpeed = 4.0f;
    float xValue = 0;
    float yValue = 0;
    bool enabled = false;


   
    
    
    void Awake()
    {
        transform.localPosition = Vector3.zero;
        transform.parent = null;
        this.enabled = false;
    }

    private void OnEnable()
    {
        transform.SetParent(Camera.main.transform);
    }

   



    void Update()
    {
        if(Input.GetMouseButtonUp(0))
        {
            transform.SetParent(null);
            
            Cursor.visible = true;
            enabled = false;
        }
        if (Input.GetMouseButtonUp(1))
        {
            
            Cursor.visible = false;
            enabled = true;
        }

        if (enabled) {

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);

            //FIXIT: THESE VALUES (-0.15 AND 0.85) NEED TO BE FIXED.

            bool xCage = viewPos.x > -0.15f && viewPos.x < 0.85f;
            bool yCage = viewPos.y > -0.20f && viewPos.y < 0.80f;

            // Check if the object is beyond the left or right boundaries
            bool beyondLeftBoundary = viewPos.x <= -0.15f && Input.GetAxis("Mouse X") > 0; // Allow right movement
            bool beyondRightBoundary = viewPos.x >= 0.85f && Input.GetAxis("Mouse X") < 0; // Allow left movement

            // Check if the object is beyond the bottom or top boundaries
            bool beyondBottomBoundary = viewPos.y <= -0.20f && Input.GetAxis("Mouse Y") > 0; // Allow upward movement
            bool beyondTopBoundary = viewPos.y >= 0.80f && Input.GetAxis("Mouse Y") < 0; // Allow downward movement

            xValue = Input.GetAxis("Mouse X") * movementSpeed * Time.deltaTime * Convert.ToInt32(xCage || beyondLeftBoundary || beyondRightBoundary);
            yValue = Input.GetAxis("Mouse Y") * movementSpeed * Time.deltaTime * Convert.ToInt32(yCage || beyondBottomBoundary || beyondTopBoundary);







            //INFO : SelectedObjectZ axis has x and z buttons 
            transform.localPosition += new Vector3(xValue, yValue,
                Input.GetAxis("SelectedObjectZ") * movementSpeed * Time.deltaTime);
            //FIXIT: CONSTRAINT Z AXIS ALSO.

        }




    }





   
 

}
