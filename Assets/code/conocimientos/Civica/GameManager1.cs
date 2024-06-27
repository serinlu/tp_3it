using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class GameManager1 : MonoBehaviour
{
    [SerializeField] private AudioClip m_correctSound = null;
    [SerializeField] private AudioClip m_incorrectSound = null;
    [SerializeField] private Color m_correctColor = Color.black;
    [SerializeField] private Color m_incorrectColor= Color.black;
    [SerializeField] private float m_waitTime = 0.0f;
    private QuizDB1 m_quizDB = null;
    private QuizUI1 m_quizUI = null;
    private AudioSource m_audioSource = null;
    public Text puntajeCorrect;
    public Text puntajeIncorrect;
    public GameObject finalCanvas;
    public GameObject[] pantallaFinal;
    public GameObject canvasPrincipal;
    public GameObject canvasPausa;
    public Button[] botones; 
    public int maxpuntajeCorrect;
    public int maxpuntajeIncorrect;
    int correct = 0;
    int incorrect = 0;
    public bool pasarEscena;
    public int indiceEscena;

    private void Start()
    {
        m_quizDB = GameObject.FindFirstObjectByType<QuizDB1>();
        m_quizUI = GameObject.FindFirstObjectByType<QuizUI1>();
        m_audioSource = GetComponent<AudioSource>();
        finalCanvas.SetActive(false);
        pantallaFinal[0].SetActive(false);
        canvasPausa.SetActive(false);
        canvasPrincipal.SetActive(true);
        foreach (Button boton in botones)
        {
            boton.interactable = true;
        }
        NextQuestion();
    }

    private void NextQuestion()
    {
        m_quizUI.Construct(m_quizDB.GetRandom(),GiveAnswer);
    }

    private void GiveAnswer(OptionButton1 optionButton)
    {
        StartCoroutine(GiveAnswerRoutin(optionButton));
    }

    private IEnumerator GiveAnswerRoutin(OptionButton1 optionButton)
    {
        if (m_audioSource.isPlaying)
        {
            m_audioSource.Stop();
        }

        //m_audioSource.clip = optionButton.Option.correcto ? m_correctSound : m_incorrectSound;
        //optionButton.SetColor(optionButton.Option.correcto ? m_correctColor : m_incorrectColor);

        //m_audioSource.Play();
        //if(optionButton.Option.correcto)
        //{
        //    correct = correct + 1;
        //    puntajeCorrect.text=correct.ToString();
        //}
        //else
        //{
        //    incorrect=incorrect+ 1;
        //    puntajeIncorrect.text=incorrect.ToString();
        //}

        //yield return new WaitForSeconds(m_waitTime);
        //if (optionButton.Option.correcto)
        //    NextQuestion();
        //else
        //    GameOver();

        m_audioSource.clip = optionButton.Option.correcto ? m_correctSound : m_incorrectSound;
        optionButton.SetColor(optionButton.Option.correcto ? m_correctColor : m_incorrectColor);
       
        m_audioSource.Play();

        yield return new WaitForSeconds(m_waitTime);

        if (optionButton.Option.correcto)
        {
            correct++;
            puntajeCorrect.text=correct.ToString();
        }
        else
        {
            incorrect++;
            puntajeIncorrect.text=incorrect.ToString();
        }
        NextQuestion();

        if (correct == maxpuntajeCorrect)
        {
            finalCanvas.SetActive(true);
            canvasPrincipal.SetActive(false);
            pantallaFinal[0].SetActive(true); 
        }
        else if (incorrect == maxpuntajeIncorrect)
        {
            finalCanvas.SetActive(true);
            pantallaFinal[1].SetActive(true);
            canvasPrincipal.SetActive(false);
            
        }

        if (pasarEscena)
        {
            cambiarEscena(indiceEscena);
        }

    }
    public void RepetirEjercicio()
    {
        correct = 0;
        incorrect = 0;
        puntajeCorrect.text = correct.ToString();
        puntajeIncorrect.text = incorrect.ToString();
        finalCanvas.SetActive(false);
        canvasPrincipal.SetActive(true);

        Start();
    }
    //private void GameOver()
    //{
    //    SceneManager.LoadScene(9);
    //}

    public void cambiarEscena(int indice)
    {
        SceneManager.LoadScene(indice);
    }
    public void PauseGame()
    {
        canvasPrincipal.SetActive(false);
        canvasPausa.SetActive(true);
    }

    public void ResumeGame()
    {
        canvasPausa.SetActive(false);
        canvasPrincipal.SetActive(true);
        
    }

}
