using UnityEngine;
using System; 				// for enum class
using System.Collections;

public class CharacterGenerator : MonoBehaviour {
    private PlayerCharacter _playerChar;

    private const int STARTING_POINTS = 350;
    private const int MIN_STARTING_ATTRIBUTE_VALUE = 10;
    private const int STARTING_VALUE = 60;
    private int _pointsLeft;

    private const int OFFSET = 5;
    private const int LINE_HEIGHT = 20;

    private const int STAT_LABEL_WIDTH = 125;
    private const int BASEVALUE_LABEL_WIDTH = 30;
    private const int BUTTON_WIDTH = 20;
    private const int BUTTON_HEIGHT = 20;

    private int _statStartingPos = 40;

    public GameObject playerPrefab;

    void Start() {
        GameObject pc = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity) as GameObject;
        pc.name = "pc";

        _playerChar = pc.GetComponent<PlayerCharacter>();

        _pointsLeft = STARTING_POINTS;

        for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
            _playerChar.GetPrimaryAttribute(i).BaseValue = STARTING_VALUE;
            _pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIBUTE_VALUE);
        }

        _playerChar.StatUpdate();
    }

    void Update() {

    }

    void OnGUI() {
        DisplayName();
        DisplayPointsLeft();

        DisplayAttributes();
        DisplayVitals();
        DisplaySkills();

        if (_playerChar.Name == "" || _pointsLeft > 0)
            DisplayCreateLabel();
        else
            DisplayCreateButton();
    }
    private void UpdateCurVitalValues() {
        for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++)
            _playerChar.GetVital(i).CurValue = _playerChar.GetVital(i).AdjustedBaseValue;
    }
    #region Display
    private void DisplayName() {
        GUI.Label(new Rect(10, 10, 50, 25), "Name: ");
        _playerChar.Name = GUI.TextArea(new Rect(65, 10, 100, 25), _playerChar.Name);
    }

    private void DisplayAttributes() {
        for (int i = 0; i < Enum.GetValues(typeof(AttributeName)).Length; i++) {
            GUI.Label(new Rect(OFFSET, 								// x
                                _statStartingPos + (i * LINE_HEIGHT), 	// y
                                STAT_LABEL_WIDTH, LINE_HEIGHT 			// Width
                    ), ((AttributeName)i).ToString());					// Height

            GUI.Label(new Rect(STAT_LABEL_WIDTH + OFFSET, 				// x
                                _statStartingPos + (i * LINE_HEIGHT), 	// y
                                STAT_LABEL_WIDTH, 						// Width
                                LINE_HEIGHT 							// Height
                    ), _playerChar.GetPrimaryAttribute(i).BaseValue.ToString());

            if (GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH,	// x
                                    _statStartingPos + (i * LINE_HEIGHT), 				// y
                                    BUTTON_WIDTH, 										// Width
                                    LINE_HEIGHT 										// Height
                                ), "-")) {

                if (_playerChar.GetPrimaryAttribute(i).BaseValue > MIN_STARTING_ATTRIBUTE_VALUE) {
                    _playerChar.GetPrimaryAttribute(i).BaseValue--;
                    _pointsLeft++;
                    _playerChar.StatUpdate();
                }
            }
            if (GUI.Button(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH,	// x
                                    _statStartingPos + (i * LINE_HEIGHT), 								// y
                                    BUTTON_WIDTH,														// Width
                                    LINE_HEIGHT 														// Heigth
                                ), "+")) {

                if (_pointsLeft > 0) {
                    _playerChar.GetPrimaryAttribute(i).BaseValue++;
                    _pointsLeft--;
                    _playerChar.StatUpdate();
                }
            }
        }
    }

    private void DisplayVitals() {
        for (int i = 0; i < Enum.GetValues(typeof(VitalName)).Length; i++) {
            GUI.Label(new Rect(OFFSET, 									    // x
                                _statStartingPos + ((i + 7) * LINE_HEIGHT), // y
                                STAT_LABEL_WIDTH, 							// Width
                                LINE_HEIGHT									// Heigth
                                ), ((VitalName)i).ToString());

            GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH, 					// x
                                _statStartingPos + ((i + 7) * LINE_HEIGHT), // y
                                STAT_LABEL_WIDTH, 							// Width
                                LINE_HEIGHT									// Heigth
                                ), _playerChar.GetVital(i).AdjustedBaseValue.ToString());
        }
    }

    private void DisplaySkills() {
        for (int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++) {
            GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2,	// x
                                _statStartingPos + ((i) * LINE_HEIGHT), 										    // y
                                STAT_LABEL_WIDTH, 																	// Width
                                 LINE_HEIGHT																		// Heigth
                                ), ((SkillName)i).ToString());

            GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 2 + STAT_LABEL_WIDTH,	// x
                                _statStartingPos + ((i) * LINE_HEIGHT), 																// y
                                STAT_LABEL_WIDTH,																						// Width
                                LINE_HEIGHT																								// Heigth=
                                ), _playerChar.GetSkill(i).AdjustedBaseValue.ToString());
        }
    }

    private void DisplayPointsLeft() {
        GUI.Label(new Rect(250, 10, 100, 25), "Points left: " + _pointsLeft.ToString());
    }

    private void DisplayCreateButton() {
        if (GUI.Button(new Rect(Screen.width / 2 - 50,						// x
                                _statStartingPos + (10 * LINE_HEIGHT), 		// y
                                100, 										// Width
                                LINE_HEIGHT									// Heigth
            ), "Create")) {
            // change the cur value to the max modified value of that vital
            UpdateCurVitalValues();
            GameObject.Find("__GameSettings").GetComponent<GameSettings>().SaveCharaterData();
            Application.LoadLevel("Level1");
        }

    }

    private void DisplayCreateLabel() {
        GUI.Label(new Rect(Screen.width / 2 - 50,						// x
                            _statStartingPos + (10 * LINE_HEIGHT), 		// y
                            100, 										// Width
                            LINE_HEIGHT									// Heigth
            ), "Creating ...", "Button");
    }
    #endregion
}
