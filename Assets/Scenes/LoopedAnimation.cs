using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LoopedAnimation : MonoBehaviour
{
    //User Defined Classes
    [Serializable]
    public class AnimationClass{
        public Sprite[] sprites = new Sprite[1];
        public float[] spriteTimer = new float[1];
        public int spriteIndex = 0;

    }
    [SerializeField]
    public AnimationClass animation;
    SpriteRenderer sr;
    float animationTimer = 0;

    void Start(){
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    void Update(){
        AnimationLooped(animation);
    }

    void AnimationLooped(AnimationClass anim){
        animationTimer += Time.deltaTime;
        sr.sprite = anim.sprites[anim.spriteIndex];
        if(animationTimer >= anim.spriteTimer[anim.spriteIndex]){
            anim.spriteIndex++;
            animationTimer = 0;
            if(anim.spriteIndex >= anim.sprites.Length){
                anim.spriteIndex = 0;
            }
        }
    }
}
