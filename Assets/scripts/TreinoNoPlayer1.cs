using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoPlayer1 : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float speed;
    public float gravity;
    public float gravityInicial;

    [SerializeField]
    bool useGravity_;

    [SerializeField]
    Vector3 externalMovement;

    [SerializeField]
    Vector3 externalMovimentDelta;

    [SerializeField]
    bool useJoystick;

    [SerializeField]
    bool running = false;

    [SerializeField]
    Transform CameraT;

    [SerializeField]
    Plataforma plataforma; //é necessario fazer esta referencia para usar variaveis da classe plataforma?

    [SerializeField]
    CharacterController charController;

    [SerializeField]
    private float smoothRotationVelocity;

    [SerializeField]
    private float smoothRotationTime;

    [SerializeField]
    private float smoothSpeedVelocity;

    [SerializeField]
    private float smoothSpeedTime;

    [SerializeField]
    float velocityY;

    private bool teleportando = false;

    int quadroAtual = 0;

    public bool useGravity
    {
        get
        {
            return useGravity_; // aqui o valor varia de true para false dependendo do set abaixo?
        }
        set
        {
            useGravity_ = value; // como o useGravity_ é do tipo bool não é necessario colocar um valor inicial para ele no start?

            if (useGravity_) //sempre q a variavel mudar e for necessario realizar mais coisas se usa o get e o set, mas aqui a unica diferença é q o valor do gravity afeta tambem o valor do velocityY?    
            {
                gravity = gravityInicial;
            }
            else
            {
                gravity = 0;
            }
        }
    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()// o que seria LateUpdate?
    {

    }
    public void andarComRotacao() //no exemplo estava só void, mas só void seria private? não haveria como colocar uma função private no LateUpdate?   
    {
        input = Vector3.zero; // aqui tem que pegar o input do jogador, por isso o Input recebe vector3.zero?
        if (!useJoystick)    //posso usar tanto vector3 quanto vecto2 quando estiver criando a rotação?  
            input = new Vector3(Input.GetAxisRaw("Horizontal"), (Input.GetAxisRaw("Vertical"));
        else
            input = new Vector3(Input.GetAxisRaw("HorizontalJoy"), (Input.GetAxisRaw("Verticaljoy"));

        Vector3 inputDir = input.normalized;

        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + CameraT.eulerAngles.y;

        if (inputDir != Vector3.zero) //aqui significa q houve um digito?
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);
        //o transform.eulerAngles trabalha com a rotação em graus?

        running = (Input.GetKey(KeyCode.LeftShift));//usando GetKey estara correndo enquanto manter precionado o LeftShift?, usando GetKeyDown ficara correndo assim que precionar o LeftShift?  

        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;

        speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothRotationVelocity, smoothSpeedTime);

        velocityY += gravity * Time.deltaTime; //o veloityY vai sempre trabalhar com a aceleração da gravidade?

        Vector3 velocity = transform.forward * speed * inputDir.magnitude + Vector3.up * velocityY;

        if (plataforma != null) //aqui quer dizer q a classe plataforma não tem espaço para guardar mais nada? 
        {
            externalMovement = plataforma.deltaMovement; //pq o externalMovement recebe o valor do deltaMovement da plataforma aqui?
            externalMovimentDelta = externalMovement * Time.deltaTime; // aqui o externalMovimentDelta recebe o externalMovement multiplicado pelo Time.deltaTime pq como esta função vai no update se quer mover o personagem a quantidade necessaria baseado no ultimo frame?
        }
        else //aqui há espaço para guardar coisas na caixa?
        {
            externalMovement = Vector3.zero;//aqui quer dizer q externalMovement não tem nenhum valor? 
            externalMovimentDelta = externalMovement * Time.deltaTime;
        }
        if (!teleportando && (velocity + externalMovement) != Vector3.zero) //este if é para poder se teleportar? // aqui o teleportando esta false, mas pq o valor do velocity + externalMoviment tem q ser diferente de vector3.zero para entrar no if? 
            charController.Move((velocity + externalMovement) * Time.deltaTime);// aqui a função Move(função interna?) do charController manda o player para onde o portal indica?

        speed = new Vector3(charController.velocity.x, charController.velocity.y, charController.velocity.z).magnitude; //pq atualizar o atualizar o speed inicial com o do charController nesta parte? 
                                                                                                                        //posso atualizar o vector3 assim?

        if (charController.isGrounded)//isGrounded é uma função interna do charController?
        {
            velocityY = 0;//o velocityY estava trabalhando com a aceleração da gravidade, isso quer dizer q quando esta no chão a gravidade é zero?
        }

        jump();//chamando uma função dentro de outra?
    }
    public void setTeleportando(bool b)
    {
        teleportando = b;
    }
    public bool isUsingJoystick()  //usar esta função no player pq é ele q sabe se esta ou não usando o joystick?
    {
        return useJoystick;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)  //pq aqui usar o collider do charController?
    {
        if (hit.gameObject.CompareTag("Plataforma"))
        {
            plataforma = hit.transform.GetComponent<Plataforma>(); //aqui parece q a plataforma esta recebendo o transform dela mesma?
            velocityY = 0;   //aqui no velocityY e no useGravity esta se desativando a gravidade para o player não tremer mais quando estiver em cima da plataforma? 
            useGravity = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sino"))
        {
            other.GetComponent<Sino>().iniciarCountdown();
        }
        if (other.gameObject.CompareTag("Player")) //o player esta colidindo com ele mesmo?
        {
            plataforma = other.transform.parent.GetComponent<Plataforma>(); //aqui a plataforma esta recebendo o transform do pai dela?
            velocityY = 0;
            useGravity = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlataformaTrigger"))
        {
            plataforma = null; //aqui a plataforma fica com espaço para guardar coisas?
            useGravity = true; //ativa a gravidade
            externalMovement = Vector3.zero;//externalMovement vai a zero
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            if (Input.GetKeyDown(KeyCode.E) && other.gameObject.GetComponent<Tile>().IsMoving()== false)
            {
                other.gameObject.GetComponent<Tile>().Move();
            }
        }
        if (other.gameObject.CompareTag("Quadro") && Input.GetKeyDown(KeyCode.E))  //mas neste caso o quadro não esta em baixo do player?
        {
            int quadroEscolhido = other.gameObject.GetComponent<Quadro>().getID();

            if (quadroEscolhido == quadroAtual)
            {
                if(quadroAtual == Quadro.quantidadeDeQuadros - 1)
                {
                    //puzzle completo;
                }
                else
                {
                    quadroAtual++;
                }
            }
            else if(quadroAtual - 1 != quadroEscolhido)
            {
                quadroAtual = 0;
            }
        }
    }


}