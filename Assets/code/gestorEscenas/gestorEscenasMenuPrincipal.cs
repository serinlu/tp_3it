using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class gestorEscenasMenuPrincipal : MonoBehaviour
{
    public bool pasarEscena;
    public int indiceEscena;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(pasarEscena)
        {
            cambiarEscena(indiceEscena);
        }
    }

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
}
