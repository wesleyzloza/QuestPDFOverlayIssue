using System.Reflection;

namespace QuestPDFOverlayIssue
{
    public class Utilities
    {
        public static Stream GetEmbeddedResource(string resourceName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null) throw new Exception($"Unable to find resource: \"{resourceName}\"");
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public static string GetEmbeddedText(string resourceName)
        {
            using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
            if (stream == null) throw new Exception($"Unable to find resource: \"{resourceName}\"");
            using var streamReader = new StreamReader(stream);
            return streamReader.ReadToEnd();
        }
    }
}
