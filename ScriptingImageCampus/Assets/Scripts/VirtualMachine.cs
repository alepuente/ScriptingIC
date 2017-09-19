using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualMachine 
{
	List<Instruction> program;
	private int PC = 0; // Program counter

	public void Reset(List<Instruction> program)
	{
		this.program = program;
		PC = 0;
	}

	public void RunStep()
	{
		if (program != null && PC >= 0 && PC < program.Count)
		{
			Instruction op = program[PC];

			switch(op.OpCode)
			{
				case OpCodes.MOVETO:
					MoveTo(op.Arguments);
				break;

				case OpCodes.GOTO:
					GoTo(op.Arguments);
				break;

				case OpCodes.NOP:
				break;
			}

			PC++;
		}
	}

    void MoveTo(List<string> args)
	{
        bool isNumber = true;
		if (args.Count >= 3)
        {
            string aux = "";
            for (int i = 0; i < 3; i++)
            {
                for (int l = 0; l < args[i].Length; l++)
                {
                    if (char.IsDigit(args[i][l]) && isNumber)
                    {
                        isNumber = true;
                    }
                    else
                    {
                        isNumber = false;
                    }
                }
                if (isNumber)
                aux += args[i] + ",";  
            }
            if (isNumber)
            Scriptable._instance.positions.Enqueue(aux);
        }
	}

	void GoTo(List<string> args)
	{
		if (args.Count > 0)
		{
			int jmpIdx = -1;

			if (int.TryParse(args[0], out jmpIdx))
				PC = jmpIdx;
		}		
	}
	
}
