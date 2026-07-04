using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BluePrinceHelper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty] private string? _welcomeMessage;

    public MainViewModel()
    {
        WelcomeMessage = "Blue Prince Solver!";
    }

    public int GetCore(string input)
    {
        if (input.Length < 4)
        {
            return 0; // Todo Actually fail here
        }

        var bucket1 = Convert.ToInt32(input[0]);
        var bucket2 = Convert.ToInt32(input[1]);
        var bucket3 = Convert.ToInt32(input[2]);
        var bucket4 = Convert.ToInt32(input[3]);
        
        // If longer length than 5, do some separating shenanigans. Probably recursion. 
        // Check operands
        
        // Have a operand string that we refresh every time? Store that, then deliver to as a return if thats the output?
        //      Maybe make that a separate function
        
        
        
        
        return bucket1 - bucket2 * bucket3 / bucket4;
    }
    
}