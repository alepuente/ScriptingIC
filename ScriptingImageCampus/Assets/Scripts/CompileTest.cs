﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompileTest : MonoBehaviour 
{
	public InputField inputField;

	Compiler compiler = new Compiler();	
	VirtualMachine vm = new VirtualMachine();


	public void OnClick()
	{
		List<Instruction> program; 

		if (compiler.Compile(inputField.text, out program))
		{
			vm.Reset(program);
		}
	}

	void Update()
	{
		vm.RunStep();
	}
}
