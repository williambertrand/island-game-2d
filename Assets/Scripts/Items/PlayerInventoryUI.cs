using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[Serializable]
public struct UIReesourceItem
{
    public string id;
    public TextMeshProUGUI uiComponent;
}

public class PlayerInventoryUI : MonoBehaviour
{


    public static PlayerInventoryUI Instance;

    [SerializeField]
    public UIReesourceItem[] resourceTexts; 

    public Dictionary<string, TextMeshProUGUI> uiTextElements;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        uiTextElements = new Dictionary<string, TextMeshProUGUI>();
        foreach(UIReesourceItem item in resourceTexts)
        {
            uiTextElements[item.id] = item.uiComponent;
        }

        resourceTexts = null;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateValue(string id, int val)
    {
        uiTextElements[id].text = "" + val;
    }
}
