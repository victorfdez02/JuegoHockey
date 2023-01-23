using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Estrella : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Descativo el objeto (opcion 1)
        gameObject.SetActive(false);
        Instantiate(col.gameObject, new Vector3(0, 0), Quaternion.identity);
        //Destruyo el objeto
    }

}
