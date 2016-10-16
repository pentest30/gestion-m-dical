using System.Collections.Generic;

namespace Gc.Core.Data
{
    public class TypeAntecedent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public static List<TypeAntecedent> TypeAntecedents { get; set; }

        public static List<TypeAntecedent> GetAntecedents()
        {
            TypeAntecedents = new List<TypeAntecedent>
            {
                new TypeAntecedent{Id = 1,Name = "Antécédents familiaux"}
                , new TypeAntecedent{Id = 2,Name = "Antécedents personnels"}
                    , new TypeAntecedent{Id = 2,Name = "Antécedents chirurgicaux"}
            };
            return TypeAntecedents;
        }
    }
}
