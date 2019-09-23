using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNaPlataforma : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 direction;

    [SerializeField]
    private float timerMax;

    [SerializeField]
    private Vector3 deltaMoviment;

    [SerializeField]
    private bool invert;//para q serve esta variavel?

    // Start is called before the first frame update
    void Start()
    {
        direction = direction.normalized;


        this.AttachTimer(timerMax, InvertDirection, isLooped: true);//para q serve esta parte?
    }

    // Update is called once per frame
    void Update()
    {
        if (!invert)
        {
            transform.Translate(direction* speed * Time.deltaTime);// o transform.translate é a ordem para se mover?
            deltaMoviment = direction * speed;//?
        }
        else
        {
            transform.Translate(-direction * speed * Time.deltaTime);
            deltaMoviment = -direction * speed;
        }

    }

    public void InvertDirection()//para q usar esta função?
    {

        if(!invert)
        {
            deltaMoviment = -direction * speed;
        }
        else
        {
            deltaMoviment = direction * speed;
        }
        invert = !invert;
    }
}
