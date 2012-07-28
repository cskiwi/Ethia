public class BaseStat{
	private int _baseValue;			// The base value of this stat
	private int _buffValue;			// The amount of the buff to this stat
	private int _xpToLevel;			// The total amount of xp needed to raise this skill
	private float _levelModifier;	// The modifier applied to the xp needed to raise this skill
	
	public BaseStat(){
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_xpToLevel = 100;
	}
	
	
	
#region Getters and setters
	// Getters and setters
	public int BaseValue {
		get { return _baseValue; }
		set { _baseValue = value; }
	}
	public int BuffValue {
		get { return _buffValue; }
		set { _buffValue = value; }
	}
	public int XpToLevel {
		get { return _xpToLevel; }
		set { _xpToLevel = value; }
	}
	public float LevelModifier {
		get { return _levelModifier; }
		set { _levelModifier = value; }
	}
#endregion
	
	private int CalculateXPToLevel(){
		return (int)(_xpToLevel * _levelModifier);	
	}
	
	public void LevelUp(){
		_xpToLevel = CalculateXPToLevel();
		_baseValue++;
	}
	
	public int AdjustedValue(){
		return _baseValue + _buffValue;	
	}
}
