using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform Tr;
    IEnumerator CO_MoveProcess;

    private void Start()
    {
        Tr = GetComponent<Transform>();
    }

    public void Move(Vector3 targetPos)
    {
        if(CO_MoveProcess != null)
        {
            StopCoroutine(CO_MoveProcess);
            CO_MoveProcess = null;
        }


        if (CO_MoveProcess == null)
        {
            CO_MoveProcess = co_move(targetPos);
            StartCoroutine(CO_MoveProcess);
        }
            
    }

    IEnumerator co_move(Vector3 targetPos)
    {
        Tr.LookAt(targetPos);

        while(true)
        {
            float distance = Vector3.Distance(Tr.position,targetPos);
            Vector3 dir = (targetPos - Tr.position).normalized;
            if (distance <= 0.2f)
            {
                Debug.Log("도착!");
                break;
            }

            Tr.Translate(Tr.forward * 2 * Time.deltaTime,Space.World);


            yield return null;
        }
    }
}
