using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public InputField _inputField;
    public Text _text;
    public Scriptable player;

    void Start()
    {
        ConsoleParser._instance.RegisterCommand("moveto", MoveTo);
    }


    void Update()
    {

    }

    public void NewCommand(string command)
    {
        _text.text += command + "\n";
        Debug.Log("All" + command);
        string[] splitNewLine = command.Split('\n');
       
        foreach (string newline in splitNewLine)
        {
            Debug.Log("newline" + newline);
            string[] auxString = newline.Split(' ');
            foreach (var item in auxString)
            {
                Debug.Log("blank" + item);
            }
            if (ConsoleParser._instance.isKeyContained(auxString[0]))
            {
                ConsoleParser._instance.ExecuteCommand(auxString[0])(newline);
            }
            else
            {
                _text.text += "Command not found  \n";
            }
        }
        _inputField.text = "";
    }

    public void MoveTo(string str)
    {
        string[] auxString = str.Split(' ');
        player.positions.Enqueue(auxString[1]);
    }


}
