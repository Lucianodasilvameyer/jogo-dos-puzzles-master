using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    Player player_ref;

    [SerializeField]
    Vector3 Destino;
    // Start is called before the first frame update
    void Start()
    {
        if (!player_ref || player_ref == null)
            player_ref = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player_ref.transform.position=Destino+Destino.foward*
        }
    }
}
