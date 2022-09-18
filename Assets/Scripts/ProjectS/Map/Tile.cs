using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ProjectS.Map
{
    public class Tile : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public static event Action<Tile> OnClick;
        
        [SerializeField] private TileDefaults tileDefaults;

        private bool isSelected;
        
        private Material Material
        {
            get
            {
                _material ??= GetComponent<MeshRenderer>().material;
                return _material;
            }
        }
        
        private Material _material;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isSelected)
            {
                Material.color = tileDefaults.HoverColor;
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!isSelected)
            {
                Material.color = tileDefaults.DefaultColor;
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick?.Invoke(this);
        }

        public void SetIsSelected(bool isTileSelected)
        {
            isSelected = isTileSelected;
            Material.color = isTileSelected ? tileDefaults.SelectedColor : tileDefaults.DefaultColor;
        }

        private void Start()
        {
            Material.color = tileDefaults.DefaultColor;
        }
    }
}
