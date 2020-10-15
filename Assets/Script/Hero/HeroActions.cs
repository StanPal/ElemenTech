using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroActions : MonoBehaviour
{
    public event System.Action<Elements.ElementalAttribute> onSkillPerformed;
    public event System.Action onPausePeformed;
    public event System.Action onGuardPerformed;
    public event System.Action onGuardExit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
