using UnityEngine;
using TMPro;

public class DietSegment : MonoBehaviour
{
    public TMP_Text dateText;
    public TMP_Text breakfastInput;
    public TMP_Text lunchInput;
    public TMP_Text dinnerInput;

    public void Init(string date, string breakfast, string lunch, string dinner)
    {
        dateText.text = date;

        breakfastInput.text = breakfast;

        lunchInput.text = lunch;

        dinnerInput.text = dinner;
    }

}

