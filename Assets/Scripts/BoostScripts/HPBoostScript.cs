using System.Collections.Generic;
using UnityEngine;

public class HPBoostScript : MonoBehaviour
{
    public List<string> Taby;
    public int pocetPridanychZivotov = 1;

    private void OnTriggerEnter(Collider collider)
    {
        if (Taby.Contains(collider.tag)) 
        {
            if (collider.CompareTag("Player"))
            {
                HracDotyk hrac = collider.GetComponent<HracDotyk>();
                if (hrac != null)
                {
                    hrac.PridajHP(pocetPridanychZivotov);
                    Destroy(gameObject);
                }
            }
        }
    }
}
