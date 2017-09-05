using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Command
{
	public string CommandName;
	public List<string> Args;
}

public class Parser 
{
	enum State
	{
		CommandName,
		Arguments
	}

	Tokenizer tokenizer = new Tokenizer();
	Command command = new Command();
	State state = State.CommandName;

	void Reset()
	{
		command.CommandName = null;
		command.Args = new List<string>();
		state = State.CommandName;
	}

	public bool Parse(string str, out Command cmd)
	{
		Reset();
		tokenizer.Start(str);

		cmd = command;

		Tokenizer.Token currentToken = tokenizer.GetNextToken();
	
		if (currentToken.Type == Tokenizer.TokenType.Empty)
		{
			return false;
		}
		
		while (currentToken.Type != Tokenizer.TokenType.EOL && currentToken.Type != Tokenizer.TokenType.Unknown)
		{
			switch(state)
			{
				case State.CommandName:
					if (currentToken.Type != Tokenizer.TokenType.Ident)
						return false;

					command.CommandName = currentToken.Lexeme; // Take current lexeme as command name
					currentToken = tokenizer.GetNextToken(); // skip to next token
					
					if (currentToken.Type == Tokenizer.TokenType.OpenParent)
					{
						state = State.Arguments;
						currentToken = tokenizer.GetNextToken(); // Skip parenthesis
					}
					else if (currentToken.Type == Tokenizer.TokenType.EOL)
						return true;
					else 
						return false;
				break;

				case State.Arguments:
					if (currentToken.Type == Tokenizer.TokenType.Number || currentToken.Type == Tokenizer.TokenType.String)
					{
						command.Args.Add(currentToken.Lexeme);
						currentToken = tokenizer.GetNextToken();
						
						if (currentToken.Type == Tokenizer.TokenType.Comma)
							currentToken = tokenizer.GetNextToken(); // skip comma
						else if (currentToken.Type == Tokenizer.TokenType.CloseParent)
						{
							currentToken = tokenizer.GetNextToken();

							if (currentToken.Type != Tokenizer.TokenType.EOL)
								return false;
							else
								return true;
						}
						else
							return false; // Syntax error! 
					}
					else if (currentToken.Type == Tokenizer.TokenType.CloseParent)
					{
						currentToken = tokenizer.GetNextToken();

						if (currentToken.Type != Tokenizer.TokenType.EOL)
							return false; // Syntax error! 
						else
							return true;
					}
					else
					{
						return false; // Syntax error!
					}
				break;
			}
		}

		return true;
	}

}
