List<KeyValuePair<string, string>> strategy = new List<KeyValuePair<string, string>>
{
    new KeyValuePair<string, string>("A", "Y"),
    new KeyValuePair<string, string>("B", "X"),
    new KeyValuePair<string, string>("C", "Z"),
};

var data = (await File.ReadAllLinesAsync("input.txt"))
    .Select(item => {
            var pair = item.Split(' ');
            return new KeyValuePair<string, string>(pair[0], pair[1]);
        }
    );

int points = 0;
int secondPoints = 0;

foreach(var pair in data.ToArray())
{
    points += GetPointsForRound(pair);

    List<KeyValuePair<string, int>> figurePoints = new()
    {
        new("R", 1),
        new("P", 2),
        new("S", 3),
    };

    secondPoints += GetPoints(pair, figurePoints);
}

Console.WriteLine($"#1: {points}");
Console.WriteLine($"#2: {secondPoints}");

int GetPointsForRound(KeyValuePair<string, string> roundPicks)
{
    int points = 0;

    points += GetPointsForPick(roundPicks.Value);
    points += GetPointsForBattleResult(roundPicks);

    return points;
}

int GetPointsForBattleResult(KeyValuePair<string, string> roundPicks)
{
    if(PaperBeatsRock(roundPicks) 
        || ScisorsBeatPaper(roundPicks)
        || RockBeatsScisors(roundPicks)) return 6;

    else if (IsDraw(roundPicks)) return 3;
    return 0;
}

bool IsDraw(KeyValuePair<string, string> roundPicks)
{
    return (roundPicks.Key == "A" && roundPicks.Value == "X")
        || (roundPicks.Key == "B" && roundPicks.Value == "Y")
        || (roundPicks.Key == "C" && roundPicks.Value == "Z");
}

bool RockBeatsScisors(KeyValuePair<string, string> roundPicks)
{
    return roundPicks.Key == "C" && roundPicks.Value == "X";

}

bool ScisorsBeatPaper(KeyValuePair<string, string> roundPicks)
{
    return roundPicks.Key == "B" && roundPicks.Value == "Z";
}

bool PaperBeatsRock(KeyValuePair<string, string> roundPicks)
{
    return roundPicks.Key == "A" && roundPicks.Value == "Y";
}

int GetPointsForPick(string pick)
{
    if(pick == "A" || pick == "X")
    {
        return 1;
    } else if(pick == "B" || pick == "Y")
    {
        return 2;
    }
    return 3;
}

int GetPoints(KeyValuePair<string, string> pair, List<KeyValuePair<string, int>> figurePoints)
{
    var pts = 0;
    if(pair.Value== "X")
    {
        if(pair.Key == "A")
        {
            pts += figurePoints.Single(x => x.Key == "S").Value;
        } else if (pair.Key == "B")
        {
            pts += figurePoints.Single(x => x.Key == "R").Value;
        }
        else
        {
            pts += figurePoints.Single(x => x.Key == "P").Value;
        }
    } 
    else if (pair.Value == "Y")
    {
        if (pair.Key == "A")
        {
            pts += figurePoints.Single(x => x.Key == "R").Value;
        }
        else if (pair.Key == "B")
        {
            pts += figurePoints.Single(x => x.Key == "P").Value;
        }
        else
        {
            pts += figurePoints.Single(x => x.Key == "S").Value;
        }
        pts += 3;
    }
    else
    {
        if (pair.Key == "A")
        {
            pts += figurePoints.Single(x => x.Key == "P").Value;
        }
        else if (pair.Key == "B")
        {
            pts += figurePoints.Single(x => x.Key == "S").Value;
        }
        else
        {
            pts += figurePoints.Single(x => x.Key == "R").Value;
        }
        pts += 6;
    }

    return pts;
};