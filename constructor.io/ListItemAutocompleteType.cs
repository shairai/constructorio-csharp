using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConstructorIO
{
    public enum ListItemAutocompleteType
    {
        [StringValue("Products")]
        Products,
        [StringValue("Search Suggestions")]
        SearchSuggestions
    }
}