using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Player OwnPlayer;
     
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 1000f))
            {
                if (hit.collider.tag == "Ground")
                {
                    OwnPlayer.Move(hit.point);
                    SocketIOManager.Getsingleton.Send_Move(hit.point);
                }
            }
        }


    }
}
