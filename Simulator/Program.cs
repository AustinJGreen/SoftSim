using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Simulator
{
    class Program
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
        static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        static string ApplicationName = "Baseball Roster Stats";

        static Player[] GetRoyalsRoster(bool useCached = true)
        {
            if (File.Exists("royals.json"))
            {
                return JsonConvert.DeserializeObject<Player[]>(File.ReadAllText("royals.json"));
            }

            UserCredential credential;

            using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }

            // Create Google Sheets API service.
            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            string spreadsheetId = "1Hkjo5c_xkfLoABYjzD_zxq4VLRR4s5g3h9Zx3EI5-8M";
            string range = "Offense!A1:S16";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            ValueRange response = request.Execute();
            IList<IList<object>> rows = response.Values;

            Player[] roster = new Player[rows.Count - 1];
            for (int i = 1; i < rows.Count; i++)
            {
                IList<object> rowData = rows[i];
                PlayerStats stats = new PlayerStats();
                stats.AtBats = int.Parse((string)rowData[1]);
                stats.Walks = int.Parse((string)rowData[9]);
                stats.Strikeouts = int.Parse((string)rowData[10]);
                stats.Singles = int.Parse((string)rowData[11]);
                stats.Doubles = int.Parse((string)rowData[12]);
                stats.Triples = int.Parse((string)rowData[13]);
                stats.HomeRuns = int.Parse((string)rowData[15]);

                Player p = new Player((string)rowData[0], stats);
                roster[i - 1] = p;
            }

            File.WriteAllText("royals.json", JsonConvert.SerializeObject(roster));
            return roster;
        }

        static void Main(string[] args)
        {
            //
            var royalsRoster = GetRoyalsRoster();
            Team us = new Team("Royals", royalsRoster);

            Team them = new Team("Mist", new Player[]
            {

            });

            Game g = new Game(us, them);
            
        }
    }
}
