using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class TreinoNoPlayer2 : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float gravityInicial;
    public float gravity;
    public float speed;

    public float tempoDeEspera = 5;
    int quadroCerto = 0;

    [SerializeField]
    Game game_ref;

    private float jumpHeight;

    [SerializeField]
    bool teleportando=false;

    [SerializeField]
    Vector3 externalMoviment;

    [SerializeField]
    Vector3 externalMovimentDelta;

    [SerializeField]
    Transform cameraT;

    [SerializeField]
    Plataforma plataforma; //é necessario fazer esta referencia sem usar o gameobject.find... no void awake para usar variaveis da classe plataforma? 

    [SerializeField]
    bool running = false;

    [SerializeField]
    float velocityY;

    [SerializeField]
    bool useJoystick;

    [SerializeField]
    public float smoothRotationTime;

    [SerializeField]
    public float smoothRotationVelocity;

    [SerializeField]
    public float smoothSpeedVelocity;

    [SerializeField]
    public float smoothSpeedTime;

    [SerializeField]
    CharacterController charController;

    [SerializeField]
    Animator animator;

    [SerializeField]
    bool useGravity_;

    public bool useGravity
    {
        get
        {
            return useGravity_;
        }
        set
        {
            useGravity_ = value;

            if(useGravity_)
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
    private void Start() //private?
    {
       
    }
    void Awake()//é melhor utilizado para configurar quaisquer referencias entre scripts e inicialização?, ja o start é utilizado só quando o componente esta ativo e isso adia qualquer parte do codigo de inicialização até que seja realmente necessario? 
    {
        transform.tag = "Player";
        externalMoviment = Vector3.zero;//pq o externalMoviment recebe um valor nulo aqui?

        animator = GetComponent<Animator>();//esta parte é para fazer a referencia do animator para ativar animações pelo player?, mas do mesmo jeito eu tenho q arrastar o componente animator para seu espaço no player?

        cameraT = Camera.main.transform;
        plataforma = null;

        if (!game_ref || game_ref == null)
            game_ref = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();


    }
    // Update is called once per frame
    void LateUpdate()
    {

        
    }
    public void andarComRotaacao()//no exemplo estava só void, mas só void seria private? não haveria como colocar uma função private no LateUpdate?
    {
        Vector3 input;
                                 //se usa vector3 pq o jogo é em 3D?
        input = Vector3.zero; // aqui tem que pegar o input do jogador, por isso o Input recebe vector3.zero q conta como valor nulo?
          
        if (!useJoystick)
            input = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else
            input= new Vector3(Input.GetAxisRaw("HorizontalJoy"),Input.GetAxisRaw("VerticalJoy"));

        Vector3 inputDir = input.normalized;//este inputDir é o mesmo input acima?

        float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;

        if (inputDir != Vector3.zero) //aqui significa se houve ou não um digito?

            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref smoothRotationVelocity, smoothRotationTime);

        running = (Input.GetKey(KeyCode.LeftShift)); //usando GetKey estara correndo enquanto manter precionado o LeftShift?, usando GetKeyDown ficara correndo assim que precionar o LeftShift?

        /* running=(Input.GetAxisRaw("run")==0)? true : false;*/ //?? // Aqui setamos o valor da bool running baseado no pressionamento da tecla shift. Running será true sempre que Shift estiver pressionado, e false quando não estiver pressionado

        float targetSpeed = (running) ? runSpeed : walkSpeed * inputDir.magnitude;

        speed = Mathf.SmoothDamp(speed, targetSpeed, ref smoothSpeedVelocity, smoothSpeedTime);

        velocityY += gravity * Time.deltaTime; 

        Vector3 velocity = transform.forward * speed * inputDir.magnitude + Vector3.up * velocityY;

        if (plataforma != null)//este !=null quer dizer q tem algo na plataforma?
        {   //este if serve para saber o quanto o player pode andar na plataforma?
            externalMoviment = plataforma.deltaMovement;//o deltaMoviment é a direção(direction) e intensidade(speed) da plataforma ou seja o vetor de movimentação dela, salvando esse valor no externalMoviment o mesmo se torna o vetor de movimentação do player na plataforma?
            externalMovimentDelta = externalMoviment * Time.deltaTime; //o externalMovimentDelta se torna o vetor de movimentação do player na plataforma mais o espaço q ele percorre a cada frame adquirido atravez do Time.deltaTime? 
        }
        else
        {
            externalMoviment= Vector3.zero;
            externalMovimentDelta = externalMoviment * Time.deltaTime;
        }                                                                   // tentar verificar se o external movement deixará o player muito longe da plataforma?
        if (!teleportando && (velocity + externalMoviment) != Vector3.zero)  //pq somar o velocity com o externalMoviment? 
            charController.Move((velocity + externalMoviment) * Time.deltaTime); //o move é uma função interna do characterController?

        speed = new Vector3(charController.velocity.x, charController.velocity.y, charController.velocity.z).magnitude; //aqui tem q atualizar a velocidade inicial com a velocidade interna do character controller q é mais precisa. mas mesmo assim pq esta criando um novo vetor?          ,para q usar o velocity entre parenteses sendo q ele tem a direção e a intencidade?           e pq o magnitude no final?

        if(charController.isGrounded)//isGrounded é uma função interna do characterController?
        {
            velocityY = 0; //a gravidade sera 0 quando estiver no chão?
        }
        Jump();
    }
    public void setTeleportando(bool b)
    {
        teleportando = b;
    }
    public bool isUsingJoystick() //não é melhor fazer uma função q nem a anterior para ja retornar o joystick como true ou false?
    {
        return useJoystick;
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)//OnControllerColliderHit é chamado quando o controlador atinge um colisor ao executar um movimento. ou seja neste caso quando o player colidiu com a plataforma apos pular?
    {
        if (hit.gameObject.CompareTag("Plataforma"))
        {
            plataforma = hit.transform.GetComponent<Plataforma>();// para q salvar o transform da plataforma aqui?
            velocityY = 0;
            useGravity = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sino"))
        {
            other.GetComponent<Sino>().iniciarCountdown();
        }
        if (other.gameObject.CompareTag("Player"))
        {
            plataforma = other.transform.parent.GetComponent<Plataforma>();  //aqui o pai vira a plataforma
            useGravity = false;                                             //transform.parent é o transform do pai
            velocityY = 0;                                                     //o other.gameObject.transform.parent dis que é o pai da plataforma
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("plataformaTrigger"))
        {
            externalMoviment = Vector3.zero;
            plataforma = null; //ao player não estar mais na plataforma a mesma não estara mais ativa?
            useGravity = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Tile"))
        {
            if (Input.GetAxisRaw("interaction") == 1 && other.gameObject.GetComponent<Tile>().IsMoving == false)//o Input.GetAxisRaw("interaction") == 1 é se ativar o tile?
            {
                other.gameObject.GetComponent<Tile>().Move();
            }
        }
        if (other.gameObject.CompareTag("Quadro") && Input.GetAxisRaw("Interaction") == 1)
        {
            int quadroEscolhido = other.gameObject.GetComponent<Quadro>().getID();

            if (quadroEscolhido == quadroCerto)
            {
                if (quadroCerto == Quadro.quantidadeDeQuadros-1)//por causa do other.gameObject.CompareTag("Quadro") não é neccesario fazer referencia para usar a variavel quantidadeDeQuadros do Quadro?
                {
                    other.gameObject.GetComponent<Quadro>().tint(Color.green);
                    other.gameObject.GetComponent<Quadro>().descerAlavanca();
                    game_ref.destravarAlavancaQuadros();
                }
                else
                {
                    other.gameObject.GetComponent<Quadro>().tint(Color.green);
                    other.gameObject.GetComponent<Quadro>().descerAlavanca();
                    quadroCerto++;
                }
            }
            else if(quadroCerto -1 != quadroEscolhido)
            {
                
                quadroCerto = 0;
                game_ref.pintarTodosOsQuadros(Color.white);
                game_ref.resetarTodosOsQuadros();
            }
        }
    }
    public void Jump()
    {
        if (Input.GetAxisRaw("Jump") == 1)
        {
            if (charController.isGrounded || plataforma != null)//o != null quer diser q a plataforma esta ativa?
            {
                float jumpVelocity = Mathf.Sqrt(-2 * gravityInicial * jumpHeight);//pq usar o gravityInicial aqui?
                velocityY = jumpVelocity; //pq a velocityY recebe o jumpVelocity?

            }
        }
    }
    public void morte()
    {
        game_ref.GameOver();
        Timer.Register(tempoDeEspera, () => SceneManager.LoadScene(0));//este Timer.Register serve para usar 2 parametros combinados?, o tempoDeEspera para esperar os 5 segundos e o ()=>SceneManager.LoadScene(0) para carregar a scene 0?
    }

}
