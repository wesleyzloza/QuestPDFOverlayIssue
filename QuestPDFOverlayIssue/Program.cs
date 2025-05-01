using QuestPDFOverlayIssue;

var saveDirectory = Path.GetTempPath();

for (int i = 0; i < 2050; i++)
{
    Console.WriteLine($"Creating document #{i + 1}");
    var fileName = Guid.NewGuid().ToString() + ".pdf";
    var filePath = Path.Combine(saveDirectory, fileName);
    var document = new Document();
    document.Create(filePath);
}