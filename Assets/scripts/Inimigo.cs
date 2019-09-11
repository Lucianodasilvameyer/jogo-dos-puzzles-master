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
        navMesh.destination = player.transform.position;
        if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
        {
            Debug.Log("Esta perto");
            atacar();
        }
    }
    void atacar()
    {
        if (podeAtacar == true)
            StartCoroutine("TempoDeAtaque");
        player.GetComponent<Player>().vida -= 40;
    }
    IEnumerator TempoDeAtaque()
    {
        podeAtacar = false;
        yield return  WaitForSeconds(1);
        podeAtacar = true;
    }

    private object WaitForSeconds(int v)
    {
        throw new NotImplementedException();
    }
}
