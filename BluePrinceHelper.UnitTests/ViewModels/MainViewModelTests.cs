using BluePrinceHelper.ViewModels;

namespace BluePrinceHelper.UnitTests.ViewModels;

public class MainViewModelTests
{
    private readonly MainViewModel _mainViewModel = new();

    [Theory]
    [InlineData("3614", 14)]
    [InlineData("86455", 18)]
    [InlineData("45292", 8)]
    public void WithValidInput_WhenGettingCore_ReturnCore(string input, int expectedOutput)
    {
        //Arrange and Act
        var result = _mainViewModel.GetCore(input);
        
        //Assert
        Assert.Equal(expectedOutput, result.lowestCoreResult);
        
    }
    
    [Theory]
    [InlineData(3, 6, 1, 4, 14)]
    [InlineData(8,6,45,5, 18)]
    [InlineData(45,2,9,2, 8)]
    [InlineData(1000, 200, 11, 2, 53)]
    public void WithValidInput_WhenCalculatingCoreFromList_GetNumericCoreResult(int coreItemOne, int coreItemTwo, int coreItemThree, int coreItemFour,  int expectedOutput)
    {
        //Arrange
        var coringList = new List<int>
        { 
            coreItemOne,
            coreItemTwo,
            coreItemThree,
            coreItemFour
        };
        
        //Act
        var result = _mainViewModel.CalculateCoreFromList(coringList);

        //Assert
        Assert.Equal(expectedOutput, result.lowestCoreValue);
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
        Assert.Equal(0, result.lowestCoreResult);
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void WithValidInput_WhenGettingDelimitedCoringItemList_ReturnList(string input, List<List<int>> expectedOutput)
    {
        //Arrange and Act
        var result = _mainViewModel.GetDelimitedCoringItemList(input);

        //Assert
        Assert.Equal(expectedOutput, result);
    }
    
    
    public static IEnumerable<object[]> TestData =>
    [
        [
            "3614",
            new List<List<int>>
            {
                new(){
                    3,
                    6,
                    1,
                    4
                }
            }
        ],
        [
            "12345",
            new List<List<int>>
            {
                new(){
                    1,
                    2,
                    3,
                    45
                },
                new(){
                    1,
                    2,
                    34,
                    5
                },
                new(){
                    1,
                    23,
                    4,
                    5
                },
                new(){
                    12,
                    3,
                    4,
                    5
                }
            }
        ]
    ];
    
}