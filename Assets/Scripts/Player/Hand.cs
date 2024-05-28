using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _isLeft = true;
    public GameObject _weapon = null;
    private Vector3 _prevPos;

    // Update is called once per frame
    void Update()
    {
        if (_weapon)
        {
            if (_isLeft)
            { //left
                if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
                {
                    _weapon.GetComponent<Gun>().Fire();
                    OVRInput.SetControllerVibration(0.5f, 0.5f);
                } //

                if (OVRInput.GetDown(OVRInput.Button.Three))
                {
                    _weapon.transform.SetParent(null);
                    GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    _weapon.GetComponent<Projectile>().enabled = true;
                    _weapon.GetComponent<Projectile>()._useGravity = true;
                    _weapon.GetComponent<Rigidbody>().isKinematic = false;
                    _weapon.GetComponent<BoxCollider>().enabled = true;

                    _weapon.gameObject.tag = "Finish";
                    Debug.Log("drop gun");
                    _weapon = null;
                    Debug.Log("dropped gun" + _weapon);
                } //weapon is projectile too
            }
            else
            { //right
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    _weapon.GetComponent<Gun>().Fire();
                    OVRInput.SetControllerVibration(0.5f, 0.5f);
                }

                if (OVRInput.GetDown(OVRInput.Button.One))
                {
                    _weapon.transform.SetParent(null);
                    GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                    _weapon.GetComponent<Projectile>().enabled = true;
                    _weapon.GetComponent<Projectile>()._useGravity = true;
                    _weapon.GetComponent<Rigidbody>().isKinematic = false;
                    _weapon.GetComponent<BoxCollider>().enabled = true;

                    _weapon.gameObject.tag = "Finish";

                    _weapon = null;
                }
            }
        }

        float moveDistance = Vector3.Distance(_prevPos, transform.position);
        GameMode.Instance.SetDeltaTime(moveDistance);
        _prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {//grab gun 
        if (collision.gameObject.CompareTag("Weapon")
                &&
                !_weapon)
        {
            _weapon = collision.gameObject;
            _weapon.GetComponent<Projectile>().enabled = false;

            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            _weapon.GetComponent<Rigidbody>().isKinematic = true;
            _weapon.GetComponent<BoxCollider>().enabled = false;
            _weapon.transform.SetParent(gameObject.transform);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.Euler(0, 90, 90);
        }
    }

    public void Dead()
    {
        if (_weapon)
        {
            Destroy(_weapon);
            _weapon = null;
        }
        if(!_isLeft)
        {
            GetComponentInParent<LaserPointer>().enabled = true;
        }
    }
}
