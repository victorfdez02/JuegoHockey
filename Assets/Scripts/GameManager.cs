using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField] private Text temporizador;
    [SerializeField] private Text resultado;
    [SerializeField] private GameObject cuadradoFinal;
    
    [SerializeField] private Text contadorIzquierda;
    [SerializeField] private Text contadorDerecha;
    
    private void Start()
    {
        resultado.enabled = false;
    }

    void Update () {
        //Si pulsa la tecla P o hace clic izquierdo empieza el juego
        if (Input.GetKeyDown(KeyCode.P) || Input.GetMouseButton(0)){
            //Cargo la escena de Juego
            Contadores.tiempo = 180;
            SceneManager.LoadScene("Juego");
        }
        
        if (Input.GetKeyDown(KeyCode.I)){
            //Cargo la escena de Inicio
            SceneManager.LoadScene("Inicio");
        }
        
        if (Contadores.tiempo >= 0){
            Contadores.tiempo -= Time.deltaTime; //Le resto el tiempo transcurrido en cada frame
            temporizador.text = formatearTiempo(Contadores.tiempo); 
            //Formateo el tiempo y lo escribo en la caja de texto
        }
//Si se ha acabado el tiempo, compruebo quién ha ganado y se acaba el juego
        else{
            temporizador.text = "00:00"; //Para evitar valores negativos	
            //Compruebo quién ha ganado
            if (Contadores.golesIzquierda > Contadores.golesDerecha){
                //Escribo y muestro el resultado
                resultado.text = "¡Jugador Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            else if (Contadores.golesDerecha > Contadores.golesIzquierda){
                //Escribo y muestro el resultado
                resultado.text = "¡Jugador Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            else{
                //Escribo y muestro el resultado
                resultado.text = "¡EMPATE!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            //Muestro el resultado, pauso el juego y devuelvo true
            resultado.enabled = true;
            Time.timeScale = 0; //Pausa
        }

    }
    string formatearTiempo(float tiempo){

        //Formateo minutos y segundos a dos dígitos
        string minutos = Mathf.Floor(tiempo / 60).ToString("00");
        string segundos = Mathf.Floor(tiempo % 60).ToString("00");
    
        //Devuelvo el string formateado con : como separador
        return minutos + ":" + segundos;
  
    }
    
    public bool comprobarFinal(){
        if (Contadores.tiempo <= 0 )
        {
            if (Contadores.golesDerecha < Contadores.golesIzquierda)
            {
                resultado.enabled = true;
                cuadradoFinal = Instantiate(cuadradoFinal, new Vector3(0,0), Quaternion.identity);
                cuadradoFinal.SetActive(true); 
                
                //Escribo y muestro el resultado
                resultado.text = "¡Equipo de la Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
            else
            {
                resultado.enabled = true;
                cuadradoFinal = Instantiate(cuadradoFinal, new Vector3(0,0), Quaternion.identity);
                cuadradoFinal.SetActive(true); 
                //Escribo y muestro el resultado
                resultado.text = "¡Equipo Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            }
        }
        
        //Si el de la izquierda ha llegado a 5
        if (Contadores.golesIzquierda == 5){
            Time.timeScale = 0; //Pausa
            cuadradoFinal = Instantiate(cuadradoFinal, new Vector3(0,0), Quaternion.identity);
            cuadradoFinal.SetActive(true);
            resultado.enabled = true;
            //Escribo y muestro el resultado
            resultado.text = "¡Equipo de la Izquierda GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            //Muestro el resultado, pauso el juego y devuelvo true
            Contadores.golesDerecha = 0;
            Contadores.golesIzquierda = 0;
            return true;
        }
        //Si el de le aderecha a llegado a 5
        else if (Contadores.golesDerecha == 5){
            Time.timeScale = 0; //Pausa
            cuadradoFinal = Instantiate(cuadradoFinal, new Vector3(0,0), Quaternion.identity);
            cuadradoFinal.SetActive(true); 

            resultado.enabled = true;
            //Escribo y muestro el resultado
            resultado.text = "¡Esquipo Derecha GANA!\nPulsa I para volver a Inicio\nPulsa P para volver a jugar";
            //Muestro el resultado, pauso el juego y devuelvo true
            Contadores.golesDerecha = 0;
            Contadores.golesIzquierda = 0;
            return true;
        }
        //Si ninguno ha llegado a 5, continúa el juego
        else{
            return false;
        }
    }
}