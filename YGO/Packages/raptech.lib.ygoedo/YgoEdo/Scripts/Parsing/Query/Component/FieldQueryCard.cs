using YgoSoul.RapTech.Lib.YgoEdo.Core.Constant;
using YgoSoul.RapTech.Lib.YgoEdo.Core.Flag;

namespace YgoSoul.RapTech.Lib.YgoEdo.Parsing.Query.Component
{
    public class FieldQueryCard
    {
        public FieldQueryCard(OCG_Zone zone, OCG_CardPosition position, uint xyzMaterials)
        {
            Zone = zone;
            Position = position;
            XyzMaterials = xyzMaterials;
        }

        public OCG_Zone Zone { get; set; }
        public OCG_CardPosition Position { get; }
        public uint XyzMaterials { get; }

        public override string ToString()
        {
            return $"Zone={Zone}, Position={Position}, XyzMaterials={XyzMaterials}";
        }
    }
}