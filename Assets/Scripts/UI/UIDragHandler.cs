using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler,
    IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform canvas;          //得到canvas的ugui坐标
    private RectTransform imgRect;        //得到图片的ugui坐标
    private Image img;

    private bool isDraging = false;
    private Vector3 oldPosition;
    private Color oldColor;
    private Vector2 offset = new Vector3();    //用来得到鼠标和图片的差值

    public Action OnDragStart;
    public Action OnDragUpdate;
    public Action OnDragEnd;

    public bool IsDraging
    {
        get
        {
            return isDraging;
        }
    }

    // Use this for initialization
    void Start()
    {
        canvas = (RectTransform)transform.parent;
        imgRect = GetComponent<RectTransform>();
        img = GetComponent<Image>();
        oldPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mouseDown = eventData.position;    //记录鼠标按下时的屏幕坐标
        Vector2 mouseUguiPos = new Vector2();   //定义一个接收返回的ugui坐标
        
        //RectTransformUtility.ScreenPointToLocalPointInRectangle()：把屏幕坐标转化成ugui坐标
        //canvas：坐标要转换到哪一个物体上，这里img父类是Canvas，我们就用Canvas
        //eventData.enterEventCamera：这个事件是由哪个摄像机执行的
        //out mouseUguiPos：返回转换后的ugui坐标
        //isRect：方法返回一个bool值，判断鼠标按下的点是否在要转换的物体上
        bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDown, Camera.main, out mouseUguiPos);
        if (isRect)   //如果在
        {
            //计算图片中心和鼠标点的差值
            offset = imgRect.anchoredPosition - mouseUguiPos;
            if (OnDragStart!=null)
                OnDragStart();

            img.color = new Color(1, 1, 1, 0.5f);
        }
        isDraging = true;
    }

    //当鼠标拖动时调用   对应接口 IDragHandler
    public void OnDrag(PointerEventData eventData)
    {
        if (isDraging)
        {
            if (OnDragUpdate != null)
                OnDragUpdate();

            //Debug.LogError("OnDrag");
            Vector2 mouseDrag = eventData.position;   //当鼠标拖动时的屏幕坐标
            Vector2 uguiPos = new Vector2();   //用来接收转换后的拖动坐标
                                               //和上面类似
            bool isRect = RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, mouseDrag, Camera.main, out uguiPos);

            if (isRect)
            {
                //设置图片的ugui坐标与鼠标的ugui坐标保持不变
                imgRect.anchoredPosition = offset + uguiPos;
            }
        }
    }

    //当鼠标结束拖动时调用   对应接口  IEndDragHandler
    public void OnEndDrag(PointerEventData eventData)
    {
        if (OnDragEnd != null)
            OnDragEnd();
        offset = Vector2.zero;
        isDraging = false;
        transform.position = oldPosition;

        img.color = new Color(1, 1, 1, 1);
        //Debug.LogError("OnEndDrag");
    }

    //当鼠标进入图片时调用   对应接口   IPointerEnterHandler
    public void OnPointerEnter(PointerEventData eventData)
    {
        //oldColor = img.color;
        //img.color = Color.white;
    }

    //当鼠标退出图片时调用   对应接口   IPointerExitHandler
    public void OnPointerExit(PointerEventData eventData)
    {
        //if (!isDraging)
        //    img.color = oldColor;
    }
}
