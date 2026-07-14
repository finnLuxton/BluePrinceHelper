using System;
using System.Collections.Generic;
using System.Linq;
using BluePrinceHelper.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BluePrinceHelper.ViewModels;

public partial class MainViewModel : ViewModelBase
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
        
        // todo Check for more separations, or if core whole number has digits greater than 4

        return coreResult;

    }

    private static int CalculateCoreFromList(List<CoringItem> coringItems)
    {
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

        foreach (var operandSet in operandSetList)
        {
            float calculation = coringItems[0].Value;

            foreach (var item in coringItems.Skip(1))
            {
                foreach (string operand in new[] {operandSet.Item1, operandSet.Item2, operandSet.Item3})
                {
                    if (operand == "sub")
                    {
                        calculation -= item.Value;
                    }

                    if (operand == "mult")
                    {
                        calculation *= item.Value;
                    }

                    if (operand == "div")
                    {
                        calculation /= item.Value;
                    }
                }
            }

            var calcIsWholeNumber = int.TryParse(calculation.ToString(), out int x);
            
            if (calcIsWholeNumber)
            {
                if (lowestCoreValue == 0 || lowestCoreValue > Convert.ToInt32(calculation))
                {
                    lowestCoreValue = Convert.ToInt32(calculation);
                }
            }
            
        }

        return lowestCoreValue;
    }
    
}