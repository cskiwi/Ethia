/// <summary>
/// Attribute.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This is the class for al of the character attributes in-game
/// </summary>
public class Attribute : BaseStat {
	new public const int STARTING_XP_COST = 50; // starting cost of all the attributes
	
    private string _name;						// the name of the attribute
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
    public Attribute(){
        _name = "";
		XpToLevel = STARTING_XP_COST;
		LevelModifier = 1.05f;
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
}
/// <summary>
/// A list of all the attributes that we will have in-game for our charaters.
/// </summary>
public enum AttributeName {
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Willpower,
	Charisma
}