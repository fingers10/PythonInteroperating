using IronPython.Hosting;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
using System.Dynamic;
using System.Text;
using static System.Console;

// Simple Expression
//ScriptEngine engine = Python.CreateEngine();
//string simpleExpression = "2 + 2";

//dynamic dynamicResult = engine.Execute(simpleExpression);
//int typedResult = engine.Execute<int>(simpleExpression);

//WriteLine($"dynamic result {dynamicResult}");
//WriteLine($"typed result {typedResult}");

//WriteLine("Press enter to exit");
//ReadLine();

//===================================================================================

// Simple Expression - Getting from User
//ScriptEngine engine = Python.CreateEngine();
//int employeeAge = 30;

//WriteLine("Please enter an expression (use token 'a' for employee age) and press enter");
//string simpleExpression = ReadLine()!;

//ScriptScope scope = engine.CreateScope();
//scope.SetVariable("a", employeeAge);

//ScriptSource scriptSource = engine.CreateScriptSourceFromString(simpleExpression, SourceCodeKind.Expression);

//dynamic dynamicResult = scriptSource.Execute(scope);
//WriteLine($"dynamic result {dynamicResult}");

//WriteLine("Press enter to exit");
//ReadLine();

//===================================================================================

// Single Statement
//ScriptEngine engine = Python.CreateEngine();
//int employeeAge = 30;

//WriteLine("Please enter a statement");
//string singleStatement = ReadLine()!;

//ScriptScope scope = engine.CreateScope();
//scope.SetVariable("a", employeeAge);

//ScriptSource scriptSource = engine.CreateScriptSourceFromString(singleStatement, SourceCodeKind.SingleStatement);

//scriptSource.Execute(scope);

//dynamic dynamicResult = scope.GetVariable("result");
//WriteLine($"Statement result {dynamicResult}");

//WriteLine("Press enter to exit");
//ReadLine();

//===================================================================================

// Python Object
//ScriptEngine engine = Python.CreateEngine();
//string tupleStatement = "employee = ('Abdul Rahman', 42)";

//ScriptScope scope = engine.CreateScope();

//ScriptSource scriptSource = engine.CreateScriptSourceFromString(tupleStatement, SourceCodeKind.SingleStatement);

//scriptSource.Execute(scope);

//dynamic pythonTuple = scope.GetVariable("employee");
//WriteLine($"Name {pythonTuple[0]} Age {pythonTuple[1]}");

//WriteLine("Press enter to exit");
//ReadLine();

//===================================================================================

// Passing custom object to External File
ScriptEngine engine = Python.CreateEngine();
HtmlElement image = new("img");

ScriptScope scope = engine.CreateScope();
scope.SetVariable("image", image);

ScriptSource scriptSource = engine.CreateScriptSourceFromFile("SetImageAttributes.py");

WriteLine($"image before executing python {image}");

scriptSource.Execute(scope);

WriteLine($"image after executing python {image}");

WriteLine("Press enter to exit");
ReadLine();

public class HtmlElement : DynamicObject
{
	private readonly Dictionary<string, object?> _attributes = new();
	private readonly string tagName;

	public HtmlElement(string tagName)
	{
		this.tagName = tagName;
	}

	public override bool TrySetMember(SetMemberBinder binder, object? value)
	{
		return _attributes.TryAdd(binder.Name, value);
	}

	public override bool TryGetMember(GetMemberBinder binder, out object? result)
	{
		result = _attributes[binder.Name];

		return true;
	}

	public override string ToString()
	{
		var sb = new StringBuilder();
		sb.Append($"<{tagName} ");

		foreach (var attribute in _attributes)
		{
			sb.Append($"{attribute.Key}=\"{attribute.Value}\" ");
		}

		sb.Append("/>");

		return sb.ToString();
	}
}

//===================================================================================