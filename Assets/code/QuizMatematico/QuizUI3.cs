using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI3 : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private Image m_questionImage = null;
    [SerializeField] private List<OpcionesBoton3> m_buttonList = null;

    public void Construct(Pregunta3 q, Action<OpcionesBoton3> callback)
    {
        m_question.text = q.text;

        m_questionImage.sprite = q.imagenSprite;
        m_questionImage.gameObject.SetActive(true);

        for (int n = 0; n < m_buttonList.Count; n++)
        {
            m_buttonList[n].Construct(q.options[n], callback);
        }
    }
}
