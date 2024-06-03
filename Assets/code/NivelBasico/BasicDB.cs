using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicDB : MonoBehaviour
{
    [SerializeField] private List<QuestionNB> m_questionlist = null;
    public int CantidadPreguntas { get; private set; }
    private List<QuestionNB> m_backup = null;

    void Start()
    {
        CantidadPreguntas = m_questionlist.Count;

    }
    private void Awake()
    {
        m_backup = m_questionlist.ToList();

    }

    public QuestionNB GetRandom(bool remove = true)
    {
        if (m_questionlist.Count == 0)
            RestoreBackup();

        int index = Random.Range(0, m_questionlist.Count);

        if (!remove)
            return m_questionlist[index];


        QuestionNB q = m_questionlist[index];        
        m_questionlist.RemoveAt(index);

        return q;
    }
 
    private void RestoreBackup()
    {
        m_questionlist = m_backup.ToList();
    }
    public int CantPreguntas()
    {
        return m_questionlist.Count;
    }
}
