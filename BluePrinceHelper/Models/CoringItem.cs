namespace BluePrinceHelper.Models;

public record CoringItem(int Value, bool UsedInCore = false)
{
    public int Value { get; set; } = Value;
    public bool UsedInCore { get; set; } = UsedInCore;
}

