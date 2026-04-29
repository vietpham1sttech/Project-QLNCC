using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace Project_QLNCC.Localization
{
    public static class Project_QLNCCLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(Project_QLNCCConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(Project_QLNCCLocalizationConfigurer).GetAssembly(),
                        "Project_QLNCC.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
