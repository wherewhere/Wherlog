﻿using System.Text.Json.Serialization;

namespace Wherlog.Models
{
    public interface IApi
    {
        [JsonPropertyName("api")]
        string Api { get; }
    }

    public interface IRaw
    {
        [JsonPropertyName("raw")]
        string Raw { get; }

        string GetContent()
        {
            bool inRow = false;
            short time = 0, count = 0;
            int index = 0;
            for (; index < Raw.Length; index++)
            {
                char c = Raw[index];
                if (inRow)
                {
                    if (c == '-')
                    {
                        count++;
                    }
                    else
                    {
                        if (count >= 3 && ++time == 2)
                        {
                            return Raw[index..];
                        }
                        else
                        {
                            inRow = false;
                            count = 0;
                        }
                    }
                }
                else if (c == '-')
                {
                    inRow = true;
                    count++;
                }
            }
            return Raw;
        }
    }

    public interface IInfo
    {
        [JsonPropertyName("type")]
        string Type { get; }
    }
}
