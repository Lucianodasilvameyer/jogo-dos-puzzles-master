using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Direcoes //o enum é um cunjunto de possibilidades q só vai ser trabalhado com uma
    {
        Cima,
        Baixo,
        Esquerda,
        Direita,
        Nenhuma
    }

    /*exemplo
      public enum alcance
      {
        baixo,
        medio,
        alto
      }
     */

    [SerializeField]
    private Direcoes direcao;//o ? significa q pode receber null

    [SerializeField]
    private float timeToMove;

    [SerializeField]
    public int id;

    [SerializeField]
    bool isMoving = false;

    [SerializeField]
    Tile tileInvisivel;
    [SerializeField]
    public bool isInvisivel;

    // Start is called before the first frame update
    private void Start()
    {
      //  direcao = Direcoes.Nenhuma;//aqui o null serve para dizer q não tem nenhum valor ,neste caso nenhuma direção para ir
    }

    // Update is called once per frame
    private void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LadoDeCima"))
        {
            direcao = Direcoes.Baixo;
            
        }
        else
        if (other.gameObject.CompareTag("LadoDeBaixo"))
        {
            direcao = Direcoes.Cima;
        }
        else
        if (other.gameObject.CompareTag("LadoDireito"))
        {
            direcao = Direcoes.Esquerda;
        }
        else
        if (other.gameObject.CompareTag("LadoEsquerdo"))
        {
            direcao = Direcoes.Direita;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LadoDeCima") || other.gameObject.CompareTag("LadoDeBaixo") || other.gameObject.CompareTag("LadoDireito") || other.gameObject.CompareTag("LadoEsquerdo"))
        {
            direcao = Direcoes.Nenhuma;
        }
    }

    public void Move() //aqui a sem parametro vai para a posição q tem q ir
    {
        if (direcao == Direcoes.Nenhuma || isMoving == true)
            return;

        Vector3 direction = Vector3.zero;//pode não entrar pq o valor pode ser null
        bool useHeight = false;
        switch (direcao)
        {
            case Direcoes.Cima:
                direction = new Vector3(0, 0, 1f);//aqui o vector ja é a ordem para se mover
                useHeight = true;
                break;

            case Direcoes.Baixo:
                direction = new Vector3(0, 0, -1f);
                useHeight = true;
                break;

            case Direcoes.Direita:
                direction = new Vector3(1, 0, 0);
                break;

            case Direcoes.Esquerda:
                direction = new Vector3(-1, 0, 0);
                break;
        }

        direction = transform.InverseTransformDirection(direction.normalized);
        

        float distanceToMove = 0;

        if (useHeight)
            distanceToMove = GetComponent<BoxCollider>().bounds.size.z;
        else
            distanceToMove = GetComponent<BoxCollider>().bounds.size.x;

        transform.DOMove(transform.position + direction * distanceToMove, timeToMove).OnComplete(() => setIsMoving(false));
        //transform.DOMove é o comando que passa aonde quer chegar em tanto tempo

        //transform.position é a posição atual
        // direction é direção
        //distanceToMove é quanto tem q andar
        //timeToMove é quanto tempo tem para chegar

        setIsMoving(true);

        if(!isInvisivel && tileInvisivel.IsMoving() == false)
        {
            print("Moveu Invisivel");
            tileInvisivel.transform.DOMove(tileInvisivel.transform.position + (direction * -1) * distanceToMove , timeToMove).OnComplete(() => tileInvisivel.setIsMoving(false));
            tileInvisivel.setIsMoving(true);
        }
        

    }

    void setIsMoving(bool b)
    {
        isMoving = b;
    }

    void Move(Direcoes? dir)//aqui a com parametro vai para a possição q se quer
    {
        if (dir == null)
            return;

        Vector3 direction = Vector3.zero;//pode não entrar pq o valor pode ser null
        bool useHeight = false;
        switch (dir)
        {
            case Direcoes.Cima:
                direction = new Vector3(0, 0, -1f);
                useHeight = true;
                break;

            case Direcoes.Baixo:
                direction = new Vector3(0, 0, 1f);
                useHeight = true;
                break;

            case Direcoes.Direita:
                direction = new Vector3(1, 0, 0);
                break;

            case Direcoes.Esquerda:
                direction = new Vector3(-1, 0, 0);
                break;
        }

        direction = direction.normalized;

        float distanceToMove = 0;

        if (useHeight)
            distanceToMove = GetComponent<MeshRenderer>().bounds.size.y;
        else
            distanceToMove = GetComponent<MeshRenderer>().bounds.size.x;

        transform.DOMove(transform.position + direction * distanceToMove, 1f);//transform.DOMove é o comando que passa aonde quer chegar em tanto tempo
                                                                              //transform.position é a posição atual
                                                                              // direction é direção
                                                                              //distanceToMove é quanto tem q andar
                                                                              //1f é quanto tempo tem para chegar
    }

    public bool IsMoving()
    {
        return isMoving;
    }
    /*
    public void Movimento1()
    {
       Vector3 direction = new Vector3(1, 0,-1f); não fazer desta forma pq é melhor fazer com parametro
        direction = direction.normalized;
        Vector3 velocity = speed * direction;
        velocity.y = body.velocity.y;
        body.velocity = velocity;
    }
    */
}