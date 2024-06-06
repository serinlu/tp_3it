using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;



public class QuizDB4 : MonoBehaviour
{
    [SerializeField] private List<Question4> m_questionList = null;

    private List<Question4> m_backup = null;
    private void Awake()
    {
        m_backup = m_questionList.ToList();
    }
    public Question4 GetRandom(bool remove=true)
    {
        if(m_questionList.Count==0)
        {
            RestoreBackup();
        }
        int index=Random.Range(0,m_questionList.Count);

        if (!remove)
        {
            return m_questionList[index];
        }

        Question4 q = m_questionList[index];
        m_questionList.RemoveAt(index);

        return q;
    }

    private void RestoreBackup()
    {
            m_questionList=m_backup.ToList();
    }
}
