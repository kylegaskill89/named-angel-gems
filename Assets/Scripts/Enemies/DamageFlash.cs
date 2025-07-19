using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    
    Material m_Material;
    float flashDuration = .1f;

    void Start()
    {
        m_Material = GetComponentInChildren<MeshRenderer>().material;
    }

    public IEnumerator TriggerDamageFlash()
    {
        m_Material.SetFloat("_FlashAmount", .6f);
        yield return new WaitForSeconds(flashDuration);
        m_Material.SetFloat("_FlashAmount", 0f);
    }

}
