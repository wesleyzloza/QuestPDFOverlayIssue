using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestPDFOverlayIssue
{
    public static class HeaderFooter
    {
        /// <summary>
        /// Adds a header and footer to the page.
        /// </summary>
        /// <param name="page">Page instance.</param>
        /// <param name="fields">Header/footer configuration.</param>
        public static PageDescriptor AddHeaderFooter(
            this PageDescriptor page,
            HeaderFooterFields fields
        )
        {
            return page.AddHeaderFooter(
                fields.HeaderLabel,
                fields.HeaderAdditionalText,
                fields.FooterLabel,
                fields.FooterAdditionalText,
                fields.FooterImagePath
            );
        }

        private static PageDescriptor AddHeaderFooter(
            this PageDescriptor page, 
            string headerLabel, string headerAdditionalText,
            string footerLabel, string footerAdditionalText,
            string? footerImagePath = null
        )
        {
            const string HEADER_RESOURCE_NAME = "QuestPDFOverlayIssue.Assets.Header.svg";
            const string FOOTER_RESOURCE_NAME = "QuestPDFOverlayIssue.Assets.Footer.svg";
            var headerImageSvg = Utilities.GetEmbeddedText(HEADER_RESOURCE_NAME);
            var footerImageSvg = Utilities.GetEmbeddedText(FOOTER_RESOURCE_NAME);

            // Setup (Size, Header & Footer Backgrounds)
            page
                .Background()
                .Decoration(decoration =>
                {
                    decoration
                        .Before()
                        .ExtendHorizontal()
                        .Svg(headerImageSvg);

                    decoration
                        .Content()
                        .Extend();

                    if (footerImagePath != null)
                    {
                        decoration
                            .After()
                            .PaddingBottom(Layout.BLEED, Unit.Inch)
                            .PaddingHorizontal(Layout.BLEED, Unit.Inch)
                            .ExtendHorizontal()
                            .Image(footerImagePath);
                    }
                    else
                    {
                        decoration
                           .After()
                           .ExtendHorizontal()
                           .Svg(footerImageSvg);
                    }
                });

            // Header
            page
                .Header()
                .Height(1.5f, Unit.Inch)
                .PaddingTop(Layout.BLEED, Unit.Inch)
                .Column(column =>
                {
                    column
                        .Item()
                        .Height(0.5625f, Unit.Inch)
                        .PaddingHorizontal(0.5f, Unit.Inch)
                        .AlignMiddle()
                        .Text(headerLabel.ToUpper())
                        .Style(TextStyles.HeaderFooter)
                        .AlignRight();

                    column
                        .Item()
                        .Height(0.5625F, Unit.Inch)
                        .PaddingTop(0.125f, Unit.Inch)
                        .PaddingHorizontal(0.5f, Unit.Inch)
                        .AlignTop()
                        .Text(headerAdditionalText)
                        .Style(TextStyles.HeaderFooterSubtitle)
                        .AlignRight();
                });

            // Footer
            page
                .Footer()
                .Height(1.5f, Unit.Inch)
                .PaddingBottom(0.125f, Unit.Inch)
                .AlignBottom()
                .Column(column =>
                {
                    column
                        .Item()
                        .PaddingBottom(0.125f, Unit.Inch)
                        .PaddingHorizontal(0.5f, Unit.Inch)
                        .Text(footerAdditionalText)
                        .Style(TextStyles.HeaderFooterSubtitle);

                    column
                        .Item()
                        .Height(0.5625f, Unit.Inch)
                        .PaddingHorizontal(0.5f, Unit.Inch)
                        .AlignMiddle()
                        .Text(footerLabel)
                        .Style(TextStyles.HeaderFooter)
                        .NormalWeight();
                });

            return page;
        }
    }

    /// <summary>
    /// Header &amp; Footer Fields
    /// </summary>
    public class HeaderFooterFields
    {
        /// <summary>
        /// Header label.
        /// </summary>
        /// <remarks>
        /// This text is displayed <i>on</i> the background placed in the
        /// document header. Note that this background image cannot be
        /// overridden.
        /// </remarks>
        public required string HeaderLabel { get; set; }

        /// <summary>
        /// Header additional text.
        /// </summary>
        /// <remarks>
        /// This text is displayed <i>below</i> the background placed in the
        /// document header. Note that this background image cannot be
        /// overridden.
        /// </remarks>
        public required string HeaderAdditionalText { get; set; }

        /// <summary>
        /// Specifies the background image that should be placed in the
        /// footer.
        /// </summary>
        /// <remarks>
        /// Typically this value would be provided to insert to logo of a sales
        /// representative on the cover page of a document. If the provided
        /// value is null then a default background image is used. Users should
        /// refer to internal documentation for more details on the
        /// layout/format of a typical footer background image.
        /// </remarks>
        public string? FooterImagePath { get; set; }

        /// <summary>
        /// Footer label.
        /// </summary>
        /// <remarks>
        /// This text is displayed <i>on</i> the background placed in the
        /// document footer.
        /// </remarks>
        public required string FooterLabel { get; set; }

        /// <summary>
        /// Footer additional text.
        /// </summary>
        /// <remarks>
        /// This text is displayed <i>above</i> the background placed in the
        /// document footer.
        /// </remarks>
        public required string FooterAdditionalText { get; set; }
    }
}
