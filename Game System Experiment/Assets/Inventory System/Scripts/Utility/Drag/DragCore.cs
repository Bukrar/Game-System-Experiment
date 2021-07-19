using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Utility.Drag
{
    public class DragCore<T> : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
        where T : class
    {
        Vector3 startPosition;
        Transform originalParent;
        IDragContainer<T> source;

        Canvas parentCanvas;

        private void Awake()
        {
            parentCanvas = GetComponentInParent<Canvas>();
            source = GetComponentInParent<IDragContainer<T>>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            startPosition = transform.position;
            originalParent = transform.parent;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            transform.SetParent(parentCanvas.transform, true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = startPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            transform.SetParent(originalParent, true);

            IDragContainer<T> container;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                container = GetContainer(eventData);
            }
            else
            {
                container = null;
            }

            if (container != null)
            {
                DragItemInSlot(container);
            }
        }

        private IDragContainer<T> GetContainer(PointerEventData eventData)
        {
            if (eventData.pointerEnter)
            {
                var container = eventData.pointerEnter.GetComponentInParent<IDragContainer<T>>();
                return container;
            }
            return null;
        }

        private void DragItemInSlot(IDragContainer<T> destination)
        {
            if (ReferenceEquals(source, destination)) return;

            if (destination == null || source == null ||
                destination.GetItem() == null ||
                ReferenceEquals(destination.GetItem(), source.GetItem()))
            {
                SimpleTransfer(destination);
                return;
            }

            Transfer(source, destination);
        }


        private void SimpleTransfer(IDragContainer<T> destination)
        {
            print("簡易交換");
            var draggingItem = source.GetItem();
            source.RemoveItem();
            destination.AddItem(draggingItem);
        }

        private void Transfer(IDragContainer<T> source, IDragContainer<T> destination)
        {
            print("兩樣交換");
            var sourceItem = source.GetItem();
            var destinationItem = destination.GetItem();

            source.RemoveItem();
            destination.RemoveItem();

            source.AddItem(destinationItem);
            destination.AddItem(sourceItem);
        }
    }
}

