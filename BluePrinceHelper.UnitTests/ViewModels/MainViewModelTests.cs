using BluePrinceHelper.Models;
using BluePrinceHelper.ViewModels;

namespace BluePrinceHelper.UnitTests.ViewModels;

public class MainViewModelTests
{
    private readonly MainViewModel _mainViewModel = new();
    
    [Theory]
    [InlineData(3, 6, 1, 4, 14)]
    [InlineData(8,6,45,5, 18)]
    [InlineData(45,2,9,2, 8)]
    [InlineData(1000, 200, 11, 2, 53)]
    public void WithValidInput_WhenCalculatingCoreFromList_GetNumericCoreResult(int coreItemOne, int coreItemTwo, int coreItemThree, int coreItemFour,  int expectedOutput)
    {
        //Arrange
        var coringList = new List<CoringItem>
        { 
            new(coreItemOne),
            new(coreItemTwo),
            new(coreItemThree),
            new(coreItemFour)
        };
        
        //Act
        var result = _mainViewModel.CalculateCoreFromList(coringList);

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

    [Theory]
    [MemberData(nameof(TestData))]
    public void WithValidInput_WhenGettingDelimitedCoringItemList_ReturnList(string input, List<List<CoringItem>> expectedOutput)
    {
        //Arrange and Act
        var result = _mainViewModel.GetDelimitedCoringItemList(input);

        //Assert
        Assert.Equal(result, expectedOutput);
    }
    
    
    public static IEnumerable<object[]> TestData =>
    [
        [
            "3614",
            new List<List<CoringItem>>
            {
                new(){
                    new CoringItem(3),
                    new CoringItem(6),
                    new CoringItem(1),
                    new CoringItem(4)
                }
            }
        ],
        [
            "12345",
            new List<List<CoringItem>>
            {
                new(){
                    new CoringItem(1),
                    new CoringItem(2),
                    new CoringItem(3),
                    new CoringItem(45)
                },
                new(){
                    new CoringItem(1),
                    new CoringItem(2),
                    new CoringItem(34),
                    new CoringItem(5)
                },
                new(){
                    new CoringItem(1),
                    new CoringItem(23),
                    new CoringItem(4),
                    new CoringItem(5)
                },
                new(){
                    new CoringItem(12),
                    new CoringItem(3),
                    new CoringItem(4),
                    new CoringItem(5)
                }
            }
        ]
    ];
    
}