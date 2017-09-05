using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleParser : MonoBehaviour
{

    public delegate string ConsoleDelegate(params string[] arguments);
    private Dictionary<string, ConsoleDelegate> _funcs;
    private static ConsoleParser _instance = null;

    Parser parser = new Parser();

    public static ConsoleParser instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<ConsoleParser>();

            return _instance;
        }
    }
    // Use this for initialization
    void Start()
    {
        _instance = this;
        _funcs = new Dictionary<string, ConsoleDelegate>();
    }

    public bool isKeyContained(string key)
    {
        return _funcs.ContainsKey(key);
    }

    public string ExecuteCommand(string key)
    {
        Command cmd = null;

        if (parser.Parse(key, out cmd))
		{
			string cmdName = cmd.CommandName.ToLower();

            if (_funcs.ContainsKey(cmdName))
			{
				string log = "Executing command: " + cmdName + "\n";
				string[] args = cmd.Args.ToArray();

                log += _funcs[cmdName](args);
				
				return log;
			}
			else
			{
                return "ERROR: Command \"" + cmdName + "\" not found.";
			}
		}
		else
		{
			return "Syntax error!";
		}
    }

    public void RegisterCommand(string key, ConsoleDelegate del)
    {        
            _funcs[key] = del;
    }
}
