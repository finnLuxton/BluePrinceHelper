using System;
using System.Collections.Generic;
using BluePrinceHelper.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BluePrinceHelper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string? _welcomeMessage;

    public MainViewModel()
    {
        WelcomeMessage = "Blue Prince Solver!";
    }
    
    // Maybe look at updating this to a return type for both operands and numeric value?
    public int GetCore(string input)
    {
        float result = 0;
        // todo move to new data structure
        var isAddFree = true;
        var isSubFree = true;
        var isMultFree = true;
        var isDivFree = true;
        
        // todo Actually fail here
        if (input.Length < 4)
        {
            return 0; 
        }

        var coringList = new List<CoringItem>()
        { // todo deal with seperation, we need to fix how we assign the input. 
            new CoringItem(Convert.ToInt32(input[0]), false),
            new CoringItem(Convert.ToInt32(input[1]), false),
            new CoringItem(Convert.ToInt32(input[2]), false),
            new CoringItem(Convert.ToInt32(input[3]), false),
        };

        // Iterate through each bucket
        // todo move this to a function
        foreach(CoringItem item in coringList)
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


        var coreIsWholeNumber = int.TryParse(result.ToString(), out int x);

        if (coreIsWholeNumber)
        {
            return Convert.ToInt32(result);
        }

        //todo Update with actual failure of return
        return 0;
    }
    
}