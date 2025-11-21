using Client;

Console.WriteLine("=== Strawberry Shake Bug Demonstration ===");
Console.WriteLine();
Console.WriteLine("This demonstrates a name clash issue when a GraphQL field is named 'equals'");
Console.WriteLine();

// Create an instance of the generated type
var stringMatch = new StringMatch 
{ 
    Equals = "test value" 
};

Console.WriteLine("✓ Created StringMatch instance");
Console.WriteLine($"  StringMatch.Equals property value: {stringMatch.Equals}");
Console.WriteLine();

// Demonstrate the ambiguity problem
Console.WriteLine("ISSUE: The property name 'Equals' clashes with object.Equals(object? obj)");
Console.WriteLine();
Console.WriteLine("Examples of the ambiguity:");

// This is ambiguous - is it calling the method or accessing the property?
// Without proper context, the compiler might not know which one you mean
Console.WriteLine($"  - stringMatch.Equals is a property: {stringMatch.Equals is string}");

// Trying to compare using Equals method becomes problematic
var another = new StringMatch { Equals = "test value" };
try
{
    // This will access the property, not call the Equals method!
    var result = stringMatch.Equals;  // Gets property value, not method
    Console.WriteLine($"  - Attempting to use Equals gives property value: {result}");
}
catch (Exception ex)
{
    Console.WriteLine($"  - Error: {ex.Message}");
}

Console.WriteLine();
Console.WriteLine("To actually call object.Equals(), you must use ((object)stringMatch).Equals(another)");
Console.WriteLine($"  Comparison result: {((object)stringMatch).Equals(another)}");
Console.WriteLine();
Console.WriteLine("This demonstrates how the 'equals' field name in GraphQL creates");
Console.WriteLine("confusing and error-prone code when generated as a C# property.");
