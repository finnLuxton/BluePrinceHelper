using System;
using System.Collections.Generic;

namespace BluePrinceHelper.ViewModels;

public class MainViewModel : ViewModelBase
{
    public (int lowestCoreResult, string lowestCoreWorking) GetCore(string? input)
    {
        var lowestCoreResult = 0;
        var lowestCoreWorking = "";
        if (input == null || input.Length < 4)
            return (0, "");

        var coringList = GetDelimitedCoringItemList(input);

        foreach (var coringSet in coringList)
        {
            var (coreFromSet, workingFromSet) = CalculateCoreFromList(coringSet);

            if ((coreFromSet < lowestCoreResult && coreFromSet > 0) || lowestCoreResult == 0) 
            {
                lowestCoreResult = coreFromSet;
                lowestCoreWorking = workingFromSet;
            }
        }

        return (lowestCoreResult, lowestCoreWorking);
    }

    public List<List<int>> GetDelimitedCoringItemList(string input)
    {
        List<List<int>> resultList = [];

        for (var delimOne = 1; delimOne < input.Length - 2; delimOne++)
        {
            for (var delimTwo = delimOne + 1; delimTwo < input.Length - 1; delimTwo++)
            {
                for (var delimThree = delimTwo + 1; delimThree < input.Length; delimThree++)
                {
                    resultList.Add([
                        Convert.ToInt32(input[..delimOne]),
                        Convert.ToInt32(input[delimOne..delimTwo]),
                        Convert.ToInt32(input[delimTwo..delimThree]),
                        Convert.ToInt32(input[delimThree..])
                    ]);
                }
            }
        }
        
        return resultList;
    }

    public string GetIntCoringInputFromNonNumericInput(string input)
    {
        var result = "";
        
        foreach (char c in input)
        {
            char upper = char.ToUpper(c);
            if (upper < 'A' || upper > 'Z')
            {
                throw new ArgumentOutOfRangeException("c", "This method only accepts standard Latin characters.");
            }
            result += upper - 'A' + 1;
        }
        
        return result;
    }
    
    public (int lowestCoreValue, string lowestCoreWorking) CalculateCoreFromList(List<int> coringItems)
    {
        var lowestCoreValue = 0;
        var lowestCoreWorking = "";

        var operandSetList = new List<(string, string, string)>
        {
            new ValueTuple<string, string, string>("div", "sub", "mult"),
            new ValueTuple<string, string, string>("div", "mult", "sub"),
            new ValueTuple<string, string, string>("mult", "sub", "div"),
            new ValueTuple<string, string, string>("mult", "div", "sub"),
            new ValueTuple<string, string, string>("sub", "div", "mult"),
            new ValueTuple<string, string, string>("sub", "mult", "div")
        };
        
        foreach (var operandSet in operandSetList)
        {
            var lowestItemIndexUsed = 0;
            var working = "";
            
            float calculation = coringItems[0];
            working += $"{coringItems[0]} ";
            
            var subUsed = false;
            var divUsed = false;
            var multUsed = false;
            
            var operands = new[] { operandSet.Item1, operandSet.Item2, operandSet.Item3 };

            foreach (var operand in operands)
            {
                for (var i = 1; i <= coringItems.Count; i++)
                {
                        switch (operand)
                        {
                            case "sub":
                                if (!subUsed && i > lowestItemIndexUsed)
                                {
                                    calculation -= coringItems[i];
                                    working += $"- {coringItems[i]} ";
                                    lowestItemIndexUsed++;
                                    subUsed = true;
                                }
                                break;
                            case "mult":
                                if (!multUsed && i > lowestItemIndexUsed)
                                {
                                    calculation *= coringItems[i];
                                    working += $"* {coringItems[i]} ";
                                    lowestItemIndexUsed++;
                                    multUsed = true;
                                }
                                break;
                            case "div":
                                if (!divUsed && i > lowestItemIndexUsed)
                                {
                                    calculation /= coringItems[i];
                                    working += $"/ {coringItems[i]} ";
                                    lowestItemIndexUsed++;
                                    divUsed = true;
                                }
                                break;
                        }
                }
            }
            var resultIsNotWhole = !int.TryParse(calculation.ToString(), out int x);

            if (resultIsNotWhole) continue;

            if ((lowestCoreValue == 0 || lowestCoreValue > Convert.ToInt32(calculation)) && calculation > 0)
            {
                lowestCoreValue = Convert.ToInt32(calculation);
                lowestCoreWorking = working;
            }
        }

        return (lowestCoreValue, lowestCoreWorking);
    }
    
}