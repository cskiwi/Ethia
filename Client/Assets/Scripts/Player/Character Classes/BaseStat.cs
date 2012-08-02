/// <summary>
/// BaseStat.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This is the baseclass for al statsi in game.
/// </summary>
public class BaseStat {
    public const int STARTING_XP_COST = 100; 	// starting cost of the base stat

    private int _baseValue;						// The base value of this stat
    private int _buffValue;						// The amount of the buff to this stat
    private int _xpToLevel;						// The total amount of xp needed to raise this skill
    private float _levelModifier;				// The modifier applied to the xp needed to raise this skill

    private string _name;						// the name of the attribute

	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
    public BaseStat() {
        _baseValue = 0;
        _buffValue = 0;
        _levelModifier = 1.1f;
        _xpToLevel = STARTING_XP_COST;
        _name = "";
    }

    #region Getters and setters
	/// <summary>
	/// Gets or sets the base value.
	/// </summary>
	/// <value>
	/// The base value.
	/// </value>
    public int BaseValue {
        get { return _baseValue; }
        set { _baseValue = value; }
    }
	/// <summary>
	/// Gets or sets the buff value.
	/// </summary>
	/// <value>
	/// The buff value.
	/// </value>
    public int BuffValue {
        get { return _buffValue; }
        set { _buffValue = value; }
    }
	/// <summary>
	/// Gets or sets the xp to level.
	/// </summary>
	/// <value>
	/// The xp to level.
	/// </value>
    public int XpToLevel {
        get { return _xpToLevel; }
        set { _xpToLevel = value; }
    }
	/// <summary>
	/// Gets or sets the level modifier.
	/// </summary>
	/// <value>
	/// The level modifier.
	/// </value>
    public float LevelModifier {
        get { return _levelModifier; }
        set { _levelModifier = value; }
    }
    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>
    /// The name.
    /// </value>
    public string Name {
        get { return _name; }
        set { _name = value; }
    }
    #endregion
	
	/// <summary>
	/// Calculates the XP to level.
	/// </summary>
	/// <returns>
	/// The XP to level.
	/// </returns>
    private int CalculateXPToLevel() {
        return (int)(_xpToLevel * _levelModifier);
    }
	
	/// <summary>
	/// Assign the new value to _xpToLevel and increase the baseValue by one.
	/// </summary>
    public void LevelUp() {
        _xpToLevel = CalculateXPToLevel();
        _baseValue++;
    }
	
	/// <summary>
	/// Recalculate the Adjusted basevalue, and returns it.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
    public int AdjustedBaseValue {
        get { return _baseValue + _buffValue; }
    }
}