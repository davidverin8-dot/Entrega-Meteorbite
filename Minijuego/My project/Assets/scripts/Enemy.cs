using UnityEngine;
using System.Collections.Generic;


public class Enemy : MonoBehaviour
{

    public float maxTimeLife = 4f;
    public float speed = 5f; // Velocidad de caída

    public float fuerzaDeDivision = 100f;

    void OnEnable()
    {
        CancelInvoke("DevolverAlPool");
        Invoke("DevolverAlPool", maxTimeLife);
    }

    private void DevolverAlPool()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        if(!Jugador.Game_paused)
        {
            // Esta es la línea que faltaba para que el meteorito caiga
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
    }
    private void OnTriggerEnter(Collider collision)
{
    
    if(collision.gameObject.tag == "Enemy")
    {
        
        collision.gameObject.SetActive(false);

        
        Vector3 direccion1 = new Vector3(-1f, 0.5f, 0).normalized;
        Vector3 direccion2 = new Vector3(1f, 0.5f, 0).normalized;

        
        GameObject mini1 = PoolMiniEnemy.SharedInstance.GetPooledObject();
        if (mini1 != null) 
        {
            mini1.SetActive(true);
            mini1.transform.position = transform.position + direccion1;
            mini1.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            mini1.GetComponent<Rigidbody>().AddForce(direccion1 * fuerzaDeDivision);
        }

        
        GameObject mini2 = PoolMiniEnemy.SharedInstance.GetPooledObject();
        if (mini2 != null)
        {
            mini2.SetActive(true);
            mini2.transform.position = transform.position + direccion2; 
            mini2.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            mini2.GetComponent<Rigidbody>().AddForce(direccion2 * fuerzaDeDivision);
        }

        
        gameObject.SetActive(false);
    }
}
}