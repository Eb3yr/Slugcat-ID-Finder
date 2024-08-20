using System.Numerics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Dynamic;
using System.ComponentModel;

namespace IDFinder
{
    public class Scavenger
    {
        // Some of these groupings aren't present in the game's code - eg colors, skills. Would it be more appropriate to move them to be properties of Scavenger as opposed to their own classes? Depends on how they're generated. Seeing as the IDFinder mod groups them there's probably a good reason. Investigate. 
        // There are other fields for which I'm not sure whether I need to implement. Things like float bristle. 
        public int ID { get; private set; }
        // If it is elite, I should have a property for which mask it uses. 
        public bool Elite { get; private set; }
        public Personality Personality { get; private set; }
        public IndividualVariations Variations { get; private set; }
        [JsonIgnore]
        public Eartlers Eartlers { get; private set; }
        public ScavColors Colors { get; private set; }
        public ScavSkills Skills { get; private set; }
        public BackDecals BackPatterns { get; private set; }    // Cannot be a struct due to inheritance
        public string AsJson()
        {
			JsonSerializerOptions opt = new()
			{
				WriteIndented = true,
				Converters =
				{
					new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
				},
				IncludeFields = true
			};
            dynamic scav = new ExpandoObject();
            scav.ID = ID;
            scav.Elite = Elite;
            scav.Personality = Personality;
            scav.Variations = Variations;
            scav.Colors = Colors;
            scav.Skills = Skills;
            scav.BackPatterns = BackPatterns;
            return JsonSerializer.Serialize(scav, opt);
        }
        public Scavenger(int ID, bool isElite = false)
        {
            this.ID = ID;
            Elite = isElite;
            Personality = new(ID);
            Skills = new(ID, Personality, Elite);

            #region ScavengerGraphics
            XORShift128.InitSeed(ID);
            Variations = new(Personality);
            Colors = new(Personality, Variations, Elite);

            for (int i = 0; i < Variations.TailSegs; i++)
                XORShift128.NextFloat();    // TailSegment constructor calls BodyChunks.Reset(), which has a single UnityEngine.Random.value call. 

            if (XORShift128.NextFloat() < 0.1f || Elite)  // this way round is deliberate. The first condition is always checked, else the RNG state would be wrong for all subsequent uses.
                BackPatterns = new HardBackSpikes(Variations, Personality);
            else
                BackPatterns = new WobblyBackTufts(Variations, Personality);

            Eartlers = new(Elite);    // Eartlers constructor does call UnityEngine.Random so must be generated
            float[,] teeth = new float[XORShift128.NextIntRange(2, 5) * 2, 2];
            float num2 = float.Lerp(0.5f, 1.5f, float.Pow(XORShift128.NextFloat(), 1.5f - Personality.Aggression));
            num2 = float.Lerp(num2, num2 * Custom.LerpMap(teeth.GetLength(0), 4f, 8f, 1f, 0.5f), 0.3f);
            float num3 = float.Lerp(num2 + 0.2f, float.Lerp(0.7f, 1.2f, XORShift128.NextFloat()), XORShift128.NextFloat());
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
            //if (Elite)
            //{
            //    int num8 = XORShift128.NextIntRange(0, 4);  // Used to determine which kind of mask the elite scavenger wears. Unused here as mask graphics are not being instantiated.
            //}
            #endregion
        }
        public static (IndividualVariations? variations, ScavColors? color, BackTuftsAndRidges? back) GetGraphics(int ID, bool Elite = false, bool genVariations = false, bool genColors = false, bool genBack = false, Personality? inPersonality = null)
        {
            // Only graphics
            // Excludes eartlers as I don't (yet?) have any way to get meaningful information out of them. Maybe in the future.
            Personality personality;
            if (inPersonality == null)
                personality = new(ID);
            else
                personality = (Personality)inPersonality;

            IndividualVariations? variations = null;
			ScavColors? colors = null;
			BackTuftsAndRidges backPatterns = null!;

			XORShift128.InitSeed(ID);
            if (genVariations || genColors || genBack)
                variations = new(personality);

			if (genColors || genBack)
                colors = new(personality, (IndividualVariations)variations!, Elite);

            if (genBack)
            {
                for (int i = 0; i < ((IndividualVariations)variations!).TailSegs; i++)
                    XORShift128.NextFloat();    // TailSegment constructor calls BodyChunks.Reset(), which has a single UnityEngine.Random.value call. 

                if (XORShift128.NextFloat() < 0.1f || Elite)  // this way round is deliberate. The first condition is always checked, else the RNG state would be wrong for all subsequent uses.
                    backPatterns = new HardBackSpikes((IndividualVariations)variations, personality);
                else
                    backPatterns = new WobblyBackTufts((IndividualVariations)variations, personality);
            }

            return (variations, colors, backPatterns);
        }
    }
    public struct Eartlers
    {
        public List<Vertex[]> points { get; private set; }
        public Eartlers(bool elite)
        {
            points = [];
            GenerateSegments(elite);
        }
        private void GenerateSegments(bool elite)
        {
            float num = elite ? 1.75f : 1f;
            Vector2 @float = new(elite ? 45f : 15f, elite ? 90f : 45f);
            float rhs = elite ? 1.5f : 1f;
            float num2 = elite ? 2f : 1f;
            float num3 = elite ? 1f : 1f;
            float num4 = elite ? 0f : 1f;
            float num5 = elite ? 2f : 1f;
            float num6 = elite ? 0f : 1f;
            List<Vertex> list = [];
            list.Add(new(new(0f, 0f), 1f));
            list.Add(new(Custom.DegToFloat2(float.Lerp(40f, 90f, XORShift128.NextFloat())) * 0.4f * rhs, 1f * num2));
            Vector2 float2 = Custom.DegToFloat2(float.Lerp(@float.X, @float.Y, XORShift128.NextFloat()) * num);
            Vector2 float3 = float2 - Custom.DegToFloat2(float.Lerp(40f, 90f, XORShift128.NextFloat()) * 0.4f * rhs);
            if (float3.X < 0.2f)
                float3 = new(float.Lerp(float3.X, float2.X, 0.4f), float3.Y);
            list.Add(new(float3, 1.5f * num3));
            list.Add(new(float2, 2f * num4));
            DefineBranch(list);
            list.Clear();
            list.Add(new(points[0][1].pos, 1f));
            int num7 = (Vector2.Distance(points[0][1].pos, points[0][2].pos) > 0.6 && XORShift128.NextFloat() < 0.5f) ? 2 : 1;
            Vector2 float4 = Vector2.Lerp(points[0][1].pos, points[0][2].pos, float.Lerp(0f, num7 == 1 ? 0.7f : 0.25f, XORShift128.NextFloat()));
            list.Add(new(float4, 1.2f));
            list.Add(new(float4 + points[0][3].pos - points[0][2].pos + Custom.DegToFloat2(XORShift128.NextFloat() * 360f) * 0.1f, 1.75f));
            DefineBranch(list);
            if (num7 == 2)
            {
                list.Clear();
                float4 = Vector2.Lerp(points[0][1].pos, points[0][2].pos, float.Lerp(0.45f, 0.7f, XORShift128.NextFloat()));
                list.Add(new(float4, 1.2f));
                list.Add(new(float4 + points[0][3].pos - points[0][2].pos + Custom.DegToFloat2(XORShift128.NextFloat() * 360f) * 0.1f, 1.75f));
                DefineBranch(list);
            }
            bool flag = XORShift128.NextFloat() < 0.5f && !elite;
            if (flag)
            {
                list.Clear();
                Vector2 float5 = Custom.DegToFloat2(90f + float.Lerp(-20f, 20f, XORShift128.NextFloat())) * float.Lerp(0.2f, 0.5f, XORShift128.NextFloat());
                if (float5.Y > this.points[0][1].pos.Y - 0.1f)
                {
                    float5 = new Vector2(float5.X, float5.Y - 0.2f);
                }
                float num8 = float.Lerp(0.8f, 2f, XORShift128.NextFloat());
                if (XORShift128.NextFloat() < 0.5f)
                {
                    float5 += Custom.DegToFloat2(float.Lerp(120f, 170f, XORShift128.NextFloat())) * float.Lerp(0.1f, 0.3f, XORShift128.NextFloat());
                    list.Add(new(new Vector2(0f, 0f), num8));
                    list.Add(new(float5, num8));
                }
                else
                {
                    list.Add(new(new Vector2(0f, 0f), 1f));
                    list.Add(new(float5, (1f + num8) / 2f));
                    list.Add(new(float5 + Custom.DegToFloat2(float.Lerp(95f, 170f, XORShift128.NextFloat())) * float.Lerp(0.1f, 0.2f, XORShift128.NextFloat()), num8));
                }
                DefineBranch(list);
            }
            if (XORShift128.NextFloat() > 0.25f || !flag || elite)
            {
                list.Clear();
                float num9 = 1f + XORShift128.NextFloat() * 1.5f;
                bool flag2 = XORShift128.NextFloat() < 0.5f;
                list.Add(new(new Vector2(0f, 0f), 1f));
                float num10 = float.Lerp(95f, 135f, XORShift128.NextFloat());
                float num11 = float.Lerp(0.25f, 0.4f, XORShift128.NextFloat()) * num5;
                list.Add(new(Custom.DegToFloat2(num10) * num11, (flag2 ? 0.8f : float.Lerp(1f, num9, 0.3f)) * num5));
                list.Add(new(Custom.DegToFloat2(num10 + float.Lerp(5f, 35f, XORShift128.NextFloat())) * float.Max(num11 + 0.1f, float.Lerp(0.3f, 0.6f, XORShift128.NextFloat())), flag2 ? 0.8f : float.Lerp(1f, num9, 0.6f)));
                list.Add(new(Vector2.Normalize(list[list.Count - 1].pos) * (list[list.Count - 1].pos.Length() + float.Lerp(0.15f, 0.25f, XORShift128.NextFloat()) * num5), num9 * num6));
                DefineBranch(list);
            }
        }
        private void DefineBranch(List<Vertex> vList)
        {
            points.Add(vList.ToArray());
            for (int i = 0; i < vList.Count; i++)
            {
                vList[i] = new(new(-vList[i].pos.X, vList[i].pos.Y), vList[i].rad);
            }
            points.Add(vList.ToArray());
        }

