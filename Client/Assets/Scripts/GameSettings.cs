using UnityEngine;
using System;				// for enum class
using System.Collections;

public class GameSettings : MonoBehaviour {
    public const string PLAYER_SPAWN_POINT = "Player Spawn Point";      // The name of the game object that the player will spawn at the start of the level

    void Awake() {
        // REMEMBER doesn't destroy when changing scenes
        DontDestroyOnLoad(this);
    }

    public void SaveCharaterData() {
        // UNDONE Send info to server
        PlayerCharacter pc = GameObject.Find("pc").GetComponent<PlayerCharacter>();

        PlayerPrefs.DeleteAll();

        PlayerPrefs.SetString("Player Name", pc.Name);

        for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
            PlayerPrefs.SetInt(((AttributeName)i).ToString() + " - Base value", pc.GetPrimaryAttribute(i).BaseValue);
            PlayerPrefs.SetInt(((AttributeName)i).ToString() + " - Xp To level", pc.GetPrimaryAttribute(i).XpToLevel);
        }

        for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
            PlayerPrefs.SetInt(((VitalName)i).ToString() + " - Base value", pc.GetVital(i).BaseValue);
            PlayerPrefs.SetInt(((VitalName)i).ToString() + " - Cur value", pc.GetVital(i).CurValue);
            PlayerPrefs.SetInt(((VitalName)i).ToString() + " - Xp To level", pc.GetVital(i).XpToLevel);
            
            // PlayerPrefs.SetString(((VitalName)i).ToString() + " - Mods", pc.GetVital(i).GetModifyingAttributeString());
        }

        for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
            PlayerPrefs.SetInt(((SkillName)i).ToString() + " - Base value", pc.GetSkill(i).BaseValue);
            PlayerPrefs.SetInt(((SkillName)i).ToString() + " - Xp To level", pc.GetSkill(i).XpToLevel);
            
            // PlayerPrefs.SetString(((SkillName)i).ToString() + " - Mods", pc.GetSkill(i).GetModifyingAttributeString());
        }
    }

    public void LoadCharaterData() {
        PlayerCharacter pc = GameObject.Find("pc").GetComponent<PlayerCharacter>();

        pc.Name = PlayerPrefs.GetString("Player Name", "Name me");
        Debug.Log(pc.Name);

        for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
            pc.GetPrimaryAttribute(i).BaseValue = PlayerPrefs.GetInt(((AttributeName)i).ToString() + " - Base value", 0);
            pc.GetPrimaryAttribute(i).XpToLevel = PlayerPrefs.GetInt(((AttributeName)i).ToString() + " - Xp To level", Attribute.STARTING_XP_COST);
        }

        for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
            pc.GetVital(i).BaseValue = PlayerPrefs.GetInt(((VitalName)i).ToString() + " - Base value", 0);
            pc.GetVital(i).XpToLevel = PlayerPrefs.GetInt(((VitalName)i).ToString() + " - Xp To level", 0);

            // Make sure you call this so that the AdjustedBaseValue will be updated before you try to call to get the curValue
            pc.GetVital(i).Update();

            // get the stored value for the curvalue of each vital
            pc.GetVital(i).CurValue = PlayerPrefs.GetInt(((VitalName)i).ToString() + " - Cur value", 1);
        }

        for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
            pc.GetSkill(i).BaseValue = PlayerPrefs.GetInt(((SkillName)i).ToString() + " - Base value", 0);
            pc.GetSkill(i).XpToLevel = PlayerPrefs.GetInt(((SkillName)i).ToString() + " - Xp To level", 0);
        }
    }
}