using UnityEngine;
using UnityEngine.UI;

public class OxygenUI : MonoBehaviour
{
    public Slider oxygenBar;
    public OxygenManager oxygenManager;

   void Update()
    {
        if (oxygenManager != null && oxygenBar != null)
        {
            oxygenBar.value = oxygenManager.oxygenLevel / oxygenManager.maxOxygen;
        }
    }
    
}