        public struct Vertex
        {
            public Vertex(Vector2 pos, float rad)
            {
                this.pos = pos;
                this.rad = rad;
            }
            public Vector2 pos;
            public float rad;
        }
    }
    public struct IndividualVariations
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
            Scruffy = 1f;   // This is how it's done in the code. I don't understand why Scruffy is assigned to three times sequentially, it feels pointless.
        }
    }
    public struct ScavColors
    {
        public HSLColor BellyColor { get; private set; }
        public HSLColor BodyColor { get; private set; }
        public HSLColor DecorationColor { get; private set; }
        public HSLColor EyeColor { get; private set; }
        public HSLColor HeadColor { get; private set; }
        public float BellyColorBlack { get; private set; }
        public float BodyColorBlack { get; private set; }
        public float HeadColorBlack { get; private set; }
        public ScavColors(Personality Personality, IndividualVariations Variations, bool Elite)
        {
            HSLColor BellyColor, BodyColor, DecorationColor, EyeColor, HeadColor;
            float num = XORShift128.NextFloat() * 0.1f;
            if (XORShift128.NextFloat() < 0.025f)
            {
                num = float.Pow(XORShift128.NextFloat(), 0.4f);
            }
            if (Elite)
            {
                num = float.Pow(XORShift128.NextFloat(), 5f);
            }
            float num2 = num + float.Lerp(-1f, 1f, XORShift128.NextFloat()) * 0.3f * float.Pow(XORShift128.NextFloat(), 2f);
            if (num2 > 1f)
            {
                num2 -= 1f;
            }
            else if (num2 < 0f)
            {
                num2 += 1f;
            }
            BodyColor = new HSLColor(num, float.Lerp(0.05f, 1f, float.Pow(XORShift128.NextFloat(), 0.85f)), float.Lerp(0.05f, 0.8f, XORShift128.NextFloat()));
            BodyColor.S = BodyColor.S * (1f - Variations.GeneralMelanin);
            BodyColor.L = float.Lerp(BodyColor.L, 0.5f + 0.5f * float.Pow(XORShift128.NextFloat(), 0.8f), 1f - Variations.GeneralMelanin);
            BodyColorBlack = Custom.LerpMap((BodyColor.RGB().R + BodyColor.RGB().G + BodyColor.RGB().B) / 3f, 0.04f, 0.8f, 0.3f, 0.95f, 0.5f);
            BodyColorBlack = float.Lerp(BodyColorBlack, float.Lerp(0.5f, 1f, XORShift128.NextFloat()), XORShift128.NextFloat() * XORShift128.NextFloat() * XORShift128.NextFloat());
            BodyColorBlack *= Variations.GeneralMelanin;
            Vector2 @float = new Vector2(BodyColor.S, float.Lerp(-1f, 1f, BodyColor.L * (1f - BodyColorBlack)));
            if (@float.Length() < 0.5f)
            {
                @float = Vector2.Lerp(@float, Vector2.Normalize(@float), Custom.InverseLerp(0.5f, 0.3f, @float.Length()));
                BodyColor = new HSLColor(BodyColor.H, Custom.InverseLerp(-1f, 1f, @float.X), Custom.InverseLerp(-1f, 1f, @float.Y));
                BodyColorBlack = Custom.LerpMap((BodyColor.RGB().R + BodyColor.RGB().G + BodyColor.RGB().B) / 3f, 0.04f, 0.8f, 0.3f, 0.95f, 0.5f);
                BodyColorBlack = float.Lerp(BodyColorBlack, float.Lerp(0.5f, 1f, XORShift128.NextFloat()), XORShift128.NextFloat() * XORShift128.NextFloat() * XORShift128.NextFloat());
                BodyColorBlack *= Variations.GeneralMelanin;
            }
            float num3;
            if (XORShift128.NextFloat() < Custom.LerpMap(BodyColorBlack, 0.5f, 0.8f, 0.9f, 0.3f))
            {
                num3 = num2 + float.Lerp(-1f, 1f, XORShift128.NextFloat()) * 0.1f * float.Pow(XORShift128.NextFloat(), 1.5f);
                num3 = float.Lerp(num3, 0.15f, XORShift128.NextFloat());
                if (num3 > 1f)
                {
                    num3 -= 1f;
                }
                else if (num3 < 0f)
                {
                    num3 += 1f;
                }
            }
            else
            {
                num3 = ((XORShift128.NextFloat() < 0.5f) ? Custom.Decimal(num + 0.5f) : Custom.Decimal(num2 + 0.5f)) + float.Lerp(-1f, 1f, XORShift128.NextFloat()) * 0.25f * float.Pow(XORShift128.NextFloat(), 2f);
                if (XORShift128.NextFloat() < float.Lerp(0.8f, 0.2f, Personality.Energy))
                {
                    num3 = float.Lerp(num3, 0.15f, XORShift128.NextFloat());
                }
                if (num3 > 1f)
                {
                    num3 -= 1f;
                }
                else if (num3 < 0f)
                {
                    num3 += 1f;
                }
            }
            HeadColor = new HSLColor((XORShift128.NextFloat() < 0.75f) ? num2 : num3, 1f, 0.05f + 0.15f * XORShift128.NextFloat());
            HeadColor.S = HeadColor.S * float.Pow(1f - Variations.GeneralMelanin, 2f);
            HeadColor.L = float.Lerp(HeadColor.L, 0.5f + 0.5f * float.Pow(XORShift128.NextFloat(), 0.8f), 1f - Variations.GeneralMelanin);
            HeadColor.S = HeadColor.S * (0.1f + 0.9f * Custom.InverseLerp(0.1f, 0f, Custom.DistanceBetweenZeroToOneFloats(BodyColor.H, HeadColor.H) * Custom.LerpMap(float.Abs(0.5f - HeadColor.L), 0f, 0.5f, 1f, 0.3f)));
            if (HeadColor.L < 0.5f)
            {
                HeadColor.L = HeadColor.L * (0.5f + 0.5f * Custom.InverseLerp(0.2f, 0.05f, Custom.DistanceBetweenZeroToOneFloats(BodyColor.H, HeadColor.H)));
            }
            HeadColorBlack = Custom.LerpMap((HeadColor.RGB().R + HeadColor.RGB().G + HeadColor.RGB().B) / 3f, 0.035f, 0.26f, 0.7f, 0.95f, 0.25f);
            HeadColorBlack = float.Lerp(HeadColorBlack, float.Lerp(0.8f, 1f, XORShift128.NextFloat()), XORShift128.NextFloat() * XORShift128.NextFloat() * XORShift128.NextFloat());
            HeadColorBlack *= 0.2f + 0.7f * Variations.GeneralMelanin;
            HeadColorBlack = float.Max(HeadColorBlack, BodyColorBlack);
            HeadColor.S = Custom.LerpMap(HeadColor.L * (1f - HeadColorBlack), 0f, 0.15f, 1f, HeadColor.S);
            if (HeadColor.L > BodyColor.L)
            {
                HeadColor = BodyColor;
            }
            if (HeadColor.S < BodyColor.S * 0.75f)
            {
                if (XORShift128.NextFloat() < 0.5f)
                {
                    HeadColor.H = BodyColor.H;
                }
                else
                {
                    HeadColor.L = HeadColor.L * 0.25f;
                }
                HeadColor.S = BodyColor.S * 0.75f;
            }
            DecorationColor = new HSLColor((XORShift128.NextFloat() < 0.65f) ? num : ((XORShift128.NextFloat() < 0.5f) ? num2 : num3), XORShift128.NextFloat(), 0.5f + 0.5f * float.Pow(XORShift128.NextFloat(), 0.5f));
            DecorationColor.L = DecorationColor.L * float.Lerp(Variations.GeneralMelanin, XORShift128.NextFloat(), 0.5f);
            EyeColor = new HSLColor(Elite ? 0f : num3, 1f, (XORShift128.NextFloat() < 0.2f) ? (0.5f + XORShift128.NextFloat() * 0.5f) : 0.5f);
            if (Variations.ColoredPupils > 0)
            {
                EyeColor.L = float.Lerp(EyeColor.L, 1f, 0.3f);
            }
            if (HeadColor.L * (1f - HeadColorBlack) > EyeColor.L / 2f && (Variations.PupilSize == 0f || Variations.DeepPupils))
            {
                EyeColor.L = EyeColor.L * 0.2f;
            }
            float value = XORShift128.NextFloat();
            float value2 = XORShift128.NextFloat();
            BellyColor = new HSLColor(float.Lerp(BodyColor.H, DecorationColor.H, value * 0.7f), BodyColor.S * float.Lerp(1f, 0.5f, value), BodyColor.L + 0.05f + 0.3f * value2);
            BellyColorBlack = float.Lerp(BodyColorBlack, 1f, 0.3f * float.Pow(value2, 1.4f));
            if (XORShift128.NextFloat() < 0.033333335f)
            {
                HeadColor.L = float.Lerp(0.2f, 0.35f, XORShift128.NextFloat());
                HeadColorBlack *= float.Lerp(1f, 0.8f, XORShift128.NextFloat());
                BellyColor.H = float.Lerp(BellyColor.H, HeadColor.H, float.Pow(XORShift128.NextFloat(), 0.5f));
            }

            // Only struct fields can have their fields written to. Struct properties cannot. As this implementation of ScavColors (which is not a class in Rain World's source) uses properties, these assignments are required.
            this.BellyColor = BellyColor;
            this.BodyColor = BodyColor;
            this.DecorationColor = DecorationColor;
            this.EyeColor = EyeColor;
            this.HeadColor = HeadColor;
        }
    }
    public struct ScavSkills
    {
        public float BlockingSkill { get; private set; }
        public float DodgeSkill { get; private set; }
        public float MeleeSkill { get; private set; }
        public float MidRangeSkill { get; private set; }
        public float ReactionSkill { get; private set; }
        public ScavSkills(int seed, Personality personality, bool elite = false) { SetUpCombatSkills(seed, personality, elite); }
        private void SetUpCombatSkills(int seed, Personality personality, bool elite)
        {
            uint x = XORShift128.x, y = XORShift128.y, z = XORShift128.z, w = XORShift128.w;    // preserving state.
            XORShift128.InitSeed(seed);
            DodgeSkill = Custom.PushFromHalf(float.Lerp((XORShift128.NextFloat() < 0.5f) ? personality.Nervous : personality.Aggression, XORShift128.NextFloat(), XORShift128.NextFloat()), 1f + XORShift128.NextFloat());
            MidRangeSkill = Custom.PushFromHalf(float.Lerp((XORShift128.NextFloat() < 0.5f) ? personality.Energy : personality.Aggression, XORShift128.NextFloat(), XORShift128.NextFloat()), 1f + XORShift128.NextFloat());
            MeleeSkill = Custom.PushFromHalf(XORShift128.NextFloat(), 1f + XORShift128.NextFloat());
            BlockingSkill = Custom.PushFromHalf(Custom.InverseLerp(0.35f, 1f, float.Lerp((XORShift128.NextFloat() < 0.5f) ? personality.Bravery : personality.Energy, XORShift128.NextFloat(), XORShift128.NextFloat())), 1f + XORShift128.NextFloat());
            ReactionSkill = Custom.PushFromHalf(float.Lerp(personality.Energy, XORShift128.NextFloat(), XORShift128.NextFloat()), 1f + XORShift128.NextFloat());

            // In-game implementation only runs this if MSC is enabled. This will have an impact on the skills regardless of whether or not the scav is an elite. Consider adding an additional property for MSC. 
            if (elite)
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
