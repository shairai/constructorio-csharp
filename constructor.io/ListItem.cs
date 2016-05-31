using System;
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
        private string _privateID;

        private List<string> _keywords;
        private HashArgs _extraArgs;

        public ListItem(string Name, ListItemAutocompleteType AutocompleteSection)
            :this(Name, StringEnum.GetStringValue(AutocompleteSection))
        {
            
        }

        public ListItem(string Name, string AutocompleteSection)
            :this()
        {
            _name = Name;
            _autocompleteSection = AutocompleteSection;
        }

        public ListItem(string PrivateID)
            :this()
        {
            _privateID = PrivateID;
        }

        public ListItem()
        {
            _keywords = new List<string>();
            _extraArgs = new HashArgs();
        }

        public HashArgs GetAsHash(bool forRemove = false)
        {
            HashArgs hash = new HashArgs();

            if (_name != null) hash.Add("item_name", _name);
            if (_autocompleteSection != null) hash.Add("autocomplete_section", _autocompleteSection);

            if (_autocompleteSection != StringEnum.GetStringValue(ListItemAutocompleteType.SearchSuggestions))
                if (_privateID != null) hash.Add("id", _privateID);

            if (!forRemove)
            {
                if (_url != null) hash.Add("url", _url);
                if (_imageUrl != null) hash.Add("image_url", _imageUrl);
                if (_description != null) hash.Add("description", _description);
                if (_keywords != null && _keywords.Count != 0) hash.Add("keywords", _keywords.ToArray());

                if (_extraArgs != null && _extraArgs.Count != 0)
                    foreach (var arg in _extraArgs)
                        hash.Add(arg.Key, arg.Value);
            }

            return hash;
        }

        public void AddKeyword(string Keyword)
        {
            _keywords.Add(Keyword);
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

        public string PrivateID
        {
            get { return _privateID; }
            set { _privateID = value; }
        }

        public List<string> Keywords
        {
            get { return _keywords; }
        }

        public object this[string key]
        {
            get { return _extraArgs[key]; }
            set { _extraArgs[key] = value; }
        }
    }
}