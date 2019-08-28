using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float walkSpeed;
    public float runSpeed;
    public float speed;
    public float gravity;

    [SerializeField]
    private float smoothSpeedTime;
    [SerializeField]
    private float smoothSpeedVelocity;
    [SerializeField]
    private float smoothRotationVelocity;
    [SerializeField]
    private float smoothRotationTime;

    [SerializeField]
    CharacterController charController;


    [SerializeField]
    Transform cameraT;

    [SerializeField]
    bool running = false;

    [SerializeField]
    float velocityY;

    [SerializeField]
    Game game_ref;

    [SerializeField]
    Sino sino_ref;



    // Start is called before the first frame update
    void Start()
    {

        if (!game_ref || game_ref == null)
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        if (!sino_ref || sino_ref == null)
            sino_ref = GameObject.FindGameObjectWithTag("Sino").GetComponent<Sino>();//quando se faz referencias pra usar funções de outros scripts, alem desta forma tambem se deve arrastar o objeto da hierarca para o inspector?
    }

    // Update is called once per frame
    void Update()
    {
        AndarComRotacao();
    }
    void AndarComRotacao()
    {

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;



        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

        if (inputDir != Vector2.zero)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);


        running = (Input.GetKey(KeyCode.LeftShift));

        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;

        speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeedVelocity, smoothSpeedTime);


        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = transform.forward * speed * inputDir.magnitude + Vector3.up * velocityY; ;

        charController.Move(velocity * Time.deltaTime);

        speed = new Vector2(charController.velocity.x, charController.velocity.z).magnitude;

        if (charController.isGrounded)
        {
            velocityY = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Sino"))
        {
            
            other.GetComponent<Sino>().iniciarCountdown();
            
        }
        //if (other.gameObject.CompareTag("BaseAlavanca")) //Sino.isTimerOn == true??
       // {    
          //  other.GetComponent<BaseAlavanca>().Activate();

        //}
    }
}
