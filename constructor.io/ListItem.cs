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
        private string _category;
        private string _url;
        private string _imageUrl;
        private string _description;
        private string _privateID;

        private List<string> _keywords;
        private HashArgs _extraArgs;

        public ListItem(string Name, ListItemAutocompleteType Category)
            :this(Name, StringEnum.GetStringValue(Category))
        {
            
        }

        public ListItem(string Name, string Category)
            :this()
        {
            _name = Name;
            _category = Category;
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

            hash.Add("item_name", _name);
            hash.Add("autocomplete_section", _category);

            if (_category != StringEnum.GetStringValue(ListItemAutocompleteType.SearchSuggestions))
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

        public string Category
        {
            get { return _category; }
            set { _category = value; }
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