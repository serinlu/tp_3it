using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI1 : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private List<OptionButton1> m_buttonlist = null;

    public void Construct(Question1 q,Action<OptionButton1> callback)
    {
        m_question.text = q.text;

        for(int i=0;i<m_buttonlist.Count;i++)
        {
            m_buttonlist[i].Construct(q.options[i],callback);
        }
    }
}
