using UnityEngine;
using UnityEngine.UI;

public class HUDInstance : MonoBehaviour
{
    public RawImage[] weaponImage;

    public static HUDInstance hudInstance { get; private set; }
    private void Awake()
    {

        hudInstance = this;
    }
}
