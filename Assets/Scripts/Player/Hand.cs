using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    // Start is called before the first frame update
    public bool _isLeft = true;
    public GameObject _weapon = null;

    // Update is called once per frame
    void Update()
    {
        if (_isLeft)
        { //왼손
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && _weapon)
            {
                _weapon.GetComponent<Gun>().Fire();
            } //발사

            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                _weapon.transform.SetParent(null);
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                _weapon.GetComponent<Projectile>().enabled = true;
            } //총 던지기
        }
        else
        { //오른손
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && _weapon)
            {
                _weapon.GetComponent<Gun>().Fire();
            } //발사

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                //총 계층 없애기
                _weapon.transform.SetParent(null);
                //손 메쉬 키기
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                _weapon.GetComponent<Projectile>().enabled = true;
                //RB 있어야.. 속도 유지됨
            } //총 던지기
        }
    }

    private void OnCollisionEnter(Collision collision)
    {   //총이 부딪혔을때
        if (collision.gameObject.CompareTag("Weapon")
                &&
                !_weapon)
        {
            _weapon = collision.gameObject;
            //무기도 충돌체, 가지고 있을 땐 사용자에게 공격 받으면 안됨
            _weapon.GetComponent<Projectile>().enabled = false;

            //손 메시 꺼버리기
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            _weapon.transform.SetParent(collision.gameObject.transform);
            //transform.position = ~~~
            //transform.rotation = ~~~
        }
    }
}
