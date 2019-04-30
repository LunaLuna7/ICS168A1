using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private float shakeDuration;
    private float shakePower;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    

    public IEnumerator shake(float duration, float power,float dampingSpeed = 1.0f)
    {
        Vector3 startPos = transform.localPosition;

        while (duration > 0)
        {
            float x = startPos.x +Random.Range(-1f, 1f) * power;
            float y = startPos.y +Random.Range(-1f, 1f) * power;

            transform.localPosition = new Vector3(x, y, startPos.z);

            duration -= Time.deltaTime * dampingSpeed;
            yield return null;
        }

        transform.localPosition = startPos;
    }
        
    /*if (duration > 0)
  {
   float x = Random.Range(-1f, 1f) * magnitude;
   float y = Random.Range(-1f, 1f) * magnitude;

   transform.localPosition = new Vector3(x, y, startPos.z);
   
   shakeDuration -= Time.deltaTime * dampingSpeed;
  }
  else
  {
   shakeDuration = 0f;
   transform.localPosition = initialPosition;
  }*/
}
