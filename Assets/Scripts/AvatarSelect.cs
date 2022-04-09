using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AvatarSelect : MonoBehaviour
{
    [SerializeField] private Sprite[] _avatars;
    [SerializeField] private Image _currentPlayerAvatar;

    private int numberAvatar = 0;
    public event UnityAction<int> beginGame;


    /// <summary>
    /// ��������� ���������� �������
    /// </summary>
    void Start()
    {
        _currentPlayerAvatar.sprite = _avatars[numberAvatar];
        NewAvatar();
    }
    /// <summary>
    /// ��������� ������� �� ������ ������
    /// </summary>
    private void NewAvatar() {
        _currentPlayerAvatar.sprite = _avatars[numberAvatar];
    }

    /// <summary>
    /// ������ ��� ���������� � ������������ �������
    /// </summary>
    public void StepLeft() {
        numberAvatar--;
        if (numberAvatar < 0) {
            numberAvatar = _avatars.Length - 1;
        }
        NewAvatar();
    }
    public void StepRight()
    {
        numberAvatar++;
        if (numberAvatar > _avatars.Length - 1) {
            numberAvatar = 0;
        }
        NewAvatar();
    }
    public void SetAvatar() {
        beginGame?.Invoke(numberAvatar);
    }
}
