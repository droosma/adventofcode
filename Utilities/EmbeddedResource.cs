using System;
using System.IO;
using System.Reflection;

namespace Utilities
{
    public class EmbeddedResource
    {
        public static string Get(string resourceName, Assembly assembly)
        {
            resourceName = FormatResourceName(assembly, resourceName);
            using (Stream resourceStream = assembly.GetManifestResourceStream(resourceName))
            {
                if (resourceStream == null)
                    return null;

                using StreamReader reader = new StreamReader(resourceStream);
                return reader.ReadToEnd();
            }

            static string FormatResourceName(Assembly assembly, string resourceName)
            {
                return assembly.GetName().Name + "." + resourceName.Replace(" ", "_")
                                                                   .Replace("\\", ".")
                                                                   .Replace("/", ".");
            }
        }
    }
}
