using System;
using CQRS.Base.DDD.Domain.SharedKernel;
using CQRS.Sales.Domain;
using NHibernate;

namespace CQRS.Base.Infrastructure.NHibernate
{
    public class SampleDataGenerator
    {
        public void Generate(ISession session)
        {
            session.Save(new Client());

            var names = new[]
                            {
                                "mordezai", "poignant", "nixey", "nobundo", "mauveine", "1aimlak", "licille", "toneelge",
                                "d'usage", "beukehou", "wfpc3", "beachleys", "carlenar", "madrigaa", "mcbridge",
                                "8617196314", "piaceva", "unmounte", "foerbindelsens", "kennethy", "midnames", "ali`pede",
                                "kosterschap", "3082352544", "jaijeev", "automatons", "uaura", "glaubwuerdig", "mackiin",
                                "nopalito", "N8EMR", "allease", "negherei", "katline", "s|nnafjelsk", "slimish",
                                "formularizing", "82089886131364451656742050394678", "zerdrueckst", "stroopvaten",
                                "KAEBSORG", "saeuselnde", "D'Alessandro", "avstengt", "allopathische", "steekann", "lokoja"
                                , "idenell", "flamy", "zersplittertest", "portmaster2e", "fllint", "hanskristiaan",
                                "gunshannon", "willie7", "konfuses", "calseca", "modekwalen", "blatnica", "ilcn-let",
                                "anytthing", "bergingsl|nnen", "explodie", "nabalas", "locurcio", "th'ambassadors",
                                "langsdoorsnede", "posesiva", "sensorivasomotor", "rostered", "pre'vales", "nonegotistic",
                                "utarta", "kraftbru", "1421187144", "soderber", "sunmok", "geschockt",
                                "vermarktungsabsichten", "nontheoretic", "mezquital", "ithnan", "4294063888", "manglar",
                                "vastgrij", "deloache", "perspectival", "climatise'", "laittoon", "deifiers", "NOIMREHT",
                                "ruffcree", "trimingham", "ludealia", "rechtes", "fahrgelderstattung", "humorously",
                                "nonvocat", "mijoter", "cogitamus"
                            };

            var r = new Random();
            foreach (var name in names)
                session.Save(new Product((ProductType)r.Next(0, 3), new Money(r.Next(1000)), name));

            session.Flush();
        }
    }
}