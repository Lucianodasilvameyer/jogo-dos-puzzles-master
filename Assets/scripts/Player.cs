using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float walkSpeed;
    public float runSpeed;
    public float speed;
    public float gravity;
    public float jumpHeight;

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

    [SerializeField]
    Animator animator;

    private bool teleportando = false;
    [SerializeField]
    Vector2 input;

    [SerializeField]
    bool useJoystick;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraT = Camera.main.transform;

        if (!game_ref || game_ref == null)
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();

        if (!sino_ref || sino_ref == null)
            sino_ref = GameObject.FindGameObjectWithTag("Sino").GetComponent<Sino>();//quando se faz referencias pra usar funções de outros scripts, alem desta forma tambem se deve arrastar o objeto da hierarca para o inspector?
    }

    // Update is called once per frame
    void LateUpdate()
    {
        AndarComRotacao();
       
    }
    void AndarComRotacao()
    {

        // pegar input do jogador
        // Input.getaxis retorna um valor de -1 a 1
        input = Vector2.zero;
        if (!useJoystick)
         input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else
            input = new Vector2(Input.GetAxisRaw("HorizontalJoy"), Input.GetAxisRaw("VerticalJoy"));

        //normalizamos para pegar apenas a direção
        Vector2 inputDir = input.normalized;

        //aqui calculamos a nossa rotação alvo utilizando o arco tangente do input, convertendo de radianos para angulos
        //e somando a rotação da camera em Y para o jogador olhar para a camera quando começar a andar
        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

        //Aqui aplicamos de fato a rotação
        if (inputDir != Vector2.zero) // vector.up pois vamos rotacionar ao redor do eixo Y multiplicado pela nossa rotação alvo (que está sendo suavizada na função smoothDampAngle)
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);

        // Aqui setamos o valor da bool running baseado no pressionamento da tecla shift. Running será true sempre que Shift estiver pressionado, e false quando não estiver pressionado
        running = (Input.GetKey(KeyCode.LeftShift));

        // salvamos nessa variavel a velocidade alvo
        // verificamos o valor de running, caso true recebemos o valor da corrida, senão da caminhada.
        // Por fim ainda multiplicamos pela magnitude do vetor do input da direção (caso não estejamos apertando tecla nenhuma, o resultado da multiplicação será 0.
        // caso estejamos apertando, o valor será 1 e não alterará o resultado)
        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;

        //atualizamos nossa velocidade atual com  a velocidade alvo suavizada
        speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeedVelocity, smoothSpeedTime);

        //aumentando a aceleração da gravidade
        velocityY += gravity * Time.deltaTime;

        // aqui calculamos o vetor de movimentação total (direções e intesindades)
        // a primeira parte é para mover o personagem para frente (transform.foward) na intensidade speed (novamente multiplicado pelo inputDir.magnitude para impedir a movimentação quando não há teclas pressionadas
        // depois adicionamos no eixo Y (vector3.up == (0,1,0)) na intensidade velocityY (que é a força da gravidade + algum possível pulo)
        Vector3 velocity = transform.forward * speed * inputDir.magnitude + Vector3.up * velocityY;

        //aqui efetuamos a movimentação chamando o método Move do Character controller
        // Na função passamos o vetor de movimentação (multiplicamos por Time.deltaTime pois como estamos na função Update, queremos mover o personagem só a quantidade necessário baseado no último frame)
        if(!teleportando)
        charController.Move(velocity * Time.deltaTime);//aqui so quando o teleportando for 




        //aqui atualizamos a velociade inicial com a velocidade interna do character controller que é mais precisa
        speed = new Vector2(charController.velocity.x, charController.velocity.z).magnitude;

        //aqui resetamos a velocidade no Y para zero, não precisamos mais cair quando já estamos no chão
        if (charController.isGrounded)
        {
            velocityY = 0;
        }



        //aqui chamamos a função de pulo
        Jump();
    }

    public void setTeleportando(bool b)
    {
        teleportando = b;
    }

    public bool isUsingJoystick()
    {
        return useJoystick;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
           // print(other.gameObject.GetComponent<Tile>().id);
            if (Input.GetKeyDown(KeyCode.E) && other.gameObject.GetComponent<Tile>().IsMoving() == false)
            {
                other.gameObject.GetComponent<Tile>().Move();
            }
        }
    }
    void Jump()
    {
        if (Input.GetAxisRaw("Jump") == 1)
        {


            if (charController.isGrounded)
            {
            
                float jumpVelocity = Mathf.Sqrt(-2 * gravity * jumpHeight);
                velocityY = jumpVelocity;
            }

        }

    }

}
