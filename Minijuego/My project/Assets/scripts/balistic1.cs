using System;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Balistic1 : MonoBehaviour
{


    public float speed = 10f;
    public float maxLifetime = 3f;
    public Vector3 targetVector;

    public static int Score = 0;

    //public Vector3 shipVelocity = new Vector3(0, 0, 0);

    public GameObject mine_asteroidPrefab;
    public float fuerzaDeDivision = 100f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void OnEnable()
    {
        // Cancelamos invocaciones anteriores por si acaso
        CancelInvoke("DesactivarBala");
        
        // En lugar de Destroy, programamos que se desactive tras maxLifetime
        Invoke("DesactivarBala", maxLifetime);
    }
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if(!Jugador.Game_paused)
        {
            movebalistic();
        }
    }

    private void movebalistic()
    {
        
        Vector3 totalVelocity = targetVector * speed ; //shipVelocity;
       
       transform.Translate(totalVelocity * Time.deltaTime,Space.World); 

    }

    private void OnTriggerEnter(Collider collision)
    {

   
        if(collision.gameObject.tag == "Enemy")
        {
            IncrementScore();
            
            Vector3 posicionImpacto = collision.gameObject.transform.position;
            Vector3 direccion1 = new Vector3(-targetVector.y, targetVector.x+0.2f, 0).normalized;
            Vector3 direccion2 = new Vector3(targetVector.y, -targetVector.x+0.2f, 0).normalized;

           // GameObject mini1 = Instantiate(mine_asteroidPrefab, posicionImpacto + direccion1 , Quaternion.identity);
           // GameObject mini2 = Instantiate(mine_asteroidPrefab, posicionImpacto + direccion2 , Quaternion.identity);
        
            GameObject mini1 = PoolMiniEnemy.SharedInstance.GetPooledObject();
           
           if (mini1 != null ) 
            {

            mini1.transform.position = posicionImpacto + direccion1;
           

            mini1.SetActive(true);
            

            mini1.GetComponent<Rigidbody>().AddForce(direccion1 * fuerzaDeDivision);
            
            }
             GameObject mini2 = PoolMiniEnemy.SharedInstance.GetPooledObject();
           
            if(mini2 != null)
            {
                 mini2.transform.position = posicionImpacto + direccion2; 
                 mini2.SetActive(true);
                mini2.GetComponent<Rigidbody>().AddForce(direccion2 * fuerzaDeDivision);
            }
            
            
            
            
            //Destroy(collision.gameObject);
            //Destroy(gameObject);
            
            collision.gameObject.SetActive(false);

            DesactivarBala();
        }
        if(collision.gameObject.tag == "Enemy2")
        {
        
         IncrementScore();
         collision.gameObject.SetActive(false);
         //Destroy(collision.gameObject);
         //Destroy(gameObject);
         DesactivarBala();
        }
    }

    private void IncrementScore()
    {
        Jugador.Score ++;
        Debug.Log(Jugador.Score);
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text= "Puntos: " + Jugador.Score;
    }

    private void DesactivarBala()
    {
        gameObject.SetActive(false);
    }

    
}
