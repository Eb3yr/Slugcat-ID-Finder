using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity_XORShift;

namespace IDFinder
{
    public class Scavenger
    {
        // Some of these groupings aren't present in the game's code - eg colors, skills. Would it be more appropriate to move them to be properties of Scavenger as opposed to their own classes? Depends on how they're generated. Seeing as the IDFinder mod groups them there's probably a good reason. Investigate. 
        // There are other fields for which I'm not sure whether I need to implement. Things like float bristle. 
        public int ID { get; private set; }
        // If it is elite, I should have a property for which mask it uses. 
        public bool Elite { get; private set; }
        public float[,] teeth {  get; private set; }    // should this be visible in the API? it makes use of UnityEngine.Random
        public Personality Personality { get; private set; }
        public IndividualVariations Variations { get; private set; }
        public ScavColors Colors { get; private set; }
        public ScavSkills Skills { get; private set; }
        public ScavBackType BackType { get; private set; }  // re-assess property name
        // Make sure the RNG is initialised correctly. I may need to go back to Personality's implementation and add back that return to the initial RNG state once the properties are all generated for example. 
        // There is a problem here. IndividualVariations doesn't initialise RNG in itself. The RNG is initialised in the Scavenger constructor just prior to its call, then followed up by this.GenerateColors(); and so on. The RNG state is very important so I can't simply instantiate, say, ScavColors without accounting for the effects of Variations' instantiation on RNG
        // I could call the RNG functions in order with like parameters and maybe improve performance by not executing functions like float.Pow, float.Lerp etc. I need to benchmark how expensive they are vs just generating the RNG, and if it's worth it implement logic to do the latter. 
        public Scavenger(int ID, bool isElite = false)
        {
            this.ID = ID;
            Elite = isElite;
            Personality = new(ID);
            Skills = new(ID, Personality, Elite);

            #region ScavengerGraphics
            XORShift128.InitSeed(ID);
            Variations = new(Personality);
            Colors = new(); // game code shows call to this.GenerateColors(); so implement that logic in the colors constructor. GenerateColors() does not initialise RNG itself.
            if (XORShift128.NextFloat() < 0.1f || isElite)  // this way round is deliberate. The first condition is always checked, else the RNG state would be wrong for all subsequent uses.
            {
                BackType = ScavBackType.HardBackSpikes;
            }
            else
            {
                BackType = ScavBackType.WobblyBackTufts;
            }
            // eartlers = new ScavengerGraphics.Eartlers(this.FirstEartlerSprite, this);    // Eartlers constructor does call UnityEngine.Random so must be generated
            teeth = new float[XORShift128.NextIntRange(2, 5) * 2, 2];
            float num2 = float.Lerp(0.5f, 1.5f, float.Pow(XORShift128.NextFloat(), 1.5f - Personality.Aggression));
            num2 = float.Lerp(num2, num2 * Custom.LerpMap(teeth.GetLength(0), 4f, 8f, 1f, 0.5f), 0.3f);
            float num3 = float.Lerp(num2 + 0.2f, float.Lerp(0.7f, 1.2f, XORShift128.NextFloat()), XORShift128.NextFloat()));
            num3 = float.Lerp(num3, Custom.LerpMap(teeth.GetLength(0), 4f, 8f, 1.5f, 0.2f), 0.4f);
            float a = 0.3f + 0.7f * XORShift128.NextFloat();
            for (int l = 0; l < teeth.GetLength(0); l++)
            {
                float num4 = (float)l / (teeth.GetLength(0) - 1);
                teeth[1, 0] = float.Lerp(a, 1f, float.Sin(num4 * 3.1415927f)) * num2;
                if (XORShift128.NextFloat() < Variations.Scruffy && XORShift128.NextFloat() < 0.2f)
                {
                    teeth[l, 0] = 0f;
                }
                teeth[l, 1] = float.Lerp(0.5f, 1f, float.Sin(num4 * 3.1415927f)) * num3;
            }
            // if Scav King
            // Scav king VultureMaskGraphics.GenerateColor() here. There is some use of UnityEngine.Random in GenerateColor, HOWEVER UnityEngine.Random.state is returned to what it was prior to the function call. Thank fuck.
            if (Elite)  // Else if to prev comment
            {
                int num8 = XORShift128.NextIntRange(0, 4);  // Used to determine which kind of mask the elite scavenger wears. Unused here as mask graphics are not being instantiated.
                // Likewise a call to VultureMaskGraphics.GenerateColor() here, but since the RNG state prior to the function call is preserved, it does not need to be ran. 
            }
            // CentipedeShellCosmetic[] shells is instantiated next. The class's constructor uses UnityEngine.Random.value, however unless the scavenger is a king, shells.Length = 0 so no objects are instantiated and the constructor is never called. 
            #endregion
        }
        // MaskGFX implementation is different to ScavengerGraphics. 
        private void GenerateColors()
        {

        }
    }
    public class IndividualVariations
    {
        public float WaistWidth
        {
            get
            {
                return Fatness * (1f - NarrowWaist);
            }
        }
        public float HeadSize { get; private set; }
        public float EartlerWidth { get; private set; }
        public float NeckThickness { get; private set; }
        public float HandsHeadColor { get; private set; }
        public float EyeSize { get; private set; }
        public float NarrowEyes { get; private set; }
        public float EyesAngle { get; private set; }
        public float Fatness { get; private set; }
        public float NarrowWaist { get; private set; }
        public float LegsSize { get; private set; }
        public float ArmThickness { get; private set; }
        public float WideTeeth { get; private set; }
        public float PupilSize { get; private set; }
        public float Scruffy { get; private set; }
        public bool ColoredEartlerTips { get; private set; }
        public bool DeepPupils { get; private set; }
        public int ColoredPupils { get; private set; }
        public int TailSegs { get; private set; }
        public float GeneralMelanin { get; private set; }
        public IndividualVariations(Personality personality, bool isElite = false)
        {
            GeneralMelanin = Custom.PushFromHalf(XORShift128.NextFloat(), 2f);
            HeadSize = Custom.ClampedRandomVariation(0.5f, 0.5f, 0.1f);
            EartlerWidth = XORShift128.NextFloat();
            EyeSize = float.Pow(float.Max(0f, float.Lerp(XORShift128.NextFloat(), float.Pow(HeadSize, 0.5f), XORShift128.NextFloat() * 0.4f)), float.Lerp(0.95f, 0.55f, personality.Sympathy));
            NarrowEyes = ((XORShift128.NextFloat() < float.Lerp(0.3f, 0.7f, personality.Sympathy)) ? 0f : float.Pow(XORShift128.NextFloat(), float.Lerp(0.5f, 1.5f, personality.Sympathy)));
            if (isElite)
            {
                NarrowEyes = 1f;
            }
            EyesAngle = float.Pow(XORShift128.NextFloat(), float.Lerp(2.5f, 0.5f, float.Pow(personality.Energy, 0.03f)));
            Fatness = float.Lerp(XORShift128.NextFloat(), personality.Dominance, XORShift128.NextFloat() * 0.2f);
            if (personality.Energy < 0.5f)
            {
                Fatness = float.Lerp(Fatness, 1f, XORShift128.NextFloat() * Custom.InverseLerp(0.5f, 0f, personality.Energy));
            }
            else
            {
                Fatness = float.Lerp(Fatness, 0f, XORShift128.NextFloat() * Custom.InverseLerp(0.5f, 1f, personality.Energy));
            }
            NarrowWaist = float.Lerp(float.Lerp(XORShift128.NextFloat(), 1f - Fatness, XORShift128.NextFloat()), 1f - personality.Energy, XORShift128.NextFloat());
            NeckThickness = float.Lerp(float.Pow(XORShift128.NextFloat(), 1.5f - personality.Aggression), 1f - Fatness, XORShift128.NextFloat() * 0.5f);
            PupilSize = 0f;
            DeepPupils = false;
            ColoredPupils = 0;
            if (XORShift128.NextFloat() < 0.65f && EyeSize > 0.4f && NarrowEyes < 0.3f)
            {
                if (XORShift128.NextFloat() < float.Pow(personality.Sympathy, 1.5f) * 0.8f)
                {
                    PupilSize = float.Lerp(0.4f, 0.8f, float.Pow(XORShift128.NextFloat(), 0.5f));
                    if (XORShift128.NextFloat() < 0.6666667f)
                    {
                        ColoredPupils = XORShift128.NextIntRange(1, 4);

                    }
                }
                else
                {
                    PupilSize = 0.7f;
                    DeepPupils = true;
                }
            }
            if (isElite)
            {
                ColoredPupils = XORShift128.NextIntRange(1, 4);
            }
            if (XORShift128.NextFloat() < GeneralMelanin)
            {
                HandsHeadColor = ((XORShift128.NextFloat() < 0.3f) ? XORShift128.NextFloat() : ((XORShift128.NextFloat() < 0.6f) ? 1f : 0f));
            }
            else
            {
                HandsHeadColor = ((XORShift128.NextFloat() < 0.2f) ? XORShift128.NextFloat() : ((XORShift128.NextFloat() < 0.8f) ? 1f : 0f));
            }
            LegsSize = XORShift128.NextFloat();
            ArmThickness = float.Lerp(XORShift128.NextFloat(), float.Lerp(personality.Dominance, Fatness, 0.5f), XORShift128.NextFloat());
            ColoredEartlerTips = (isElite || XORShift128.NextFloat() < 1f / float.Lerp(1.2f, 10f, GeneralMelanin));
            WideTeeth = XORShift128.NextFloat();
            TailSegs = (XORShift128.NextFloat() < 0.5f) ? 0 : XORShift128.NextIntRange(1, 5);
            Scruffy = 0f;
            if (XORShift128.NextFloat() < 0.25f)
            {
                Scruffy = float.Pow(XORShift128.NextFloat(), 0.3f);
            }
            Scruffy = 1f;
        }
    }
    // Spelling it this way causes me psychic damage. I really hope some Americans come along and use this library to make it worth it.
    public class ScavColors
    {
        public HSLColor BellyColor{ get; private set; }
        public HSLColor BlackColor{ get; private set; }
        public HSLColor EyeColor{ get; private set; }
        public HSLColor HeadColor{ get; private set; }
        // There's a few others not included in the IDFinder mod BUT present in the code. Not entirely sure how they're used but they are. 
        // Also consider blended colours. They're properties with a getter but no setter. Need to look into where it's used and what visual effects it has.
        public ScavColors() { }
    }
    public class ScavSkills
    {
        public float BlockingSkill { get; private set; }
        public float DodgeSkill { get; private set; }
        public float MeleeSkill { get; private set; }
        public float MidRangeSkill { get; private set; }
        public float ReactionSkill { get; private set; }
        public ScavSkills(int seed, Personality personality, bool isElite) { SetUpCombatSkills(seed, personality, isElite); }
        private void SetUpCombatSkills(int seed, Personality personality, bool isElite)
        {
            uint x = XORShift128.x, y = XORShift128.y, z = XORShift128.z, w = XORShift128.w;    // preserving state. Not sure if this is necessary.
            XORShift128.InitSeed(seed);
            DodgeSkill = Custom.PushFromHalf(float.Lerp((XORShift128.NextFloat() < 0.5f) ? personality.Nervous : personality.Aggression, XORShift128.NextFloat(), XORShift128.NextFloat()), 1f + XORShift128.NextFloat());
            MidRangeSkill = Custom.PushFromHalf(float.Lerp((XORShift128.NextFloat() < 0.5f) ? personality.Energy : personality.Aggression, XORShift128.NextFloat(), XORShift128.NextFloat()), 1f + XORShift128.NextFloat());
            MeleeSkill = Custom.PushFromHalf(XORShift128.NextFloat(), 1f + XORShift128.NextFloat());
            BlockingSkill = Custom.PushFromHalf(Custom.InverseLerp(0.35f, 1f, float.Lerp((XORShift128.NextFloat() < 0.5f) ? personality.Bravery : personality.Energy, XORShift128.NextFloat(), XORShift128.NextFloat())), 1f + XORShift128.NextFloat());
            ReactionSkill = Custom.PushFromHalf(float.Lerp(personality.Energy, XORShift128.NextFloat(), XORShift128.NextFloat()), 1f + XORShift128.NextFloat());

            // In-game implementation only runs this if MSC is enabled. This will have an impact on the skills regardless of whether or not the scav is an elite. Consider adding an additional property for MSC. 
            if (isElite)
            {
                float num = float.Lerp(personality.Dominance, 1f, 0.15f);
                DodgeSkill = float.Lerp(DodgeSkill, 1f, num * 0.15f); ;
                MidRangeSkill = float.Lerp(MidRangeSkill, 1f, num * 0.1f);
                BlockingSkill = float.Lerp(BlockingSkill, 1f, num * 0.1f);
                ReactionSkill = float.Lerp(ReactionSkill, 1f, num * 0.05f);
            }
            else
            {
                float num2 = 1f - personality.Dominance;
                DodgeSkill = float.Lerp(DodgeSkill, 0f, num2 * 0.085f);
                MidRangeSkill = float.Lerp(MidRangeSkill, 0f, num2 * 0.085f);
                BlockingSkill = float.Lerp(BlockingSkill, 0f, num2 * 0.05f);
                ReactionSkill = float.Lerp(ReactionSkill, 0f, num2 * 0.15f);
            }
            XORShift128.InitState(x, y, z, w);
        }
    }
}
