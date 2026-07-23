using System;
using System.Collections.Generic;
using BluePrinceHelper.Models;

namespace BluePrinceHelper.ViewModels;

public class MainViewModel : ViewModelBase
{
    public int GetCore(string? input)
    {
        var lowestCoreResult = 0;
        if (input == null || input.Length < 4 )
        {
            return 0;
        }

        var coringList = GetDelimitedCoringItemList(input);

        foreach (var coringSet in coringList)
        {
            var coreFromSet = CalculateCoreFromList(coringSet);

            if ((coreFromSet < lowestCoreResult && coreFromSet > 0) || lowestCoreResult == 0) 
            {
                lowestCoreResult = coreFromSet;
            }
        }

        return lowestCoreResult;
    }

    public List<List<CoringItem>> GetDelimitedCoringItemList(string input)
    {
        List<List<CoringItem>> resultList = new List<List<CoringItem>>();

        for (var delimOne = 1; delimOne < input.Length - 2; delimOne++)
        {
            for (var delimTwo = delimOne + 1; delimTwo < input.Length - 1; delimTwo++)
            {
                for (var delimThree = delimTwo + 1; delimThree < input.Length; delimThree++)
                {
                    resultList.Add([
                        new CoringItem(Convert.ToInt32(input[..delimOne])),
                        new CoringItem(Convert.ToInt32(input[delimOne..delimTwo])),
                        new CoringItem(Convert.ToInt32(input[delimTwo..delimThree])),
                        new CoringItem(Convert.ToInt32(input[delimThree..]))
                    ]);
                }
            }
        }
        
        return resultList;
    }
    
    public int CalculateCoreFromList(List<CoringItem> coringItems)
    {
        var lowestCoreValue = 0;
        
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
            float calculation = coringItems[0].Value;
            
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
                                    calculation -= coringItems[i].Value;
                                    lowestItemIndexUsed++;
                                    subUsed = true;
                                }
                                break;
                            case "mult":
                                if (!multUsed && i > lowestItemIndexUsed)
                                {
                                    calculation *= coringItems[i].Value;
                                    lowestItemIndexUsed++;
                                    multUsed = true;
                                }
                                break;
                            case "div":
                                if (!divUsed && i > lowestItemIndexUsed)
                                {
                                    calculation /= coringItems[i].Value;
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
            }
        }

        return lowestCoreValue;
    }
    
}