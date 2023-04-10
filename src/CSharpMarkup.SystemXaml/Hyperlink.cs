using System;

namespace CSharpMarkup.SystemXaml
{
    public static partial class Helpers
    {
        public static Hyperlink Hyperlink(Uri uri, params InlineCollectionItem[] content) => Hyperlink(content).NavigateUri(uri);
    }
}