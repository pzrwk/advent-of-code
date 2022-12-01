var text = File.ReadAllLines("input.txt");

List<int> result = new();
int caloriesCount = 0;

foreach (var line in text)
{
    if (line == "")
    {
        result.Add(caloriesCount);
        caloriesCount = 0;
    }
    else
    {
        caloriesCount += Convert.ToInt32(line);
    }
}

var mostCalories = result.Max();
Console.WriteLine($"Elf number {result.IndexOf(mostCalories)} carrying {mostCalories} calories");

// Part 2
var caloriesOfTop3 = result.OrderDescending().Take(3).Sum();

Console.WriteLine($"Calories carried by top 3 elves: {caloriesOfTop3}");