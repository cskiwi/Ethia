/// <summary>
/// Modifiedstat.cs
/// Glenn Latomme (glenn.latomme@gmail.com)
/// 
/// This is the base class for all stats that will be modifable by attributes
/// </summary>
using System.Collections.Generic;				// for the list<>

public class ModifiedStat : BaseStat {
	private List<ModifyingAttribute> _mods;		// A list of Attributes that modify this stat
	private int _modValue;						// The amount added to the base value from the modifiers
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat(){
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	/// <summary>
	/// Adds a ModifyingAttribute to our list of mdos for this modifiedStat.
	/// </summary>
	/// <param name='mod'>
	/// A ModifyingAttribute.
	/// </param>
	public void AddModifier( ModifyingAttribute mod ){
		_mods.Add(mod);		
	}
	
	/// <summary>
	/// resets _modValue to 0.
	/// Check to see if we have at least one ModyingAttribute in our list of mods
	/// If we do, then iterate through the list an add the AdjestedBaseValue * att.ratio
	/// </summary>
	private void CalculateModValue(){
		_modValue = 0;
		
		if(_mods.Count > 0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
	}
	
	/// <summary>
	/// Overwriteing the AdjustedBaseValue function from Basestat.
	/// 
	/// Recalculate the Adjusted basevalue, and returns it.
	/// </summary>
	/// <value>
	/// The adjusted base value.
	/// </value>
	public new int AdjustedBaseValue{
		get{ return BaseValue + BuffValue + _modValue; }
	}
	
	/// <summary>
	/// Update this instance.
	/// </summary>
	public void Update(){
		CalculateModValue();
	}
	
    public string GetModifyingAttributeString() {
        string temp = "";
        for (int i = 0; i < _mods.Count; i++) {
            temp += _mods[i].attribute.Name;
            temp += "_";
            temp += _mods[i].ratio;

            if (i < _mods.Count -1) {
                temp += "|";                
            }
        }
        
        return temp;
    }
}

/// <summary>
/// A structure that will hold an Attribute and a ratio that will be added as a modifying attribbute to our ModifiedStats.
/// </summary>
public struct ModifyingAttribute {
	public Attribute attribute; // The attribute to be used as a modifier
	public float ratio;			// The percent of the attributes AdjustedBaseValue that will be applied to the ModifiedStat
	
	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name='att'>
	/// The attributed to be used
	/// </param>
	/// <param name='rat'>
	///	The ratio to use
	/// </param>
	public ModifyingAttribute (Attribute att, float rat){
		attribute = att;
		ratio = rat;
	}
}