using UnityEngine;
using UnityEngine.Events;



/// <summary>
/// ����� �������� 
/// 1. ����������� ������ ���������� 
/// 2. ����������� ������ �� ��������
/// </summary>
[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody))]
public class Reception : MonoBehaviour
{
    public event UnityAction newConsumerByeBegin;
    public event UnityAction leaveConsumer;

    /// <summary>
    /// ����������� ������ ����������
    /// </summary>
    /// <param name="collision"></param>
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Consumer enemy))
        {
            newConsumerByeBegin?.Invoke();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Consumer enemy))
        {
            leaveConsumer?.Invoke();
        }
    }


    /// <summary>
    /// ������� ���� �� ���������
    /// </summary>
    /*
    float doubleClickStart = 0;
    void OnMouseUp()
    {
        if ((Time.time - doubleClickStart) < 0.3f)
        {
            this.OnDoubleClick();
            doubleClickStart = -1;
        }
     else
        {
            doubleClickStart = Time.time;
        }
    }
    void OnDoubleClick()
    {
        Debug.Log("Double Clicked!");
    }
    */
}
