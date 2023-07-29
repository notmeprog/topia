using UnityEngine;

public class DestroyableMain : MonoBehaviour
{
    [SerializeField] GameObject destroyParticle;

    public void TakeDamage()
    {
        Instantiate(destroyParticle, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}
