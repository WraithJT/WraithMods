using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;

namespace WraithMods.Feats
{
    public class MagicalAptitude
    {
        private static readonly string FeatName = "MagicalAptitudeFeat";
        private static readonly string FeatGuid = "E47A36AB-EBCC-4D94-9888-B795ABD398F3";
        private static readonly string BasicFeatSelectionGuid = "247a4068296e8be42890143f451b4b45";
        public static void Configure()
        {
            FeatureConfigurator.New(FeatName, FeatGuid).Configure();

            FeatureSelectionConfigurator.For(BasicFeatSelectionGuid);

            FeatureSelectionConfigurator.For(BasicFeatSelectionGuid).RemoveFromFeatures().Configure();
            //FeatureConfigurator.For(BasicFeatSelectionGuid).SetDescription();


            //Kingmaker.Localization.LocalizedString newDescription = LocalizationTool.CreateString();
        }
    }
}
