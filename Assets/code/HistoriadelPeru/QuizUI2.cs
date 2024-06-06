using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuizUI2 : MonoBehaviour
{
    [SerializeField] private Text m_pregunta = null;
    [SerializeField] private List<OptionButton2> m_buttonList = null;

    public void Construtc(Preguntas2 q,Action<OptionButton2> callback) 
    { 
        m_pregunta.text = q.text;

        for (int n = 0; n < m_buttonList.Count; n++)
        {
            m_buttonList[n].Construtc(q.opciones[n], callback);
        }
    }
}
