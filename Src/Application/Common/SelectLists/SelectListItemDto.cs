namespace Application.Common.SelectLists;

public sealed class SelectListItemDto
{
    public SelectListItemDto(string value, string text)
    {
        Value = value;
        Text = text;
    }

    public string Value { get; }
    public string Text { get; }
}