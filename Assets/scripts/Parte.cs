using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parte : MonoBehaviour
{
    [SerializeField]
    Parte parte;
    
    private bool movimentoParaBaixo = false;
    private bool movimentoParaCima = false;
    private bool movimentoParaEsquerda = false;
    private bool movimentoParaDireita = false;

    [SerializeField]
    private float speed;

    ParteControladora parteControladora;

    Rigidbody body;




    // Start is called before the first frame update
    void Start()
    {
        if (!body || body == null)
            body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
       if(movimentoParaBaixo==true && Input.GetKeyDown(KeyCode.S))
       {
            Movimento4();
       }
       else if(movimentoParaCima==true && Input.GetKeyDown(KeyCode.W))
       {
            Movimento3();
       }
       else if(movimentoParaEsquerda==true && Input.GetKeyDown(KeyCode.A))
       {
            Movimento2();
       }
       else if (movimentoParaDireita == true && Input.GetKeyDown(KeyCode.D))
       {
            Movimento1();
       }

    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("LadoDeCima"))
        {
            movimentoParaBaixo = true;
        }
        if (collision.gameObject.CompareTag("LadoDeBaixo"))
        {
            movimentoParaCima = true;
        }
        if (collision.gameObject.CompareTag("LadoDireito"))
        {
            movimentoParaEsquerda = true;
        }
        if (collision.gameObject.CompareTag("LadoEsquerdo"))
        {
            movimentoParaDireita = true;
        }
    }
    public void Movimento1()
    {

        Vector3 input = new Vector3(1, 0,-1f);
        Vector3 direction = input.normalized;
        Vector3 velocity = speed * direction;
        velocity.y = body.velocity.y;   
        body.velocity = velocity;
    }
    public void Movimento2()
    {
        Vector3 input = new Vector3(-1, 0, -1f);
        Vector3 direction = input.normalized;
        Vector3 velocity = speed * direction;
        velocity.y = body.velocity.y;
        body.velocity = velocity;
    }
    public void Movimento3()
    {
        Vector3 input = new Vector3(0, 1, -1f);
        Vector3 direction = input.normalized;
        Vector3 velocity = speed * direction;
        velocity.x = body.velocity.x;
        body.velocity = velocity;
    }
    public void Movimento4()
    {
        Vector3 input = new Vector3(0, -1, -1f);
        Vector3 direction = input.normalized;
        Vector3 velocity = speed * direction;
        velocity.x = body.velocity.x;
        body.velocity = velocity;
    }
}  
