using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleParser : MonoBehaviour
{

    public delegate void ConsoleDelegate(string str);
    private Dictionary<string, ConsoleDelegate> _funcs;

    public static ConsoleParser _instance;
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

    public ConsoleDelegate ExecuteCommand(string key)
    {
        return _funcs[key];
    }

    public void RegisterCommand(string key, ConsoleDelegate del)
    {        
            _funcs.Add(key, del);
    }
}
