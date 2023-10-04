using UnityEngine;

public class SeasonSwitch : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 60f;

    public Transform clockHandTransform;
    private float _day;



    private void Update()
    {
        _day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float dayNormalized = _day % 1f;

        float rotationDegreesPerDay = 360f;

        float angle = dayNormalized * rotationDegreesPerDay;

        
        clockHandTransform.eulerAngles = new Vector3(0, 0, dayNormalized * rotationDegreesPerDay);
    }
}
