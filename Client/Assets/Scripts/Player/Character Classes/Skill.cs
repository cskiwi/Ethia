/// <summary>
/// Skill.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This class contains all the extra functions for a charaters skills
/// </summary>
public class Skill : ModifiedStat {
	private bool _known;        // A bool to toggle if a charater knows the skill
	
    /// <summary>
    /// Initializes a new instance of the <see cref="Skill"/> class.
    /// </summary>
	public Skill(){
		_known = false;
		XpToLevel = 25;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value>
	/// <c>true</c> if known; otherwise, <c>false</c>.
	/// </value>
	public bool Known{
		get{ return _known; }
		set{ _known = value; } 
	}
}

/// <summary>
/// A list of all the skills that we will have in-game for our charaters
/// </summary>
public enum SkillName{
	Melee_Offence,
	Melee_Defence,
	Ranged_Offence,
	Ranged_Defence,
	Magic_Offence,
	Magic_Defence
}
