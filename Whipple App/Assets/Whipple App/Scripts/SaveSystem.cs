using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private void Awake() => Instance = this;

    public PatientData patientData;
    public MultiPatientData multiPatientData;

    public int index; // index
    public int selectedIndex; // selected block(patient) index

    private DatabaseReference databaseReference;

    private void Start()
    {
        patientData = new PatientData();
        multiPatientData = new MultiPatientData();

        if (AppManager.Instance.role == AppManager.Role.Patient)
        {
            if (PlayerPrefs.GetString(PlayerPrefsManager.PatientName, "") != "")
                LoadPatientFromJson();
        }

        if (AppManager.Instance.role == AppManager.Role.Nurse)
        {

        }

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public int GetIndex()
    {
        return multiPatientData.patientData.Count - 1;
    }

    public void SavePatiendData()
    {
        string name = PlayerPrefs.GetString(PlayerPrefsManager.PatientName);
        int age = PlayerPrefs.GetInt(PlayerPrefsManager.PatientAge);
        string uid = PlayerPrefs.GetString(PlayerPrefsManager.PatientUID);

        patientData._patientName = name;
        patientData._patientAge = age;
        patientData._patientUID = uid;

        string data = JsonUtility.ToJson(patientData);
        string filePath = Application.persistentDataPath + "/" + uid + ".json";
        System.IO.File.WriteAllText(filePath, data);

        string json = JsonUtility.ToJson(patientData);
        databaseReference.Child("users").Child(patientData._patientName + " (UID: " + patientData._patientUID + ")").SetRawJsonValueAsync(json);

        Debug.Log("<color=#ff0000><b>Data Saved</b></color>");
        Debug.Log(filePath);
        Debug.Log(data);
    }

    public void LoadPatientFromJson()
    {
        string uid = PlayerPrefs.GetString(PlayerPrefsManager.PatientUID);

        string filePath = Application.persistentDataPath + "/" + uid + ".json";
        string data = System.IO.File.ReadAllText(filePath);

        Debug.Log("Loaded File: " + filePath);
        Debug.Log("Data: " + data);

        patientData = JsonUtility.FromJson<PatientData>(data);

        foreach (var dietData in patientData.dietData)
        {
            AppManager.Instance.AddDietSegment(dietData._date, dietData.breakfast, dietData.lunch, dietData.dinner);
        }
        foreach (var physioData in patientData.physioData)
        {
            AppManager.Instance.AddPhysioSegment(physioData._date, physioData.morning_band, physioData.morning_spiro, physioData.afternoon_band, physioData.afternoon_spiro, physioData.evening_band, physioData.evening_spiro);
        }
    }

    public void LoadPatientDatabase()
    {
        string filePath = Application.persistentDataPath + "/patientsDatabase.json";
        string data = System.IO.File.ReadAllText(filePath);

        Debug.Log("Loaded File: " + filePath);
        Debug.Log("Data: " + data);

        multiPatientData = JsonUtility.FromJson<MultiPatientData>(data);

        for (int i = 0; i < multiPatientData.patientData.Count; i++)
        {
            AppManager.Instance.AddPatientSegment(multiPatientData.patientData[i]._patientName, multiPatientData.patientData[i]._patientAge.ToString(), multiPatientData.patientData[i]._patientUID, i);

            index = i;
        }
    }

    public void AddSelectedPatientData(int i)
    {
        selectedIndex = i;

        AppManager.Instance.DeletePreviousData();

        foreach (var dietData in multiPatientData.patientData[selectedIndex].dietData)
        {
            AppManager.Instance.AddDietSegment(dietData._date, dietData.breakfast, dietData.lunch, dietData.dinner);
        }
        foreach (var physioData in multiPatientData.patientData[selectedIndex].physioData)
        {
            AppManager.Instance.AddPhysioSegment(physioData._date, physioData.morning_band, physioData.morning_spiro, physioData.afternoon_band, physioData.afternoon_spiro, physioData.evening_band, physioData.evening_spiro);
        }
    }

    public void SaveMultiPatientData()
    {
        int NurseCode = PlayerPrefs.GetInt("NurseCode", 0);

        if (NurseCode == 0)
        {
            NurseCode = Random.Range(1000, 10000);
            PlayerPrefs.SetInt("NurseCode", NurseCode);
        }

        string data = JsonUtility.ToJson(multiPatientData);
        string filePath = Application.persistentDataPath + "/patientsDatabase.json";

        System.IO.File.WriteAllText(filePath, data);

        string json = JsonUtility.ToJson(multiPatientData);
        databaseReference.Child("users").Child("Patient Database" + " (UID : " + NurseCode + ")").SetRawJsonValueAsync(json);

        Debug.Log("<color=#ff0000><b>Data Saved</b></color>");
        Debug.Log("Loaded File: " + filePath);
        Debug.Log("Data: " + data);
    }

    public void AddMultiPatientData(string name, int age, string uid)
    {
        PatientData patientData = new PatientData();

        patientData._patientName = name;
        patientData._patientAge = age;
        patientData._patientUID = uid;

        multiPatientData.patientData.Add(patientData);

        SaveMultiPatientData();
    }

    public void AddMultiPatientDietData(string date, string breakfast, string lunch, string dinner)
    {
        multiPatientData.patientData[index].AddDiet(date, breakfast, lunch, dinner);
    }
    public void AddMultiPatientPhysioData(string date, string mb, string ms, string ab, string aS, string eb, string es)
    {
        multiPatientData.patientData[index].AddPhysio(date, mb, ms, ab, aS, eb, es);
    }
}

[System.Serializable]
public class MultiPatientData
{
    public List<PatientData> patientData = new List<PatientData>();
}

[System.Serializable]
public class PatientData
{
    public string _patientName;
    public int _patientAge;
    public string _patientUID;
    public List<DietData> dietData = new List<DietData>();
    public List<PhysioData> physioData = new List<PhysioData>();

    public void AddDiet(string date, string breakfast, string lunch, string dinner)
    {
        dietData.Add(new DietData(date, breakfast, lunch, dinner));
    }
    public void AddPhysio(string date, string mb, string ms, string ab, string aS, string eb, string es)
    {
        physioData.Add(new PhysioData(date, mb, ms, ab, aS, eb, es));
    }
}

[System.Serializable]
public class DietData
{
    public string _date;
    public string breakfast;
    public string lunch;
    public string dinner;

    public DietData(string d, string b, string l, string di)
    {
        _date = d;
        breakfast = b;
        lunch = l;
        dinner = di;
    }
}

[System.Serializable]
public class PhysioData
{
    public string _date;
    public string morning_band;
    public string morning_spiro;
    public string afternoon_band;
    public string afternoon_spiro;
    public string evening_band;
    public string evening_spiro;

    public PhysioData(string d, string mb, string ms, string ab, string aS, string eb, string es)
    {
        _date = d;
        morning_band = mb;
        morning_spiro = ms;
        afternoon_band = ab;
        afternoon_spiro = aS;
        evening_band = eb;
        evening_spiro = es;
    }
}