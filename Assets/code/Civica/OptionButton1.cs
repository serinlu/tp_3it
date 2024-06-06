using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OptionButton1 : MonoBehaviour
{
    private Button m_button = null; 
    private Image m_image = null;
    private Color m_originalColor= Color.black;
    private Text m_text = null;
    public Option1 Option { get; set; }

    private void Awake()
    {
        m_button=GetComponent<Button>();
        m_image=GetComponent<Image>();
        m_text=transform.GetChild(0).GetComponent<Text>();
        m_originalColor = m_image.color;
    }

    public void Construct(Option1 option, Action<OptionButton1> callback)
    {
        m_text.text = option.text;

        m_button.onClick.RemoveAllListeners();
        m_button.enabled = true;
        m_image.color = m_originalColor;
        Option = option;

        m_button.onClick.AddListener(delegate 
        {
            callback(this);
        }) ;

    }

    public void SetColor(Color c)
    {
        m_button.enabled = false;
        m_image.color = c;
    }
}
