using System.Xml.Linq;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos;

namespace Apps.Termbase.io.Utils;

public class GlossaryExporter
{
    private XDocument tbxDocument;

    public GlossaryExporter(Stream inputFileStream)
    {
        this.tbxDocument = XDocument.Load(inputFileStream);
    }

    public bool SkipEmptyTerms { get; set; }
    
    public Glossary ExportGlossary(string glossaryTitle = "Default Title")
    {
        XNamespace ns = "urn:iso:std:iso:30042:ed-2";

        var title = tbxDocument.Descendants(ns + "title").FirstOrDefault()?.Value ?? glossaryTitle;
        var description = tbxDocument.Descendants(ns + "p").FirstOrDefault()?.Value ?? "Default Description";

        var conceptEntries = ParseGlossaryConceptEntries(ns).ToList();

        var glossary = new Glossary(conceptEntries)
        {
            Title = title,
            SourceDescription = description
        };

        return glossary;
    }

    private IEnumerable<GlossaryConceptEntry> ParseGlossaryConceptEntries(XNamespace ns)
    {
        var conceptEntries = tbxDocument.Descendants(ns + "conceptEntry");
        foreach (var entry in conceptEntries)
        {
            var id = entry.Attribute("id")?.Value ?? Guid.NewGuid().ToString();
            var languageSections = entry.Elements(ns + "langSec").Select(langSec => ParseLanguageSection(langSec, ns)).Where(section => section != null).ToList();

            if (!SkipEmptyTerms || languageSections.Any(section => section.Terms.Any()))
            {
                var glossaryConceptEntry = new GlossaryConceptEntry(id, languageSections);
                yield return glossaryConceptEntry;
            }
        }
    }

    private GlossaryLanguageSection ParseLanguageSection(XElement langSec, XNamespace ns)
    {
        var lang = langSec.Attribute(XNamespace.Xml + "lang")?.Value;
        if (string.IsNullOrWhiteSpace(lang)) return null;

        var terms = langSec.Descendants(ns + "term").Where(term => !string.IsNullOrEmpty(term.Value)).Select(term => new GlossaryTermSection(term.Value)).ToList();

        return terms.Count > 0 ? new GlossaryLanguageSection(lang, terms) : null;
    }
}
