using TMPro;
using UnityEngine;

public class PatientBlock : MonoBehaviour
{
    public TMP_Text patientNameText;
    public TMP_Text patientUIDText;

    private string patientName;
    private string patientAge;
    private string patientUID;

    public int index;

    public void Init(string name, string age, string uid, int i)
    {
        patientName = name;
        patientAge = age;
        patientUID = uid;

        patientNameText.text = patientName;
        patientUIDText.text = "#" + patientUID;

        index = i;
    }

    public void PatientSelected()
    {
        SaveSystem.Instance.AddSelectedPatientData(index);

        AppManager.Instance.PatientSelected(index);
    }
}
