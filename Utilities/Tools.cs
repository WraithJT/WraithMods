using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using System;
using System.Linq;

namespace WraithMods.Utilities
{
    public static class Tools
    {
        public static T[] AppendToArray<T>(this T[] array, T value)
        {
            var len = array?.Length ?? 0;
            var result = new T[len + 1];
            if (len > 0)
            {
                Array.Copy(array, result, len);
            }
            result[len] = value;
            return result;
        }

        public static void AddFeatures(this BlueprintFeatureSelection selection, params BlueprintFeature[] features)
        {
            selection.AddFeatures(features.Select(f => f.ToReference<BlueprintFeatureReference>()).ToArray());
        }

        public static void AddFeatures(this BlueprintFeatureSelection selection, params BlueprintFeatureReference[] features)
        {
            foreach (var feature in features)
            {
                if (!selection.m_AllFeatures.Contains(feature))
                {
                    selection.m_AllFeatures = selection.m_AllFeatures.AppendToArray(feature);
                }
                if (!selection.m_Features.Contains(feature))
                {
                    selection.m_Features = selection.m_Features.AppendToArray(feature);
                }
            }
            selection.m_AllFeatures = selection.m_AllFeatures.OrderBy(feature => feature.Get().Name ?? feature.Get().name).ToArray();
            selection.m_Features = selection.m_Features.OrderBy(feature => feature.Get().Name ?? feature.Get().name).ToArray();
        }

        public static T Ref<T>(string v) where T : BlueprintReferenceBase
        {
            var tref = Activator.CreateInstance<T>();
            tref.deserializedGuid = BlueprintGuid.Parse(v);
            return tref;
        }
    }
}
