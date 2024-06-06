using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class OptionButton_3 : MonoBehaviour
{
    private Text m_text = null;
    private Button m_button = null;
    private Image m_image = null;
    private Color m_originalColor = Color.black;

    public Option_3 Option { get; private set; }

    private void Awake()
    {
        m_button = GetComponent<Button>();
        m_image = GetComponent<Image>();
        m_text = transform.GetChild(0).GetComponent<Text>();
        m_originalColor = m_image.color;
    }
    public void Construct(Option_3 option, Action<OptionButton_3> callback)
    {
        m_text.text = option.Text;

        m_button.onClick.RemoveAllListeners();
        m_button.enabled = true;
        m_image.color = m_originalColor;

        Option = option;

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
