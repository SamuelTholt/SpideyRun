using System.Collections.Generic;
using UnityEngine;

public class ScoreBoostScript : MonoBehaviour
{
    public List<string> Taby;
    public int webCount = 10;
    private void OnTriggerEnter(Collider collider)
    {
        if (Taby.Contains(collider.tag))
        {
            if (collider.CompareTag("Player"))
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.PlusCountTakenWebs(webCount);
                }
                Destroy(gameObject); // Boost sa znièí po použití   
            }
        }
    }


}
