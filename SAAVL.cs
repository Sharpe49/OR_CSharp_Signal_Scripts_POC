using Orts.Formats.Msts;
using System.Collections.Generic;
using System.Linq;

namespace Orts.Simulation.Signalling
{
    public class SAAVL : CsSignalScript
    {
        public SAAVL()
        {

        }

        public override void Initialize()
        {

        }

        public override void Update()
        {
            List<string> nextSignalTextAspects = GetNextSignalTextAspects(MstsSignalFunction.NORMAL);

            if (BlockState != MstsBlockState.CLEAR)
            {
                MstsSignalAspect = MstsSignalAspect.STOP_AND_PROCEED;
                TextSignalAspect = "FR_S_BAL";
            }
            else if (nextSignalTextAspects.FindAll(x => x == "FR_C"
                || x == "FR_S_BAL"
                || x == "FR_S_BAPR"
                || x == "FR_S_BM"
                || x == "FR_SCLI"
                || x == "FR_RR_A"
                || x == "FR_RR_ACLI"
                || x == "FR_RR"
                || x == "FR_RRCLI_A"
                || x == "FR_RRCLI_ACLI"
                || x == "FR_RRCLI"
                ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.APPROACH_1;
                TextSignalAspect = "FR_A";
            }
            else if (IsSignalFeatureEnabled("USER1")
                && nextSignalTextAspects.FindAll(x => x == "FR_A"
                    || x == "FR_R"
                    ).Count > 0)
            {
                MstsSignalAspect = MstsSignalAspect.APPROACH_2;
                TextSignalAspect = "FR_ACLI";
            }
            else
            {
                MstsSignalAspect = MstsSignalAspect.CLEAR_1;
                TextSignalAspect = "FR_VL";
            }

            DrawState = DefaultDrawState(MstsSignalAspect);
        }
    }
}