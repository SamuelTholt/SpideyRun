using UnityEngine;

public class StrafeEventRelay : MonoBehaviour
{
    public void FinishStrafe()
    {
        // Nájde komponent PohybHraca na rodièovi a zavolá FinishStrafe
        PohybHraca pohyb = GetComponentInParent<PohybHraca>();
        if (pohyb != null)
        {
            pohyb.FinishStrafe();
        }
        else
        {
            Debug.LogWarning("PohybHraca script not found on parent!");
        }
    }
}
