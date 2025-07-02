namespace JPBillJobDetail.Models
{
    public class CellData
    {
        public string Text { get; set; } = string.Empty;
        public int Span { get; set; } = 1;
        public bool IsHeader { get; set; } = false;
        public TextAlignment Alignment { get; set; } = TextAlignment.Left;
    }

    public enum TextAlignment
    {
        Left,
        Center,
        Right
    }
}
