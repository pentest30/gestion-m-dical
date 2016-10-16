using System;
using System.Collections.Generic;

namespace GC.MV
{
    public class AutoCompleteMv
    {
        private List<String> AutoCompleteSource()
        {
            var source = new List<string>{"FNS+ équilibre leucocytaire",
                "groupage sanguain",
                "glycélie a jeun",
                "Hémogobine glyquée(HbA1c)",
                "Blian d'hémostase:TP TCK TCA TQ INR TS",
                "Blian rénal:urée_créatinine",
                "Bilan ibflammatoire:VS CRP",
                "Bilan thyroidien:TSH T3 T4",
                "ASLO",
                "IDR a la tuberculine"
            };
            return source;
        } 
        public List<string> ListNames { get { return AutoCompleteSource(); } }
    }
}
