using UnityEngine;

public class SeasonSwitch : MonoBehaviour
{
    private const float REAL_SECONDS_PER_INGAME_DAY = 240f;
    public GameObject hybridMakerObject;
    public Transform clockHandTransform;
    private float _day;
    private float seasonTime = 60f;


    private void Update()
    {
        seasonTime -= Time.deltaTime;

        if (seasonTime <= 0f)
        {
            seasonTime = 60f;
            hybridMakerObject.SendMessage("SeasonChange");
        }
        
        _day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;

        float dayNormalized = _day % 1f;

        float rotationDegreesPerDay = 360f;

        
        
        clockHandTransform.eulerAngles = new Vector3(0, 0, dayNormalized * rotationDegreesPerDay);
    }
}
