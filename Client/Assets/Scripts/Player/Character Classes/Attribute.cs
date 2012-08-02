/// <summary>
/// Attribute.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This is the class for al of the character attributes in-game
/// </summary>
public class Attribute : BaseStat {
	new public const int STARTING_XP_COST = 50; // starting cost of all the attributes
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
    public Attribute(){
		XpToLevel = STARTING_XP_COST;
		LevelModifier = 1.05f;
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