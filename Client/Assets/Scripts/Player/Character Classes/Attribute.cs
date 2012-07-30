public class Attribute : BaseStat {
	public Attribute(){
		XpToLevel = 50;
		LevelModifier = 1.05f;
	}
}
public enum AttributeName {
	Might,
	Constitution,
	Nimbleness,
	Speed,
	Concentration,
	Willpower,
	Charisma
}