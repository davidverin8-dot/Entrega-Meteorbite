using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    
    public float thrustForce = 100f;
    public float rotationSpeed = 100f;
    private Rigidbody _r1g1d;

    public GameObject gun, bulleetPrefab;

    public static int Score =0;


//Aqui le pedi ayuda a la IA para que me ayudara a calcular los limites de la pantalla ya que depende del ordenador son distintos, y me dio esta solucion, que funciona perfectamente;

    public float yBorderLimit;

    public float xBorderLimit;

    float margen = 0.5f;

    public static bool Game_paused=false;

    public  GameObject  Pause_menu=null;
    public  GameObject  Boton=null;

   



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        _r1g1d = GetComponent<Rigidbody>();

        yBorderLimit = Camera.main.orthographicSize;
        xBorderLimit = yBorderLimit * Camera.main.aspect;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        var newPosition = transform.position;

        if (newPosition.x > xBorderLimit + margen)
        {
            newPosition.x = -xBorderLimit-margen;
        }
        else if (newPosition.x < -xBorderLimit - margen)
        {
            newPosition.x = xBorderLimit+margen;
        }
        if (newPosition.y > yBorderLimit + margen)
        {
            newPosition.y = -yBorderLimit-margen;
        }
        else if (newPosition.y < -yBorderLimit - margen)
        {
            newPosition.y = yBorderLimit+margen;
        }
        transform.position = newPosition;

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Jugador.Game_paused = !Jugador.Game_paused;
            Time.timeScale = 0f;
            Pause_menu.SetActive(true);
            Boton.SetActive(true);
            Debug.Log("Pausa");
        }

        if(!Jugador.Game_paused)
        {
            Time.timeScale = 1f;
            movecharacter();
        }


    }

    private void movecharacter()
    {
       Pause_menu.SetActive(false);
       Boton.SetActive(false);
       float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
       float thrust =Input.GetAxis("Thrust") * Time.deltaTime;

        Vector3 thrustDirection = transform.right;

        _r1g1d.AddForce(thrustDirection * thrust * thrustForce);

        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
           // GameObject bullet = Instantiate(bulleetPrefab, gun.transform.position, Quaternion.identity);
            
            // Balistic1 balaScript = bullet.GetComponent<Balistic1>();

            // balaScript.targetVector = transform.right;
             GameObject bullet = PoolBalistic.SharedInstance.GetPooledObject();
             
            if (bullet != null) {
                bullet.transform.position = gun.transform.position;
                bullet.transform.rotation = gun.transform.rotation;
                
                Balistic1 balaScript = bullet.GetComponent<Balistic1>();

                balaScript.targetVector = transform.right;

                

                bullet.SetActive(true);

                
  } 

             //balaScript.shipVelocity = _r1g1d.linearVelocity;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Enemy2")
        {
            Score = 0;
             SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.Log("He sido colisionado por otra cosa,uwu");
        }
       
    }
    public void Reanudar()
{
    Jugador.Game_paused = false;
    Time.timeScale = 1f;
    Pause_menu.SetActive(false);
}

    
}
