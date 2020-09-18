using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;

public class DebugConsole : MonoBehaviour
{
    [Header("Console Properties")]
    public bool showConsole = false;
    public bool showCommandHelp = false;
    private string inputString;
    private Vector2 commandListScroll;

    [Header("Console Commands")]
    public static DebugCommand setStone;
    public static DebugCommand setWood;
    public static DebugCommand<int> setGold;
    public static DebugCommand quitGame;
    public static DebugCommand help;

    public List<object> commandList;

    private void Awake()
    {
        quitGame = new DebugCommand("Quit_Game", "Quits the game entirely.", "Quit_Game", () =>
        {
            UnityEditor.EditorApplication.isPlaying = false;
        });

        setWood = new DebugCommand("Set_Wood", "Sets your current stone amount to 1000.", "Set_Wood", () =>
        {
            DummyGameManager.dummyGameManager._currentWood += 1000;
        });

        setGold = new DebugCommand<int>("Set_Gold", "Sets your current gold amount to the given number.", "Set_Gold <Gold_Amount>", (x) =>
        {
            DummyGameManager.dummyGameManager._currentGold += x;
        });

        help = new DebugCommand("Help", "Shows a list of commands.", "Help", () =>
        {
            showCommandHelp = true;
        });

        commandList = new List<object>
        {
            quitGame,
            setWood,
            setGold,
            help
        };
    }

    public void OnToggleDebug(bool value)
    {
        showConsole = value;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            showConsole = !showConsole;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (showConsole)
            {
                HandleInput();
                inputString = "";
            }
        }
    }

    private void OnGUI()
    {
        if (!showConsole)
        {
            return;
        }

        float y = 0f;

        if (showCommandHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect viewport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);
            commandListScroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), commandListScroll, viewport);

            for (int i = 0; i < commandList.Count; i++)
            {
                DebugCommandBase command = commandList[i] as DebugCommandBase;
                string label = $"{command._commandFormat} = {command._commandDescription}";
                Rect labelRect = new Rect(5, 20 * i, viewport.width - 100, 20);
                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();      
            y += 100;
        }

        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.Label(new Rect(0, y+ 30, Screen.width, 30), "Type 'Help' for all commands.");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        inputString = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), inputString);
    }

    private void HandleInput()
    {
        string[] properties = inputString.Split(' ');
        for (int i = 0; i < commandList.Count; i++)
        {
            DebugCommandBase commandBase = commandList[i] as DebugCommandBase;
            
            if (inputString.Contains(commandBase._commandID))
            {
                if (commandList[i] as DebugCommand != null)
                {
                    (commandList[i] as DebugCommand).Invoke();
                }

                else if (commandList[i] as DebugCommand<int> != null)
                {
                    (commandList[i] as DebugCommand<int>).Invoke(int.Parse(properties[1]));
                }
            }
        }
    }
}
