namespace Tactics
{
    public delegate void InputCallback(Tile[] selection);

    public delegate void InputSelection(Tile selectedTile, int tileOptionsIndex);

    public delegate int[][] GraphFactory();

    public delegate Tile TileFactory(int index);

    public delegate void voidDelegate();

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
        EditionStart
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