using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OperaHouse {
    public class BookIndex {
        public List<PageElement> _elements = new List<PageElement>();

        private int _currentPage = 1;
        public int MaxPage { get => ( _elements.Count / 12 ) + ( _elements.Count % 12 > 0 ? 1 : 0 ); }

        public List<PageElement> GetNextPageElements() {
            _currentPage = _currentPage + 1 <= MaxPage ? _currentPage + 1 : _currentPage;
            return _elements.GetRange(( _currentPage - 1 ) * 12, _elements.Count - ( _currentPage * 12 ));
        }

        public List<PageElement> GetPrevPageElements() {
            return null;
        }
    }
}