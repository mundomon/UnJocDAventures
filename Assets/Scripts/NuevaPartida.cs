using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NuevaPartida : MonoBehaviour {

    public void CargarPartidaNueva(){
        SceneManager.LoadScene("Partida Nueva");
    }
}
