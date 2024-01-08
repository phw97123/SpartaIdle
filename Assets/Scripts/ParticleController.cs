using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] ParticleSystem particle; 
    public void StartPlayNotify()
    {
        particle.Play(); 
    }
}
