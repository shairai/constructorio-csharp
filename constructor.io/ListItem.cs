﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructorIO
{
    using HashArgs = Dictionary<string, object>;

    public class ListItem
    {
        private string _name;
        private string _autocompleteSection;
        private string _url;
        private string _imageUrl;
        private string _description;
        private string _id;
        private int _suggestedScore = -1;

        private string _originalName;

        private List<string> _keywords;
        private Dictionary<string, string> _metadata;
        private HashArgs _extraArgs;

        public ListItem(string Name, ListItemAutocompleteType AutocompleteSection)
            :this(Name, StringEnum.GetStringValue(AutocompleteSection))
        {

        }

        /// <summary>
        /// Create a new ListItem.
        /// </summary>
        /// <param name="Name">The name of the ListItem</param>
        /// <param name="AutocompleteSection">The section this item belongs to.</param>
        /// <param name="ID">A private ID you can use to assosciate records</param>
        /// <param name="Description">A description of the ListItem</param>
        /// <param name="Url">A URL for the ListItem</param>
        /// <param name="ImageUrl">An Image URL for the ListItem</param>
        /// <param name="SuggestedScore">Suggested score ranking for list item. Set to -1 to ignore. </param>
        /// <param name="Keywords">Keywords that represent your item</param>
        public ListItem(string Name = null, string AutocompleteSection = null, string ID = null,
            string Description = null, string Url = null, string ImageUrl = null, int SuggestedScore = -1,
            IEnumerable<string> Keywords = null, IDictionary<string, string> Metadata = null)
            :this()
        {
            _originalName = _name = Name;
            _autocompleteSection = AutocompleteSection;
            _id = ID;
            _description = Description;
            _url = Url;
            _imageUrl = ImageUrl;
            _suggestedScore = SuggestedScore;
            if (Keywords != null) _keywords.AddRange(Keywords);
            if (Metadata != null)
            {
                _metadata = new Dictionary<string, string>(Metadata);
            }
        }

        public ListItem()
        {
            _keywords = new List<string>();
            _metadata = new Dictionary<string, string>();
            _extraArgs = new HashArgs();
        }

        internal HashArgs GetAsHash()
        {
            HashArgs outputHash = new HashArgs();

            if (_name != null) outputHash["item_name"] = _name;
            if (_autocompleteSection != null) outputHash["autocomplete_section"] = _autocompleteSection;

            if (_autocompleteSection != StringEnum.GetStringValue(ListItemAutocompleteType.SearchSuggestions))
                if (_id != null) outputHash["id"] = _id;

            if (_url != null) outputHash["url"] = _url;
            if (_imageUrl != null) outputHash["image_url"] = _imageUrl;
            if (_description != null) outputHash["description"] = _description;
            if (_keywords != null && _keywords.Count != 0) outputHash["keywords"] = _keywords.ToArray();
            if (_metadata != null && _metadata.Count != 0) outputHash["metadata"] = _metadata.ToDictionary(x => x);
            if (_suggestedScore >= 0 && _suggestedScore <= 100) outputHash["suggested_score"] = _suggestedScore;

            if (_extraArgs != null && _extraArgs.Count != 0)
                Util.Merge(_extraArgs, outputHash);

            return outputHash;
        }

        internal HashArgs GetAsRemoveHash()
        {
            HashArgs outputHash = new HashArgs();

            if (_name != null) outputHash["item_name"] = _name;
            if (_autocompleteSection != null) outputHash["autocomplete_section"] = _autocompleteSection;

            if (_autocompleteSection != StringEnum.GetStringValue(ListItemAutocompleteType.SearchSuggestions))
                if (_id != null) outputHash["id"] = _id;

            return outputHash;
        }

        internal HashArgs GetAsModifyHash()
        {
            HashArgs outputHash = new HashArgs();

            if(_id != null)
            {
                outputHash["id"] = _id;
                if(_name != null) outputHash["new_item_name"] = _name;
            }
            else
            {
                if(_originalName != null) outputHash["item_name"] = _originalName;
                outputHash["new_item_name"] = _name;
            }
            
            if (_autocompleteSection != null) outputHash["autocomplete_section"] = _autocompleteSection;
            
            if (_url != null) outputHash["url"] = _url;
            if (_imageUrl != null) outputHash["image_url"] = _imageUrl;
            if (_description != null) outputHash["description"] = _description;
            if (_keywords != null && _keywords.Count != 0) outputHash["keywords"] = _keywords.ToArray();
            if (_metadata != null && _metadata.Count != 0) outputHash["metadata"] = _metadata.ToDictionary(x => x);
            if (_suggestedScore >= 0 && _suggestedScore <= 100) outputHash["suggested_score"] = _suggestedScore;

            if (_extraArgs != null && _extraArgs.Count != 0)
                Util.Merge(_extraArgs, outputHash);

            return outputHash;
        }

        public void AddKeyword(string Keyword)
        {
            _keywords.Add(Keyword);
        }

        public void AddMetadata(string Key, string Value)
        {
            _metadata.Add(Key, Value);
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string AutocompleteSection
        {
            get { return _autocompleteSection; }
            set { _autocompleteSection = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public List<string> Keywords
        {
            get { return _keywords; }
            set { _keywords = value; }
        }

        public int SuggestedScore
        {
            get { return _suggestedScore; }
            set { _suggestedScore = value; }
        }

        public object this[string key]
        {
            get { return _extraArgs[key]; }
            set { _extraArgs[key] = value; }
        }
    }
}