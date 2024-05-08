using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionQuest
{
    internal class ReflectionHelper
    {
        public MethodInfo GetNonStaticPublicClassMemberMethodWithLargestArgumentList(string assemblyPath)
        {
            Console.WriteLine("Searching for the non-static public class member method with the largest argument list...");
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                MethodInfo largestMethod = assembly.GetTypes()
                    .SelectMany(type => type.GetMethods(BindingFlags.Public | BindingFlags.Instance)) // Get all public instance methods
                    .OrderByDescending(method => method.GetParameters().Length) // Order methods by the number of parameters
                    .FirstOrDefault(); // Get the method with the most parameters

                if (largestMethod != null)
                {
                    Console.WriteLine($"Method with most parameters: {largestMethod.Name}, Parameter count: {largestMethod.GetParameters().Length}");
                    return largestMethod;
                }
                else
                {
                    Console.WriteLine("No methods found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return null; // Return null if no method is found
        }
    

    public void FindMethodWithLocalVariablesOfTypeIntAndBool(string assemblyPath)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                foreach (Type type in assembly.GetTypes())
                {
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        // Get the method body if it is available
                        MethodBody body = method.GetMethodBody();
                        if (body != null)
                        {
                            bool hasInt = false;
                            bool hasBool = false;

                            foreach (LocalVariableInfo localVariable in body.LocalVariables)
                            {
                                if (localVariable.LocalType == typeof(int))
                                {
                                    hasInt = true;
                                }
                                else if (localVariable.LocalType == typeof(bool))
                                {
                                    hasBool = true;
                                }

                                // If both types are found, print the method name and break
                                if (hasInt && hasBool)
                                {
                                    Console.WriteLine($"Method {method.Name} in type {type.FullName} has both int and bool local variables.");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }


        public Type GetTypeThatImplementsIEnumerable(string assemblyPath)
        {
            Console.WriteLine("Searching for a type that implements IEnumerable...");
            try
            {
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IEnumerable).IsAssignableFrom(type))
                    {
                        Console.WriteLine($"Type {type.Name} implements IEnumerable.");
                        return type; // Retorna el primer tipo encontrado que implementa IEnumerable
                    }
                }

                Console.WriteLine("No types implementing IEnumerable were found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return null; // Retorna null si no se encuentra ningún tipo que implemente IEnumerable
        }
    
            public Type GetTypeThatHasNestedTypeInSpanish(string assemblyPath)
            {
                Console.WriteLine("Searching for a type that has a nested type with a Spanish name...");
                try
                {
                    Assembly assembly = Assembly.LoadFrom(assemblyPath);
                    foreach (Type type in assembly.GetTypes())
                    {
                        foreach (Type nestedType in type.GetNestedTypes())
                        {
                            // Verifica si el nombre del tipo anidado contiene caracteres típicos del español
                            if (nestedType.Name.Any(c => "áéíóúñÁÉÍÓÚÑ".Contains(c)))
                            {
                                Console.WriteLine($"Type {type.Name} has a nested type with a Spanish name: {nestedType.Name}");
                                return nestedType; // Retorna el primer tipo anidado encontrado con nombre en español
                            }
                        }
                    }

                    Console.WriteLine("No nested types with Spanish names were found.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
                return null; // Retorna null si no se encuentra ningún tipo anidado con nombre en español
            }
        }
    
}
