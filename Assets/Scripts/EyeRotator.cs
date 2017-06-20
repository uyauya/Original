using UnityEngine;

public class EyeRotator : MonoBehaviour 
{
	public Vector3 RotationL;
	public Vector3 RotationR;
	private Animator _animator;

	// Use this for initialization
	void Start () {
		this._animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void OnAnimatorIK(int layerIndex) {
		this._animator.SetBoneLocalRotation (HumanBodyBones.LeftEye, Quaternion.Euler (this.RotationL));
		this._animator.SetBoneLocalRotation (HumanBodyBones.RightEye, Quaternion.Euler (this.RotationR));
	}
}
