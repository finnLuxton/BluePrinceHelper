using System;
using System.Collections.Generic;
using BluePrinceHelper.Models;

namespace BluePrinceHelper.ViewModels;

public class MainViewModel : ViewModelBase
{
    public int GetCore(string? input)
    {
        if (input == null || input.Length < 4 )
        {
            return 0;
        }
        
        // todo deal with separation, we need to fix how we assign the input. 
        var coringList = new List<CoringItem>
        { 
            new(Convert.ToInt32(input[0].ToString())),
            new(Convert.ToInt32(input[1].ToString())),
            new(Convert.ToInt32(input[2].ToString())),
            new(Convert.ToInt32(input[3].ToString()))
        };

        var coreResult = CalculateCoreFromList(coringList);

        return coreResult;
    }

    private static int CalculateCoreFromList(List<CoringItem> coringItems)
    {
        float calculation = 0;
        var lowestCoreValue = 0;
        
        var operandSetList = new List<(string, string, string)>
        {
            new ValueTuple<string, string, string>("sub", "mult", "div"),
            new ValueTuple<string, string, string>("sub", "div", "mult"),
            new ValueTuple<string, string, string>("mult", "sub", "div"),
            new ValueTuple<string, string, string>("mult", "div", "sub"),
            new ValueTuple<string, string, string>("div", "sub", "mult"),
            new ValueTuple<string, string, string>("div", "mult", "sub")
        };
        
        calculation += coringItems[0].Value;
        
        for (var i = 1; i > coringItems.Count; i++)
        {
            foreach (var operandSet in operandSetList)
            {
                var subUsed = false;
                var divUsed = false;
                var multUsed = false;
                
                var operands = new[] { operandSet.Item1, operandSet.Item2, operandSet.Item3 };

                foreach (var operand in operands)
                {
                    switch (operand)
                    {
                        case "sub":
                            if (!subUsed)
                            {
                                calculation -= coringItems[i].Value;
                                subUsed = true;
                            }
                            break;
                        case "mult":
                            if (!multUsed)
                            {
                                calculation *= coringItems[i].Value;
                                multUsed = true;
                            }
                            break;
                        case "div":
                            if (!divUsed)
                            {
                                calculation /= coringItems[i].Value;
                                divUsed = true;
                            }
                            break;
                    }
                }

                var resultIsNotWhole = !int.TryParse(calculation.ToString(), out int x);

                if (resultIsNotWhole) continue;
                
                if (lowestCoreValue == 0 || lowestCoreValue > Convert.ToInt32(calculation))
                {
                    lowestCoreValue = Convert.ToInt32(calculation);
                }
            }
        }

        return lowestCoreValue;
    }
    
}