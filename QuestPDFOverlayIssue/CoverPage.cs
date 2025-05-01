using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace QuestPDFOverlayIssue
{
    public class CoverPage(HeaderFooterFields data) : IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(Layout.PAGE_SIZE);
                page.DefaultTextStyle(TextStyles.Body);
                page.AddHeaderFooter(data);
            });
        }
    }
}
