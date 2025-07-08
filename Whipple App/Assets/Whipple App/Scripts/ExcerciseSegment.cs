using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExcerciseSegment : MonoBehaviour
{
    public TMP_Text dateText;

    public TMP_Text morningInput_Band;
    public TMP_Text morningInput_Spiro;
    public TMP_Text afternoonInput_Band;
    public TMP_Text afternoonInput_Spiro;
    public TMP_Text eveningInput_Band;
    public TMP_Text eveningInput_Spiro;

    public void Init(string date, string morningBand, string morningSpiro, string afternoonBand, string afternoonSpiro, string eveningBand, string eveningSpiro)
    {
        dateText.text = date;

        morningInput_Band.text = morningBand;
        morningInput_Spiro.text = morningSpiro;

        afternoonInput_Band.text = afternoonBand;
        afternoonInput_Spiro.text = afternoonSpiro;

        eveningInput_Band.text = eveningBand;
        eveningInput_Spiro.text = eveningSpiro;
    }
}

