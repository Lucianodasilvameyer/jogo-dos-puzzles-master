using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{


    [SerializeField]
    Transform destino;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            other.transform.position = destino.position;
            Camera.main.transform.rotation = destino.rotation;
            other.GetComponent<Player>().setTeleportando(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
           other.GetComponent<Player>().setTeleportando(false);
        }
    }


}
