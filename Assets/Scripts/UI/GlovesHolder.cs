using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GlovesHolder : MonoBehaviour
{
    [SerializeField] private GameObject _gloveElementPrefab;
    private Stack<GameObject> _gloves = new Stack<GameObject>();
    private Stack<GameObject> _inactiveGloves = new Stack<GameObject>();

    public void SetGloves(int value)
    {
        switch (value)
        {
            case int i when value > _gloves.Count:
                while (_gloves.Count<i)
                    TurnOnGlove();
                break;
            
            case int i when value < 0:
                while (_gloves.Count>0)
                    TurnOffGlove();
                break;
            
            case int i when value < _gloves.Count:
                while (_gloves.Count>i)
                    TurnOffGlove();
                break;
        }
    }

    private void TurnOnGlove()
    {
        if (_inactiveGloves.Any())
        {
            var glove = _inactiveGloves.Pop();
            glove.SetActive(true);
            _gloves.Push(glove);
        }
        else
        {
            _gloves.Push(Instantiate(_gloveElementPrefab, transform));
        }
    }

    private void TurnOffGlove()
    {
        if (_gloves.Any())
        {
            var glove = _gloves.Pop();
            glove.SetActive(false);
            _inactiveGloves.Push(glove);
        }
    }
}