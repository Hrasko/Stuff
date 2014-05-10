namespace Tactics
{
    public delegate void InputCallback(Tile[] selection);

    public delegate void InputSelection(Tile selectedTile, int tileOptionsIndex);

    public enum ActionType
    {
        free,
        movement,
        minor,
        standard
    }

    public enum Size
    {
        Small,
        Medium,
        Large
    }

    public enum InputSelectionType
    {
        None,
        Single,
        Area,
        Cone,
        Edition
    }

    public enum TargetingType
    {
        single,
        all,
        burst,
        blast
    }

    public enum Vision
    {
        Normal,
        LowLight,
        DarkVision
    }

}