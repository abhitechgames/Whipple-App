using System;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class AppManager : MonoBehaviour
{
    [SerializeField] private GameObject introPanel;

    [Header("PATIENT OR NURSE PANEL")]
    [SerializeField] private GameObject patientNursePanel;
    [SerializeField] private GameObject patientPanel;
    [SerializeField] private GameObject nursePanel;
    [SerializeField] private Image patientButton;
    [SerializeField] private Image nurseButton;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Image nextButton;
    [SerializeField] private Sprite filledRect;
    [SerializeField] private TMP_Text nextText;

    public enum Role
    {
        Patient, Nurse
    }

    public Role role;


    [Space]
    [Header("PATIENT WINDOW")]
    [SerializeField] private GameObject addPatientWindow;
    [SerializeField] private TMP_InputField patientNameInput;
    [SerializeField] private TMP_InputField patientAgeInput;
    [SerializeField] private TMP_InputField patientUIDInput;
    [SerializeField] private GameObject trackerWindow;
    [SerializeField] private GameObject faqScreen;
    [SerializeField] private GameObject backButton;
    [SerializeField] private Animator dots3ScreenAnimator;

    [Space]
    [Header("NURSE WINDOW")]
    [SerializeField] private GameObject addMultiPatientWindow;
    [SerializeField] private TMP_InputField multiPatientNameInput;
    [SerializeField] private TMP_InputField multiPatientAgeInput;
    [SerializeField] private TMP_InputField multiPatientUIDInput;

    [SerializeField] private GameObject patientSegment;
    [SerializeField] private Transform multiPatientPanel;


    [Space]
    [Header("DIET PANEL")]
    [SerializeField] private GameObject dietSegment;
    [SerializeField] private Transform dietPanel;


    [Space]
    [Header("EXCERCISE PANEL")]
    [SerializeField] private GameObject excerciseSegment;
    [SerializeField] private Transform excercisePanel;

    [Space]
    [Header("DIET INPUT WINDOW")]
    [SerializeField] private GameObject dietInputWindow;
    [SerializeField] private TMP_Text dietDate;
    [SerializeField] private TMP_InputField breakfastInput;
    [SerializeField] private TMP_InputField lunchInput;
    [SerializeField] private TMP_InputField dinnerInput;

    [Header("EXCERCISE INPUT WINDOW")]
    [SerializeField] private GameObject excerciseInputWindow;
    [SerializeField] private TMP_Text excerciseDate;
    [SerializeField] private TMP_InputField morningInput_Band;
    [SerializeField] private TMP_InputField morningInput_Spiro;
    [SerializeField] private TMP_InputField eveningInput_Band;
    [SerializeField] private TMP_InputField eveningInput_Spiro;
    [SerializeField] private TMP_InputField afternoonInput_Band;
    [SerializeField] private TMP_InputField afternoonInput_Spiro;

    [Header("FILE LOCATION")]
    [SerializeField] private GameObject fileLocationBlock;
    [SerializeField] private TMP_Text fileLocationText;

    private string date;

    public static AppManager Instance;
    public void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        Screen.fullScreen = false;
    }

    public void Start()
    {
        Call(ObjectDisabler(introPanel, 3.5f));

        if (PlayerPrefs.GetInt(PlayerPrefsManager.Role, 0) == 0)
        {
            Call(ObjectEnabler(patientNursePanel, 3.75f));
        }
        else if (PlayerPrefs.GetInt(PlayerPrefsManager.Role, 0) == 1)
        {
            role = Role.Patient;

            Call(ObjectEnabler(patientPanel, 3.75f));
            if (PlayerPrefs.GetInt(PlayerPrefsManager.PatientAdded, 0) == 0)
            {
                Call(ObjectEnabler(addPatientWindow, 3.75f));
            }
            else
            {
                Call(ObjectEnabler(trackerWindow, 3.75f));
            }

            backButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt(PlayerPrefsManager.Role, 0) == 2)
        {
            role = Role.Nurse;

            Call(ObjectEnabler(nursePanel, 3.75f));

            backButton.SetActive(true);

            SaveSystem.Instance.LoadPatientDatabase();
        }
    }

    public void PatientSelected(int index)
    {
        Call(ObjectEnabler(trackerWindow, 0f));

        Debug.Log("Patient Selected: " + index);
    }

    public void Call(IEnumerator action)
    {
        StartCoroutine(action);
    }

    IEnumerator ObjectDisabler(GameObject G, float T)
    {
        yield return new WaitForSeconds(T);
        G.SetActive(false);
    }
    IEnumerator ObjectEnabler(GameObject G, float T)
    {
        yield return new WaitForSeconds(T);
        G.SetActive(true);
    }

    public void EnableFAQScreen()
    {
        faqScreen.SetActive(true);
    }

    private const string surveyLink = "https://forms.gle/FXXfLTRqsbMoPpbf9";
    public void TakeSurvey()
    {
        Application.OpenURL(surveyLink);
    }

    public void Done()
    {
        if (patientNameInput.text == "" || patientAgeInput.text == "" || patientUIDInput.text == "")
            return;

        PlayerPrefs.SetInt(PlayerPrefsManager.PatientAdded, 1);
        PlayerPrefs.SetString(PlayerPrefsManager.PatientName, patientNameInput.text);
        PlayerPrefs.SetInt(PlayerPrefsManager.PatientAge, Int32.Parse(patientAgeInput.text));
        PlayerPrefs.SetString(PlayerPrefsManager.PatientUID, patientUIDInput.text);

        SaveSystem.Instance.SavePatiendData();

        Call(ObjectDisabler(addPatientWindow, 0f));
        Call(ObjectEnabler(trackerWindow, .1f));
    }

    public void EnableDotScreen()
    {
        dots3ScreenAnimator.SetTrigger("Enabled");
    }

    public void DisableDotScreen()
    {
        dots3ScreenAnimator.SetTrigger("Disabled");
    }

    private void AddExcercise()
    {
        UniversalPopUps.ShowCalendarPopUp(OnDateSelected_Excercise);
    }

    public void OnDateSelected_Excercise(DateTime p_dateTime)
    {
        date = p_dateTime.ToShortMonthName() + " " + p_dateTime.Day.ToString() + " " + p_dateTime.Year.ToString();

        excerciseDate.text = date.ToString();
        Call(ObjectEnabler(excerciseInputWindow, .1f));
    }

    public void AddPatient()
    {
        string name = multiPatientNameInput.text;
        string age = multiPatientAgeInput.text;
        string uid = multiPatientUIDInput.text;

        if (name != "" || age != "" || uid != "")
        {
            SaveSystem.Instance.AddMultiPatientData(name, int.Parse(age), uid);

            int i = SaveSystem.Instance.GetIndex();

            AddPatientSegment(name, age, uid, i);

            multiPatientNameInput.text = "";
            multiPatientAgeInput.text = "";
            multiPatientUIDInput.text = "";

            Call(ObjectDisabler(addMultiPatientWindow, 0f));
        }

    }

    private void AddDiet()
    {
        UniversalPopUps.ShowCalendarPopUp(OnDateSelected_Diet);
    }

    public void OnDateSelected_Diet(DateTime p_dateTime)
    {
        date = p_dateTime.ToShortMonthName() + " " + p_dateTime.Day.ToString() + " " + p_dateTime.Year.ToString();

        dietDate.text = date.ToString();
        Call(ObjectEnabler(dietInputWindow, .1f));
    }

    public void SelectPatientOrNurse(int r)
    {
        if (r == 0)
        {
            role = Role.Patient;
            patientButton.color = selectedColor;
            nurseButton.color = Color.white;
        }
        else
        {
            role = Role.Nurse;
            patientButton.color = Color.white;
            nurseButton.color = selectedColor;
        }

        nextButton.sprite = filledRect;
        nextText.color = Color.black;
    }

    public void Next()
    {
        if (role == Role.Patient)
        {
            PlayerPrefs.SetInt(PlayerPrefsManager.Role, 1); // 1 - Patient

            backButton.SetActive(false);

            Call(ObjectDisabler(patientNursePanel, 0f));
            Call(ObjectEnabler(patientPanel, .1f));
            Call(ObjectEnabler(addPatientWindow, .1f));
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsManager.Role, 2); // 2 - Nurse

            backButton.SetActive(true);

            Call(ObjectDisabler(patientNursePanel, 0f));
            Call(ObjectEnabler(nursePanel, .1f));
        }
    }
    public void AddDietBlock()
    {
        if (role == Role.Nurse)
        {
            SaveSystem.Instance.multiPatientData.patientData[SaveSystem.Instance.selectedIndex].AddDiet(date, breakfastInput.text, lunchInput.text, dinnerInput.text);

            SaveSystem.Instance.SaveMultiPatientData();
        }
        else
        {
            SaveSystem.Instance.patientData.AddDiet(date, breakfastInput.text, lunchInput.text, dinnerInput.text);

            SaveSystem.Instance.SavePatiendData();
        }


        Call(ObjectDisabler(dietInputWindow, 0f));

        AddDietSegment(date, breakfastInput.text, lunchInput.text, dinnerInput.text);

        breakfastInput.text = "";
        lunchInput.text = "";
        dinnerInput.text = "";
    }
    public void AddPhysioBlock()
    {
        if (role == Role.Nurse)
        {
            SaveSystem.Instance.multiPatientData.patientData[SaveSystem.Instance.selectedIndex].AddPhysio(date, morningInput_Band.text, morningInput_Spiro.text, afternoonInput_Band.text, afternoonInput_Spiro.text, eveningInput_Band.text, eveningInput_Spiro.text);

            SaveSystem.Instance.SaveMultiPatientData();
        }
        else
        {
            SaveSystem.Instance.patientData.AddPhysio(date, morningInput_Band.text, morningInput_Spiro.text, afternoonInput_Band.text, afternoonInput_Spiro.text, eveningInput_Band.text, eveningInput_Spiro.text);

            SaveSystem.Instance.SavePatiendData();
        }

        Call(ObjectDisabler(excerciseInputWindow, 0f));

        AddPhysioSegment(date, morningInput_Band.text, morningInput_Spiro.text, afternoonInput_Band.text, afternoonInput_Spiro.text, eveningInput_Band.text, eveningInput_Spiro.text);

        morningInput_Band.text = "";
        morningInput_Spiro.text = "";
        afternoonInput_Band.text = "";
        afternoonInput_Spiro.text = "";
        eveningInput_Band.text = "";
        eveningInput_Spiro.text = "";
    }

    public void AddPatientSegment(string name, string age, string uid, int index)
    {
        Instantiate(patientSegment, multiPatientPanel).GetComponent<PatientBlock>().Init(name, age, uid, index);
    }
    public void AddDietSegment(string d, string b, string l, string dd)
    {
        Instantiate(dietSegment, dietPanel).GetComponent<DietSegment>().Init(d, b, l, dd);
    }
    public void AddPhysioSegment(string d, string mb, string ms, string ab, string aS, string eb, string es)
    {
        Instantiate(excerciseSegment, excercisePanel).GetComponent<ExcerciseSegment>().Init(d, mb, ms, ab, aS, eb, es);
    }

    public void DeletePreviousData()
    {
        while (dietPanel.childCount > 0)
        {
            DestroyImmediate(dietPanel.GetChild(0).gameObject);
        }
        while (excercisePanel.childCount > 0)
        {
            DestroyImmediate(excercisePanel.GetChild(0).gameObject);
        }
    }

    private string apiUrl = "https://www.convertcsv.io/api/v1/json2csv";
    private string authToken = "24b81b83c136f826409ff8f062f2672a03f53abe";

    public void ExportData()
    {
        StartCoroutine(UploadJSONFile());

        StartCoroutine(FileLocationBlock());
    }

    IEnumerator UploadJSONFile()
    {
        string filePath = Application.persistentDataPath + "/patientsDatabase.json";

        if (!File.Exists(filePath))
        {
            Debug.LogError("JSON file not found at: " + filePath);
            yield break;
        }

        byte[] fileData = File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("infile", fileData, "data.json", "application/json");

        UnityWebRequest request = UnityWebRequest.Post(apiUrl, form);
        request.SetRequestHeader("Authorization", "Token " + authToken);

        string outputFilePath = Path.Combine(Application.persistentDataPath, "output.csv");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error uploading JSON: " + request.error);
        }
        else
        {
            File.WriteAllText(outputFilePath, request.downloadHandler.text);
            Debug.Log("CSV saved to: " + outputFilePath);
            Application.OpenURL(outputFilePath);
        }
    }

    IEnumerator FileLocationBlock ( )
    {
        yield return new WaitForSeconds (0f);

        if (role == Role.Patient)
        fileLocationText.text = Application.persistentDataPath + "/" + PlayerPrefs.GetString(PlayerPrefsManager.PatientUID) + ".json";
        else
        fileLocationText.text = Application.persistentDataPath + "/patientsDatabase.json";

        
        fileLocationBlock.SetActive(true);

        yield return new WaitForSeconds (4f);
        fileLocationBlock.SetActive(false);
    }

}
