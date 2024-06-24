using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class QuizDB3 : MonoBehaviour
{
    [SerializeField] private List<Pregunta3> m_questionList = null;

    private List<Pregunta3> m_backup = null;

    private void Awake()
    {
        m_backup = m_questionList.ToList();
    }
    public Pregunta3 GetRandom(bool remove = true)
    {
        if(m_questionList.Count == 0) 
            RestoreBackup();

        int index = Random.Range(0, m_questionList.Count);

        if(!remove) 
            return m_questionList[index];

        Pregunta3 q = m_questionList[index];
        m_questionList.RemoveAt(index);

        return q;
    }

    private void RestoreBackup()
    {
        m_questionList = m_backup.ToList();
    }
}
