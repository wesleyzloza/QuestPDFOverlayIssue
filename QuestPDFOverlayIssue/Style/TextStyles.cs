using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestPDFOverlayIssue
{
    /// <summary>
    /// Text Styles
    /// </summary>
    /// https://www.questpdf.com/api-reference/text/basics.html#typography-pattern
    public class TextStyles
    {
        public static TextStyle Body => TextStyle
            .Default
            .FontFamily(Theme.BODY_FONT_FAMILY)
            .FontSize(Theme.BODY_FONT_SIZE)
            .LineHeight(1.2f);

        public static TextStyle HeaderFooter => TextStyle
            .Default
            .FontColor(Color.FromHex(Theme.HEADING_FONT_COLOR))
            .FontFamily(Theme.HEADING_FONT_FAMILY)
            .FontSize(14)
            .Bold()
            .LineHeight(1);

        public static TextStyle HeaderFooterSubtitle => TextStyle
            .Default
            .FontFamily(Theme.HEADING_FONT_FAMILY)
            .FontSize(12)
            .LineHeight(1.15f);
    }
}
