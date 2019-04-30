using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(activateParticlesandDestroy(other));
        }
    }

    public IEnumerator activateParticlesandDestroy(Collider other,float duration = .3f)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        ps.Play();
        yield return new WaitForSeconds(duration);
        ps.Stop();
        yield return new WaitForSeconds(duration);
        Destroy(other.gameObject);
    } 
}
