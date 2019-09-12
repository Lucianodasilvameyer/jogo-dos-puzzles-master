using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]//o NavMeshAgent trabalha com IA?


public class Inimigo : MonoBehaviour
{
    private GameObject player;
    private NavMeshAgent navMesh;
    private bool podeAtacar;
    [SerializeField]
    int dano;
    [SerializeField]
    float tempoAtaque;
    Timer timer;

    [SerializeField]
    Transform posicaoRetorno;


    // Start is called before the first frame update
    void Start()
    {
        podeAtacar = true;
        player = GameObject.FindWithTag("Player");
        navMesh = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            Debug.Log("Esta perto");
            atacar();
        }
        else if (Vector3.Distance(transform.position, player.transform.position) >= 40f)
        {
            navMesh.destination = posicaoRetorno.position; ;
        }
        else
        {
            navMesh.destination = player.transform.position;
        }
    }
    void atacar()
    {
        if (podeAtacar == true)
        {
            podeAtacar = false;
            player.GetComponent<Player>().vida -= dano;
            timer = Timer.Register(tempoAtaque, resetAtaque); // aqui cria o timer
        }
            
        
    }
    void resetAtaque()
    {
        podeAtacar = true;
    }

    
}
