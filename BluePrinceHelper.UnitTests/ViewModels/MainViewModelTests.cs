using BluePrinceHelper.ViewModels;

namespace BluePrinceHelper.UnitTests.ViewModels;

// Tests follow a naming structure following AAA
// Arrange, Act, Assert

public class MainViewModelTests
{
    public MainViewModel _MainViewModel;

    public MainViewModelTests()
    {
        _MainViewModel = new MainViewModel();
    }
    // [Fact] public void WithValidInput_WhenCalculatingCoreValue_GetNumericCoreResult()
    // {
    //     //Arrange
    //     
    //     //Act
    //     
    //     //Assert
    // }
    //
    
    [Theory]
    [InlineData("0")]
    [InlineData("-5")]
    [InlineData("")]
    [InlineData(null)]
    public void WithInvalidInput_WhenCalculatingCoreValue_ReturnNoCore(string invalidCoringTargetInput)
    {
        //Arrange, Act
        var result = _MainViewModel.GetCore(invalidCoringTargetInput);

        //Assert
        Assert.Equal(0, result);
    }
    
}