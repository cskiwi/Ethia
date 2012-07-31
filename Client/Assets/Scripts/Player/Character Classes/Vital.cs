/// <summary>
/// Vital.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This class contains all the extra functions for a charaters vitals
/// </summary>
public class Vital : ModifiedStat {
	private int _curValue;      // this is the current value of this vital
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital(){
		_curValue = 0;
		XpToLevel = 40;
		LevelModifier = 1.1f;
	}
	
	/// <summary>
	/// When getting the curent value, it checks that it is not greater than our adjusted Base value.
	/// if it is, make it the same as our Adjusted Base value
	/// </summary>
	/// <value>
	/// The current value.
	/// </value>
	public int CurValue{
		get { 
			if (_curValue > AdjustedBaseValue)
				_curValue = AdjustedBaseValue;
			
			return _curValue;
		}
		set { _curValue = value; }
	}
}

/// <summary>
/// A list of all the vitals that we will have in-game for our charaters.
/// </summary>
public enum VitalName{
	Health,
	Energy,
	Mana
}