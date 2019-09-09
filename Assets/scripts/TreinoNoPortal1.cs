using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNoPortal1 : MonoBehaviour
{
    [SerializeField]
    Transform Destino;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.position = Destino.position;
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
