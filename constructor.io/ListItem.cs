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
        private string _id;

        private string _originalName;

        private List<string> _keywords;
        private HashArgs _extraArgs;

        public ListItem(string Name, ListItemAutocompleteType AutocompleteSection)
            :this(Name, StringEnum.GetStringValue(AutocompleteSection))
        {

        }

        public ListItem(string Name = null, string AutocompleteSection = null, string ID = null, string Description = null, string URL = null, string ImageURL = null)
            :this()
        {
            _originalName = _name = Name;
            _autocompleteSection = AutocompleteSection;
            _id = ID;
            _description = Description;
            _url = URL;
            _imageUrl = ImageURL;
        }

        public ListItem()
        {
            _keywords = new List<string>();
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

            if (_extraArgs != null && _extraArgs.Count != 0)
                Util.Merge(_extraArgs, outputHash);

            return outputHash;
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

        public string ID
        {
            get { return _id; }
            set { _id = value; }
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