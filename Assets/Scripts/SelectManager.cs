using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;//追加
using UnityEngine.UI;//追加

public class SelectManager : MonoBehaviour
{
    [SerializeField] int _boardXMax;
    [SerializeField] int _boardYMax;
    
    [SerializeField] Transform _tableTransform;
    [SerializeField] GameObject _p1CursorObj;
    [SerializeField] GameObject[,] _boardObjs;

    [Header("確認用")]
    //SerializeFieldは確認用
    [SerializeField] List<GameObject> _ListObjs;
    [SerializeField] int _1pBoardX = 0;
    [SerializeField] int _1pBoardY = 0;
    [SerializeField] EventSystem eventSystem;
    [SerializeField] GameObject _btnObj;

    // Start is called before the first frame update
    void Start()
    {
        // 子オブジェクトを全て取得する
        foreach (Transform child in _tableTransform)
        {
            // 子オブジェクトに対する処理をここに書く
            _ListObjs.Add(child.gameObject);

        }

        _boardObjs = new GameObject[_boardXMax, _boardYMax];   

        int num = 0;
        for (int i = 0; i < _boardXMax;i++)
        {
            for (int j = 0; j < _boardYMax ;j++)
            {
                _boardObjs[i, j] = _ListObjs[j+(_boardXMax*num)];
                Debug.Log(_boardObjs[i, j].name);
                _boardObjs[i, j] .GetComponent<Button>().onClick.AddListener(EnterChara); //これによってボタンの処理が走るのでなく機能が備わっただけ
            }
            num++;
        }
        
        _p1CursorObj.transform.SetParent(_boardObjs[0, 0].transform);
        
        //ローカルポジリセット
        _p1CursorObj.transform.localPosition = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.UpArrow))
        {
            if(_1pBoardX == 0)
            {
                _1pBoardX = _boardXMax - 1;
            }
            else
            {
                _1pBoardX --;
            }
            _p1CursorObj.transform.SetParent(_boardObjs[_1pBoardX, _1pBoardY].transform);
            _p1CursorObj.transform.localPosition = Vector3.zero;
            Debug.Log(_p1CursorObj.transform.parent + "ボードポジ：" + _1pBoardX+","+ _1pBoardY);
        }

        if (Input.GetKeyDown (KeyCode.DownArrow))
        {
            if(_1pBoardX == (_boardXMax - 1))
            {
                _1pBoardX = 0;
            }
            else
            {
                _1pBoardX ++;
            }
            _p1CursorObj.transform.SetParent(_boardObjs[_1pBoardX, _1pBoardY].transform);
            _p1CursorObj.transform.localPosition = new Vector3(0,0,0);
            Debug.Log(_p1CursorObj.transform.parent + "ボードポジ：" + _1pBoardX+","+ _1pBoardY);

        }
        if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            if(_1pBoardY == 0)
            {
                _1pBoardY = _boardYMax - 1;
            }
            else
            {
                _1pBoardY --;
            }
            _p1CursorObj.transform.SetParent(_boardObjs[_1pBoardX, _1pBoardY].transform);
            _p1CursorObj.transform.localPosition = new Vector3(0,0,0);
            Debug.Log(_p1CursorObj.transform.parent + "ボードポジ：" + _1pBoardX+","+ _1pBoardY);

        }

        if (Input.GetKeyDown (KeyCode.RightArrow))
        {
            if(_1pBoardY == (_boardYMax - 1))
            {
                _1pBoardY = 0;
            }
            else
            {
                _1pBoardY ++;
            }
            _p1CursorObj.transform.SetParent(_boardObjs[_1pBoardX, _1pBoardY].transform);
            _p1CursorObj.transform.localPosition = new Vector3(0,0,0);
            Debug.Log(_p1CursorObj.transform.parent.name + "ボードポジ：" + _1pBoardX+","+ _1pBoardY);

        }
        if (Input.GetKeyDown (KeyCode.K))
        {

            _boardObjs[_1pBoardX, _1pBoardY].GetComponent<Button>().onClick.Invoke();//falseで呼ばれる
        }
    }

    void EnterChara()
    {
        //有効なイベントシステムを取得
        eventSystem = EventSystem.current;
        
        //カーソルを置いてSpaceした場合
        if(eventSystem.currentSelectedGameObject==null) //Buttonを手動でクリックした時だけ入るため
        {
            Debug.Log("選んだキャラ"+ _boardObjs[_1pBoardX, _1pBoardY].name + "ボードポジ：" + _1pBoardX+","+ _1pBoardY);

        }
        //ボタンを直接クリックした場合
        else
        {
            _btnObj = eventSystem.currentSelectedGameObject;


            //カーソルオブジェクトを子供にする
            _p1CursorObj.transform.SetParent(_btnObj.transform);

            switch(_btnObj.transform.localPosition.y)
            {
                case 0:
                    _1pBoardX = 1;
                    break;
                case -100:
                    _1pBoardX = 2;
                    break;
                case 100:
                    _1pBoardX = 0;
                    break;
                default:
                    Debug.Log("X座標に相当なし");
                    break;
            }
            switch(_btnObj.transform.localPosition.x)
            {
                case 100:
                    _1pBoardY = 1;
                    break;
                case 0:
                    _1pBoardY = 0;
                    break;
                case 200:
                    _1pBoardY = 2;
                    break;
                default:
                    Debug.Log("Y座標に相当なし");
                    break;
            }

            _p1CursorObj.transform.localPosition = new Vector3(0,0,0);
        
            Debug.Log("選んだキャラ"+ _btnObj.name + "ボードポジ：" + _1pBoardX+","+ _1pBoardY);
        }
    }
}
