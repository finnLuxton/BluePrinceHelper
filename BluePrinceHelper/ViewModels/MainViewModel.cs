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

    // Could update the function to return a result list?
    private static int CalculateCoreFromList(List<CoringItem> coringItems)
    {
        float calculation = 0;
        var lowestCoreValue = 0;

        var operandSet = new List<(string, string, string)>
        {
            new ValueTuple<string, string, string>("sub", "mult", "div"),
            new ValueTuple<string, string, string>("sub", "div", "mult"),
            new ValueTuple<string, string, string>("mult", "sub", "div"),
            new ValueTuple<string, string, string>("mult", "div", "sub"),
            new ValueTuple<string, string, string>("div", "sub", "mult"),
            new ValueTuple<string, string, string>("div", "mult", "sub")
        };

        foreach (var operandItem in operandSet)
        {
            calculation = 0;
            calculation += coringItems[0].Value;

            foreach (var item in coringItems.Skip(1))
            {
                if (operandItem.Item1 == "sub")
                {
                    calculation -= item.Value;
                }

                if (operandItem.Item2 == "mult")
                {
                    calculation *= item.Value;
                }

                if (operandItem.Item3 == "div")
                {
                    calculation /= item.Value;
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