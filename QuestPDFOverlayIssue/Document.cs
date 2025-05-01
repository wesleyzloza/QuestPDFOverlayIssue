using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using static QuestPDF.Fluent.DocumentOperation;

namespace QuestPDFOverlayIssue
{
    public class Document()
    {
        private List<string> _sections = [];

        public void Create(string savePath)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            _sections = [];

            var overlayPath = Path.GetTempFileName();
            var overlayFileStream = File.Create(overlayPath);
            Utilities.GetEmbeddedResource("QuestPDFOverlayIssue.Assets.Overlay.pdf").CopyTo(overlayFileStream);
            overlayFileStream.Close();

            var headerFooterFields = new HeaderFooterFields()
            {
                HeaderLabel = "Header Label",
                HeaderAdditionalText = "Header Additional Text",
                FooterLabel = "Footer Label",
                FooterAdditionalText = "Footer Additional Text"
            };

            TryAddCoverPage(headerFooterFields, overlayPath);
            MergeSections(savePath);
        }

        private void TryAddCoverPage(HeaderFooterFields data, string overlayPath)
        {
            var baseCoverPagePath = Path.GetTempFileName();
            var baseCoverPage = new CoverPage(data);
            baseCoverPage.GeneratePdf(baseCoverPagePath);

            if (!string.IsNullOrEmpty(overlayPath))
            {
                try
                {
                    var extendedCoverPagePath = Path.GetTempFileName();

                    LoadFile(baseCoverPagePath)
                        .OverlayFile(new LayerConfiguration
                        {
                            FilePath = overlayPath,
                            TargetPages = "1",
                            SourcePages = "1",
                            RepeatSourcePages = "1"
                        })
                        .Save(extendedCoverPagePath);

                    _sections.Add(extendedCoverPagePath);
                }
                catch (Exception ex) {
                    throw new Exception(
                        "Failed to create cover page. " +
                        "An error occurred while adding the overlay image.", ex);
                }
            }
            else
            {
                _sections.Add(baseCoverPagePath);
            }
        }

        private void MergeSections(string savePath)
        {
            var sourceFile = _sections.First();
            if (sourceFile == null) throw new Exception("Unable able to create the document as no sections have been specified.");

            var documentOperation = LoadFile(sourceFile);
            for (var i = 1; i < _sections.Count; i++)
            {
                var section = _sections[i];
                documentOperation = documentOperation.MergeFile(section);
            }

            documentOperation.Save(savePath);
        }
    }
}
