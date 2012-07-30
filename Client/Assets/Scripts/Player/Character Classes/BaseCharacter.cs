using UnityEngine;
using System;				// for enum class
using System.Collections;

public class BaseCharacter : MonoBehaviour {
	private string _name;
	private int _level;
	private uint _freeXp;
	
	private Attribute[] _primaryAttribute;
	private Vital[] _vital;
	private Skill[] _skill;

	public void Awake(){
		_name = string.Empty;
		_level = 0;
		_freeXp = 0;
		
		_primaryAttribute = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		_vital = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		_skill = new Skill[Enum.GetValues(typeof(SkillName)).Length];
		
		SetupPrimaryAttribute();
		SetupVitals();
		SetupSkills();
	}
	
	public String Name{
		get{ return _name; }
		set{ _name = value; }
	}
	
	public int Level{
		get{ return _level; }
		set{ _level = value; }
	}
	
	public uint FreeXp{
		get{ return _freeXp; }
		set{ _freeXp = value; }
	}
	
	public void AddXP(uint xp){
		_freeXp += xp;
		
		CalculateLevel();
	}
	
	// TODO calculate level
	public void CalculateLevel(){
	}
	
	public void StatUpdate(){
		for (int i = 0; i < _vital.Length; i++)
			_vital[i].Update();
		
		for (int i = 0; i < _skill.Length; i++)
			_skill[i].Update();
	}
#region Setup
	private void SetupPrimaryAttribute(){
		for (int i = 0; i < _primaryAttribute.Length; i++)
			_primaryAttribute[i] = new Attribute();
	}
	
	private void SetupVitals(){
		for (int i = 0; i < _vital.Length; i++)
			_vital[i] = new Vital();
	}
	
	private void SetupSkills(){
		for (int i = 0; i < _skill.Length; i++)
			_skill[i] = new Skill();
	}
	
	private void SetupVitalModifiers(){	
		// Health
		GetVital((int)VitalName.Health).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), .5f));
		// Energy
		GetVital((int)VitalName.Energy).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 1f));
		// Mana
		GetVital((int)VitalName.Mana).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 1f));
	}
	
	private void SetupSkillModifiers(){
		// Melee_Offence
		GetSkill((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Might), .33f));
		GetSkill((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));
		// Melee_Defence
		GetSkill((int)SkillName.Melee_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Melee_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), .33f));
		// Ranged_Offence
		GetSkill((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .33f));
		// Ranged_Defence
		GetSkill((int)SkillName.Ranged_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Ranged_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), .33f));
		// Magic_Offence
		GetSkill((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), .33f));
		GetSkill((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		// Magic_Defence
		GetSkill((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), .33f));
		GetSkill((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), .33f));
	}
#endregion
#region Getters
	public Attribute GetPrimaryAttribute(int index){
		return _primaryAttribute[index];	
	}
	
	public Vital GetVital(int index){
		return _vital[index];	
	}
	
	public Skill GetSkill(int index){
		return _skill[index];	
	}
#endregion
}
