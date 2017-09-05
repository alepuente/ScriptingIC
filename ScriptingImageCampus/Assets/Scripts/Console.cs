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
        ConsoleParser.instance.RegisterCommand("moveto", MoveTo);
    }


    void Update()
    {

    }

    public void NewCommand(string command)
    {
        _text.text += command + "\n";
        _text.text += ConsoleParser.instance.ExecuteCommand(command) + "\n";
        _inputField.text = "";
    }

    public string MoveTo(params string[] args)
    {
        if (args.Length >= 3)
        {
            string aux = "";
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0; l < args[i].Length; l++)
                {
                    if (char.IsDigit(args[i][l])){}
                    else
                    {
                        return "Invalid Argument \"" + args[i][l] + "\"";
                    }
                }
                aux += args[i] + ",";
            }
            player.positions.Enqueue(aux);
            return "Moving to " + aux;
        }
        else
        {
            return "Not enought arguments";
        }
    }


}
