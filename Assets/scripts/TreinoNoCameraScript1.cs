using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreinoNo : MonoBehaviour
{
    [SerializeField]
    Player player_ref;

    string axisX, axisY;
    // Start is called before the first frame update
    void Start()
    {
        if (!player_ref || player_ref == null)
            player_ref = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        if (!player_ref.isUsingJoystick())//aqui não esta usando joystick, então esta configurando 2 mouses para 2 players?
        {                                 //esta parte não deveria estar no script do game?
            axisX = "Mouse X"; 
            axisY = "Mouse Y";
        }
        else
        {
            axisX = "Right Stick X";
            axisY = "Right Stick Y";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
