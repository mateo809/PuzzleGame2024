using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;
public class CodeLocker : MonoBehaviour
{

    [SerializeField]private List<Transform> _wheelList = new List<Transform>();
    private Vector3 _wheelMidPos;
    




    private void DetectClickedWheel()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //if (Physics.Raycast(ray, out hit))
        Debug.DrawRay(ray.origin,ray.direction.normalized * 1000, Color.red, 10);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (!hit.collider.gameObject.CompareTag("Wheel"))
            {
                return;
            }
            hit.transform.Rotate(hit.transform.forward, 36);
        }



        //if (Physics.Raycast(ray, out hit, 1000))
        //{
        //    Debug.Log("HIT LOCK");  

            //    Transform wheelTrans = _wheelList.Aggregate((currentSmallest, next) =>
            //    (next.position - hit.point).magnitude < (currentSmallest.position - hit.point).magnitude ? next : currentSmallest);

            //    wheelTrans.Rotate(wheelTrans.forward, 36);

            //    //if (hit.point.y > _wheelMidPos.y)
            //    //{
            //    //    //tourner roue vers le bas
            //    //    //Rotate by 36 degrees because 360 degrees / 10 faces
            //    //    wheelTrans.Rotate(wheelTrans.forward, 36);
            //    //}
            //    //else
            //    //{
            //    //    //tourner roue vers le haut
            //    //    wheelTrans.Rotate(wheelTrans.forward, -36);
            //    //}

            //}
    }

    private void CheckCode()
    {

    }

    private void OnMouseDown()
    {
        DetectClickedWheel();
    }
}
