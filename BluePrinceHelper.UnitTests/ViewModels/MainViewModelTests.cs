using System.Runtime.CompilerServices;
using BluePrinceHelper.ViewModels;

namespace BluePrinceHelper.UnitTests.ViewModels;

// Tests follow a naming structure following AAA
// Arrange, Act, Assert

public class MainViewModelTests
{
    private readonly MainViewModel _mainViewModel = new();
    
    [Theory]
    [InlineData("3614", 14)]
    [InlineData("86455", 18)]
    [InlineData("45292", 53)]
    //[InlineData("1000200112", 53)] // todo maybe remove this one? it's a bit of a peculiar example
    public void WithValidInput_WhenCalculatingCoreValue_GetNumericCoreResult(string validCoringInput, int expectedOutput)
    {
        //Arrange, Act
        var result = _mainViewModel.GetCore(validCoringInput);

        //Assert
        Assert.Equal(expectedOutput, result);
    }
    
    
    [Theory]
    [InlineData("0")]
    [InlineData("-5")]
    [InlineData("")]
    [InlineData(null)]
    public void WithInvalidInput_WhenCalculatingCoreValue_ReturnNoCore(string invalidCoringTargetInput)
    {
        //Arrange, Act
        var result = _mainViewModel.GetCore(invalidCoringTargetInput);

        //Assert
        Assert.Equal(0, result);
    }
    
}