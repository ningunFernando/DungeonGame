using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChargeCircles : MonoBehaviour
{
    public Image circle;
    public float durationTime = 3f;
    public float rechargeTime = 3f;


    void Start()
    {
        StartCoroutine(RunCircle());
    }

    private IEnumerator RunCircle()
    {
        float time = 0f;
        circle.fillAmount = 0f; 

        while (time < durationTime)
        {
            circle.fillAmount = time / durationTime;
            time += Time.deltaTime;
            yield return null; 
        }

        circle.fillAmount = 1f;
        StartCoroutine(ChargeCircle());

  
    }
    private IEnumerator ChargeCircle()
    {
        float time = 0f;
        circle.fillAmount = 0f;

        while (time < rechargeTime)
        {
            circle.fillAmount = 1-(time / rechargeTime);
            time += Time.deltaTime;
            yield return null; 
        }

        circle.fillAmount = 0f;


    }
}