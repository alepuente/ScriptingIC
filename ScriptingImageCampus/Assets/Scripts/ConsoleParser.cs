using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleParser : MonoBehaviour {

    public static ConsoleParser _instance;
    public Dictionary<string, string[]> _funcs;

    delegate void MyDelegate(params int[] parameters);
    MyDelegate _action;

	// Use this for initialization
	void Start () {
        _instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RegisterCommand(string key,string[] parameters)
    {
        _funcs.Add();
    }
}
