using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{


    [SerializeField]
    float yaw;
    [SerializeField]
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

    [SerializeField]
    Player player_ref;

    string axisX, axisY;

    // Use this for initialization
    void Start()
    {
        if (!player_ref || player_ref == null)
            player_ref = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //caso lockCursor seja true
        if (lockCursor)
        {
            

            Cursor.lockState = CursorLockMode.Locked;
            
            Cursor.visible = false;
        }

        if (!player_ref.isUsingJoystick())
        {
            axisX = "Mouse X";
            axisY = "Mouse Y";
        }
        else
        {
            axisX = "Right Stick X";
            axisY = "Right Stick Y";
          
        }



    }

    // Update is called once per frame
    // 
    void LateUpdate()
    {


        
       
        yaw += Input.GetAxis(axisX) * mouseSensitivity;
        pitch -= Input.GetAxis(axisY) * mouseSensitivity;

        
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


