using UnityEngine;

public class StrafeEventRelay : MonoBehaviour
{
    public void FinishStrafe()
    {
        // N�jde komponent PohybHraca na rodi�ovi a zavol� FinishStrafe
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
