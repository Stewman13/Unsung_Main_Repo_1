using UnityEngine;
using System.Collections;

public class __Animation : MonoBehaviour {

	public static __Animation inst;

	public Animator anim;

	void Awake()
	{
		inst = this;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void AnimationWalk(){

		//anim.SetInteger("HeroIsLerping",1);

	}
}
