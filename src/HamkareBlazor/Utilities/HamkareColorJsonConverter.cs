// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HamkareBlazor.Utilities;

public sealed class HamkareColorJsonConverter : JsonConverter<HamkareColor>
{
    public override HamkareColor Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                return new HamkareColor();

            return HamkareColor.Parse(value);
        }

        if (reader.TokenType == JsonTokenType.StartObject)
        {
            using var document = JsonDocument.ParseValue(ref reader);
            var root = document.RootElement;

            var r = root.GetProperty("R").GetByte();
            var g = root.GetProperty("G").GetByte();
            var b = root.GetProperty("B").GetByte();
            var a = root.GetProperty("A").GetByte();

            return new HamkareColor(r, g, b, a);
        }

        throw new JsonException(
            $"Cannot deserialize {nameof(HamkareColor)} from {reader.TokenType}.");
    }

    public override void Write(
        Utf8JsonWriter writer,
        HamkareColor value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value);
    }
}
