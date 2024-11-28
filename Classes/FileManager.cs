using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HangmanGame.Classes
{
    public class FileManager
    {
        private string filePathJson = "JSON/Hangman.json";

        public List<T> LoadDataFromJson<T>(string sectionName)
        {
            if (!File.Exists(filePathJson))
            {
                AnsiConsole.MarkupLine("[red]JSON-fil hittades inte.");
                return new List<T>();
            }
            try
            {
                string jsonData = File.ReadAllText(filePathJson);

                var jsonObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonData);

                if (jsonObject != null && jsonObject.ContainsKey(sectionName))
                {
                    return JsonSerializer.Deserialize<List<T>>(jsonObject[sectionName]) ?? new List<T>();
                }

                AnsiConsole.MarkupLine($"[yellow]Sektionen '{sectionName}' hittades inte i JSON-filen.[/]");

            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[yellow]Fel vid deserialisering av JSON: {ex.Message}[/]");
            }
            return new List<T>();
        }

        public void SaveDataToJson<T>(List<T> data, string sectionName)
        {
            var jsonObject = new Dictionary<string, JsonElement>();

            if (File.Exists(filePathJson))
            {
                try
                {
                    string jsonData = File.ReadAllText(filePathJson);

                    jsonObject = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonData)
                            ?? new Dictionary<string, JsonElement>();

                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[yellow]Fel vid laddning av JSON-fil: {ex.Message}[/]");

                }
            }
            jsonObject[sectionName] = JsonSerializer.SerializeToElement(data);

            try
            {
                string updatedJson = JsonSerializer.Serialize(jsonObject, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePathJson, updatedJson);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[yellow]Fel vid skrivning till JSON-fil: {ex.Message}[/]");
            }
        }
    }
}
