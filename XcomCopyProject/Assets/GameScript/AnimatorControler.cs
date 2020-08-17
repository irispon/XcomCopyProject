using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorControler : SingletonObject<AnimatorControler>
{
    // Start is called before the first frame update
    [SerializeField]
    private AnimatiorInfo[] animators;
    [HideInInspector]
    public Dictionary<WeaponType, RuntimeAnimatorController> animations;
    public override void Init()
    {
       
    }
    void Start()
    {
        animations = new Dictionary<WeaponType, RuntimeAnimatorController>();
        foreach(AnimatiorInfo animator in animators)
        {
            animations.Add(animator.name, animator.controller);

        }
        animators = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    public struct AnimatiorInfo
    {
        public WeaponType name;
        public RuntimeAnimatorController controller;
    }
}
