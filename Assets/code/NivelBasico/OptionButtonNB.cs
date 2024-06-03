using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class OptionButtonNB : MonoBehaviour
{
    private Text m_text = null;
    private Button m_button = null;
    private Image m_image = null;
    private Color m_originaColor = Color.black;

    public OptionNB Option { get; set; }

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_image = GetComponent<Image>();
        m_text = transform.GetChild(0).GetComponent<Text>();

        m_originaColor = m_image.color;
    }
    public void Construct(OptionNB options, Action<OptionButtonNB> callback)
    {
        m_text.text = options.text;

        m_button.onClick.RemoveAllListeners();
        m_button.enabled = true;
        m_image.color = m_originaColor;

        Option = options;

        m_button.onClick.AddListener(delegate
        {
            callback(this);
        });
    }
    public void SetColor(Color c)
    {
        m_button.enabled = false;
        m_image.color = c;
    }
}
