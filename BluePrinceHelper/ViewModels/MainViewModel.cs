using System;
using System.Collections.Generic;
using BluePrinceHelper.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BluePrinceHelper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string? _welcomeMessage;

    public int GetCore(string input)
    {
        // todo deal with separation, we need to fix how we assign the input. 
        var coringList = new List<CoringItem>
        { 
            new (Convert.ToInt32(input[0].ToString()), false),
            new (Convert.ToInt32(input[1].ToString()), false),
            new (Convert.ToInt32(input[2].ToString()), false),
            new (Convert.ToInt32(input[3].ToString()), false)
        };

        var coreResult = CalculateCoreFromList(coringList);
        
        // todo Check for more separations, or if core whole number has digits greater than 4
        
        var coreIsWholeNumber = int.TryParse(coreResult.ToString(), out int x);

        return coreIsWholeNumber ? Convert.ToInt32(coreResult) : 0;
    }

    private static float CalculateCoreFromList(List<CoringItem> coringItems)
    {
        float result = 0;
        // todo move to new data structure
        var isAddFree = true;
        var isSubFree = true;
        var isMultFree = true;
        var isDivFree = true;
        
        foreach(var item in coringItems)
        {
            if (isAddFree)
            {
                result += item.Value;
                item.UsedInCore = true;
                isAddFree = false;
                continue;
            }

            if (isSubFree)
            {
                result -= item.Value;
                item.UsedInCore = true;
                isSubFree = false;
                continue;
            }

            if (isMultFree)
            {
                result *= item.Value;
                item.UsedInCore = true;
                isMultFree = false;
                continue;
            }

            if (isDivFree)
            {
                result /= item.Value;
                item.UsedInCore = true;
                isDivFree = false;
                continue;
            }
        }

        return result;
    }
    
}