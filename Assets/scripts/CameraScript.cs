using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    
    float yaw;
   
    float pitch;

    
    [SerializeField]
    float mouseSensitivity = 3f;
    
    [SerializeField]
    float distanceFromTargetX;
   
    [SerializeField]
    float distanceFromTargetY;

    
    [SerializeField]
    float pitchMin = -40;
    
    [SerializeField]
    float pitchMax = 80;

   
    [SerializeField]
    bool lockCursor;

    
    Vector3 currentRotation;
    
    [SerializeField]
    float rotationSmoothTime = 0.12f;

    
    [SerializeField]
    Vector3 rotationSmoothVelocity;

    [SerializeField]
    Transform target; 

    
    [SerializeField]
    LayerMask cameraLayerMask;

    // Use this for initialization
    void Start()
    {

        //caso lockCursor seja true
        if (lockCursor)
        {
            

            Cursor.lockState = CursorLockMode.Locked;
            
            Cursor.visible = false;
        }


    }

    // Update is called once per frame
    // 
    void LateUpdate()
    {


        
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);

        
        Vector3 targetRotation = new Vector3(pitch, yaw);


        
        currentRotation = Vector3.SmoothDamp(currentRotation, targetRotation, ref rotationSmoothVelocity, rotationSmoothTime);
        
        transform.eulerAngles = currentRotation;

        
        transform.position = target.position - transform.forward * distanceFromTargetX + transform.up * distanceFromTargetY;

        
        CheckWall();


    }

    void CheckWall()
    {
       
        RaycastHit hit;
        
        Vector3 raystart = target.position;
        
        Vector3 dir = (transform.position - target.position).normalized;

        
        float dist = Vector3.Distance(transform.position, target.position);

        
        if (Physics.Raycast(raystart, dir, out hit, dist, cameraLayerMask))
        {

            
            float hitDistance = hit.distance;

           
            Vector3 castCenterHit = target.position + (dir.normalized * hitDistance);
            
            transform.position = castCenterHit;
        }

    }
}


