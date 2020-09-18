using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyGameManager : MonoBehaviour
{
    [Header("Resource Properties")]
    private int currentGold;
    private int currentStone;
    private int currentWood;

    public int _currentGold
    {
        get { return this.currentGold; }
        set 
        {
            currentGold = value;
            goldUITextField.text = "Current Gold: " + currentGold.ToString();
        }
    }
    public int _currentStone
    {
        get { return this.currentStone; }
        set
        {
            currentStone = value;
            stoneUITextField.text = "Current Stone: " + currentStone.ToString();
        }
    }

    public int _currentWood
    {
        get { return this.currentWood; }
        set
        {
            currentWood = value;
            woodUITextField.text = "Current Wood: " + currentWood.ToString();
        }
    }

    [Header("UI Properties")]
    [SerializeField] private Text goldUITextField;
    [SerializeField] private Text stoneUITextField;
    [SerializeField] private Text woodUITextField;

    public static DummyGameManager dummyGameManager;

    private void Awake()
    {
        dummyGameManager = this;
        _currentGold = 0;
        _currentStone = 0;
        _currentWood = 0;
    }

}
