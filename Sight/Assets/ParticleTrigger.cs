using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem ps;
    [SerializeField]
    private float duration = 0.3f;

    // Start is called before the first frame update

        //Note to useful, need to access particle system using ps.main then accessing emission, which has start time, etc.
    void Start()
    {
        ps.Stop();
    }

    public IEnumerator showParticles(float time)
    {
        ps.Play();
        yield return new WaitForSeconds(time);
        ps.Stop();
    } 
    // Update is called once per frame
    void Update()
    {
       
    }
}
