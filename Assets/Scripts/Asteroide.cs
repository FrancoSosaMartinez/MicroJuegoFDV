using UnityEngine;

public class Asteroide : MonoBehaviour
{
    public bool esGrande = true;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
    }
}
