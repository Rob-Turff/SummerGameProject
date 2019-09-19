using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Src.Stages
{
    public static class StageFactory
    {
        public static IStage GetStage(StageIdentifier stageIdentifier)
        {
            switch (stageIdentifier)
            {
                case StageIdentifier.DEFAULT:
                    return new DefaultStage();
                default:
                    throw new NotImplementedException("The supplied stage identifier does not yet have a corresponding stage defined");
            }
        }
    }

    public enum StageIdentifier
    {
        DEFAULT
    }
}
