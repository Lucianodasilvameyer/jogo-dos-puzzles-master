using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowPortal : MonoBehaviour
{
    public GameObject rightPortal;
    public GameObject leftPortal;

    GameObject mainCamera;

    [SerializeField]
    Camera camera_ref; //aqui a referencia é do camera target ou do main camera?, não preciso fazer a referencia dela no start?
    // Start is called before the first frame update
    void Start()
    {
        if (!mainCamera || mainCamera == null)
            mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left click");
            throwPortal(leftPortal);
        }
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("right click");
            throwPortal(rightPortal);
        }
    }
    void throwPortal(GameObject portal)
    {
        int x = Screen.width / 2;
        int y = Screen.height / 2;

        Ray ray = camera_ref.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray ,out hit))
        {
            portal.transform.position = hit.point;
        }
    }
}
