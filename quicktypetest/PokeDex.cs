// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using QuickType;
//
//    var data = Pokedex.FromJson(jsonString);

namespace QuickType
{
    using System;
    using System.Net;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class Pokedex
    {
        [JsonProperty("pokemon")]
        public Pokemon[] Pokemon { get; set; }
    }

    public partial class Pokemon
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("num")]
        public string Num { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("img")]
        public string Img { get; set; }

        [JsonProperty("type")]
        public string[] Type { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("candy")]
        public string Candy { get; set; }

        [JsonProperty("candy_count")]
        public long? CandyCount { get; set; }

        [JsonProperty("egg")]
        public Egg Egg { get; set; }

        [JsonProperty("spawn_chance")]
        public double SpawnChance { get; set; }

        [JsonProperty("avg_spawns")]
        public double AvgSpawns { get; set; }

        [JsonProperty("spawn_time")]
        public string SpawnTime { get; set; }

        [JsonProperty("multipliers")]
        public double[] Multipliers { get; set; }

        [JsonProperty("weaknesses")]
        public Weakness[] Weaknesses { get; set; }

        [JsonProperty("next_evolution")]
        public Evolution[] NextEvolution { get; set; }

        [JsonProperty("prev_evolution")]
        public Evolution[] PrevEvolution { get; set; }
    }

    public partial class Evolution
    {
        [JsonProperty("num")]
        public string Num { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public enum Weakness { Bug, Dark, Dragon, Electric, Fairy, Fighting, Fire, Flying, Ghost, Grass, Ground, Ice, Poison, Psychic, Rock, Steel, Water };

    public enum Egg { NotInEggs, OmanyteCandy, The10Km, The2Km, The5Km };

    public partial class Pokedex
    {
        public static Pokedex FromJson(string json) => JsonConvert.DeserializeObject<Pokedex>(json, Converter.Settings);
    }

    static class WeaknessExtensions
    {
        public static Weakness? ValueForString(string str)
        {
            switch (str)
            {
                case "Bug": return Weakness.Bug;
                case "Dark": return Weakness.Dark;
                case "Dragon": return Weakness.Dragon;
                case "Electric": return Weakness.Electric;
                case "Fairy": return Weakness.Fairy;
                case "Fighting": return Weakness.Fighting;
                case "Fire": return Weakness.Fire;
                case "Flying": return Weakness.Flying;
                case "Ghost": return Weakness.Ghost;
                case "Grass": return Weakness.Grass;
                case "Ground": return Weakness.Ground;
                case "Ice": return Weakness.Ice;
                case "Poison": return Weakness.Poison;
                case "Psychic": return Weakness.Psychic;
                case "Rock": return Weakness.Rock;
                case "Steel": return Weakness.Steel;
                case "Water": return Weakness.Water;
                default: return null;
            }
        }

        public static Weakness ReadJson(JsonReader reader, JsonSerializer serializer)
        {
            var str = serializer.Deserialize<string>(reader);
            var maybeValue = ValueForString(str);
            if (maybeValue.HasValue) return maybeValue.Value;
            throw new Exception("Unknown enum case " + str);
        }

        public static void WriteJson(this Weakness value, JsonWriter writer, JsonSerializer serializer)
        {
            switch (value)
            {
                case Weakness.Bug: serializer.Serialize(writer, "Bug"); break;
                case Weakness.Dark: serializer.Serialize(writer, "Dark"); break;
                case Weakness.Dragon: serializer.Serialize(writer, "Dragon"); break;
                case Weakness.Electric: serializer.Serialize(writer, "Electric"); break;
                case Weakness.Fairy: serializer.Serialize(writer, "Fairy"); break;
                case Weakness.Fighting: serializer.Serialize(writer, "Fighting"); break;
                case Weakness.Fire: serializer.Serialize(writer, "Fire"); break;
                case Weakness.Flying: serializer.Serialize(writer, "Flying"); break;
                case Weakness.Ghost: serializer.Serialize(writer, "Ghost"); break;
                case Weakness.Grass: serializer.Serialize(writer, "Grass"); break;
                case Weakness.Ground: serializer.Serialize(writer, "Ground"); break;
                case Weakness.Ice: serializer.Serialize(writer, "Ice"); break;
                case Weakness.Poison: serializer.Serialize(writer, "Poison"); break;
                case Weakness.Psychic: serializer.Serialize(writer, "Psychic"); break;
                case Weakness.Rock: serializer.Serialize(writer, "Rock"); break;
                case Weakness.Steel: serializer.Serialize(writer, "Steel"); break;
                case Weakness.Water: serializer.Serialize(writer, "Water"); break;
            }
        }
    }

    static class EggExtensions
    {
        public static Egg? ValueForString(string str)
        {
            switch (str)
            {
                case "Not in Eggs": return Egg.NotInEggs;
                case "Omanyte Candy": return Egg.OmanyteCandy;
                case "10 km": return Egg.The10Km;
                case "2 km": return Egg.The2Km;
                case "5 km": return Egg.The5Km;
                default: return null;
            }
        }

        public static Egg ReadJson(JsonReader reader, JsonSerializer serializer)
        {
            var str = serializer.Deserialize<string>(reader);
            var maybeValue = ValueForString(str);
            if (maybeValue.HasValue) return maybeValue.Value;
            throw new Exception("Unknown enum case " + str);
        }

        public static void WriteJson(this Egg value, JsonWriter writer, JsonSerializer serializer)
        {
            switch (value)
            {
                case Egg.NotInEggs: serializer.Serialize(writer, "Not in Eggs"); break;
                case Egg.OmanyteCandy: serializer.Serialize(writer, "Omanyte Candy"); break;
                case Egg.The10Km: serializer.Serialize(writer, "10 km"); break;
                case Egg.The2Km: serializer.Serialize(writer, "2 km"); break;
                case Egg.The5Km: serializer.Serialize(writer, "5 km"); break;
            }
        }
    }

    public static class Serialize
    {
        public static string ToJson(this Pokedex self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    public class Converter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Weakness) || t == typeof(Egg) || t == typeof(Weakness?) || t == typeof(Egg?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (t == typeof(Weakness))
                return WeaknessExtensions.ReadJson(reader, serializer);
            if (t == typeof(Egg))
                return EggExtensions.ReadJson(reader, serializer);
            if (t == typeof(Weakness?))
            {
                if (reader.TokenType == JsonToken.Null) return null;
                return WeaknessExtensions.ReadJson(reader, serializer);
            }
            if (t == typeof(Egg?))
            {
                if (reader.TokenType == JsonToken.Null) return null;
                return EggExtensions.ReadJson(reader, serializer);
            }
            throw new Exception("Unknown type");
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var t = value.GetType();
            if (t == typeof(Weakness))
            {
                ((Weakness)value).WriteJson(writer, serializer);
                return;
            }
            if (t == typeof(Egg))
            {
                ((Egg)value).WriteJson(writer, serializer);
                return;
            }
            throw new Exception("Unknown type");
        }

        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters = { new Converter() },
        };
    }
}

