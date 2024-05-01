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
                _weapon.GetComponent<Projectile>().enabled = true;
                //RB �־��.. �ӵ� ������
            } //�� ������
        }
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
            _weapon.transform.SetParent(collision.gameObject.transform);
            //transform.position = ~~~
            //transform.rotation = ~~~
        }
    }
}
