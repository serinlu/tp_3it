using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizUI5 : MonoBehaviour
{
    [SerializeField] private Text m_question = null;
    [SerializeField] private List<OptionButton5> m_buttonlist = null;


    public void Construct(Question5 q,Action<OptionButton5> callback)
    {
        m_question.text = q.text;

        for(int i=0;i<m_buttonlist.Count;i++)
        {
            m_buttonlist[i].Construct(q.options[i],callback);
        }

    }

}
