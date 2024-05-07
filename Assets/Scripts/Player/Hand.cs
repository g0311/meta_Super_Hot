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
        if (_isLeft)
        { //�޼�
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) && _weapon)
            {
                _weapon.GetComponent<Gun>().Fire();
            } //�߻�

            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                _weapon.transform.SetParent(null);
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                _weapon.GetComponent<Projectile>().enabled = true;
                _weapon.GetComponent<Projectile>()._isGravity = true;

                _weapon.GetComponent<Rigidbody>().isKinematic = false;
                _weapon.GetComponent<CapsuleCollider>().enabled = true;
            } //�� ������
        }
        else
        { //������
            if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger) && _weapon)
            {
                _weapon.GetComponent<Gun>().Fire();
            } //�߻�

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                //�� ���� ���ֱ�
                _weapon.transform.SetParent(null);
                //�� �޽� Ű��
                GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
                _weapon.GetComponent<Rigidbody>().isKinematic = false;
                _weapon.GetComponent<Projectile>().enabled = true;
                _weapon.GetComponent<Projectile>()._isGravity = true;
                _weapon.GetComponent<CapsuleCollider>().enabled = true;
            } //�� ������
        }

        float moveDistance = Vector3.Distance(_prevPos, transform.position);
        Debug.Log("move distance = " + moveDistance);
        GameMode.Instance._deltaTime = moveDistance * 0.0000001f;
        _prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {   //���� �ε�������
        if (collision.gameObject.CompareTag("Weapon")
                &&
                !_weapon)
        {
            _weapon = collision.gameObject;
            //���⵵ �浹ü, ������ ���� �� ����ڿ��� ���� ������ �ȵ�
            _weapon.GetComponent<Projectile>().enabled = false;

            //�� �޽� ��������
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            _weapon.GetComponent<Rigidbody>().isKinematic = true;
            _weapon.GetComponent<CapsuleCollider>().enabled = false;
            _weapon.transform.SetParent(gameObject.transform);
            _weapon.transform.localPosition = Vector3.zero;
            _weapon.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
