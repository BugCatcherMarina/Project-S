using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ProjectS.Map;

namespace ProjectS.UI
{
    public class Status : MonoBehaviour
    {
        private Tile selectedTile = null;
        private Tile currentTile = null;
        // Start is called before the first frame update
        void Start()
        {
            Tile.OnClick += OnTileClicked;
            Tile.OnPointerEnter += OnTilePointerEnter;
            Tile.OnPointerExit += OnTilePointerExit;

        }


        void OnTileClicked(Tile tile) 
        {
            selectedTile = tile;
            UpdateText();
        }
        void OnTilePointerEnter(Tile tile)
        {
            currentTile = tile;
            UpdateText();
        }
        void OnTilePointerExit(Tile tile)
        {
            currentTile = null;
            UpdateText();
        }


        void UpdateText() 
        {
            string _selectedTile = "NULL";
            string _currentTile = "NULL";
            if (selectedTile != null) _selectedTile = selectedTile.transform.name;
            if (currentTile != null) _currentTile = currentTile.transform.name;



            GetComponent<TextMeshProUGUI>().text = _selectedTile + "is selected. Hovering over " + _currentTile;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}