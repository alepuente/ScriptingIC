using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class Scriptable : MonoBehaviour {

    Script script = new Script();
    public string[] scenePositions;
    public  Queue<string> positions;
    public int speed;


	// Use this for initialization
	void Start () {
        string scriptCode = @"    			
			function update (position, dt)
				obj.Move(position, dt)
			end";

        UserData.RegisterType<Scriptable>();

        DynValue obj = UserData.Create(this);

        script.Globals.Set("obj", obj);
        script.DoString(scriptCode);

        positions = new Queue<string>();

        foreach (string item in scenePositions)
        {
            positions.Enqueue(item);
        }
	}

    public void Move(string pos, float time)
    {
        string[] aux = pos.Split(',');
        Vector3 auxVec;
        auxVec = new Vector3(int.Parse(aux[0]), int.Parse(aux[1]), int.Parse(aux[2]));

        if (Vector3.Distance(transform.position, auxVec) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, auxVec, time * speed);
        }
        else
        {
            positions.Dequeue();
        }
    }

    void Update()
    {
        if (positions.Count > 0)
        {            
        script.Call(script.Globals["update"], positions.Peek(),Time.deltaTime);
        }
    }
}
