using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public InputField _inputField;
    public Text _text;

    void Start()
    {

    }


    void Update()
    {

    }

    public void NewCommand(string command)
    {
        _text.text += command + "\n";      
        string[] auxString = command.Split(' ');
        auxString[0].ToUpper();
        if (ConsoleParser._instance._funcs.ContainsKey(auxString[0]))
        {
            //_action = auxString[0];
        }
        else
        {
            string[] aux;
            for (int i = 1; i < auxString.Length - 1; i++)
            {
                for (int x = 0; i < auxString.Length - 1; i++)
                {
                    aux[x] = auxString[i];
                }
            }
            ConsoleParser._instance.RegisterCommand(aux);
        }*/
        _inputField.text = "";
    }

    
}
