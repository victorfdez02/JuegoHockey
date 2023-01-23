using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Añade esta librería apra poder utilizar los elemento de User Interface */
using UnityEngine.UI;

public class Bola : MonoBehaviour {
    
    //Estrella

    [SerializeField] private GameObject estrella;
    [SerializeField] private Vector2 limites;

    //Velocidad
   [SerializeField] private float velocidad = 30.0f;

//Cajas de texto de los contadores
   [SerializeField] private Text contadorIzquierda;
   [SerializeField] private Text contadorDerecha;
   
   AudioSource fuenteDeAudio;
//Clips de audio
   [SerializeField] private AudioClip audioGol, audioRaqueta, audioRebote;

   private GameManager _gameManager;

//variable para contabilizar el tiempo inicializada a 180 segundos (3 minutos)

   //Se ejecuta al arrancar
   void Start () {
       //Velocidad inicial hacia la derecha
       GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
       //Pongo los contadores a 0
       contadorIzquierda.text = Contadores.golesIzquierda.ToString();
       contadorDerecha.text = Contadores.golesDerecha.ToString();
       fuenteDeAudio = GetComponent<AudioSource>();
       //Desactivo la caja de resultado
        //Quito la pausa
       Time.timeScale = 1;
       _gameManager = FindObjectOfType<GameManager>();
   }
   
   void Update () {

       //Incremento la velocidad de la bola
       velocidad = velocidad + 2 * Time.deltaTime;

       //Si aún no se ha acabado el tiempo, decremento su valor y lo muestro en la caja de texto
       
       if (Contadores.tiempo <= 170)
       {
           if (Contadores.aux < 1)
           {
               float x = Random.Range(-limites.x, limites.x);
               float y = Random.Range(-limites.y, limites.y);
               Vector2 posicion = new Vector2(x, y);
               estrella = Instantiate(estrella, posicion, Quaternion.identity);
               estrella.SetActive(true); 
               Contadores.aux++;
           }
       }
   }
   
   //Se ejecuta al colisionar
   void OnCollisionEnter2D(Collision2D micolision){

       //transform.position es la posición de la bola
       //micolision contiene toda la información de la colisión
       //Si la bola colisiona con la raqueta:
       //micolision.gameObject es la raqueta
       //micolision.transform.position es la posición de la raqueta

       //Si choca con la raqueta izquierda
       if (micolision.gameObject.name == "ChicaIzq"){

           //Valor de x
           int x = 1;

           //Valor de y
           int y = direccionY(transform.position, micolision.transform.position);

           //Vector de dirección
           Vector2 direccion = new Vector2(x, y);

           //Aplico velocidad
           GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
           
       }

       //Si choca con la raqueta derecha
       if (micolision.gameObject.name == "ChicaDcha"){

           //Valor de x
           int x = -1;

           //Valor de y
           int y = direccionY(transform.position, micolision.transform.position);

           //Vector de dirección
           Vector2 direccion = new Vector2(x, y);

           //Aplico velocidad
           GetComponent<Rigidbody2D>().velocity = direccion * velocidad;

       }
       
       if (micolision.gameObject.name == "ChicaIzq2"){
           //Valor de x
           int x = 1;
           //Valor de y
           int y = direccionY(transform.position, micolision.transform.position);
           //Vector de dirección
           Vector2 direccion = new Vector2(x, y);
           //Aplico velocidad
           GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
       }

       //Si choca con la raqueta derecha
       if (micolision.gameObject.name == "ChicaDcha2"){
           //Valor de x
           int x = -1;
           //Valor de y
           int y = direccionY(transform.position, micolision.transform.position);
           //Vector de dirección
           Vector2 direccion = new Vector2(x, y);
           //Aplico velocidad
           GetComponent<Rigidbody2D>().velocity = direccion * velocidad;
       }
       
       fuenteDeAudio.clip = audioRaqueta;
       fuenteDeAudio.Play();
       if (micolision.gameObject.name == "Arriba" || micolision.gameObject.name == "Abajo"){

           //Reproduzco el sonido del rebote
           fuenteDeAudio.clip = audioRebote;
           fuenteDeAudio.Play();

       }
   }

//Método para calcular la direccion de Y (deevuelve un número entero int)
   int direccionY(Vector2 posicionBola, Vector2 posicionRaqueta)
   {

       if (posicionBola.y > posicionRaqueta.y)
       {
           return 1; //Si choca por la parte superior de la raqueta, sale hacia arriba
       }
       else if (posicionBola.y < posicionRaqueta.y)
       {
           return -1; //Si choca por la parte inferior de la raqueta, sale hacia abajo
       }
       else
       {
           return 0; //Si choca por la parte central de la raqueta, sale en horizontal
       }
   }
   
/* Añade como nuevo método ANTES de la última llave de cierre } de la clase */

//Reinicio la posición de la bola
   public void reiniciarBola(string direccion){

       //Posición 0 de la bola
       transform.position = Vector2.zero;
       //Vector2.zero es lo mismo que new Vector2(0,0);

       //Velocidad inicial de la bola
       velocidad = 30;

       //Velocidad y dirección
       if (direccion == "Derecha"){
           //Incremento goles al de la derecha
           Contadores.golesDerecha++;
           //Lo escribo en el marcador
           contadorDerecha.text = Contadores.golesDerecha.ToString();
           //Reinicio la bola
           GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
           //Vector2.right es lo mismo que new Vector2(1,0)
           if (!_gameManager.comprobarFinal()){
               GetComponent<Rigidbody2D>().velocity = Vector2.right * velocidad;
               //Vector2.right es lo mismo que new Vector2(1,0)
           }	
       }
       else if (direccion == "Izquierda"){
           //Incremento goles al de la izquierda
           Contadores.golesIzquierda++;
           //Lo escribo en el marcador
           contadorIzquierda.text = Contadores.golesIzquierda.ToString();
           //Reinicio la bola
           GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
           //Vector2.left es lo mismo que new Vector2(-1,0)
           if (!_gameManager.comprobarFinal()){
               GetComponent<Rigidbody2D>().velocity = Vector2.left * velocidad;
               //Vector2.left es lo mismo que new Vector2(-1,0)
           }
       }
       fuenteDeAudio.clip = audioGol;
       fuenteDeAudio.Play();
   }
   
   //Compruebo si alguno ha llegado a 5 goles
}